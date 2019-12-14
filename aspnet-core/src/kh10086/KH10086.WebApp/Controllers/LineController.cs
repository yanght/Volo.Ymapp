using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KH10086.WebApp.Models.Line;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.Kh10086;
using X.PagedList;

namespace KH10086.WebApp.Controllers
{
    public class LineController : AbpController
    {
        private readonly ILineAppService _lineApp;
        private readonly ICategoryAppService _categoryApp;
        public LineController(ILineAppService lineApp, ICategoryAppService categoryApp)
        {
            _lineApp = lineApp;
            _categoryApp = categoryApp;
        }

        public async Task<IActionResult> TypeList(string lineType, int pageIndex = 1)
        {
            int pageSize = 20;
            LineTypeListViewModel model = new LineTypeListViewModel();
            var result = _lineApp.GetLineList(new GetLineListDto()
            {
                LineCategoryType = lineType,
                SkipCount = (pageIndex - 1) * pageSize,
                MaxResultCount = pageSize
            });
            if (!string.IsNullOrWhiteSpace(lineType))
            {
                var category = await _categoryApp.GetAsync(Guid.Parse(lineType));
                model.Category = category;
            }
            model.Lines = result.Items.ToList();
            model.TotalCount = result.TotalCount;
            var usersAsIPagedList = new StaticPagedList<LineListDto>(model.Lines, pageIndex, pageSize, (int)model.TotalCount);
            ViewBag.Pager = usersAsIPagedList;
            return View(model);
        }

        public async Task<IActionResult> CountryList(Guid categoryId, int pageIndex = 1)
        {
            int pageSize = 20;
            LineCountryListViewModel model = new LineCountryListViewModel();
            string continent = ""; string country = "";
            if (categoryId != Guid.Empty)
            {
                var category = await _categoryApp.GetAsync(categoryId);
                if (category.ParentId == Guid.Empty)
                {
                    continent = category.Name;
                }
                else
                {
                    country = category.Name;
                }

                model.Category = category;
            }
            var result = _lineApp.GetLineList(new GetLineListDto()
            {
                CategoryId = categoryId,
                Continent = continent,
                Country = country,
                SkipCount = (pageIndex - 1) * pageSize,
                MaxResultCount = pageSize
            });
            model.Lines = result.Items.ToList();
            model.TotalCount = result.TotalCount;
            var usersAsIPagedList = new StaticPagedList<LineListDto>(model.Lines, pageIndex, pageSize, (int)model.TotalCount);
            ViewBag.Pager = usersAsIPagedList;
            return View(model);
        }

        [Route("line/{id}.html")]
        public async Task<IActionResult> Index(int id)
        {
            LineDetailViewModel model = new LineDetailViewModel();
            var line = await _lineApp.GetLineByLineId(id);
            model.Line = line;
            return View(model);
        }
    }
}