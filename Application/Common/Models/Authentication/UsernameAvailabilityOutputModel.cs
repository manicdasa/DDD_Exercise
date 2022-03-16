using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Application.Common.Models
{
    public class UsernameAvailabilityOutputModel
    {
        public bool Available { get; set; }
        public string Message { get; set; }
        public List<string> UsernameSuggestions { get; set; }
    }
}
