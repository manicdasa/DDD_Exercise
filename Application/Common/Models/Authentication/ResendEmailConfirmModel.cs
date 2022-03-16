using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Models
{
    public class ResendEmailConfirmModel
    {
        [Required]
        public string Username { get; set; }
    }
}
