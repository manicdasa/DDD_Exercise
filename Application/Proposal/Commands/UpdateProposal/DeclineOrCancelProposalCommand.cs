using System;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Models;
using System.Linq;
using GhostWriter.Domain.Defaults;
using GhostWriter.Domain.Enums;
using System.Collections.Generic;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Common.Helpers;

namespace GhostWriter.Application.Proposal.Commands.UpdateProposal
{
    /// <summary>
    /// Decline or cancel the proposal by customer or author
    /// </summary>
    public class DeclineOrCancelProposalCommand : IRequest<ExtendedOutputModelList<NotificationSignalRDTO>> 
    {
        public int ProposalId { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
    }

    public class DeclineOrCancelProposalCommandHandler : IRequestHandler<DeclineOrCancelProposalCommand, ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IProposalService _proposalService;
        private readonly INotificationService _notificationService;

        public DeclineOrCancelProposalCommandHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IProposalService proposalService, INotificationService notificationService)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _proposalService = proposalService;
            _notificationService = notificationService;
        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> Handle(DeclineOrCancelProposalCommand request, CancellationToken cancellationToken)
        {
            if (!(request.RoleName == UserRoleDefaults.CustomerRoleName || request.RoleName == UserRoleDefaults.GhostwriterRoleName))
                throw new AuthorizationException($"User {request.Username} is unauthorized to decline/cancel the offer.");

            var user = await _userManagementFactory.FindUser(request.Username);

            if(user == null)
                throw new NotFoundException($"User {request.Username} not found.");

            if (!_userManagementFactory.IsInRole(user, request.RoleName))
                throw new AuthorizationException($"User {request.Username} is unauthorized to decline/cancel the offer.");

            var proposal = _context.Proposals.Where(x => 
                    x.Id == request.ProposalId 
                    && (request.RoleName == UserRoleDefaults.CustomerRoleName 
                        ? x.HeadProposal.Project.CustomerId == user.Id
                        : (request.RoleName == UserRoleDefaults.GhostwriterRoleName ? x.HeadProposal.GHWId == user.Id : 1==0))
                    && x.ChildProposal == null).FirstOrDefault();

            if (proposal == null)
                throw new NotFoundException($"The proposal is not found or there is a counterproposal to this proposal.");

            var result = await _proposalService.DeclineCancelProposal(request.RoleName, proposal, cancellationToken);

            var notifications = await CreateNotificationsAndLogs(proposal, request.Username, result.AdditionalInformation.ProposalStatus.ToString().ToLower(), cancellationToken);

            return new ExtendedOutputModelList<NotificationSignalRDTO>()
            {
                Message = result.Message,
                Success = result.Success,
                AdditionalInformation = notifications
            };
        }

        public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogs(Domain.Entities.Proposal prop, string username, string action, CancellationToken cancellationToken)
        {
            Message message = new Message() { Conversation = prop.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"Project is {action} by {username}." };
            _context.Messages.Add(message);
            await _context.SaveChangesAsync(cancellationToken);

            var notificationMessage = $"{prop.ProposalType.ToString()} '{prop.HeadProposal.Project.ProjectTopic}' is {action} by {username}.";
            var adminMessage = notificationMessage;
            var detailsLink = PathBuilderHelper.ProjectDetailsPath(prop.HeadProposal.Project.Id);
            var notificationType = prop.ProposalType == ProposalType.Bid ? NotificationType.Bid : NotificationType.NewOffer;
            var receiverId = username == prop.HeadProposal.Ghostwriter.UserName ? prop.HeadProposal.Project.CustomerId : prop.HeadProposal.GHWId;

            var notifications = await _notificationService.SendNotifications(cancellationToken, prop.Id, notificationMessage, detailsLink, notificationType, true, adminMessage, receiverId);
            _notificationService.AddSidePanelNotifications(ref notifications, prop, EventType.Delete, prop.ProposalType == ProposalType.Bid ? PanelTab.Bid : PanelTab.Offer, prop.HeadProposal.GHWId == receiverId ? prop.HeadProposal.Project.CustomerId : prop.HeadProposal.GHWId);

            return notifications;
        }
    }
}
