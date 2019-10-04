using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Volo.Ymapp.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(YmappHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class YmappConsoleApiClientModule : AbpModule
    {
        
    }
}
