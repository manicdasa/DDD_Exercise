using GhostWriter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class ProposalStatusHistory
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Proposal Status
        /// </summary>
        public ProposalStatus ProposalStatus { get; set; }

        /// <summary>
        /// Date Created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Proposal
        /// </summary>
        public virtual Proposal Proposal { get; set; }
    }
}
