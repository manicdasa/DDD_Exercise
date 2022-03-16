using System;
using System.ComponentModel.DataAnnotations;

namespace GhostWriter.Application.Common.Models
{
    public class RegisterModel
    {
        [StringLength(128, MinimumLength = 5)]
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Passwword is required")]
        public string Password { get; set; }
    }
}
