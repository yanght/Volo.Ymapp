using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace Volo.Ymapp.Task
{
    class Program
    {

        static void Main(string[] args)
        {


            //注册编码提供程序
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Console.OutputEncoding = System.Text.Encoding.GetEncoding("gb2312");//第一种方式：指定编码
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            string response = HttpClientHelper.HttpRequest("https://tispfile.utourworld.com/upload/op/xml/agentLine/index.xml",encoding:Encoding.GetEncoding("GBK"));

            //Encoding encoding = System.Text.Encoding.Default;
            //byte[] data = encoding.GetBytes(response);


            // response = Encoding.GetEncoding("GB2312").GetString();
            //Console.WriteLine(response);
            //string ss = MD5Help.Get32MD5("AGENT201908091643194|agentTest");
            //Console.WriteLine(ss);

            //response = HttpUtility.UrlDecode(response, Encoding.GetEncoding("GBK"));

            // response = Encoding.GetEncoding("GBK").GetString(data);

            //string response = UTourApiClient.getUserLoginVerification();
            // string response = UTourApiClient.getRealTimeTeamStockNum("TS1800265774", "1204898");
            Console.WriteLine(response);
            Console.ReadLine();
        }


    }
}
