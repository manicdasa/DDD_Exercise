using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Infrastructure.Settings
{
    public class CopyLeaksConfigSettings
    {
        public string Email { get; set; }
        public string ApiKey { get; set; }
        public bool Sandbox { get; set; }
    }
}
