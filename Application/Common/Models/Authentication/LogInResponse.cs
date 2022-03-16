using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Models
{
    public class LogInResponse
    {
        public string User { get; set; }
        public string Token { get; set; }
        public List<string> UserRoles { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
