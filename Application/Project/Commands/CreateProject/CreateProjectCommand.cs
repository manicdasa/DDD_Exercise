using System;
using System.Collections.Generic;
using GhostWriter.Application.Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Models;
using System.Linq;
using GhostWriter.Domain.Defaults;
using GhostWriter.Application.Common.Mappings;
using AutoMapper;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Common.Helpers;
using GhostWriter.Domain.Enums;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Application.Defaults;
using GhostWriter.Domain.Services;

namespace GhostWriter.Application.Project.Commands
{
    public class CreateProjectCommand
    {

        public DateTime Deadline { get; set; }
        public decimal PricePerPage { get; set; }
        public string ProjectTopic { get; set; }
        public string Description { get; set; }
        public int PagesNo { get; set; }
        public int MinimumDegreeId { get; set; }
        public int LanguageId { get; set; }
        public int KindOfWorkId { get; set; }
        public List<int> ExpertiseAreaIds { get; set; }
    }

    public class CreateProjectCommandExtended : CreateProjectCommand, IRequest<ExtendedOutputModelTemp<NotificationSignalRDTO>>, IMapFrom<CreateProjectCommand>
    {
        public bool IsPublished { get; set; }
        public string CustomerUsername { get; set; }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommandExtended, ExtendedOutputModelTemp<NotificationSignalRDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly INotificationService _notificationService;
        private readonly IPriceCalculatorService _priceCalculatorService;
        private readonly IProjectFactory _projectFactory;

        public CreateProjectCommandHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, INotificationService notificationService, IPriceCalculatorService priceCalculatorService, IProjectFactory projectFactory)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _notificationService = notificationService;
            _priceCalculatorService = priceCalculatorService;
            _projectFactory = projectFactory;
        }

        public async Task<ExtendedOutputModelTemp<NotificationSignalRDTO>> Handle(CreateProjectCommandExtended request, CancellationToken cancellationToken)
        {
            var customer = await _userManagementFactory.FindUser(request.CustomerUsername);

            if (customer == null)
                throw new NotFoundException($"Author {request.CustomerUsername} not found.");

            if (!_userManagementFactory.IsInRole(customer, UserRoleDefaults.CustomerRoleName))
                throw new AuthorizationException($"User {customer.UserName} is unauthorized to create a project.");

            var kindOfWork = _context.KindOfWorks.Find(request.KindOfWorkId);
            if (kindOfWork == null)
                throw new Exception($"Please enter project's kind of work.");

            var language = _context.Languages.Find(request.LanguageId);
            if (language == null)
                throw new Exception($"Please enter project's language.");

            var expertiseArea = _context.ExpertiseAreas.Where(x => request.ExpertiseAreaIds.Contains(x.Id)).FirstOrDefault();
            if (expertiseArea == null)
                throw new Exception($"Please enter at least one project's area of expertise.");

            var minDegree = _context.Degrees.Where(x => x.Id == request.MinimumDegreeId).FirstOrDefault();
            if (minDegree == null)
                throw new Exception($"Please enter min degree.");

            var entity = await _projectFactory.Create(request.Description, kindOfWork, new List<ExpertiseArea>() { expertiseArea }, customer, 
                language, minDegree, request.PricePerPage, request.ProjectTopic,
                request.PagesNo, request.IsPublished, request.Deadline);
            _context.Projects.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            int customerRoleId = _userManagementFactory.FindRoleIdByName(UserRoleDefaults.CustomerRoleName);
            var userRole = _context.ApplicationUserRoles.Where(x => x.UserId == customer.Id && x.RoleId == customerRoleId).SingleOrDefault();

            if (userRole == null)
                throw new NotFoundException("Customer's additional data not found.");

            if (userRole.UserRoleData == null)
            {
                userRole.UserRoleData = new UserRoleData()
                {
                    ApplicationUserRole = userRole,
                    JobsPostedCnt = 1
                };

                _context.ApplicationUserRoles.Update(userRole);
                await _context.SaveChangesAsync(cancellationToken);
            } 
            else
            {
                userRole.UserRoleData.JobsPostedCnt++;
                await _context.SaveChangesAsync(cancellationToken);
            }

            List<NotificationSignalRDTO> notifications = new List<NotificationSignalRDTO>(); 

            if (request.IsPublished)
                notifications = await NotifyAuthors(entity, cancellationToken);

            return new ExtendedOutputModelTemp<NotificationSignalRDTO>()
            {
                AdditionalInformation = entity.Id,
                AdditionalInformationNotification = notifications,
                Success = true,
                Message = string.Empty
            };
        }

        public async Task<List<NotificationSignalRDTO>> NotifyAuthors( Domain.Entities.Project project, CancellationToken cancellationToken)
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

                var userIds = query.Where(x => userrolesdatas.Contains(x.UserRoleData)).Select(x => x.ApplicationUser.Id).ToArray();

                var notificationMessage = $"New project created and we think you might be a good fit to work on it! It's about '{project.ProjectTopic}'";
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
