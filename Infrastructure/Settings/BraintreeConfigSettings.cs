using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Infrastructure.Settings
{
    public class BraintreeConfigSettings
    {
        public string Environment { get; set; }
        public string MerchantId { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
