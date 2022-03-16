using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Application.DTOs
{
    public class PaypalInfoDTO
    {
        public bool Sandbox { get; set; }
        public string PaypalClientID { get; set; }
        public string MerchantId { get; set; }
    }
}
