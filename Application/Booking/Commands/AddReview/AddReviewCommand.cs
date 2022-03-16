using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Defaults;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Domain.Enums;
using System;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Helpers;

namespace GhostWriter.Application.Booking.Commands.AddReview
{
    public class AddReviewCommand
    {
        public int StarRating { get; set; }
        public int BookingId { get; set; }
    }
    public class AddReviewCommandExtended : AddReviewCommand, IRequest<ExtendedOutputModelList<NotificationSignalRDTO>>
    {
       public string CustomerUsername { get; set; }
        public string Comment { get; set; }
    }

    public class AddReviewCommandExtendedHandler : IRequestHandler<AddReviewCommandExtended, ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly INotificationService _notificationService;

        public AddReviewCommandExtendedHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, INotificationService notificationService)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _notificationService = notificationService;
        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> Handle(AddReviewCommandExtended request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerUsername))
                throw new AuthorizationException($"User is unauthorized to write a review.");

            var user = await _userManagementFactory.FindUser(request.CustomerUsername);

            if (user is null)
                throw new NotFoundException($"User {request.CustomerUsername} not found.");

            if (!_userManagementFactory.IsInRole(user, UserRoleDefaults.CustomerRoleName))
                throw new AuthorizationException($"User is unauthorized to write a review.");

            var booking = _context.Bookings.Find(request.BookingId);

            if (booking is null)
                throw new NotFoundException($"Booking {request.BookingId} not found.");

            if (booking.HeadProposal.Project.Customer.UserName != request.CustomerUsername)
                throw new AuthorizationException($"User is unauthorized to write a review for this booking.");

            if (!BookingStatusGroups.Closed.Contains(booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault().BookingStatus)) 
                throw new AuthorizationException($"User cannot write a review for this booking because it is not closed yet.");

            if (_context.Rates.Where(x => x.Booking.Id == request.BookingId && x.RateWriter == RateWriter.WrittenByCustomer).FirstOrDefault() != null)
                throw new AuthorizationException($"A review has been already added for this project.");

            try
            {
                Rate rate = new Rate()
                {
                    Booking = booking,
                    Comment = request.Comment,
                    StarRating = request.StarRating,
                    RateWriter = RateWriter.WrittenByCustomer
                };

                booking.LastUpdate = DateTime.UtcNow;
                _context.Bookings.Update(booking);

                _context.Rates.Add(rate);
                await _context.SaveChangesAsync(cancellationToken);

                var notifications = await CreateNotificationsAndLogs(booking, request.CustomerUsername, cancellationToken);

                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Success = true,
                    Message = string.Empty,
                    AdditionalInformation = notifications
                };
            }
            catch (Exception ex)
            {
                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Success = false,
                    Message = ex.InnerException.Message
                };
            }
        }

        public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogs(Domain.Entities.Booking booking, string username, CancellationToken cancellationToken)
        {
            _context.Messages.Add(new Message() { Conversation = booking.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"{username} added a review." });
            await _context.SaveChangesAsync(cancellationToken);

            var notificationMessage = $"{username} added a review for {(username == booking.HeadProposal.Ghostwriter.UserName ? booking.HeadProposal.Project.Customer.UserName : booking.HeadProposal.Ghostwriter.UserName)} regarding project '{booking.HeadProposal.Project.ProjectTopic}'.";
            var adminMessage = notificationMessage;
            var detailsLink = PathBuilderHelper.BookingDetailsPath(booking.Id, booking.HeadProposal.Id);
            var notificationType = NotificationType.ActiveProject;
            int receiverId = username == booking.HeadProposal.Ghostwriter.UserName ? booking.HeadProposal.Project.Customer.Id : booking.HeadProposal.Ghostwriter.Id;

            var notifications = await _notificationService.SendNotifications(cancellationToken, booking.HeadProposal.Id, notificationMessage, detailsLink, notificationType, true, adminMessage, receiverId);

            return notifications;
        }
    }
}
