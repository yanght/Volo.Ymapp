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

        [HttpGet]
        public string Get(string url)
        {
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
        public string Post([FromBody] string url, object datajson)
        {
            return HttpClientPost(url, datajson);
        }

        private static string HttpClientPost(string url, object datajson)
        {
            HttpClient httpClient = new HttpClient();//http对象
            //表头参数
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //转为链接需要的格式
            HttpContent httpContent = new JsonContent(datajson);
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
    public class JsonContent : StringContent
    {
        public JsonContent(object obj) :
           base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }
}