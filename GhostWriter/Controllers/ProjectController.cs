using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.Project.Commands;
using GhostWriter.Application.Project.Queries;
using GhostWriter.Application.DTOs;
using GhostWriter.Application.Project.Queries.GetAuthorsBroadcastProjects;
using GhostWriter.Application.Proposal.Commands.CreateProposal;
using GhostWriter.Domain.Defaults;
using AutoMapper;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Application.Proposal.Queries.GetCustomerBids;
using GhostWriter.WebUI.Hubs;
using Microsoft.AspNetCore.SignalR;
using GhostWriter.Application.Defaults;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;

namespace GhostWriter.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin,Customer,Ghostwriter")]
    public class ProjectController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public ProjectController(IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _mapper = mapper;
            _notificationHubContext = hubContext;
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(CreateProject))]
        public async Task<ActionResult<ExtendedOutputModelTemp<NotificationSignalRDTO>>> CreateProject(CreateProjectCommand command)
        {
            CreateProjectCommandExtended request = (CreateProjectCommandExtended)_mapper.Map(command, typeof(CreateProjectCommand), typeof(CreateProjectCommandExtended));
            request.CustomerUsername = User.FindFirstValue(ClaimTypes.Name);
            request.IsPublished = true;

            var result = await Mediator.Send(request);

            if (result.Success)
            {
                result.AdditionalInformationNotification.ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                return result;
            }
            else
                throw new Exception("Error with creating project !");
            
        }

        [HttpPost(nameof(CreateProjectAndOfferToAuthor))]
        public async Task<ActionResult<OutputModel>> CreateProjectAndOfferToAuthor(CreateProjectCommand command, int authorId)
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;

            CreateProjectCommandExtended request = (CreateProjectCommandExtended)_mapper.Map(command, typeof(CreateProjectCommand), typeof(CreateProjectCommandExtended));
            request.CustomerUsername = username;
            request.IsPublished = false;

            var retVal = await Mediator.Send(request);

            var result = await Mediator.Send(new CreateProposalCommand() { GHWId = authorId, CustomerUsername = username, ProjectId = retVal.AdditionalInformation, RoleName = UserRoleDefaults.CustomerRoleName });

            if (result.Success)
            {
                result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));

                return new OutputModel()
                {
                    Message = result.Message,
                    Success = result.Success
                };
            }
            else
            {
                throw new Exception(result.Message);
            }


            
        }

        [HttpPut(nameof(EditProjectDetails))]
        public async Task<ActionResult<OutputModel>> EditProjectDetails(EditProjectDetailsCommand command)
        {
            EditProjectDetailsCommandExtended request = (EditProjectDetailsCommandExtended)_mapper.Map(command, typeof(EditProjectDetailsCommand), typeof(EditProjectDetailsCommandExtended));
            request.CustomerUsername = User.FindFirst(ClaimTypes.Name).Value;
            request.EditWholeEntity = true;

            var result = await Mediator.Send(request);

            if (result.Success)
            {
                result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
               
                return new OutputModel()
                {
                    Message = result.Message,
                    Success = result.Success
                };
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        [HttpPut(nameof(ChangeBroadcastInfo))]
        public async Task<ActionResult<OutputModel>> ChangeBroadcastInfo(int projectId, bool isPublished)
        {
            EditProjectDetailsCommandExtended command = new EditProjectDetailsCommandExtended()
            {
                Id = projectId,
                IsPublished = isPublished,
                EditWholeEntity = false,
                CustomerUsername = User.FindFirst(ClaimTypes.Name).Value
            };
            var result = await Mediator.Send(command);

            if (result.Success)
            {
                result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
                
                return new OutputModel()
                {
                    Message = result.Message,
                    Success = result.Success
                };
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        [HttpPut(nameof(DeleteProject))]
        public async Task<ActionResult<OutputModel>> DeleteProject(int projectId)
        {
            DeleteProjectCommand request = new DeleteProjectCommand()
            {
                CustomerUsername = User.FindFirst(ClaimTypes.Name).Value,
                ProjectId = projectId
            };

            var result = await Mediator.Send(request);

            if (result.Success)
            {
                result.AdditionalInformation.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                result.AdditionalInformation.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
                
                return new OutputModel()
                {
                    Message = result.Message,
                    Success = result.Success
                };
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        #region Get methods

        [HttpGet(nameof(GetProjectDetails))]
        public async Task<ActionResult<ProjectDetailsDTO>> GetProjectDetails(int projectId)
        {
            GetProjectQuery query = new GetProjectQuery() 
            { 
                ProjectId = projectId,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            return await Mediator.Send(query);
        }

        [HttpGet(nameof(GetAuthorsLiveBroadcast))]
        public async Task<ActionResult<PagedList<ProjectDTO>>> GetAuthorsLiveBroadcast([FromQuery] GetAuthorsBroadcastProjectsQuery query)
        {
            int userid;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out userid);

            GetAuthorsBroadcastProjectsQueryExtended request = (GetAuthorsBroadcastProjectsQueryExtended)_mapper.Map(query, typeof(GetAuthorsBroadcastProjectsQuery), typeof(GetAuthorsBroadcastProjectsQueryExtended));
            request.GHWUsername = User.FindFirst(ClaimTypes.Name).Value;
            request.GHWId = userid;


            return await Mediator.Send(request);
        }

        /// <summary>
        /// Gets customers open bided projects
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetCustomersOpenProjects))]
        public async Task<PagedList<ProjectBidsDTO>> GetCustomersOpenProjects([FromQuery] GetCustomerBidsQuery request)
        {
            var query = new GetCustomerBidsQueryExtended()
            {
                CustomerUsername = User.FindFirst(ClaimTypes.Name).Value,
                Page = request.Page,
                PageSize = request.PageSize,
                AreaOfExpertiseIds = request.AreaOfExpertiseIds,
                LanguageIds = request.LanguageIds,
                MinimumDegreeId = request.MinimumDegreeId,
                Deadline = request.Deadline,
                KindOfWorkIds = request.KindOfWorkIds,
                NoPagesFromRange = request.NoPagesFromRange,
                NoPagesToRange = request.NoPagesToRange
            };

            return await Mediator.Send(query);
        }

        #endregion
    }
}