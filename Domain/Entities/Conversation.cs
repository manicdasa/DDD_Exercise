using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class Conversation
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date Created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// HeadProposal Id reference
        /// </summary>
        public int HeadProposalId { get; set; }

        /// <summary>
        /// Head Proposal
        /// </summary>
        public virtual HeadProposal HeadProposal { get; set; }

        /// <summary>
        /// Binary Document
        /// </summary>
        public virtual Document BinaryDocument { get; set; }

        /// <summary>
        /// Messages
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }
    }
}
