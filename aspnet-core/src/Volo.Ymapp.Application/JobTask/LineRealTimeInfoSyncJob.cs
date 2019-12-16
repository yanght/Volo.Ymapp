using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Ymapp.Kh10086;

namespace Volo.Ymapp.JobTask
{
    public class LineRealTimeInfoSyncJob : BackgroundJob<LineRealTimeInfoSyncJobArgs>, ITransientDependency
    {
        private ILineAppService _lineApp;
        public LineRealTimeInfoSyncJob(ILineAppService lineApp)
        {
            _lineApp = lineApp;
        }
        public override void Execute(LineRealTimeInfoSyncJobArgs args)
        {
            _lineApp.LineTeamAsync().GetAwaiter().GetResult();
        }
    }
    public class LineRealTimeInfoSyncJobArgs
    {
    }
}
