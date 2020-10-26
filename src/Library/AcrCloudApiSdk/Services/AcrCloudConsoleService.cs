using AcrCloudApiSdk.Interfaces;
using AcrCloudApiSdk.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AcrCloudApiSdk
{
    public class AcrCloudConsoleService : IAcrCloudConsoleService
    {
        private readonly string AccountAccessKey;
        private readonly string AccountAccessSecret;
        private readonly string BaseUrl;
        private readonly string BroadcastDatabaseMonitoringProjectName;
        private readonly string BucketName;
        private readonly int ChannelPerPage = 50;

        public AcrCloudConsoleService(string accountAcccessKey, string accountAccessSecret, string baseUrl, string broadcastDatabaseMonitoringProjectName, string bucketName, int channelPerPage = 50)
        {
            AccountAccessKey = accountAcccessKey;
            AccountAccessSecret = accountAccessSecret;
            BaseUrl = baseUrl;
            BroadcastDatabaseMonitoringProjectName = broadcastDatabaseMonitoringProjectName;
            BucketName = bucketName;
            ChannelPerPage = channelPerPage;
        }

        public async Task<ChannelResponseModel> GetChannelsAsync()
        {
            string reqUrl = $"{BaseUrl}acrcloud-monitor-streams?page=1&project_name={BroadcastDatabaseMonitoringProjectName}&per_page={ChannelPerPage}";
            string httpMethod = "GET";
            string httpUri = "/v1/acrcloud-monitor-streams";
            string signatureVersion = "1";

            string timestamp =
                ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds)
                .ToString();

            string sigStr = httpMethod + "\n" + httpUri + "\n" + AccountAccessKey + "\n" + signatureVersion + "\n" +
                            timestamp;
            string signature = EncryptByHMACSHA1(sigStr, AccountAccessSecret);
            var headerParams = new NameValueCollection
            {
                { "access-key", AccountAccessKey },
                { "signature-version", signatureVersion },
                { "signature", signature },
                { "timestamp", timestamp }
            };
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(reqUrl);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 120000;
                    webRequest.Headers.Add(headerParams);

                    var webResponse = await webRequest.GetResponseAsync().ConfigureAwait(false);
                    using (var responseStream = webResponse.GetResponseStream())
                    {
                        var streamReader = new StreamReader(responseStream);
                        var responseString = await streamReader.ReadToEndAsync();
                        return JsonConvert.DeserializeObject<ChannelResponseModel>(responseString);
                    }
                }
            }
            catch (WebException exp)
            {
                Console.WriteLine(exp);
                return PrepareResponse(exp);
            }
            return PrepareResponse();
        }

        public async Task<ProjectResponseModel> GetProjectsAsync()
        {
            string reqUrl = $"{BaseUrl}acrcloud-monitor-streams/projects";
            string httpMethod = "GET";
            string httpUri = "/v1/acrcloud-monitor-streams/projects";
            string signatureVersion = "1";

            string timestamp =
                ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds)
                .ToString();

            string sigStr = httpMethod + "\n" + httpUri + "\n" + AccountAccessKey + "\n" + signatureVersion + "\n" +
                            timestamp;
            string signature = EncryptByHMACSHA1(sigStr, AccountAccessSecret);
            var headerParams = new NameValueCollection
            {
                { "access-key", AccountAccessKey },
                { "signature-version", signatureVersion },
                { "signature", signature },
                { "timestamp", timestamp }
            };
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(reqUrl);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 120000;
                    webRequest.Headers.Add(headerParams);

                    var webResponse = await webRequest.GetResponseAsync().ConfigureAwait(false);
                    using (var responseStream = webResponse.GetResponseStream())
                    {
                        var streamReader = new StreamReader(responseStream);
                        var responseString = await streamReader.ReadToEndAsync();
                        return JsonConvert.DeserializeObject<ProjectResponseModel>(responseString);
                    }
                }
            }
            catch (WebException exp)
            {
                Console.WriteLine(exp);
                return new ProjectResponseModel();// PrepareResponse(exp);
            }
            return new ProjectResponseModel();// PrepareResponse();
        }

        #region private methods

        private string EncryptByHMACSHA1(string input, string key)
        {
            HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] stringBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedValue = hmac.ComputeHash(stringBytes);
            return EncodeToBase64(hashedValue);
        }

        private static string EncodeToBase64(byte[] input)
        {
            return Convert.ToBase64String(input, 0, input.Length);
        }

        private ChannelResponseModel PrepareResponse(WebException exp = null)
        {

            var errorResponse = new ChannelResponseModel
            {
                ErrorStatus = exp.Status.ToString(),
                Error = exp?.Message ?? "Some error occured"
            };
            return errorResponse;
        }

        #endregion
    }
}
