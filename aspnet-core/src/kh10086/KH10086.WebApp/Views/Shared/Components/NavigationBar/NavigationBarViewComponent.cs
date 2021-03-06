﻿using KH10086.WebApp.Models.Partial;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;
using Volo.Ymapp.Dtos;

namespace KH10086.WebApp.Views.Shared.Components
{
    public class NavigationBarViewComponent : ViewComponent
    {
        private readonly ICategoryAppService _categoryApp;
        public NavigationBarViewComponent(ICategoryAppService categoryApp)
        {
            _categoryApp = categoryApp;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            NavigationViewModel model = new NavigationViewModel();
            var result = await _categoryApp.GetCategoryTree(new GetCategoryTreeDto() { Type = CategoryType.LineCountry });
            model.CountryCategorys = result;
            model.LineTypeList = _categoryApp.GetCategoryListByType(CategoryType.LineType);
            return View(model);
        }
    }
}
