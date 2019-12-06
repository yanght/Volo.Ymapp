using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace KH10086.WebApp.Controllers
{
    public class LineController : AbpController
    {
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}