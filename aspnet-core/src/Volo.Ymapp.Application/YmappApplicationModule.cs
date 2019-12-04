using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Ymapp.Workers;

namespace Volo.Ymapp
{
    [DependsOn(
        typeof(YmappDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(YmappApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule)
        )]
    public class YmappApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<YmappApplicationModule>();
                //options.AddProfile<YmappApplicationAutoMapperProfile>();
            });
        }
        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            var workManager = (IBackgroundWorkerManager)context.ServiceProvider.GetService(typeof(IBackgroundWorkerManager));
            var worker = (TestWork)context.ServiceProvider.GetService(typeof(TestWork));
            workManager.Add(worker);
            base.OnPreApplicationInitialization(context);
        }
    }
}
