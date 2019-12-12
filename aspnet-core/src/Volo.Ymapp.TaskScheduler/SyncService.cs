using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Serilog;

namespace Volo.Ymapp.TaskScheduler
{
    public class SyncService
    {
        public async Task StartAsync()
        {
            var provider = RegisterServices();
            Scheduler = provider.GetService(typeof(IScheduler)) as IScheduler;
            await Scheduler.Start();
            Log.Information("Quartz调度已启动...");
        }

        public async Task StopAsync()
        {
            await Scheduler.Shutdown();
            Log.Information("Quartz调度结束...");
            Log.CloseAndFlush();
        }

        #region Utils
        private IScheduler Scheduler { get; set; }
        private static ServiceProvider RegisterServices()
        {
            Log.Information("配置依赖注入...");
            var configuration = ReadFromAppSettings();
            var services = new ServiceCollection();

            #region 

            services.AddScoped<SyncService>();
            services.AddDbContext<DbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("ConnectionStrings")));
            //services.AddScoped<IService, Service>();

            #endregion

            #region Quartz

            Log.Information("配置Quartz...");
            services.AddScoped<IJobFactory, JobFactory>();
            services.AddSingleton(service =>
            {
                var option = new QuartzOption(configuration);
                var sf = new StdSchedulerFactory(option.ToProperties());
                var scheduler = sf.GetScheduler().Result;
                scheduler.JobFactory = service.GetService<IJobFactory>();
                return scheduler;
            });
            services.AddScoped<SyncJob>();
            //此处不能写成services.AddScoped<IJob,SyncJob>(); 会造成在找不到SyncJob

            #endregion
            
            var provider = services.BuildServiceProvider();
            return provider;
        }

        private static IConfigurationRoot ReadFromAppSettings()
        {
            //读取appsettings.json
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
        }

        #endregion
    }
}
