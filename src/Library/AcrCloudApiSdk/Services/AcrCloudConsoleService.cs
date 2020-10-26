using AcrCloudApiSdk.Helpers;
using AcrCloudApiSdk.Interfaces;
using AcrCloudApiSdk.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private readonly int ChannelPerPage;

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
        public async Task<AcrUploadResponse> Upload(string accessKey,
        string accessSecret, string audioId, string audioTitle, string bucketName, string dataType,
        byte[] audioData, int timeoutSecond)
        {
            string reqUrl = $"{BaseUrl}{AudioEndpoint.UrlPrepend}";
            string httpAction = AudioEndpoint.HttpAction;
            string httpMethod = "POST";
            string signatureVersion = "1";

            var userParams = new Dictionary<string, object>();

            string timestamp =
                ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds)
                .ToString();

            var newLine = "\n";
            string sigStr = $"{httpMethod}{newLine}" +
                $"{httpAction}{newLine}" +
                $"{accessKey}{newLine}" +
                $"{signatureVersion}{newLine}" +
                $"{timestamp}";

            string signature = EncryptByHMACSHA1(sigStr, accessSecret);
            var headerParams = PrepareHeadersParams(accessKey, signatureVersion, signature, timestamp);

            var postParams = new Dictionary<string, object>
            {
                { AudioKeys.TitleParam, audioTitle },
                { AudioKeys.AudioIdParam, audioId },
                { AudioKeys.BucketNameParam, bucketName },
                { AudioKeys.DatatypeParam, dataType },
                { AudioKeys.AudioFileParm, audioData }
            };

            int i = 0;
            foreach (var item in userParams)
            {
                postParams.Add("custom_key[" + i + "]", item.Key);
                postParams.Add("custom_value[" + i + "]", item.Value);
                i++;
            }

            string res = await PostHttp(reqUrl, postParams, headerParams, timeoutSecond);
            return JsonConvert.DeserializeObject<AcrUploadResponse>(res);
        }
        public async Task<AcrUploadResponse> Upload(string audioId, string audioTitle, string filePath, string fileType)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                byte[] audioBytes = reader.ReadBytes((int)fs.Length);

                var timeout = 60;

                return await Upload(AccountAccessKey, AccountAccessSecret,
                    audioId, audioTitle, BucketName, fileType, audioBytes, timeout);


            }
        }
        public async Task<bool> Delete(string acrId)
        {
            string reqUrl = $"{BaseUrl}audios/{acrId}";
            string httpAction = $"/v1/audios/{acrId}";
            string httpMethod = "DELETE";
            string signatureVersion = "1";

            string timestamp =
            ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds)
            .ToString();

            string sigStr = httpMethod + "\n" + httpAction + "\n" + AccountAccessKey + "\n" + signatureVersion + "\n" +
                            timestamp;
            string signature = EncryptByHMACSHA1(sigStr, AccountAccessSecret);

            var headerParams = PrepareRequstHeaders(AccountAccessKey, signatureVersion, signature, timestamp);

            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(reqUrl);
                if (webRequest != null)
                {
                    webRequest.Method = "DELETE";
                    webRequest.Timeout = 120000;
                    webRequest.Headers.Add(headerParams);
                    using (var response = (HttpWebResponse)await webRequest.GetResponseAsync())
                        return response.StatusCode == HttpStatusCode.NoContent;
                }
                return false;
            }
            catch (Exception exp)
            {
                return false;
            }
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
        private async Task<string> PostHttp(string url, IDictionary<string, object> postParams, IDictionary<string, object> headerParams = null, int timeoutSecond = 60)
        {
            string result = "";
            string BOUNDARYSTR = "acrcloud***copyright***2015***" + DateTime.Now.Ticks.ToString("x");
            string BOUNDARY = "--" + BOUNDARYSTR + "\r\n";
            var ENDBOUNDARY = Encoding.ASCII.GetBytes("--" + BOUNDARYSTR + "--\r\n\r\n");

            var stringKeyHeader = BOUNDARY +
                                  "Content-Disposition: form-data; name=\"{0}\"" +
                                  "\r\n\r\n{1}\r\n";
            var filePartHeader = BOUNDARY +
                                 "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                                 "Content-Type: application/octet-stream\r\n\r\n";

            var memStream = new MemoryStream();
            foreach (var item in postParams)
            {
                if (item.Value is string)
                {
                    string tmpStr = string.Format(stringKeyHeader, item.Key, item.Value);
                    byte[] tmpBytes = Encoding.UTF8.GetBytes(tmpStr);
                    memStream.Write(tmpBytes, 0, tmpBytes.Length);
                }
                else if (item.Value is byte[])
                {
                    var header = string.Format(filePartHeader, item.Key, item.Key);
                    var headerbytes = Encoding.UTF8.GetBytes(header);
                    memStream.Write(headerbytes, 0, headerbytes.Length);
                    byte[] sample = (byte[])item.Value;
                    memStream.Write(sample, 0, sample.Length);
                    memStream.Write(Encoding.UTF8.GetBytes("\r\n"), 0, 2);
                }
            }

            memStream.Write(ENDBOUNDARY, 0, ENDBOUNDARY.Length);

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream writer = null;
            StreamReader myReader = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = timeoutSecond * 1000;
                request.Method = "POST";
                if (headerParams != null)
                {
                    foreach (var item in headerParams)
                    {
                        if (item.Value is string)
                        {
                            request.Headers.Add(item.Key + ":" + item.Value);
                        }
                    }
                }

                request.ContentType = "multipart/form-data; boundary=" + BOUNDARYSTR;

                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);

                writer = request.GetRequestStream();
                writer.Write(tempBuffer, 0, tempBuffer.Length);
                writer.Flush();
                writer.Close();
                writer = null;

                response = (HttpWebResponse)await request.GetResponseAsync();
                myReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = myReader.ReadToEnd();
            }
            catch (WebException exp)
            {
                //_logger.Error(exp);
            }
            catch (Exception exp)
            {
                //_logger.Error(exp);
            }
            finally
            {
                if (memStream != null)
                    memStream.Close();

                if (writer != null)
                    writer.Close();

                if (myReader != null)
                    myReader.Close();

                if (request != null)
                    request.Abort();

                if (response != null)
                    response.Close();
            }

            return result;
        }
        private NameValueCollection PrepareRequstHeaders(string accessKey, string signatureVersion, string signature, string timestamp)
        {
            return new NameValueCollection
            {
                { HeadersKeys.AccessKey, accessKey },
                { HeadersKeys.signatureVersion, signatureVersion },
                { HeadersKeys.Signature, signature },
                { HeadersKeys.Timestamp, timestamp }
            };
        }
        private Dictionary<string, object> PrepareHeadersParams(string accessKey, string signatureVersion, string signature, string timestamp)
        {
            return new Dictionary<string, object>
            {
                { HeadersKeys.AccessKey, accessKey },
                { HeadersKeys.signatureVersion, signatureVersion },
                { HeadersKeys.Signature, signature },
                { HeadersKeys.Timestamp, timestamp }
            };
        }

        #endregion
    }
}
