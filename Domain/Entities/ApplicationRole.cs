using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostWriter.Domain.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}