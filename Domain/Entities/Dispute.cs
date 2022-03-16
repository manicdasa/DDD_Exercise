using GhostWriter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class Dispute
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Reason the dispute is made
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Dispute status
        /// </summary>
       public DisputeStatus DisputeStatus { get; set; }

        /// <summary>
        /// Resolution obervation fullfiled by admin
        /// </summary>
        public string Resolution { get; set; }

        /// <summary>
        /// Refund Amount given back to the customer
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// Amount that will be paid to author
        /// </summary>
        public decimal PaymentToAuthor{ get; set; }

        /// <summary>
        /// Date Created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Date Closed
        /// </summary>
        public DateTime DateClosed { get; set; }

        /// <summary>
        /// Booking
        /// </summary>
        public virtual Booking Booking { get; set; }
    }
}
