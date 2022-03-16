using GhostWriter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class Proposal
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Financial Offer
        /// </summary>
        public decimal FinancialOffer { get; set; }

        /// <summary>
        /// Financial Offer with Calculated Service Charges
        /// </summary>
        public decimal ServiceCharges { get; set; }

        /// <summary>
        /// Date Created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Proposal's last update date
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Deadline
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Total Number of Milestones. We are not currently not using this field.
        /// </summary>
        public int MilestonesCnt { get; set; }

        /// <summary>
        /// Personal Message
        /// </summary>
        public string PersonalMessage { get; set; }

        /// <summary>
        /// Personal Type. Detects weather proposal is initiated by customer or ghostwriter.
        /// </summary>
        public ProposalType ProposalType { get; set; }

        /// <summary>
        /// Head Proposal
        /// </summary>
        public virtual HeadProposal HeadProposal { get; set; }

        /// <summary>
        /// Parent Counter Proposal
        /// </summary>
        public virtual Proposal ParentProposal { get; set; }

        /// <summary>
        /// Parent Counter Proposal Id
        /// </summary>
        public int? ParentProposalId { get; set; }

        /// <summary>
        /// Child Counter Proposal
        /// </summary>
        public virtual Proposal ChildProposal { get; set; }

        /// <summary>
        /// Child Counter Proposal Id
        /// </summary>
        public int? ChildProposalId { get; set; }

        /// <summary>
        /// Proposal Status History
        /// </summary>
        public virtual ICollection<ProposalStatusHistory> ProposalStatuses { get; set; }

    }
}
