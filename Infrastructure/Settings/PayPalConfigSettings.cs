using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Infrastructure.Settings
{
    public class PayPalConfigSettings
    {
        public bool Sandbox { get; set; }
        public string PaypalClientID { get; set; }
        public string PaypalClientSecret { get; set; }
        public string MerchantId { get; set; }
    }
}
