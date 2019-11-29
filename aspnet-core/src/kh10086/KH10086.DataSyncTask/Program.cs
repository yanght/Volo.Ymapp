using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Topshelf;

namespace KH10086.DataSyncTask
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            HostFactory.Run(x =>
            {
                x.UseLog4Net();
                x.Service<ServiceRunner>();
                x.SetDescription("康辉旅游线路数据同步服务");
                x.SetDisplayName("KH10086DataSyncTask");
                x.SetServiceName("KH10086DataSyncTask");
                x.EnablePauseAndContinue();
            });
        }
    }
}
