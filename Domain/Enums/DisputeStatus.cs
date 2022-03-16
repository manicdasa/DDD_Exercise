using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Enums
{
    public enum DisputeStatus
    {
        /// <summary>
        /// Dispute is created by the customer. Dispute is active and admin needs to resolve it.
        /// </summary>
        Active = 0,

        /// <summary>
        /// Dispute is accepted by admin. Certain amount of money is transferred back to the customer. Dispute is resolved.
        /// </summary>
        Accepted = 1,

        /// <summary>
        /// Dispute is declined by admin. The project is closed normally. Dispute is resolved.
        /// </summary>
        Declined = 2
    }
}
