using GhostWriter.Domain.Enums;
using System.Collections.Generic;

namespace GhostWriter.Domain.Entities
{
    public class KindOfWork
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
        /// Kind of work is a customizable field. FieldStatus shows weather the value added by user is pending/approved/rejected
        /// </summary>
        public FieldStatus FieldStatus { get; set; }

        /// <summary>
        /// User Role Data
        /// </summary>
        public virtual ICollection<UserRoleData> UserRoleDatas { get; set; }

        /// <summary>
        /// Projects
        /// </summary>
        public virtual ICollection<Project> Projects { get; set; }
    }
}
