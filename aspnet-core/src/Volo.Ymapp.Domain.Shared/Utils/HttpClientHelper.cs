using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Ymapp.Utils
{
    public static class HttpClientHelper
    {
        /// <summary>
        /// 发起POST同步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>
        /// <param name="headers">填充消息头</param>        
        /// <returns></returns>
        public static string HttpPost(string url, string postData = null, string contentType = null, int timeOut = 30, Dictionary<string, string> headers = null)
        {
            postData = postData ?? "";
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
                {
                    if (contentType != null)
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

                    HttpResponseMessage response = client.PostAsync(url, httpContent).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }


        /// <summary>
        /// 发起POST异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>
        /// <param name="headers">填充消息头</param>        
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string url, string postData = null, string contentType = null, int timeOut = 30, Dictionary<string, string> headers = null)
        {
            postData = postData ?? "";
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, timeOut);
                if (headers != null)
                {
                    foreach (var header in headers)
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
                {
                    if (contentType != null)
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

                    HttpResponseMessage response = await client.PostAsync(url, httpContent);
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }


        /// 发起GET同步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string HttpGet(string url, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                HttpResponseMessage response = client.GetAsync(url).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// 发起GET异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<string> HttpGetAsync(string url, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                HttpResponseMessage response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }
        /// <summary>
        /// Http 请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">请求参数（可选参数）</param>
        /// <param name="method">请求方法（可选参数，默认：GET）</param>
        /// <param name="timeout">连接超时（可选参数,默认: 100,000 毫秒（100 秒））</param>
        /// <param name="encoding">编码（可选参数，默认：utf-8）</param>
        /// <param name="contentType">Content-type HTTP 标头（可选参数）</param>
        /// <param name="userAgent">模拟浏览器UA（可选参数）</param>
        /// <param name="headers">HTTP 标头（可选参数）</param>
        /// <returns>远程服务器返回的数据</returns>
        public static string HttpRequest(string url, string data = null, string method = "GET", int? timeout = null, Encoding encoding = null, string contentType = "application/x-www-form-urlencoded", string userAgent = null, Dictionary<string, string> headers = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var myWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myWebRequest.ContentType = contentType;
            myWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
            myWebRequest.Method = method;
            if (!string.IsNullOrWhiteSpace(userAgent))
            {
                myWebRequest.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                myWebRequest.Timeout = timeout.Value;
            }
            if (myWebRequest.RequestUri.Scheme == "https")
            {
                myWebRequest.ProtocolVersion = HttpVersion.Version11;
                myWebRequest.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CertificateValidationCallback);
                // 这里设置了协议类型。
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;// SecurityProtocolType.Tls1.2; 
                myWebRequest.KeepAlive = false;
                ServicePointManager.CheckCertificateRevocationList = true;
                ServicePointManager.DefaultConnectionLimit = 100;
                ServicePointManager.Expect100Continue = false;
            }
            if (headers != null && headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> item in headers)
                {
                    myWebRequest.Headers[item.Key] = item.Value;
                }
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(data))
                {
                    using (Stream stream = myWebRequest.GetRequestStream())
                    {
                        var bytes = encoding.GetBytes(data);
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                }
                using (HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse())
                {
                    if (myWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream receiveStream = myWebResponse.GetResponseStream())
                        {
                            using (StreamReader readStream = new StreamReader(receiveStream, encoding))
                            {
                                return readStream.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        throw new WebException(myWebResponse.StatusDescription);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myWebRequest.Abort();
            }
        }
        /// <summary>
        /// (SSL) 证书验证回调
        /// </summary>
        /// <param name="sender">一个对象，它包含此验证的状态信息。</param>
        /// <param name="certificate">用于对远程方进行身份验证的证书。</param>
        /// <param name="chain">与远程证书关联的证书颁发机构链。</param>
        /// <param name="sslPolicyErrors">与远程证书关联的一个或多个错误。</param>
        /// <returns>是否接受指定证书进行身份验证</returns>
        private static bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            return true;
        }
    }
}
