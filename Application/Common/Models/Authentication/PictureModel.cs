using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GhostWriter.Application.Common.Models.Authentication
{
    public class PictureModel
    {
        public string Type { get; set; }
        public byte[] Data { get; set; }
        public bool FormatSupported { get; set; }
        public bool SuccessfullyConverted { get; set; }
    }
}
