using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using GhostWriter.Application.DTOs;
using AutoMapper;
using System.Collections.Generic;
using GhostWriter.Application.Project.Queries;
using GhostWriter.Application.Project.Commands;
using GhostWriter.Application.Common.Models;
using GhostWriter.WebUI.Hubs;
using Microsoft.AspNetCore.SignalR;
using GhostWriter.WebUI.Hubs.Clients;
using System;
using GhostWriter.Application.Defaults;
using System.Linq;

namespace GhostWriter.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin,Customer,Ghostwriter")]
    public class ChatController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub, IChatClient> _chatHubContext;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public ChatController(IMapper mapper, IHubContext<ChatHub, IChatClient> chatHubContext, IHubContext<NotificationHub> notificationHubContext)
        {
            _mapper = mapper;
            _chatHubContext = chatHubContext;
            _notificationHubContext = notificationHubContext;
        }

        /// <summary>
        /// Gets all messages of a booking, since it's proposal has been created
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(SendMessage))]
        public async Task<OutputModel> SendMessage(int headProposalId, int exactEntityId, string message)
        {
            SendMessageCommand request = new SendMessageCommand()
            {
                HeadProposalId = headProposalId,
                ExactEntityId = exactEntityId,
                Message = message,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            var addResponse = await Mediator.Send(request);

            if (addResponse.Success == true)
            {
                await _chatHubContext.Clients.Groups(addResponse.SuccessPayload.ConversationId.ToString()).ReceiveMessage(addResponse.SuccessPayload);             
                addResponse.Notifications.Where(x => x.NotificationDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.ReceiveNotification, x.NotificationDTO));
                addResponse.Notifications.Where(x => x.PanelObjectDTO != null).ToList().ForEach(x => _notificationHubContext.Clients.Group(x.ReceiverUserId.ToString()).SendAsync(SignalRMethodNames.RefreshSidePanel, x.PanelObjectDTO));
                return new OutputModel() { Success = addResponse.Success, Message = addResponse.Message };
            }
            else
            {
                throw new Exception(addResponse.Message);
            }
        }

        /// <summary>
        /// Gets all messages of a booking, since it's proposal has been created
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAllMessagesByBooking))]
        public async Task<List<MessageDTO>> GetAllMessagesByBooking(int bookingId)
        {
            GetAllMessagesQuery request = new GetAllMessagesQuery()
            {
                HeadProposalId = null,
                BookingId = bookingId,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            return await Mediator.Send(request);
        }
        /// <summary>
        /// Gets all messages of a proposal
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAllMessagesByHeadProposal))]
        public async Task<List<MessageDTO>> GetAllMessagesByHeadProposal(int headProposalId)
        {
            GetAllMessagesQuery request = new GetAllMessagesQuery()
            {
                BookingId = null,
                HeadProposalId = headProposalId,
                Username = User.FindFirst(ClaimTypes.Name).Value
            };

            return await Mediator.Send(request);
        }
    }
}
