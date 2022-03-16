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
using GhostWriter.Domain.Entities;
using System.Collections.Generic;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Common.Helpers;
using AutoMapper;
using GhostWriter.Application.Defaults;
using GhostWriter.Domain.Services;

namespace GhostWriter.Application.Proposal.Commands.CreateProposal
{
    public class CreateProposalCommand : IRequest<ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        public int ProjectId { get; set; }
        public decimal FinancialOffer { get; set; }
        public int GHWId { get; set; }
        public string CustomerUsername { get; set; }
        public string RoleName { get; set; }
    }

    public class CreateProposalCommandHandler : IRequestHandler<CreateProposalCommand, ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IProposalService _proposalService;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly IPriceCalculatorService _priceCalculatorService;

        public CreateProposalCommandHandler(IApplicationDbContext context, IMapper mapper, IProposalService proposalService, IUserManagementFactory userManagementFactory, INotificationService notificationService, IPriceCalculatorService priceCalculatorService)
        {
            _context = context;
            _proposalService = proposalService;
            _userManagementFactory = userManagementFactory;
            _notificationService = notificationService;
            _mapper = mapper;
            _priceCalculatorService = priceCalculatorService;
        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> Handle(CreateProposalCommand request, CancellationToken cancellationToken)
        {
            if (request.GHWId == default(int))
                throw new AuthorizationException($"User is unauthorized to bid on a project.");

            if (!(request.RoleName == UserRoleDefaults.CustomerRoleName || request.RoleName == UserRoleDefaults.GhostwriterRoleName))
                throw new AuthorizationException($"User is unauthorized to offer or bid on the project.");

            var ghw = _userManagementFactory.FindUserById(request.GHWId);

            if (ghw == null)
                throw new NotFoundException($"Author not found.");

            if (!_userManagementFactory.IsInRole(ghw, UserRoleDefaults.GhostwriterRoleName))
                throw new AuthorizationException($"User is unauthorized to bid on a project.");

            var project = _context.Projects.Find(request.ProjectId);

            if (project == null)
                throw new NotFoundException(nameof(Domain.Entities.Project), request.ProjectId);

            if (!string.IsNullOrWhiteSpace(request.CustomerUsername))
            {
                var customer = await _userManagementFactory.FindUser(request.CustomerUsername);

                if(request.RoleName == UserRoleDefaults.CustomerRoleName)
                {
                    if (customer == null)
                        throw new NotFoundException($"User {request.CustomerUsername} not found.");

                    if (!_userManagementFactory.IsInRole(customer, UserRoleDefaults.CustomerRoleName))
                        throw new AuthorizationException($"User {request.CustomerUsername} is unauthorized to offer a project to an author.");

                    if (project.CustomerId != customer.Id)
                        throw new AuthorizationException($"Customer {request.CustomerUsername} is unauthorized to make an offer on a project that is not created by them.");
                }
            }
            else if (!_priceCalculatorService.ValidatePrice(request.FinancialOffer, project.PagesNo))
                throw new Exception($"Minimal financial offer must be above {project.PagesNo * BusinessDefaults.MinimalPricePerPage}");

            var headProposal = _context.HeadProposals.Where(x => x.GHWId == ghw.Id && x.Project.Id == project.Id).FirstOrDefault();

            var proposalType = request.RoleName == UserRoleDefaults.CustomerRoleName ? ProposalType.Offer : ProposalType.Bid;

            if (headProposal != null)
            {
                var lastProposalStatus = _context.Proposals.Where(x => x.HeadProposal == headProposal && x.ChildProposal == null).FirstOrDefault().ProposalStatuses.OrderByDescending(x => x.DateCreated).FirstOrDefault();

                if (lastProposalStatus.ProposalStatus == ProposalStatus.Accepted || lastProposalStatus.ProposalStatus == ProposalStatus.Active)
                    throw new Exception($"An active bid/offer for this project and author already exists.");
            }
            else
            {
                var proposalCreationResult = await _proposalService.CreateHeadProposal(ghw, project, proposalType, cancellationToken);

                if (!proposalCreationResult.Success) 
                    return   new ExtendedOutputModelList<NotificationSignalRDTO>()
                    {
                        Message = proposalCreationResult.Message,
                        Success = proposalCreationResult.Success
                    };

                headProposal = proposalCreationResult.AdditionalInformation;
            }

            var result = await _proposalService.CreateProposal(headProposal, request.FinancialOffer, project, proposalType, cancellationToken);

            if (result.Success)
            {
                var notifications = await CreateNotificationsAndLogs(result.AdditionalInformation, request.RoleName, cancellationToken);

                return new ExtendedOutputModelList<NotificationSignalRDTO>()
                {
                    Message = result.Message,
                    Success = result.Success,
                    AdditionalInformation = notifications,
                    
                };
            }

            return new ExtendedOutputModelList<NotificationSignalRDTO>()
            {
                Message = result.Message,
                Success = result.Success
            };
        }

        public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogs(Domain.Entities.Proposal proposal, string roleName, CancellationToken cancellationToken)
        {
            _context.Messages.Add(new Message() { Conversation = proposal.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"{proposal.ProposalType.ToString()} created." });
            await _context.SaveChangesAsync(cancellationToken);

            var notificationMessage = $"{proposal.ProposalType.ToString()} created for project '{proposal.HeadProposal.Project.ProjectTopic}'.";
            var adminMessage = notificationMessage;
            var detailsLink = PathBuilderHelper.ProjectDetailsPath(proposal.HeadProposal.Project.Id);
            var notificationType = proposal.ProposalType == ProposalType.Bid ? NotificationType.Bid : NotificationType.NewOffer;
            var receiverId = roleName == UserRoleDefaults.CustomerRoleName ? proposal.HeadProposal.GHWId : proposal.HeadProposal.Project.CustomerId;

            var notifications = await _notificationService.SendNotifications(cancellationToken, proposal.Id, notificationMessage, detailsLink, notificationType, true, adminMessage, receiverId);
            _notificationService.AddSidePanelNotifications(ref notifications, proposal, EventType.Create, proposal.ProposalType == ProposalType.Bid ? PanelTab.Bid : PanelTab.Offer, roleName == UserRoleDefaults.CustomerRoleName ? proposal.HeadProposal.Project.CustomerId : proposal.HeadProposal.GHWId);

            return notifications;
        }
    }
}
