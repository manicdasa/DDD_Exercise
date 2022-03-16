using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Entities
{
    public class ServiceCharge
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Service Charge Amount
        /// </summary>
        public decimal ChargeAmount { get; set; }

        /// <summary>
        /// Defines weather the amount is expressed in percentage or currency
        /// </summary>
        public bool IsPercentage { get; set; }

        /// <summary>
        /// Default charge used in case there's no service charge insertrted for the date of the project's creation
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Service Charge Type
        /// </summary>
        public virtual ServiceChargeType ServiceChargeType { get; set; }

        /// <summary>
        /// Projects
        /// </summary>
        public virtual ICollection<Project> Projects { get; set; }
    }
}
