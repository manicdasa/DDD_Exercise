using System;
using System.ComponentModel.DataAnnotations;

namespace GhostWriter.Application.Common.Models
{
    public class UsernameAvailabilityInputModel
    {
        [Required]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
