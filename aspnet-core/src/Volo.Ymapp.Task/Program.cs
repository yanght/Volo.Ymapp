using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Volo.Ymapp.Task
{
    class Program
    {

        static void Main(string[] args)
        {

            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //string response = HttpClientHelper.HttpGet("http://tispfiletest.utourworld.com/upload/op/xml/agentLine/index.xml", dic);

            //Console.WriteLine(response);
            //string ss = MD5Help.Get32MD5("AGENT201908091643194|agentTest");
            //Console.WriteLine(ss);

            //string response = UTourApiClient.getUserLoginVerification();
            string response = UTourApiClient.getRealTimeTeamStockNum("TS1800265774", "1204898");
            Console.WriteLine(response);
            Console.ReadLine();
        }


    }
}
