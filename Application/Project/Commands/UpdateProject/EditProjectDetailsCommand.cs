using System;
using System.Collections.Generic;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Application.Common.Exceptions;
using GhostWriter.Domain.Defaults;
using System.Linq;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Common.Helpers;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.Project.Commands
{
    public class EditProjectDetailsCommand
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public decimal MaxBudget { get; set; }
        public string ProjectTopic { get; set; }
        public string Description { get; set; }
        public int PagesNo { get; set; }
        public int MinimumDegreeId { get; set; }
        public int LanguageId { get; set; }
        public int KindOfWorkId { get; set; }
        public List<int> ExpertiseAreaIds { get; set; }
    }

    public class EditProjectDetailsCommandExtended : EditProjectDetailsCommand, IRequest<ExtendedOutputModelList<NotificationSignalRDTO>>, IMapFrom<EditProjectDetailsCommand>
    {
        public string CustomerUsername { get; set; }
        public bool EditWholeEntity { get; set; }
        public bool IsPublished { get; set; }
    }

    public class EditProjectDetailsCommandHandler : IRequestHandler<EditProjectDetailsCommandExtended, ExtendedOutputModelList<NotificationSignalRDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IProposalService _proposalService;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly INotificationService _notificationService;

        public EditProjectDetailsCommandHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IProposalService proposalService, INotificationService notificationService)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _proposalService = proposalService;
            _notificationService = notificationService;
        }

        public async Task<ExtendedOutputModelList<NotificationSignalRDTO>> Handle(EditProjectDetailsCommandExtended request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _userManagementFactory.FindUser(request.CustomerUsername);

                if (customer == null)
                    throw new NotFoundException($"Customer not found.");

                if (!_userManagementFactory.IsInRole(customer, UserRoleDefaults.CustomerRoleName))
                    throw new AuthorizationException($"User {customer.UserName} is unauthorized to create a project.");

                var entity = await _context.Projects.FindAsync(request.Id);

                if (entity == null)
                    throw new Exception("Project not found.");

                if (_context.Bookings.Where(x => x.HeadProposal.Project.Id == entity.Id).FirstOrDefault() != null)
                    throw new Exception("Project cannot be edited because there's a booking made.");

                var existingProposals = _context.Proposals.Where(x => x.HeadProposal.Project.Id == entity.Id && x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus == Domain.Enums.ProposalStatus.Active).ToList();

               if (!request.EditWholeEntity && !request.IsPublished && existingProposals.Any())
                    throw new Exception($"You have pending bids. Please decline all bids and then click on stop broadcasting the project again.");
          
                if (request.EditWholeEntity)
                {
                    if (_context.KindOfWorks.Find(request.KindOfWorkId) == null)
                        throw new Exception($"Please enter project's kind of work.");

                    if (_context.Languages.Find(request.LanguageId) == null)
                        throw new Exception($"Please enter project's language.");

                    if (_context.ExpertiseAreas.Where(x => request.ExpertiseAreaIds.Contains(x.Id)).FirstOrDefault() == null)
                        throw new Exception($"Please enter at least one project's area of expertise.");

                    for (int i = entity.ExpertiseAreas.Count - 1; i >= 0; i--)
                    {
                        entity.ExpertiseAreas.Remove(entity.ExpertiseAreas.ElementAt(i));
                    }

                    //entity.Description = request.Description;
                    //entity.ExpertiseAreas = _context.ExpertiseAreas.Where(x => request.ExpertiseAreaIds.Contains(x.Id)).ToList();
                    //entity.KindOfWorkId = request.KindOfWorkId;
                    //entity.Language = _context.Languages.Find(request.LanguageId);
                    //entity.MaxBudget = request.MaxBudget;
                    //entity.MinimumDegree = _context.Degrees.Find(request.MinimumDegreeId);
                    //entity.PagesNo = request.PagesNo;
                    //entity.PlannedBudget = request.MaxBudget;
                    //entity.ProjectTopic = request.ProjectTopic;
                    //entity.Deadline = request.Deadline;
                    //entity.LastUpdate = DateTime.UtcNow;

                    _context.Projects.Update(entity);
                    await _context.SaveChangesAsync(cancellationToken);

                    foreach (var existingProposal in existingProposals)
                    {
                        await _proposalService.DeclineCancelProposal(UserRoleDefaults.CustomerRoleName, existingProposal, cancellationToken);
                    }

                    await _context.SaveChangesAsync(cancellationToken);

                    var notifications = await CreateNotificationsAndLogs(existingProposals, cancellationToken);

                    return new ExtendedOutputModelList<NotificationSignalRDTO>()
                    {
                        Success = true,
                        Message = string.Empty,
                        AdditionalInformation = notifications
                    };
                }
                else
                {
                  //  entity.IsPublished = request.IsPublished;
                    _context.Projects.Update(entity);
                    await _context.SaveChangesAsync(cancellationToken);

                    List<NotificationSignalRDTO> notifications = new List<NotificationSignalRDTO>();

                    if (request.IsPublished) 
                        notifications = await NotifyAuthors(entity, cancellationToken);

                    return new ExtendedOutputModelList<NotificationSignalRDTO>()
                    {
                        Success = true,
                        Message = string.Empty,
                        AdditionalInformation = notifications
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<NotificationSignalRDTO>> CreateNotificationsAndLogs(List<Domain.Entities.Proposal> existingProposals, CancellationToken cancellationToken)
        {
            List<NotificationSignalRDTO> notifications = new List<NotificationSignalRDTO>();
            List<Message> logMessages = new List<Message>();

            foreach (var prop in existingProposals)
            {
                logMessages.Add(new Message() { Conversation = prop.HeadProposal.Conversation, DateTimeSent = DateTime.UtcNow, IsLogMessage = true, MessageText = $"{prop.HeadProposal.Project.Customer.UserName} modified project, so {prop.ProposalType.ToString().ToLower()} is cancelled." });

                var notificationMessage = $"{prop.HeadProposal.Project.Customer.UserName} modified project '{prop.HeadProposal.Project.ProjectTopic}', so your {prop.ProposalType.ToString().ToLower()} is cancelled.";
                var adminMessage = $"{prop.HeadProposal.Project.Customer.UserName} modified project '{prop.HeadProposal.Project.ProjectTopic}', so it's {prop.ProposalType.ToString().ToLower()} (Id = {prop.Id}) is cancelled.";
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

        public async Task<List<NotificationSignalRDTO>> NotifyAuthors(Domain.Entities.Project project, CancellationToken cancellationToken)
        {
            try
            {
                var query = _context.ApplicationUserRoles.Where(x =>
                   x.UserRoleData.Languages.Contains(project.Language)
                   && x.UserRoleData.KindOfWorks.Contains(project.KindOfWork)
                   && x.UserRoleData.HighestDegree.Stage >= project.MinimumDegree.Stage);

                var userrolesdatas = query.Select(x => x.UserRoleData).ToList();

                for (int i = userrolesdatas.Count - 1; i >= 0; i--)
                {
                    if (!userrolesdatas[i].ExpertiseAreas.Intersect(project.ExpertiseAreas).Any())
                        userrolesdatas.Remove(userrolesdatas[i]);
                }

                query = _proposalService.ExcludeActiveProposals(query);

                var userIds = query.Where(x => userrolesdatas.Contains(x.UserRoleData)).Select(x => x.ApplicationUser.Id).ToArray();

                var notificationMessage = $"New project posted and we think you might be a good fit to work on it! It's about '{project.ProjectTopic}'";
                var adminMessage = string.Empty;
                var detailsLink = PathBuilderHelper.ProjectDetailsPath(project.Id);
                var notificationType = NotificationType.LiveBroadcast;

                var notifications = await _notificationService.SendNotifications(cancellationToken, 0, notificationMessage, detailsLink, notificationType, false, adminMessage, userIds);

                return notifications;
            }
            catch (Exception ex)
            {
                var aaaa = ex.Message;
                return new List<NotificationSignalRDTO>();

            }
        }
    }
}
