using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Ymapp;
using Volo.Ymapp.EntityFrameworkCore;

namespace KH10086.WebApp
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule)
        , typeof(AbpAutofacModule)// 在模块上添加依赖AbpAutofacModule
        , typeof(YmappApplicationModule)
        , typeof(YmappEntityFrameworkCoreDbMigrationsModule)
        )]
    public class WebAppWebModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {

            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMvcWithDefaultRoute();
        }
    }
}
