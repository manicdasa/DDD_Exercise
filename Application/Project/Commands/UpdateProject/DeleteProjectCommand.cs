using System;
using System.Collections.Generic;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.Common.Exceptions;
using GhostWriter.Domain.Defaults;
using System.Linq;
using GhostWriter.Application.Common.Helpers;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.Project.Commands
{
    public class DeleteProjectCommand : IRequest<ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        public int ProjectId { get; set; }
        public string CustomerUsername { get; set; }
    }

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IProposalService _proposalService;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly INotificationService _notificationService;

        public DeleteProjectCommandHandler(IApplicationDbContext context, INotificationService notificationService, IUserManagementFactory userManagementFactory, IProposalService proposalService)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _notificationService = notificationService;
            _proposalService = proposalService;
        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {

            var customer = await _userManagementFactory.FindUser(request.CustomerUsername);

            if (customer == null)
                throw new NotFoundException($"Customer not found.");

            var project = await _context.Projects.FindAsync(request.ProjectId);

            if (project is null)
                throw new Exception("Project not found.");

            if (project.CustomerId != customer.Id)
                throw new AuthorizationException($"{request.CustomerUsername} is not authorized to delete this project.");

            var booking = _context.Bookings.Where(x => x.HeadProposal.Project.Id == project.Id).FirstOrDefault();
            if (booking != null && booking.IsNotInStatus(BookingStatusGroups.Closed))
                throw new Exception("Project cannot be edited because there's an ongoing project.");

            try
            {
                //project.ProjectStatus = Domain.Enums.ProjectStatus.Deleted;
                //project.LastUpdate = DateTime.UtcNow;

                _context.Projects.Update(project);
                await _context.SaveChangesAsync(cancellationToken);

                var existingProposals = _context.Proposals.Where(x => x.HeadProposal.Project.Id == project.Id && x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus == Domain.Enums.ProposalStatus.Active).ToList();

                foreach(var existingProposal in existingProposals)
                {
                    await _proposalService.DeclineCancelProposal(UserRoleDefaults.CustomerRoleName, existingProposal, cancellationToken);
                }

                var notifications = await CreateNotificationsAndLogs(existingProposals, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

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

        public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogs(List<Domain.Entities.Proposal> existingProposals, CancellationToken cancellationToken)
        {
            List<NotificationSignalRDTO> notifications = new List<NotificationSignalRDTO>();
            List<Message> logMessages = new List<Message>();

            foreach (var prop in existingProposals)
            {
                logMessages.Add(new Message() { Conversation = prop.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"{prop.HeadProposal.Project.Customer.UserName} deleted project '{prop.HeadProposal.Project.ProjectTopic}', so the {prop.ProposalType.ToString().ToLower()} is cancelled." });

                var notificationMessage = $"{prop.HeadProposal.Project.Customer.UserName} deleted project '{prop.HeadProposal.Project.ProjectTopic}', so your {prop.ProposalType.ToString().ToLower()} is cancelled.";
                var adminMessage = $"{prop.HeadProposal.Project.Customer.UserName} deleted project '{prop.HeadProposal.Project.ProjectTopic}', so it's {prop.ProposalType.ToString().ToLower()} (Id = {prop.Id}) is cancelled.";
                var detailsLink = PathBuilderHelper.ProjectDetailsPath(prop.HeadProposal.Project.Id);
                var notificationType = prop.ProposalType == ProposalType.Bid ? NotificationType.Bid : NotificationType.NewOffer;

                var sendResult = await _notificationService.SendNotifications(cancellationToken, prop.Id, notificationMessage, detailsLink, notificationType, true, adminMessage, prop.HeadProposal.GHWId);
                _notificationService.AddSidePanelNotifications(ref notifications, prop, EventType.Delete, prop.ProposalType == ProposalType.Bid ? PanelTab.Bid : PanelTab.Offer, prop.HeadProposal.Project.CustomerId);

                notifications.AddRange(sendResult);
            }

            _context.Messages.AddRange(logMessages);
            await _context.SaveChangesAsync(cancellationToken);

            return notifications;
        }
    }
}
