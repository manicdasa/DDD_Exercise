using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using GhostWriter.Domain.Entities;
using System;

namespace GhostWriter.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        
        /// <summary>
        /// Projects the user has made in the customer role
        /// </summary>
        public virtual ICollection<Project> Projects { get; set; }

        /// <summary>
        /// Uploaded documents
        /// </summary>
        public virtual ICollection<Document> Documents { get; set; }

        /// <summary>
        /// Proposals the user has accepted in the Ghostwriter role
        /// </summary>
        public virtual ICollection<HeadProposal> Proposals { get; set; }
    }
}