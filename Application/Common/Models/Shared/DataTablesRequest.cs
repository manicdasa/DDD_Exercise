using System;
using System.Collections.Generic;
using System.Text;

namespace GhostWriter.Application.Common.Models.Shared
{
    public class DataTablesRequest
    {
        public int PageNo { get; set; }
        public int PageLength { get; set; }
        public string Search { get; set; }
    }
}
