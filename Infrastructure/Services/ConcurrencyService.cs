using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.DTOs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GhostWriter.Infrastructure.Services
{
    public static class ConcurrencyService
    {
        public static ConcurrentDictionary<int, object> CheckBookingWithHeadProposal = new ConcurrentDictionary<int, object>();

        public static ConcurrentDictionary<int, object> CheckBookingWithBookingId = new ConcurrentDictionary<int, object>();

    }
}
