using GhostWriter.Application.Common.Models.Chat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface IConversationService
    {
        Task<int> GetConversationIdFromHeadProposalId(ChatConversationQuery request);
        Task<int> GetConversationIdFromBookingId(ChatConversation1Query request);
    }
}
