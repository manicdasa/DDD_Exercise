using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface IEmailer
    {
        bool SendEmail(List<string> to, string subject, string body, bool isHtml, List<string> cc = null);

        bool SendFormatedEmail(List<string> to, string subject, string templateName, List<KeyValuePair<string, string>> valuesToReplace, List<string> cc = null);
    }
}
