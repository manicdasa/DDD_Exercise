using System;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.DTOs;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using GhostWriter.Domain.Enums;
using GhostWriter.Application.Common.Helpers;
using GhostWriter.Domain.Defaults;

namespace GhostWriter.Application.Project.Commands
{
    public class SendMessageCommand : IRequest<ResponseWithPayload<MessageDTO>>
    {
        public string Username { get; set; }
        public int HeadProposalId { get; set; }
        public int ExactEntityId { get; set; }
        public string Message { get; set; }
    }
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ResponseWithPayload<MessageDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IProposalService _proposalService;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public SendMessageCommandHandler(IApplicationDbContext context, IMapper mapper, IProposalService proposalService, IUserManagementFactory userManagementFactory, INotificationService notificationService)
        {
            _context = context;
            _proposalService = proposalService;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<ResponseWithPayload<MessageDTO>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to access the booking chat.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user == null)
                throw new NotFoundException($"User {request.Username} not found.");

            var headProposal = _context.HeadProposals.Find(request.HeadProposalId);

            if (headProposal == null)
                throw new NotFoundException($"Head proposal {request.HeadProposalId} not found.");

            var booking = _context.Bookings.Where(x => x.HeadProposal.Id == request.HeadProposalId).FirstOrDefault();

            if (headProposal.Ghostwriter.UserName != request.Username && headProposal.Project.Customer.UserName != request.Username)
                throw new AuthorizationException($"User is unauthorized to access the booking chat.");

            var conversation = _context.Conversations.Find(headProposal.Conversation.Id);

            if (conversation == null)
                throw new NotFoundException($"Conversation {headProposal.Conversation.Id} not found.");

            List<ProposalStatus> inactiveStatues = new List<ProposalStatus>() { ProposalStatus.Cancelled, ProposalStatus.Declined, ProposalStatus.Deleted };
            if (_context.Proposals.Where(x => x.HeadProposal.Id == headProposal.Id && x.ChildProposal == null && inactiveStatues.Contains(x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus)).Any())
                throw new Exception($"You can't send a message for this bid/offer because it's inactive.");

            if (booking != null && booking.IsNotInStatus(BookingStatusGroups.OpenNoDispute))
                throw new Exception($"You can't send a message a project in this status.");

            try
            {
                var message = new Message()
                {
                   Conversation = conversation,
                   DateTimeSent = DateTime.UtcNow,
                   MessageText = request.Message,
                   SentByUser = user
                };

                _context.Messages.Add(message);
                await _context.SaveChangesAsync(cancellationToken);

                var messageDTO = _mapper.Map<MessageDTO>(message);

                var proposalType = _context.Proposals.Where(x => x.HeadProposal.Id == headProposal.Id && x.ChildProposal == null).FirstOrDefault().ProposalType;
                var notifications = await CreateNotification(conversation, booking != null, booking != null ? booking.Id : default(int), proposalType, user.UserName, cancellationToken);
                
                if (booking != null)
                {
                    var panelNotifications = _notificationService.AddSidePanelNotifications(booking, EventType.Change, PanelTab.Chat, booking.HeadProposal.GHWId, booking.HeadProposal.Project.CustomerId);
                    notifications.AddRange(panelNotifications);
                }
                else
                {
                    var proposal = _context.Proposals.Find(request.ExactEntityId);
                    if (proposal != null)
                    {
                        var panelNotifications = _notificationService.AddSidePanelNotifications(proposal, EventType.Change, proposal.ProposalType == ProposalType.Bid ? PanelTab.Bid : PanelTab.Offer, proposal.HeadProposal.GHWId, proposal.HeadProposal.Project.CustomerId);
                        notifications.AddRange(panelNotifications);
                    }
                }

                return new ResponseWithPayload<MessageDTO>()
                {
                    Success = true,
                    Message = string.Empty,
                    SuccessPayload = messageDTO,
                    Notifications = notifications.ToList()
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithPayload<MessageDTO>()
                {
                    Success = false,
                    Message = ex.InnerException.Message
                };
            }
        }

        public async Task<List<NotificationSignalRDTO>> CreateNotification(Domain.Entities.Conversation conversation, bool isBooking, int bookingId, ProposalType proposalType, string username, CancellationToken cancellationToken)
        {
            var notificationMessage = $"New message from {username} regarding project '{conversation.HeadProposal.Project.ProjectTopic}'.";
            var adminMessage = notificationMessage;
            var detailsLink = isBooking ? PathBuilderHelper.BookingDetailsPath(bookingId, conversation.HeadProposal.Id) : PathBuilderHelper.ProjectDetailsPath(conversation.HeadProposal.Project.Id);
            var notificationType = isBooking ? NotificationType.ActiveProject : (proposalType == ProposalType.Bid ? NotificationType.Bid : NotificationType.NewOffer);
            var receiverId = conversation.HeadProposal.Ghostwriter.UserName == username ? conversation.HeadProposal.Project.Customer.Id : conversation.HeadProposal.Ghostwriter.Id;

            var notifications = await _notificationService.SendNotifications(cancellationToken, conversation.HeadProposal.Id, notificationMessage, detailsLink, notificationType, false, "", receiverId);

            return notifications;
        }
    }
}
