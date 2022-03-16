using GhostWriter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GhostWriter.Application.DTOs
{
    public class BookingStatusDTO : IComparable<BookingStatusDTO>, IComparer<BookingStatusDTO>
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public BookingStatus Closed { get; internal set; }

        public int Compare([AllowNull] BookingStatusDTO x, [AllowNull] BookingStatusDTO y)
        {
            if (x == null || y == null) return -1;
            if (x.Id < y.Id) return -1;
            if (x.Id == y.Id) return 0;
            return 1;
        }

        public int CompareTo([AllowNull]BookingStatusDTO other)
        {
            if (other == null) return -1;
            if (this.Id < other.Id) return -1;
            if (this.Id == other.Id) return 0;
            return 1;
        }
    }

    public static class BookingStatusDescriptionValues
    {
        static string Inactive = "Being worked on. Payment not made yet";
        static string Active = "Being worked on";
        static string InDispute = "In dispute";
        static string PlagiarismCheckDone = "Plagiarism check done";
        static string FinalVersionSubmitted = "Final version summited";
        static string Closed = "Closed after the customer accepted it";
        static string ClosedAfterDispute = "Closed after dispute";
        static string Cancelled = "Cancelled by customer (no payment done)";
        static string NoStatus = "Error: This booking has no status yet";

        public static Tuple<int, string> GetDescriptionTuple(BookingStatus bookingStatus)
        {
            switch (bookingStatus)
            {
                case BookingStatus.Inactive:
                    return new Tuple<int, string>((int)bookingStatus, Inactive);
                case BookingStatus.Active:
                    return new Tuple<int, string>((int)bookingStatus, Active);
                case BookingStatus.InDispute:
                    return new Tuple<int, string>((int)bookingStatus, InDispute);
                case BookingStatus.PlagiarismCheckDone:
                    return new Tuple<int, string>((int)bookingStatus, PlagiarismCheckDone);
                case BookingStatus.FinalVersionSubmitted:
                    return new Tuple<int, string>((int)bookingStatus, FinalVersionSubmitted);
                case BookingStatus.Closed:
                    return new Tuple<int, string>((int)bookingStatus, Closed);
                case BookingStatus.ClosedAfterDispute:
                    return new Tuple<int, string>((int)bookingStatus, ClosedAfterDispute);
                case BookingStatus.Cancelled:
                    return new Tuple<int, string>((int)bookingStatus, Cancelled);
                default:
                    return new Tuple<int, string>((int)bookingStatus, NoStatus);
            }
        }

        public static BookingStatusDTO GetDescription(BookingStatus bookingStatus)
        {
            switch (bookingStatus)
            {
                case BookingStatus.Inactive:
                    return new BookingStatusDTO() { Id = (int)bookingStatus, Value = Inactive};
                case BookingStatus.Active:
                    return new BookingStatusDTO() { Id = (int)bookingStatus, Value = Active };
                case BookingStatus.InDispute:
                    return new BookingStatusDTO() { Id = (int)bookingStatus, Value = InDispute };
                case BookingStatus.PlagiarismCheckDone:
                    return new BookingStatusDTO() { Id = (int)bookingStatus, Value = PlagiarismCheckDone };
                case BookingStatus.FinalVersionSubmitted:
                    return new BookingStatusDTO() { Id = (int)bookingStatus, Value = FinalVersionSubmitted };
                case BookingStatus.Closed:
                    return new BookingStatusDTO() { Id = (int)bookingStatus, Value = Closed };
                case BookingStatus.ClosedAfterDispute:
                    return new BookingStatusDTO() { Id = (int)bookingStatus, Value = ClosedAfterDispute };
                case BookingStatus.Cancelled:
                    return new BookingStatusDTO() { Id = (int)bookingStatus, Value = Cancelled };
                default:
                    return new BookingStatusDTO() { Id = (int)bookingStatus, Value = NoStatus };
            }
        }
    }
}
