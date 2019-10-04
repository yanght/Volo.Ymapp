using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Volo.Ymapp.EntityFrameworkCore
{
    [DependsOn(
        typeof(YmappEntityFrameworkCoreModule)
        )]
    public class YmappEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<YmappMigrationsDbContext>();
        }
    }
}
