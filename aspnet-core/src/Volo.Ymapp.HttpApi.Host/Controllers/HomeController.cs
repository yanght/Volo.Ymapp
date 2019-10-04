using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Ymapp.Controllers
{
    public class HomeController : AbpController
    {
        public ActionResult Index()
        {
            //TODO: Enabled once Swagger supports ASP.NET Core 3.x
            //return Redirect("/swagger");
            return Content("OK: Volo.Ymapp.HttpApi.Host is running...");
        }
    }
}
