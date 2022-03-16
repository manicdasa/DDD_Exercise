using GhostWriter.Domain.Enums;
using System;

namespace GhostWriter.Domain.Entities
{
    public class Transaction
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Total Price
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// BrainTree Transaction Id
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// PaymentMethodNonce - transaction information used by BrainTree
        /// </summary>
        public string PaymentMethodNonce { get; set; }

        /// <summary>
        /// Gets weather the transaction was successfully made
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// BillingAddress
        /// </summary>
        public string TransactionMessage { get; set; }

        /// <summary>
        /// Date Created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Payment Type
        /// </summary>
        public PaymentType PaymentType { get; set; }

        /// <summary>
        /// Booking
        /// </summary>
        public virtual Booking Booking { get; set; }
    }
}
