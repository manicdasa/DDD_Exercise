using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostWriter.Domain.Entities;

namespace GhostWriter.Domain.Entities
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public int? UserRoleDataId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual UserRoleData UserRoleData { get; set; }
    }
}