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
    public class CreateDisputeCommand : IRequest<ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        public string Username { get; set; }
        public int BookingId { get; set; }
        public string Message { get; set; }
    }
    public class CreateDisputeCommandHandler : IRequestHandler<CreateDisputeCommand, ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IProposalService _proposalService;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly INotificationService _notificationService;

        public CreateDisputeCommandHandler(IApplicationDbContext context, IProposalService proposalService, IUserManagementFactory userManagementFactory, INotificationService notificationService)
        {
            _context = context;
            _proposalService = proposalService;
            _userManagementFactory = userManagementFactory;
            _notificationService = notificationService;
        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> Handle(CreateDisputeCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to create a dispute.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user is null)
                throw new NotFoundException($"Customer {request.Username} not found.");

            var booking = _context.Bookings.Find(request.BookingId);

            if (booking is null)
                throw new NotFoundException(nameof(Domain.Entities.Booking), request.BookingId);

            if (booking.HeadProposal.Project.CustomerId != user.Id)
                throw new Exception($"A dispute cannot be added for this booking by customer {request.Username}.");

            var bookingStatus = booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault();
            if (bookingStatus != null && (!BookingStatusGroups.RequiredForDisputeState.Contains(bookingStatus.BookingStatus)))
                throw new Exception($"A dispute cannot be opened.");

            try
            {
                var dispute = new Dispute()
                {
                    Booking = booking,
                    DateCreated = DateTime.UtcNow,
                    Reason = request.Message,
                    DisputeStatus = DisputeStatus.Active
                };

                _context.Disputes.Add(dispute);
                await _context.SaveChangesAsync(cancellationToken);

                BookingStatusHistory bookingStatusHistory = new BookingStatusHistory()
                {
                    Booking = booking,
                    DateCreated = DateTime.UtcNow,
                    BookingStatus = BookingStatus.InDispute
                };

                booking.LastUpdate = DateTime.UtcNow;
                _context.Bookings.Update(booking);

                _context.BookingStatusHistories.Add(bookingStatusHistory);
                await _context.SaveChangesAsync(cancellationToken);

                var notifications = await CreateNotificationsAndLogs(booking, request.Message, cancellationToken);

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

        //TODO: SignalR - notify admin and author that dispute is created
        public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogs(Domain.Entities.Booking booking, string disputeMessage, CancellationToken cancellationToken)
        {
            string reason = string.IsNullOrWhiteSpace(disputeMessage) ? string.Empty : $"Reason: {disputeMessage}";
            _context.Messages.Add(new Message() { Conversation = booking.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"A dispute is created. {reason}" });
            await _context.SaveChangesAsync(cancellationToken);

            var notificationMessage = $"A dispute is created for project '{booking.HeadProposal.Project.ProjectTopic}'.";
            var adminMessage = notificationMessage;
            var detailsLink = PathBuilderHelper.BookingDetailsPath(booking.Id, booking.HeadProposal.Id);
            var notificationType = NotificationType.ActiveProject;

            var notifications = await _notificationService.SendNotifications(cancellationToken, booking.HeadProposal.Id, notificationMessage, detailsLink, notificationType, true, adminMessage, booking.HeadProposal.GHWId);
            _notificationService.AddSidePanelNotifications(ref notifications, booking, EventType.Change, PanelTab.Chat, booking.HeadProposal.Project.CustomerId);


            return notifications;
        }
    }
}
