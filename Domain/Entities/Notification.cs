using GhostWriter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int HeadProposalId { get; set; }
        public int ProposalId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectTopic { get; set; }
        public string AuthorUsername { get; set; }
        public string CustomerUsername { get; set; }
        public string DetailsLink { get; set; }
        public bool IsSeen { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public NotificationType NotificationType { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
    }
}
