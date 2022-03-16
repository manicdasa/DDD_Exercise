using GhostWriter.Application.Common.Interfaces;
using System;
using PayoutsSdk.Core;
using PayoutsSdk.Payouts;
using PayPalHttp;
using System.Collections.Generic;
using System.Threading.Tasks;
using GhostWriter.Infrastructure.Settings;
using GhostWriter.Application.Common.Models;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace GhostWriter.Infrastructure.Services
{
    public class PayPalPayoutService : IPayoutService
    {
        private readonly PayPalConfigSettings _payPalConfigSettings;

        public PayPalPayoutService(PayPalConfigSettings payPalConfigSettings)
        {
            _payPalConfigSettings = payPalConfigSettings;
        }

        public PayPalHttp.HttpClient client()
        {
            PayPalEnvironment environment;

            if (_payPalConfigSettings.Sandbox)
                environment = new SandboxEnvironment(_payPalConfigSettings.PaypalClientID, _payPalConfigSettings.PaypalClientSecret);
            else
                environment = new LiveEnvironment(_payPalConfigSettings.PaypalClientID, _payPalConfigSettings.PaypalClientSecret);

            PayPalHttpClient client = new PayPalHttpClient(environment);
            return client;
        }

        public async Task<OutputModel> CreatePayoutInEuros(string payerID, decimal amount)
        {
            try
            {
                var retString = string.Empty;
                var body = new CreatePayoutRequest()
                {
                    SenderBatchHeader = new SenderBatchHeader()
                    {
                        EmailMessage = $"Congrats on recieving {amount}eur",
                        EmailSubject = "You received a payout!!"
                    },
                    Items = new List<PayoutItem>(){
                    new PayoutItem()
                    {
                        RecipientType="PAYPAL_ID",
                        Amount=new Currency()
                        {
                            CurrencyCode="EUR",
                            Value=amount.ToString(),
                        },
                    Receiver=payerID,
                    }
                }
                };
                PayoutsPostRequest request = new PayoutsPostRequest();
                request.RequestBody(body);
                var response = await client().Execute(request);
                var result = response.Result<CreatePayoutResponse>();
                retString += $"Status: {result.BatchHeader.BatchStatus}";
                retString += $"\nBatch Id: {result.BatchHeader.PayoutBatchId}";
                retString += $"\nLinks:";
                foreach (LinkDescription link in result.Links)
                {
                    retString += $"\t{link.Rel}: {link.Href}\tCall Type: {link.Method}";
                }

                retString += $"\n\n\n\n\n";

                PayoutsGetRequest request1 = new PayoutsGetRequest(result.BatchHeader.PayoutBatchId);
                var getResponse = await client().Execute(request1);
                var result1 = getResponse.Result<PayoutBatch>();
                retString += $"Status: {result1.BatchHeader.BatchStatus}";
                retString += $"Item: {result1.Items[0].PayoutItemId}";
                retString += $"Status: {result1.BatchHeader.BatchStatus}";
                retString += $"Links:";
                foreach (LinkDescription link in result1.Links)
                {
                    retString += $"\t{link.Rel}: {link.Href}\tCall Type: {link.Method}";
                }
                if (result1.BatchHeader.BatchStatus == "SUCCESS")
                    return new OutputModel()
                    {
                        Message = retString,
                        Success = true
                    };
                else
                    throw new Exception($"Payment unsuccessful. Payment status: {result1.BatchHeader.BatchStatus}");
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<OutputModel> GetAuthorsPaypalCredentials(string code)
        {
            var authTokenRes = await GetAuthorisationToken(code);

            if (!authTokenRes.Success)
                return authTokenRes;
                
            var payerIdRes = await GetPayerId(authTokenRes.Message);

            return payerIdRes;
        }

        private async Task<OutputModel> GetAuthorisationToken(string code)
        {
            try
            {
                //setup reusable http client
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.BaseAddress = new Uri("https://api-m.paypal.com");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.ConnectionClose = true;

                //Post body content
                var values = new List<KeyValuePair<string, string>>();
                values.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                values.Add(new KeyValuePair<string, string>("code", code));
                var content = new FormUrlEncodedContent(values);

                var authenticationString = $"{_payPalConfigSettings.PaypalClientID}:{_payPalConfigSettings.PaypalClientSecret}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/v1/identity/openidconnect/tokenservice");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                requestMessage.Content = content;

                //make the request
                var task = await client.SendAsync(requestMessage);
                var response = task.Headers.Age;

                return new OutputModel()
                {
                    Success = true,
                    Message = await task.Content.ReadAsStringAsync()
                };

            }
            catch (Exception ex)
            {
                return new OutputModel()
                {
                    Message = ex.InnerException.Message,
                    Success = false
                };
            }
        }

        private async Task<OutputModel> GetPayerId(string authToken)
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri("https://api-m.paypal.com");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Post body content
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("schema", "openid "));
            var content = new FormUrlEncodedContent(values);

            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authToken));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "v1/oauth2/token/userinfo");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", base64EncodedAuthenticationString);
            requestMessage.Content = content;

            //make the request
            var task = await client.SendAsync(requestMessage);
            var response = await task.Content.ReadAsStringAsync();
            return new OutputModel()
            {
                Success = true,
                Message = await task.Content.ReadAsStringAsync() //TODO: once the API call becomes successful, parse the PayerId and PaypalEmail and store in two separate strings
            };
        }
    }
}
