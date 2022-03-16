using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Enums
{
    public enum ProposalStatus
    {
        /// <summary>
        /// The proposal is currently active. Booking has not been made yet
        /// </summary>
        Active = 0,

        /// <summary>
        /// The proposal has been cancelled by the user that has made the proposal
        /// </summary>
        Cancelled = 1, 

        /// <summary>
        /// The proposal has been declined by the user to whom the proposal is offered/bidded
        /// </summary>
        Declined = 2, 

        /// <summary>
        /// The proposal is accepted. Booking is made
        /// </summary>
        Accepted = 3,

        /// <summary>
        /// The proposal is deleted
        /// </summary>
        Deleted = 4
    }
}
