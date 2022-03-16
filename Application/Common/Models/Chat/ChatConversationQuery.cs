using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Application.Common.Models.Chat
{
    public class ChatConversationQuery
    {
        public int HeadProposalId { get; set; }
        public string Username { get; set; }
    }

    public class ChatConversation1Query
    {
        public int BookingId { get; set; }
        public string Username { get; set; }
    }
}

