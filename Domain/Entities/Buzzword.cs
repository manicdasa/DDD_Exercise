using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class Buzzword
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// User Role Datas
        /// </summary>
        public virtual ICollection<UserRoleData> UserRoleDatas { get; set; }

        /// <summary>
        /// Projects
        /// </summary>
        public virtual ICollection<Project> Projects { get; set; }
    }
}
