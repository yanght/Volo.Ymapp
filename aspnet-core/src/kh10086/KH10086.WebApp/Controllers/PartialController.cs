using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KH10086.WebApp.Models.Partial;
using Microsoft.AspNetCore.Mvc;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;

namespace KH10086.WebApp.Controllers
{
    public class PartialController : Controller
    {
        private readonly ICategoryAppService _categoryApp;
        public PartialController(ICategoryAppService categoryApp)
        {
            _categoryApp = categoryApp;
        }
        public async Task<IActionResult> NavigationBar()
        {
            NavigationViewModel model = new NavigationViewModel();
            var result = await _categoryApp.GetCategoryTree(new GetCategoryTreeDto() { Type = CategoryType.Line });
            model.CountryCategorys = result;
            return View(model);
        }
    }
}