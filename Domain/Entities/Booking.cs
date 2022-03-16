using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class Booking
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Boolean detecting weather the final document has undertaken the plagiarism check
        /// </summary>
        public bool PlagueScanned { get; set; }

        /// <summary>
        /// Boolean detecting weather the ghostwriter has received the booking email confirmation
        /// </summary>
        public bool GHWReceivedConfirmation { get; set; }

        /// <summary>
        /// Total Price paid by customer
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Total service charges
        /// </summary>
        public decimal TotalServiceCharges { get; set; }

        /// <summary>
        /// Additional note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Booking creation date
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Booking's last update date
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Head proposal
        /// </summary>
        public virtual HeadProposal HeadProposal { get; set; }


        /// <summary>
        /// Payments
        /// </summary>
        public virtual ICollection<Transaction> Transactions { get; set; }

        /// <summary>
        /// Disputes
        /// </summary>
        public virtual ICollection<Dispute> Disputes { get; set; }

        /// <summary>
        /// Booking status history
        /// </summary>
        public virtual ICollection<BookingStatusHistory> BookingStatusHistories { get; set; }

        /// <summary>
        /// Uploaded documents by author
        /// </summary>
        public virtual ICollection<Document> Documents { get; set; }
    }
}
