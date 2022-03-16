using System;
using System.Collections.Generic;
using System.Text;
using GhostWriter.Application.Defaults;

namespace GhostWriter.Application.Common.Helpers
{
    public static class PathBuilderHelper
    {
        public static string BookingDetailsPath(int bookingId, int headProposalId)
        {
            var retVal = FileSystemDefaults.BookingDetailsPage;
            retVal = retVal.Replace("%BOOKING_ID%", bookingId.ToString());
            retVal = retVal.Replace("%HEAD_PROPOSAL_ID%", headProposalId.ToString());

            return retVal;
        }

        public static string ProjectDetailsPath(int projectId)
        {
            var retVal = FileSystemDefaults.ProjectDetailsPage;
            retVal = retVal.Replace("%PROJECT_ID%", projectId.ToString());

            return retVal;
        }
    }
}
