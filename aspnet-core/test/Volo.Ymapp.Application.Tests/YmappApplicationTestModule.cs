using Volo.Abp.Modularity;

namespace Volo.Ymapp
{
    [DependsOn(
        typeof(YmappApplicationModule),
        typeof(YmappDomainTestModule)
        )]
    public class YmappApplicationTestModule : AbpModule
    {

    }
}