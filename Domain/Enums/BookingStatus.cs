using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Enums
{
    public enum BookingStatus
    {
        /// <summary>
        /// The booking is currently inactive. This happens when the customer/author has accepted the bid/offer, but the payment is not made yet.
        /// </summary>
        Inactive = 0,

        /// <summary>
        /// The booking is currently active. Customer has made the payment and is waiting for the document submission
        /// </summary>
        Active = 1,

        /// <summary>
        /// Final version of the document is submitted by the author. Once the customer agrees, booking will move to Completed status.
        /// </summary>
        FinalVersionSubmitted = 2,
        
        /// <summary>
        /// Plagiarism check of the project's final version has been done.
        /// </summary>
        PlagiarismCheckDone = 6,

        /// <summary>
        /// The booking is successfully completed. No dispute is created. Booking's proposal is closed.
        /// </summary>
        Closed = 3,

        /// <summary>
        /// THe booking is in dispute.
        /// </summary>
        InDispute = 4,

        /// <summary>
        /// The project has been closed after a dispute
        /// </summary>
        ClosedAfterDispute = 5,

        /// <summary>
        /// The inactive project has been cancelled by customer
        /// </summary>
        Cancelled = 7
    }
}
