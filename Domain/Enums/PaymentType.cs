using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Enums
{
    public enum PaymentType
    {
        /// <summary>
        /// Customer paid to the website
        /// </summary>
        PaymentToSystem = 0,

        /// <summary>
        /// Website made a refund to the customer
        /// </summary>
        Refund = 1,

        /// <summary>
        /// Website paid to the ghostwriter
        /// </summary>
        PaymentToGhostWriter = 2 
    }
}
