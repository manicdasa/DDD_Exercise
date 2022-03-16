using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Application.DTOs
{
    public class AuthorStatsDTO
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateRegistered { get; set; }
        public int NoActiveProjects { get; set; }
        public int NoClosedProjects { get; set; }
    }

    public class AuthorDetailedStatsDTO : AuthorStatsDTO
    {
        public int Id { get; set; }
        public int NoTotalProjects { get; set; }
        public string LastProjectTitle { get; set; }
        public int LastProjectId { get; set; }
    }
}
