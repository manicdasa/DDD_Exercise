using GhostWriter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class Milestone
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date Planned
        /// </summary>
        public DateTime DatePlanned { get; set; }

        /// <summary>
        /// Realisation date
        /// </summary>
        public DateTime DateRealised { get; set; }

        /// <summary>
        /// Is Final Milestone
        /// </summary>
        public bool IsFinalMilestone { get; set; }

        /// <summary>
        /// Milestone status
        /// </summary>
        public MilestoneStatus MilestoneStatus { get; set; }


        /// <summary>
        /// Booking
        /// </summary>
        public virtual Booking Booking { get; set; }

        /// <summary>
        /// Binary Document
        /// </summary>
        public virtual Document BinaryDocument { get; set; }
    }
}
