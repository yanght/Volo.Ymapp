using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Topshelf;
using Volo.Abp;
using Volo.Abp.Threading;
using Volo.Ymapp.Kh10086;
using Volo.Ymapp.TaskScheduler.Jobs;

namespace Volo.Ymapp.TaskScheduler
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var application = AbpApplicationFactory.Create<TaskSchedulerModule>(options =>
            {
                options.UseAutofac(); //Autofac integration
            }))
            {
                //注册编码提供程序
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                InstanceLog();
                var rc = HostFactory.Run(x =>
                {
                    x.Service<SyncService>(s =>
                    {
                        s.ConstructUsing(name => new SyncService());
                        s.WhenStarted(async tc => await tc.StartAsync()); //调用此方法前勿有太多操作，会造成服务启动失败
                    s.WhenStopped(async tc => await tc.StopAsync());
                    });
                    x.RunAsLocalSystem();

                    x.SetDescription("SyncJob Description");
                    x.SetDisplayName("SyncJob DisplayName");
                    x.SetServiceName("SyncJob ServiceName");
                });
                var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
                Environment.ExitCode = exitCode;
            }
        }

        private static void InstanceLog()
        {
            //配置Serilog
            var template = "{Timestamp:HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path: "logs/log.txt", outputTemplate: template, rollingInterval: RollingInterval.Day)
                .WriteTo.Console(LogEventLevel.Information)
                .CreateLogger();
        }
    }
}
