using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Application.DTOs
{
    public class DashboardStatsDTO
    {
        public int ActiveProjects { get; set; }
        public int DisputeProjects { get; set; }
        public int NewProjects { get; set; }
        public int NewUsers { get; set; }
    }
}
