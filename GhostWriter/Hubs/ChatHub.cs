using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models.Chat;
using GhostWriter.WebUI.Hubs.Clients;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GhostWriter.WebUI.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IConversationService _conversationService;

        public ChatHub(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task Connect(int headProposalId)
        {
            ChatConversationQuery request = new ChatConversationQuery()
            {
                HeadProposalId = headProposalId,
                Username = Context.User.FindFirst(ClaimTypes.Name).Value
            };

            var conversationId =  await _conversationService.GetConversationIdFromHeadProposalId(request);

            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
