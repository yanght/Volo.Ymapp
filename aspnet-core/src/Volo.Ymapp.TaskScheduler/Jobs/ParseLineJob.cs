using System;
using System.Threading.Tasks;
using Quartz;
using Serilog;

namespace Volo.Ymapp.TaskScheduler.Jobs
{
    public class ParseLineJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Log.Information("线路解析任务开始...");



            await Task.FromResult(true);
        }
    }
}
