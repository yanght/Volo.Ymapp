using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.IdentityServer.Jwt;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Volo.Ymapp;
using Volo.Ymapp.MultiTenancy;

namespace KH10086.WebApp
{
    [DependsOn(
       //typeof(YmappHttpApiModule),
       typeof(AbpAutofacModule),
       //typeof(AbpAspNetCoreMultiTenancyModule),
       //typeof(YmappApplicationModule),
        typeof(AbpAspNetCoreMvcModule)
       //typeof(YmappEntityFrameworkCoreDbMigrationsModule),
       //typeof(AbpAspNetCoreMvcUiBasicThemeModule),
       //typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
       //typeof(AbpAccountWebIdentityServerModule)
       )]
    public class WebAppWebModule:AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
