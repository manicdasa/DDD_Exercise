using System;

namespace GhostWriter.Application.DTOs
{
    public class CustomerStatsDTO
    {
        public string Username { get; set; }
        public DateTime DateRegistered { get; set; }
        public int NoActiveProjects { get; set; }
        public int NoClosedProjects { get; set; }
    }

    public class CustomerDetailedStatsDTO : CustomerStatsDTO
    {
        public int Id { get; set; }
        public int NoProjectsCreated { get; set; }
        public string LastProjectTitle { get; set; }
        public int LastProjectId { get; set; }
        public int NoUnassignedProjects { get; set; }
    }
}
