using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Enums
{
    public enum ProjectStatus
    {
        /// <summary>
        /// Project is in the creation process and visible only to the customer.
        /// </summary>
        InCreation = 0,

        /// <summary>
        /// Project is open (active) and unassigned to any ghostwriter.
        /// </summary>
        Open = 1,

        /// <summary>
        /// Project assigned to a ghostwriter and a booking for the project is made.
        /// </summary>
        Closed = 2,

        /// <summary>
        /// Deleted project
        /// </summary>
        Deleted = 3
    }
}
