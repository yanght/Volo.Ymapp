using log4net;
using System;

namespace Volo.Ymapp.Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.Configure(); //使用前先配置

            LogHelper.Info("1212121");
            LogHelper.Warn("异常", new Exception("456"));

            Console.ReadKey();
        }
    }
}
