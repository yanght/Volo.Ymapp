using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Volo.Ymapp.ApiTransfer.Controllers
{
    [Route("api/transfer")]
    public class TransferController : Controller
    {
        private HttpClient client = new HttpClient();
        private readonly string host = "https://tispapitest.utourworld.com/";
        [HttpGet]
        public string Get(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return "";
            var response = client.GetAsync(url).Result.EnsureSuccessStatusCode();//请求转发
            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                if (t != null)
                {
                    return t.Result;
                }
            }
            return "";
        }

        [HttpPost]
        public string Post(string url, string datajson)
        {
            if (string.IsNullOrWhiteSpace(url)) return "";
           
            return HttpClientPost(url, datajson);
        }

        private static string HttpClientPost(string url, string datajson)
        {
            HttpClient httpClient = new HttpClient();//http对象
            //表头参数
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //转为链接需要的格式
            HttpContent httpContent = new StringContent(datajson);
            //请求
            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                if (t != null)
                {
                    return t.Result;
                }
            }
            return "";
        }
    }
}