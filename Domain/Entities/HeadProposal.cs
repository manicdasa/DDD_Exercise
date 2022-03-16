using GhostWriter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class HeadProposal
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Proposal's project
        /// </summary>
        public virtual Project Project { get; set; }

        /// <summary>
        /// Foreign key: GhostWriter's Id (references ApplicationUser table)
        /// </summary>
        public int GHWId { get; set; }

        /// <summary>
        /// Date Created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Last Update
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Proposal Type
        /// </summary>
        public ProposalType ProposalType { get; set; }

        /// <summary>
        /// Final Proposal
        /// </summary>
       // public virtual Proposal FinalProposal { get; set; }

        public virtual ApplicationUser Ghostwriter { get; set; }

        /// <summary>
        /// Proposal's conversation
        /// </summary>
        public virtual Conversation Conversation { get; set; }
    }
}
