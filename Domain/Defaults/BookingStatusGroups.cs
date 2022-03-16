using GhostWriter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Defaults
{
    public static partial class BookingStatusGroups
    {
        /// <summary>
        /// Closed, ClosedAfterDispute, Cancelled
        /// </summary>
        public static List<BookingStatus> Closed = new List<BookingStatus>() { BookingStatus.Closed, BookingStatus.ClosedAfterDispute, BookingStatus.Cancelled };

        /// <summary>
        /// Closed, ClosedAfterDispute
        /// </summary>
        public static List<BookingStatus> ClosedNoCancelled = new List<BookingStatus>() { BookingStatus.Closed, BookingStatus.ClosedAfterDispute };

        /// <summary>
        /// Inactive, Active, FinalVersionSubmitted, PlagiarismCheckDone, InDispute
        /// </summary>
        public static List<BookingStatus> Open = new List<BookingStatus>() { BookingStatus.Active, BookingStatus.Inactive, BookingStatus.FinalVersionSubmitted, BookingStatus.PlagiarismCheckDone, BookingStatus.InDispute };

        /// <summary>
        /// Inactive, Active, FinalVersionSubmitted, PlagiarismCheckDone
        /// </summary>
        public static List<BookingStatus> OpenNoDispute = new List<BookingStatus>() { BookingStatus.Active, BookingStatus.Inactive, BookingStatus.FinalVersionSubmitted, BookingStatus.PlagiarismCheckDone };

        /// <summary>
        /// Active, FinalVersionSubmitted, PlagiarismCheckDone, InDispute
        /// </summary>
        public static List<BookingStatus> Active = new List<BookingStatus>() { BookingStatus.Active, BookingStatus.FinalVersionSubmitted, BookingStatus.PlagiarismCheckDone, BookingStatus.InDispute };

        /// <summary>
        /// Active, FinalVersionSubmitted, PlagiarismCheckDone
        /// </summary>
        public static List<BookingStatus> ActiveNoDispute = new List<BookingStatus>() { BookingStatus.Active, BookingStatus.FinalVersionSubmitted, BookingStatus.PlagiarismCheckDone };


        #region States for translating to another state

        /// <summary>
        /// States of booking that can go to FinalVersionSubmitted state (Active)
        /// </summary>
        public static List<BookingStatus> RequiredForFinalVersionState = new List<BookingStatus>() { BookingStatus.Active };

        /// <summary>
        /// States of booking that can go to RequiredForPlagiarismCheckState state (FinalVersionSubmitted)
        /// </summary>
        public static List<BookingStatus> RequiredForPlagiarismCheckState = new List<BookingStatus>() { BookingStatus.FinalVersionSubmitted };

        /// <summary>
        /// States of booking that can go to InDispute state (Active, FinalVersionSubmitted, PlagiarismCheckDone)
        /// </summary>
        public static List<BookingStatus> RequiredForDisputeState = new List<BookingStatus>() { BookingStatus.Active, BookingStatus.FinalVersionSubmitted, BookingStatus.PlagiarismCheckDone };

        /// <summary>
        /// States of booking that can go to Closed/ClosedWithDispute state (FinalVersionSubmitted, PlagiarismCheckDone, InDisp)
        /// </summary>
        public static List<BookingStatus> RequiredForClosedState = new List<BookingStatus>() { BookingStatus.FinalVersionSubmitted, BookingStatus.PlagiarismCheckDone, BookingStatus.InDispute };

        /// <summary>
        /// States of booking that can go to Cancelled state (Inactive)
        /// </summary>
        public static List<BookingStatus> RequiredForCancelledState = new List<BookingStatus>() { BookingStatus.Inactive };

        #endregion

        /// <summary>
        /// Inactive
        /// </summary>
        public static List<BookingStatus> Inactive = new List<BookingStatus>() { BookingStatus.Inactive };

        /// <summary>
        /// FinalVersionSubmitted
        /// </summary>
        public static List<BookingStatus> FinalVersionSubmitted = new List<BookingStatus>() { BookingStatus.FinalVersionSubmitted };

        /// <summary>
        /// InDispute
        /// </summary>
        public static List<BookingStatus> InDispute = new List<BookingStatus>() { BookingStatus.InDispute };
    }
}
