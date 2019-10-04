using Volo.Ymapp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Volo.Ymapp.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(YmappEntityFrameworkCoreDbMigrationsModule),
        typeof(YmappApplicationContractsModule)
        )]
    public class YmappDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<BackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
