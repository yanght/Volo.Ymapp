using KH10086.WebApp.Models.Partial;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;

namespace KH10086.WebApp.Views.Shared.Components.LineTypeSliderBar
{
    public class LineTypeSliderBarViewComponent : ViewComponent
    {
        private readonly ICategoryAppService _categoryApp;
        public LineTypeSliderBarViewComponent(ICategoryAppService categoryApp)
        {
            _categoryApp = categoryApp;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result =  _categoryApp.GetCategoryListByType(CategoryType.LineType);
            return View(result);
        }

    }
}
