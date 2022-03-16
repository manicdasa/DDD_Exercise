using GhostWriter.Application.Common.Interfaces;
using Copyleaks.SDK.V3.API;
using Copyleaks.SDK.V3.API.Models.Callbacks;
using Copyleaks.SDK.V3.API.Models.Requests;
using Copyleaks.SDK.V3.API.Models.Requests.Properties;
using Copyleaks.SDK.V3.API.Models.Responses;
using Copyleaks.SDK.V3.API.Models.Types;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using GhostWriter.Infrastructure.Settings;

namespace GhostWriter.Infrastructure.Services
{
    public class CopyleaksPlagiarismChecker : IPlagiarismChecker
    {
        private LoginResponse loginResponse;
        //private SemaphoreSlim mySemaphoreSlim;
        private readonly CopyLeaksConfigSettings _copyLeaksConfigSettings;
        private readonly IWordGenerator _wordGenerator;

        public CopyleaksPlagiarismChecker(CopyLeaksConfigSettings copyLeaksConfigSettings, IWordGenerator wordGenerator)
        {
            _copyLeaksConfigSettings = copyLeaksConfigSettings;
            _wordGenerator = wordGenerator;
           // mySemaphoreSlim = semaphoreSlim;
        }

        public string CreateCopyleaksScanId(int documentId)
        {
            return $"{documentId}_{_wordGenerator.GenerateRandomString()}";
        }

        public int GetIdFromScanId(string scanId)
        {
            if (string.IsNullOrWhiteSpace(scanId))
                return default(int);

            var pieces = scanId.Split(new[] { '_' });

            if (pieces.Length < 1)
                return default(int);

            var id = pieces[0];

            int documentId;
            int.TryParse(id, out documentId);

            return documentId;
        }

        public async Task CheckForPlagiarismAsync(string baseUrl, string fileName, string filePath, string scanId)
        {
            //await mySemaphoreSlim.WaitAsync();
            try
            {
                if (loginResponse == null || loginResponse.Expire < DateTime.Now.AddHours(-1))
                    using (var id = new CopyleaksIdentityApi())
                    {
                        loginResponse = await id.LoginAsync(_copyLeaksConfigSettings.Email, _copyLeaksConfigSettings.ApiKey);
                    }
            }
            finally
            {
                //mySemaphoreSlim.Release();
                var aa = 5;
            }

            try
            {
                using (var api = new CopyleaksScansApi(eProduct.Businesses))
                {
                    // Convert the file to base64 string
                    string base64 = Convert.ToBase64String(await File.ReadAllBytesAsync(filePath));

                    api.SubmitFileAsync(scanId, new FileDocument
                    {
                        // A base64 data string of a file.
                        // If you would like to scan plain text, encode it as base64 and submit it.
                        Base64 = base64,
                        // The name of the file as it will appear in the Copyleaks scan report
                        // Make sure to include the right extension for your filetype.
                        Filename = fileName,

                        PropertiesSection = new EducationScanProperties
                        {
                            // You can test the integration with the Copyleaks API for free using the sandbox mode.
                            Sandbox = _copyLeaksConfigSettings.Sandbox,
                            // Register to receive asynchronous notifications
                            Webhooks = new Webhooks
                            {
                                // Triggered on every new result
                                NewResult = new Uri($"{baseUrl}/results?scanId={scanId}"),
                                // Triggerd on every status change, i.e: completed, error, creditsChecked , indexed
                                Status = new Uri($"{baseUrl}/{{STATUS}}?scanId={scanId}"),
                            },

                        }
                    }, loginResponse.Token).Wait();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

    }
}
