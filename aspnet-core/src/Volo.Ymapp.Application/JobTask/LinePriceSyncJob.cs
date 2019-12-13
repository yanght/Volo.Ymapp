using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Ymapp.Kh10086;

namespace Volo.Ymapp.JobTask
{
    public class LinePriceSyncJob : BackgroundJob<LinePriceSyncJobArgs>, ITransientDependency
    {
        private ILineAppService _lineApp;
        public LinePriceSyncJob(ILineAppService lineApp)
        {
            _lineApp = lineApp;
        }
        public override void Execute(LinePriceSyncJobArgs args)
        {
            _lineApp.LinePriceAsync().GetAwaiter().GetResult();
        }
    }

    public class LinePriceSyncJobArgs
    {
    }
}
