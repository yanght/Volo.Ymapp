using System;
using System.Threading.Tasks;
using Quartz;
using Serilog;

namespace Volo.Ymapp.TaskScheduler
{
    public class SyncJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Log.Information("同步开始...");
        }
    }
}
