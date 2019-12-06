using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KH10086.WebApp.Models;
using Volo.Ymapp.Kh10086;
using Volo.Abp.AspNetCore.Mvc;

namespace KH10086.WebApp.Controllers
{
    public class HomeController : AbpController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILineAppService _lineApp;
        public HomeController(ILogger<HomeController> logger, ILineAppService lineApp)
        {
            _logger = logger;
            _lineApp = lineApp;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _lineApp.GetLineByLineId(1);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
