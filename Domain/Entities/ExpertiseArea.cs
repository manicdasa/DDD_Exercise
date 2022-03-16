using GhostWriter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class ExpertiseArea
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
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Expertise area is a customizable field. FieldStatus shows weather the value added by user is pending/approved/rejected
        /// </summary>
        public FieldStatus FieldStatus { get; set; }

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
