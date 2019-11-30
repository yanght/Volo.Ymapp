using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Volo.Ymapp.TaskScheduler
{
    [DependsOn(
       typeof(AbpAutofacModule),
       typeof(YmappApplicationContractsModule)
       )]

    public class TaskSchedulerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }       
    }
}
