using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace Volo.Ymapp.Workers
{
    public class TestWork : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        public TestWork(AbpTimer timer) : base(timer)
        {
            timer.Period = 5000;
        }
        protected override void DoWork()
        {
            Log.Information("TestWork.....");
        }
    }
}
