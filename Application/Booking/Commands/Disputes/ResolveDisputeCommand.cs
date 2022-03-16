using System;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using System.Linq;
using GhostWriter.Domain.Defaults;
using GhostWriter.Domain.Enums;
using System.Transactions;
using GhostWriter.Domain.Entities;
using System.Collections.Generic;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Common.Helpers;

namespace GhostWriter.Application.Booking.Commands.Disputes
{
    public class ResolveDisputeCommand : IRequest<ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        public string Username { get; set; }
        public int BookingId { get; set; }
        public string Message { get; set; }

        public decimal RefundAmount { get; set; }
        public decimal PaymentAmountAuthor { get; set; }

        public DisputeStatus NewDisputeStatus { get; set; }
    }
    public class AcceptDisputeCommandHandler : IRequestHandler<ResolveDisputeCommand, ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IProposalService _proposalService;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly INotificationService _notificationService;

        public AcceptDisputeCommandHandler(IApplicationDbContext context, IProposalService proposalService, IUserManagementFactory userManagementFactory, INotificationService notificationService)
        {
            _context = context;
            _proposalService = proposalService;
            _userManagementFactory = userManagementFactory;
            _notificationService = notificationService;
        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> Handle(ResolveDisputeCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to resolve a dispute.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user is null)
                throw new NotFoundException($"Customer {request.Username} not found.");

            if (request.NewDisputeStatus == DisputeStatus.Active)
                throw new NotFoundException($"Admin {request.Username} needs to either accept either decline the dispute.");

            var booking = _context.Bookings.Find(request.BookingId);

            if (booking is null)
                throw new NotFoundException(nameof(Domain.Entities.Booking), request.BookingId);

            var bookingStatuses = booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).ToList();
            if (bookingStatuses.FirstOrDefault() != null && (!BookingStatusGroups.InDispute.Contains(bookingStatuses.FirstOrDefault().BookingStatus)))
                throw new Exception($"A dispute cannot be accepted because the booking is not in dispute.");

            var lastDispute = booking.Disputes.OrderByDescending(x => x.DateCreated).FirstOrDefault();
            if(lastDispute == null || lastDispute.DisputeStatus != DisputeStatus.Active)
                throw new Exception($"There's no active dispute for this project.");

            try
            {
                lastDispute.DateClosed = DateTime.UtcNow;
                lastDispute.Resolution = request.Message;
                lastDispute.DisputeStatus = request.NewDisputeStatus;
                lastDispute.RefundAmount = request.RefundAmount;
                lastDispute.PaymentToAuthor = request.PaymentAmountAuthor;
               
                booking.LastUpdate = DateTime.UtcNow;
                _context.Bookings.Update(booking);

                _context.Disputes.Update(lastDispute);
                await _context.SaveChangesAsync(cancellationToken);

                var notifications = await CreateNotificationsAndLogs(booking, request.Message, request.NewDisputeStatus.ToString().ToLower(), cancellationToken);

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

        public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogs(Domain.Entities.Booking booking, string adminsMessage, string disputeStatus,  CancellationToken cancellationToken)
        {
            string message = string.IsNullOrWhiteSpace(adminsMessage) ? string.Empty : $"Resolution: {adminsMessage}";
            _context.Messages.Add(new Message() { Conversation = booking.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"Admin {disputeStatus} the dispute. {message}" });
            await _context.SaveChangesAsync(cancellationToken);

            var notificationMessage = $"Admin {disputeStatus} the dispute for project '{booking.HeadProposal.Project.ProjectTopic}'.";
            var adminMessage = notificationMessage;
            var detailsLink = PathBuilderHelper.BookingDetailsPath(booking.Id, booking.HeadProposal.Id);
            var notificationType = NotificationType.ActiveProject;

            var notifications = await _notificationService.SendNotifications(cancellationToken, booking.HeadProposal.Id, notificationMessage, detailsLink, notificationType, false, adminMessage, booking.HeadProposal.GHWId, booking.HeadProposal.Project.CustomerId);

            return notifications;
        }
    }
}
