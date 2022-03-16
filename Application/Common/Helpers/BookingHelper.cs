using GhostWriter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostWriter.Application.Common.Helpers
{
    public static class BookingHelper
    {
        public static BookingStatus LastStatus(this Domain.Entities.Booking booking)
        {
            return booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault().BookingStatus;
        }

        public static bool IsInStatus(this Domain.Entities.Booking booking, BookingStatus status)
        {
            return booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault().BookingStatus == status;
        }

        public static bool IsInOneOfStatuses(this Domain.Entities.Booking booking, List<BookingStatus> statuses)
        {
            return statuses.Contains(booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault().BookingStatus);
        }

        public static bool IsNotInStatus(this Domain.Entities.Booking booking, List<BookingStatus> statuses)
        {
            return !statuses.Contains(booking.BookingStatusHistories.OrderByDescending(x => x.DateCreated).FirstOrDefault().BookingStatus);
        }
    }
}
