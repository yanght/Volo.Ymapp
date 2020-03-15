using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Ymapp.Kh10086;

namespace Volo.Ymapp.Controllers
{
    public class HomeController : AbpController
    {
        private readonly ILineAppService _lineApp;
        public HomeController(ILineAppService lineApp)
        {
            _lineApp = lineApp;
        }
        public ActionResult Index()
        {
            //_lineApp.GetContinents();
            //TODO: Enabled once Swagger supports ASP.NET Core 3.x
            return Redirect("/swagger");
            //return Content("OK: Volo.Ymapp.HttpApi.Host is running...");
        }
    }
}
