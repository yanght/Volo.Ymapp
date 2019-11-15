using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Volo.Ymapp.Task
{
    class Program
    {

        static void Main(string[] args)
        {
            string response = UTourApiClient.getUserLoginVerification();
            Console.WriteLine(response);
            Console.ReadLine();
        }


    }
}
