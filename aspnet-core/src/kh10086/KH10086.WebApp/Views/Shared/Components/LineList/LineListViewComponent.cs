using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Ymapp.Kh10086;

namespace KH10086.WebApp.Views.Shared.Components.LineList
{
    public class LineListViewComponent : ViewComponent
    {
        private readonly ILineAppService _lineApp;
        public LineListViewComponent(ILineAppService lineApp)
        {
            _lineApp = lineApp;
        }
        public async Task<IViewComponentResult> InvokeAsync(int number)
        {
            var result = _lineApp.GetLineList(new GetLineListDto() { SkipCount = 0, MaxResultCount = number, Recommend = 1 });
            return View(result.Items);
        }
    }
}
