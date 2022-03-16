using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Domain.Defaults;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Domain.Enums;
using System;
using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Entities;
using System.Collections.Generic;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Common.Helpers;
using AutoMapper;

namespace GhostWriter.Application.Booking.Commands.AddReview
{
    public class ConfirmProjectCommand : IRequest<ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        public int BookingId { get; set; }
        public string CustomerUsername { get; set; }

        public class ConfirmProjectCommandHandler : IRequestHandler<ConfirmProjectCommand, ExtendedOutputModelList<NotificationSignalRDTO>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IUserManagementFactory _userManagementFactory;
            private readonly INotificationService _notificationService;
            private readonly IMapper _mapper;
            public ConfirmProjectCommandHandler(IApplicationDbContext context, IMapper mapper, IUserManagementFactory userManagementFactory, INotificationService notificationService)
            {
                _context = context;
                _userManagementFactory = userManagementFactory;
                _notificationService = notificationService;
                _mapper = mapper;
            }

            public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> Handle(ConfirmProjectCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.CustomerUsername))
                    throw new AuthorizationException($"User is unauthorized to confirm a project.");

                var user = await _userManagementFactory.FindUser(request.CustomerUsername);

                if (user is null)
                    throw new NotFoundException($"User {request.CustomerUsername} not found.");

                var booking = _context.Bookings.Find(request.BookingId);

                if (booking is null)
                    throw new NotFoundException($"Booking {request.BookingId} not found.");

                if (booking.HeadProposal.Project.Customer.UserName != request.CustomerUsername && !_userManagementFactory.IsInRole(user, UserRoleDefaults.CustomerRoleName) && !_userManagementFactory.IsInRole(user, UserRoleDefaults.AdminRoleName))
                    throw new AuthorizationException($"User is unauthorized to confirm a project.");

                var lastDispute = booking.Disputes.OrderByDescending(x => x.DateCreated).FirstOrDefault();
                if (lastDispute != null && lastDispute.DisputeStatus == DisputeStatus.Active)
                    throw new Exception($"Customer cannot confirm for this booking because it has an active nonresolved dispute.");

                if (!BookingStatusGroups.RequiredForClosedState.Contains(booking.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus))
                    throw new Exception($"Customer cannot confirm for this booking.");
                try
                {
                    BookingStatusHistory newStatus = new BookingStatusHistory()
                    {
                        Booking = booking,
                        DateCreated = DateTime.UtcNow,
                        BookingStatus = booking.Disputes.Any() ? BookingStatus.ClosedAfterDispute : BookingStatus.Closed
                    };

                    booking.LastUpdate = DateTime.UtcNow;
                    _context.Bookings.Update(booking);

                    booking.BookingStatusHistories.Add(newStatus);
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

            public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogs(Domain.Entities.Booking booking,  string username, CancellationToken cancellationToken)
            {
                _context.Messages.Add(new Message() { Conversation = booking.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"{(booking.Disputes.Any() ? "Admin" : username)} successfully closed the project." });
                await _context.SaveChangesAsync(cancellationToken);

                var notificationMessage = $"{(booking.Disputes.Any() ? "Admin" : username  )} successfully closed the project '{booking.HeadProposal.Project.ProjectTopic}'.";
                var adminMessage = notificationMessage;
                var detailsLink = PathBuilderHelper.BookingDetailsPath(booking.Id, booking.HeadProposal.Id);
                var notificationType = NotificationType.ActiveProject;

                var notifications = await _notificationService.SendNotifications(cancellationToken, booking.HeadProposal.Id, notificationMessage, detailsLink, notificationType, true, adminMessage, booking.HeadProposal.GHWId);
                _notificationService.AddSidePanelNotifications(ref notifications, booking, EventType.Change, PanelTab.Chat, booking.HeadProposal.Project.CustomerId);

                return notifications;
            }
        }
    }
}
