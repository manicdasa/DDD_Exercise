using GhostWriter.Domain.Enums;
using System;

namespace GhostWriter.Domain.Entities
{
    public class BookingStatusHistory
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Proposal Status
        /// </summary>
        public BookingStatus BookingStatus { get; set; }

        /// <summary>
        /// Date Created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Booking
        /// </summary>
        public virtual Booking Booking { get; set; }
    }
}
