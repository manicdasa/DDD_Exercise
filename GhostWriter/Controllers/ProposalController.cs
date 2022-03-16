using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.Proposal.Commands.CreateProposal;
using GhostWriter.Application.Proposal.Commands.UpdateProposal;
using GhostWriter.Application.Proposal.Queries.GetAuthorsActiveProposalInfo;
using GhostWriter.Application.DTOs;
using System.Collections.Generic;
using GhostWriter.Domain.Defaults;
using GhostWriter.Application.Proposal.Queries.GetProposalInfo;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Application.Proposal.Queries.GetCustomerGeneratedOffers;
using GhostWriter.Application.Proposal.Queries.GetCustomerProjectsBids;
using GhostWriter.Application.Proposal.Queries.GetLastProjectProposal;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Defaults;
using Microsoft.AspNetCore.SignalR;
using GhostWriter.WebUI.Hubs;
using System.Linq;
using System;

namespace GhostWriter.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin,Customer,Ghostwriter")]
    public class ProposalController : ApiControllerBase
    {
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public ProposalController(IHubContext<NotificationHub> hubContext)
        {
            _notificationHubContext = hubContext;
        }

        /// <summary>
        /// Author creates a new proposal of Bid type unless it exists and is in active status
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateBid))]
        public async Task<ActionResult<OutputModel>> CreateBid(int projectId, decimal financialOffer)
        {
            int ghwid;
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out ghwid);

            CreateProposalCommand command = new CreateProposalCommand()
            {
                GHWId = ghwid,
                ProjectId = projectId,
                FinancialOffer = financialOffer,
                RoleName = UserRoleDefaults.GhostwriterRoleName
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

        /// <summary>
        /// Customer creates a new proposal of Offer for an author unless it exists and is in active status
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateOffer))]
        public async Task<ActionResult<OutputModel>> CreateOffer(int projectId, int authorId)
        {
            CreateProposalCommand command = new CreateProposalCommand()
            {
                CustomerUsername = User.FindFirst(ClaimTypes.Name).Value,
                GHWId = authorId,
                ProjectId = projectId,
                RoleName = UserRoleDefaults.CustomerRoleName
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

        /// <summary>
        ///Cancels/Declines proposal by the author
        /// </summary>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        [HttpPut(nameof(DropProposalAuthor))]
        public async Task<ActionResult<OutputModel>> DropProposalAuthor(int proposalId)
        {
            DeclineOrCancelProposalCommand command = new DeclineOrCancelProposalCommand()
            {
                Username = User.FindFirst(ClaimTypes.Name).Value,
                RoleName = UserRoleDefaults.GhostwriterRoleName,
                ProposalId = proposalId
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

        /// <summary>
        /// Cancels/Declines proposal by the customer
        /// </summary>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        [HttpPut(nameof(DropProposalCustomer))]
        public async Task<ActionResult<OutputModel>> DropProposalCustomer(int proposalId)
        {
            DeclineOrCancelProposalCommand command = new DeclineOrCancelProposalCommand()
            {
                Username = User.FindFirst(ClaimTypes.Name).Value,
                RoleName = UserRoleDefaults.CustomerRoleName,
                ProposalId = proposalId
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

        #region Get methods

        /// <summary>
        /// Gets author's new offers
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAuthorsActiveOffers))]
        public async Task<PagedList<ProposalDTO>> GetAuthorsActiveOffers()
        {
            var query = new GetAuthorsActiveOffersQuery() { GHWUsername = User.FindFirst(ClaimTypes.Name).Value };

            return await Mediator.Send(query);
            
        }

        /// <summary>
        /// Gets customer's new offers
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetCustomersNewOffers))]
        public async Task<PagedList<ProposalDTO>> GetCustomersNewOffers()
        {
            var query = new GetCustomersActiveBidsQuery() { CustomerUsername = User.FindFirst(ClaimTypes.Name).Value };

            return await Mediator.Send(query);
        }

        /// <summary>
        /// Gets offers generated by customer that are active or declined
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetCustomersGeneratedOffers))]
        public async Task<PagedList<ProposalDTO>> GetCustomersGeneratedOffers([FromQuery] PaginationModel paginationModel)
        {
            var query = new GetCustomerProposalsQuery()
            {
                CustomerUsername = User.FindFirst(ClaimTypes.Name).Value,
                Page = paginationModel.Page,
                PageSize = paginationModel.PageSize,
                ProposalType = Domain.Enums.ProposalType.Offer,
                PossibleStatuses = new List<Domain.Enums.ProposalStatus>() { Domain.Enums.ProposalStatus.Active }
            };

            return await Mediator.Send(query);
        }

        /// <summary>
        /// Gets all author bids - short information about the proposal's project and bid's latest status
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAuthorsBids))]
        public async Task<PagedList<ProposalDTO>> GetAuthorsBids()
        {
            var query = new GetAuthorsBidsQuery() { GHWUsername = User.FindFirst(ClaimTypes.Name).Value };

            return await Mediator.Send(query);
        }

        /// <summary>
        /// Gets bids for a specific project
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetCustomersProjectBids))]
        [Authorize(Roles = "Customer")]
        public async Task<PagedList<ProposalDTO>> GetCustomersProjectBids(int projectId)
        {
            var query = new GetCustomerProjectsBidsQuery() { CustomerUsername = User.FindFirst(ClaimTypes.Name).Value, ProjectId = projectId  };

            return await Mediator.Send(query);
        }

        /// <summary>
        /// Gets authors last bid for a specific project
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAuthorsLastBid))]
        [Authorize(Roles = "Ghostwriter")]
        public async Task<ProposalInfoDTO> GetAuthorsLastBid(int projectId)
        {
            var query = new GetLastProjectProposalQuery()
            {
                ProjectId = projectId,
                RoleName = UserRoleDefaults.GhostwriterRoleName,
                ProposalType = Domain.Enums.ProposalType.Bid,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                ShowOnlyActiveProposals = true
            };

            return await Mediator.Send(query);
        }

        /// <summary>
        /// Gets authors last offer for a specific project
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAuthorsLastOffer))]
        [Authorize(Roles = "Ghostwriter")]
        public async Task<ProposalInfoDTO> GetAuthorsLastOffer(int projectId)
        {
            var query = new GetLastProjectProposalQuery()
            {
                ProjectId = projectId,
                RoleName = UserRoleDefaults.GhostwriterRoleName,
                ProposalType = Domain.Enums.ProposalType.Offer,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                ShowOnlyActiveProposals = true
            };

            return await Mediator.Send(query);
        }

        /// <summary>
        /// Gets authors last offer for a specific project
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetCustomersLastOffer))]
        [Authorize(Roles = "Customer")]
        public async Task<ProposalInfoDTO> GetCustomersLastOffer(int projectId)
        {
            var query = new GetLastProjectProposalQuery()
            {
                ProjectId = projectId,
                RoleName = UserRoleDefaults.CustomerRoleName,
                ProposalType = Domain.Enums.ProposalType.Offer,
                Username = User.FindFirst(ClaimTypes.Name).Value,
                ShowOnlyActiveProposals = false
            };

            return await Mediator.Send(query);
        }

        /// <summary>
        /// Gets details of the proposal with the proposalId
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetProposalDetails))]
        public async Task<ProposalDetailsDTO> GetProposalDetails(int proposalId)
        {
            var query = new GetProposalInfoQuery() { Username = User.FindFirst(ClaimTypes.Name).Value, ProposalId = proposalId };

            return await Mediator.Send(query);
        }

        #endregion
    }
}
