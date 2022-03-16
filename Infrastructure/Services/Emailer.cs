using GhostWriter.Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Infrastructure.Settings;

namespace GhostWriter.Infrastructure.Services
{
    public class Emailer : IEmailer
    {
        private readonly SMPTConfigSettings _smtpConfigSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Emailer(SMPTConfigSettings smtpConfigSettings, IHttpContextAccessor httpContextAccessor)
        {
            _smtpConfigSettings = smtpConfigSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        // TODO: Test this once sendgrid is up and running
        public bool SendEmail(List<string> to, string subject, string body, bool isHtml, List<string> cc = null)
        {
            try
            {
                if (_smtpConfigSettings.Enabled)
                {
                    using var smtp = GetSmtpClient();

                    var message = new MailMessage
                    {
                        Sender = new MailAddress(_smtpConfigSettings.From),
                        From = new MailAddress(_smtpConfigSettings.From, _smtpConfigSettings.FromDisplay),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = isHtml,
                    };

                    if (to.Any())
                        message.To.Add(string.Join(",", to.Where(email => !string.IsNullOrWhiteSpace(email))));
                    else
                        throw new ArgumentException("The list of recipient emails can not be empty");

                    if (cc != null && cc.Any())
                        message.CC.Add(string.Join(",", cc.Where(email => !string.IsNullOrWhiteSpace(email))));

                    smtp.Send(message);
                }

                return true;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to send email message.", e);
            }
            
        }

        public bool SendFormatedEmail(List<string> to, string subject, string templateName, List<KeyValuePair<string, string>> valuesToReplace, List<string> cc = null)
        {
            var body = File.ReadAllText("Content/EmailTemplates/" + templateName);

            if (valuesToReplace != null && valuesToReplace.Count > 0)
                foreach (var pair in valuesToReplace)
                    body = body.Replace("{{" + pair.Key + "}}", pair.Value);


            body = body.Replace("{{hostingEnviroment}}", $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}");

            return SendEmail(to, subject, body, true, cc);
        }

        private SmtpClient GetSmtpClient()
        {
            var smtp = new SmtpClient(_smtpConfigSettings.Server) { Timeout = 10000 };

            if (!string.IsNullOrWhiteSpace(_smtpConfigSettings.Username))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(
                    _smtpConfigSettings.Username,
                    _smtpConfigSettings.Password);

                smtp.EnableSsl = _smtpConfigSettings.EnableSSL;
                smtp.Port = _smtpConfigSettings.Port;
            }

            return smtp;
        }
    }
}
