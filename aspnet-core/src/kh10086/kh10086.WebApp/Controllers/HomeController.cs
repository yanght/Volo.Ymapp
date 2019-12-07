﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KH10086.WebApp.Models;
using Volo.Ymapp.Kh10086;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;
using KH10086.WebApp.Models.Home;

namespace KH10086.WebApp.Controllers
{
    public class HomeController : AbpController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILineAppService _lineApp;
        private readonly ICategoryAppService _categoryApp;
        public HomeController(ILogger<HomeController> logger, ILineAppService lineApp
            , ICategoryAppService categoryApp)
        {
            _logger = logger;
            _lineApp = lineApp;
            _categoryApp = categoryApp;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel viewModel = new HomeViewModel();
            var areaCategorys = await _categoryApp.GetCategoryTree(new GetCategoryTreeDto() { Type = CategoryType.Line });
            viewModel.AreaCategorys = areaCategorys;
            return View(viewModel);
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
