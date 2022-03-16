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

namespace GhostWriter.Application.Booking.Commands.CreateBooking
{
    public class AcceptProposalCreateBookingCommand : IRequest<ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        public string Username { get; set; }
        public string RoleName { get; set; }
        public int ProposalId { get; set; }
    }
    public class AcceptProposalCreateBookingCommandHandler : IRequestHandler<AcceptProposalCreateBookingCommand, ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IProposalService _proposalService;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly INotificationService _notificationService;

        //private static Semaphore _semaphore;

        public AcceptProposalCreateBookingCommandHandler(IApplicationDbContext context, IProposalService proposalService, IUserManagementFactory userManagementFactory, INotificationService notificationService)
        {
            _context = context;
            _proposalService = proposalService;
            _userManagementFactory = userManagementFactory;
            _notificationService = notificationService;
        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> Handle(AcceptProposalCreateBookingCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to accept an offer.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if (user is null)
                throw new NotFoundException($"Author {request.Username} not found.");

            var proposal = _context.Proposals.Find(request.ProposalId);

            if (proposal is null)
                throw new NotFoundException(nameof(Domain.Entities.Proposal), request.ProposalId);

            //_semaphore = new Semaphore(0, 1, proposal.HeadProposal.Project.Id.ToString());

            if (_context.Bookings.Where(x => x.HeadProposal.Project.Id == proposal.HeadProposal.Project.Id).Any())
                throw new Exception("Project is already assigned.");

            if ((request.RoleName == UserRoleDefaults.GhostwriterRoleName && proposal.HeadProposal.GHWId != user.Id) || (request.RoleName == UserRoleDefaults.CustomerRoleName && proposal.HeadProposal.Project.CustomerId != user.Id))
                throw new AuthorizationException($"User {request.Username} is unauthorized to accept this offer/bid.");

            if ((request.RoleName == UserRoleDefaults.GhostwriterRoleName && proposal.ProposalType != ProposalType.Offer) || (request.RoleName == UserRoleDefaults.CustomerRoleName && proposal.ProposalType != ProposalType.Bid))
                throw new AuthorizationException($"User {request.Username} is unauthorized to accept this offer/bid because it was created by themselves.");

            var proposalStatus = proposal.ProposalStatuses.OrderByDescending(x => x.DateCreated).FirstOrDefault();
            if (proposalStatus != null && proposalStatus.ProposalStatus != ProposalStatus.Active)
                throw new Exception($"A booking cannot be created because the offer is not active anymore.");

            try
            {
               // proposal.HeadProposal.Project.ProjectStatus = ProjectStatus.Closed;

                _context.Projects.Update(proposal.HeadProposal.Project);
                await _context.SaveChangesAsync(cancellationToken);

                var otherExistingProposals = _context.Proposals.Where(x => x.Id != proposal.Id && x.HeadProposal.Project.Id == proposal.HeadProposal.Project.Id && x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus == ProposalStatus.Active).ToList();
                foreach (var prop in otherExistingProposals)
                {
                    await _proposalService.DeclineCancelProposal(request.RoleName, prop, cancellationToken);
                }
                var notifications = await CreateNotificationsAndLogsOtherProposals(otherExistingProposals, request.Username, cancellationToken);
             
                proposal.ProposalStatuses.Add(new ProposalStatusHistory()
                {
                    Proposal = proposal,
                    DateCreated = DateTime.UtcNow,
                    ProposalStatus = ProposalStatus.Accepted
                });
                proposal.LastUpdate = DateTime.UtcNow;
                _context.Proposals.Update(proposal);
                await _context.SaveChangesAsync(cancellationToken);

                Domain.Entities.Booking booking = new Domain.Entities.Booking()
                {
                    HeadProposal = proposal.HeadProposal,
                    TotalPrice = proposal.FinancialOffer,
                    TotalServiceCharges = proposal.ServiceCharges,
                    DateCreated = DateTime.UtcNow,
                    LastUpdate = DateTime.UtcNow
                };

                BookingStatusHistory newStatus = new BookingStatusHistory()
                {
                    DateCreated = DateTime.UtcNow,
                    Booking = booking,
                    BookingStatus = BookingStatus.Inactive
                };
                
                _context.BookingStatusHistories.Add(newStatus);
                _context.Bookings.Add(booking);

                await _context.SaveChangesAsync(cancellationToken);

                //_semaphore.Release();

                notifications.AddRange(await CreateNotificationsAndLogs(booking, proposal, request.Username, cancellationToken));

                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Success = true,
                    Message = string.Empty,
                    AdditionalInformation = notifications
                };
            }
            catch(Exception ex)
            {
                //_semaphore.Release();

                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Success = false,
                    Message = ex.InnerException.Message
                };
            }
        }
        public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogs(Domain.Entities.Booking booking, Domain.Entities.Proposal proposal, string username, CancellationToken cancellationToken)
        {
            _context.Messages.Add(new Message() { Conversation = booking.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"{username} accepted {proposal.ProposalType.ToString().ToLower()} from {proposal.HeadProposal.Ghostwriter.UserName}." });
            await _context.SaveChangesAsync(cancellationToken);

            var notificationMessage = $"Congrats, {username} accepted your {proposal.ProposalType.ToString().ToLower()} for project '{proposal.HeadProposal.Project.ProjectTopic}'!";
            var detailsLink = PathBuilderHelper.ProjectDetailsPath(booking.HeadProposal.Project.Id);
            var adminMessage = $"{username} accepted {proposal.ProposalType.ToString().ToLower()} for project '{proposal.HeadProposal.Project.ProjectTopic}'.";
            var notificationType = proposal.ProposalType == ProposalType.Bid ? NotificationType.Bid : NotificationType.NewOffer;
            int receiverId = username == proposal.HeadProposal.Ghostwriter.UserName ? proposal.HeadProposal.Project.CustomerId : proposal.HeadProposal.GHWId;

            var notifications = await _notificationService.SendNotifications(cancellationToken, proposal.Id, notificationMessage, detailsLink, notificationType, true, adminMessage, receiverId);

           _notificationService.AddSidePanelNotifications(ref notifications, booking, EventType.Create, PanelTab.Chat, username == proposal.HeadProposal.Ghostwriter.UserName ? proposal.HeadProposal.GHWId : proposal.HeadProposal.Project.CustomerId);

            var deleteProposalNotifications = _notificationService.AddSidePanelNotifications(proposal, EventType.Delete, proposal.ProposalType == ProposalType.Bid ? PanelTab.Bid : PanelTab.Offer, proposal.HeadProposal.GHWId, proposal.HeadProposal.Project.CustomerId);
            notifications.AddRange(deleteProposalNotifications);

            return notifications;
        }

        public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogsOtherProposals(List<Domain.Entities.Proposal> existingProposals, string username, CancellationToken cancellationToken)
        {
            List<NotificationSignalRDTO> notifications = new List<NotificationSignalRDTO>();
            List<Message> logMessages = new List<Message>();

            foreach (var prop in existingProposals)
            {
                logMessages.Add(new Message() { Conversation = prop.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"{username} accepted another offer for project {prop.HeadProposal.Project.ProjectTopic}, so the {prop.ProposalType.ToString().ToLower()} is cancelled." });

                var notificationMessage = $"Project '{prop.HeadProposal.Project.ProjectTopic}' is assigned to other author. Your {prop.ProposalType.ToString().ToLower()} is no longer active.";
                var detailsLink = PathBuilderHelper.ProjectDetailsPath(prop.HeadProposal.Project.Id);
                var notificationType = prop.ProposalType == Domain.Enums.ProposalType.Bid ? Domain.Enums.NotificationType.Bid : Domain.Enums.NotificationType.NewOffer;

                var sendResult = await _notificationService.SendNotifications(cancellationToken, prop.Id, notificationMessage, detailsLink, notificationType, false, "", prop.HeadProposal.GHWId);
                _notificationService.AddSidePanelNotifications(ref notifications, prop, EventType.Delete, prop.ProposalType == ProposalType.Bid ? PanelTab.Bid : PanelTab.Offer);

                notifications.AddRange(sendResult);
            }

            _context.Messages.AddRange(logMessages);
            await _context.SaveChangesAsync(cancellationToken);

            return notifications;
        }
    }
}
