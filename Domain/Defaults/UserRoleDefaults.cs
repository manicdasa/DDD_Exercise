using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Domain.Defaults
{
    /// <summary>
    /// Represents default values related to Application User Roles
    /// </summary>
    public static partial class UserRoleDefaults
    {
        /// <summary>
        /// Gets the name of 'Admin' user role
        /// </summary>
        public static string AdminRoleName => "Admin";

        /// <summary>
        /// Gets the name of 'Customer' user role
        /// </summary>
        public static string CustomerRoleName => "Customer";

        /// <summary>
        /// Gets the name of 'Ghostwriter' user role
        /// </summary>
        public static string GhostwriterRoleName => "Ghostwriter";
    }
}
