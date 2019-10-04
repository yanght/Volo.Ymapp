using Volo.Ymapp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Volo.Ymapp
{
    [DependsOn(
        typeof(YmappEntityFrameworkCoreTestModule)
        )]
    public class YmappDomainTestModule : AbpModule
    {

    }
}