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
using GhostWriter.Application.DTOs;
using System.Collections.Generic;
using GhostWriter.Application.Common.Helpers;

namespace GhostWriter.Application.Booking.Commands.AddReview
{
    public class CancelProjectCommand : IRequest<ExtendedOutputModel<Domain.Entities.Booking>>
    {
        public int BookingId { get; set; }
        public string CustomerUsername { get; set; }

        public class CancelProjectCommandHandler : IRequestHandler<CancelProjectCommand, ExtendedOutputModel<Domain.Entities.Booking>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IUserManagementFactory _userManagementFactory;
            private readonly INotificationService _notificationService;

            public CancelProjectCommandHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, INotificationService notificationService)
            {
                _context = context;
                _userManagementFactory = userManagementFactory;
                _notificationService = notificationService;
            }

            public async Task<ExtendedOutputModel<Domain.Entities.Booking>> Handle(CancelProjectCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.CustomerUsername))
                    throw new AuthorizationException($"User is unauthorized to confirm a project.");

                var user = await _userManagementFactory.FindUser(request.CustomerUsername);

                if (user is null)
                    throw new NotFoundException($"User {request.CustomerUsername} not found.");

                if (!_userManagementFactory.IsInRole(user, UserRoleDefaults.CustomerRoleName) && !_userManagementFactory.IsInRole(user, UserRoleDefaults.AdminRoleName))
                    throw new AuthorizationException($"User is unauthorized to confirm a project.");

                var booking = _context.Bookings.Find(request.BookingId);

                if (booking is null)
                    throw new NotFoundException($"Booking {request.BookingId} not found.");

                if (booking.HeadProposal.Project.Customer.UserName != request.CustomerUsername && _userManagementFactory.IsInRole(user, UserRoleDefaults.CustomerRoleName))
                    throw new AuthorizationException($"User is unauthorized to to confirm this project.");

                var lastStatus = booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault();
                if (!BookingStatusGroups.RequiredForCancelledState.Contains(booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault().BookingStatus))
                    throw new Exception($"Customer cannot cancel project because it's not inactive.");
                try
                {
                    BookingStatusHistory newStatus = new BookingStatusHistory()
                    {
                        Booking = booking,
                        DateCreated = DateTime.UtcNow,
                        BookingStatus = BookingStatus.Cancelled
                    };

                    booking.LastUpdate = DateTime.UtcNow;
                    _context.Bookings.Update(booking);

                    booking.BookingStatusHistories.Add(newStatus);
                    await _context.SaveChangesAsync(cancellationToken);

                    var notifications = await CreateNotificationsAndLogs(booking, cancellationToken);

                    return new ExtendedOutputModel<Domain.Entities.Booking>()
                    {
                        Success = true,
                        Message = string.Empty,
                        AdditionalInformation = booking
                    };
                }
                catch (Exception ex)
                {
                    return new ExtendedOutputModel<Domain.Entities.Booking>()
                    {
                        Success = false,
                        Message = ex.InnerException.Message
                    };
                }
            }

            public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogs(Domain.Entities.Booking booking, CancellationToken cancellationToken)
            {
                _context.Messages.Add(new Message() { Conversation = booking.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"{booking.HeadProposal.Project.Customer.UserName} cancelled the ongoing project. Project will be deleted." });
                await _context.SaveChangesAsync(cancellationToken);

                var notificationMessage = $"{booking.HeadProposal.Project.Customer.UserName} cancelled the ongoing project '{booking.HeadProposal.Project.ProjectTopic}'. Project will be deleted..";
                var adminMessage = notificationMessage;
                var detailsLink = PathBuilderHelper.BookingDetailsPath(booking.Id, booking.HeadProposal.Id);
                var notificationType = NotificationType.ActiveProject;

                var notifications = await _notificationService.SendNotifications(cancellationToken, booking.HeadProposal.Id, notificationMessage, detailsLink, notificationType, true, adminMessage, booking.HeadProposal.GHWId);
                _notificationService.AddSidePanelNotifications(ref notifications, booking, EventType.Delete, PanelTab.Chat, booking.HeadProposal.Project.CustomerId);

                return notifications;
            }
        }
    }
}
