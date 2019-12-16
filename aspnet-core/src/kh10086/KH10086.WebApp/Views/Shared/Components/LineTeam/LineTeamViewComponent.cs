using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Ymapp.Kh10086;

namespace KH10086.WebApp.Views.Shared.Components.LineTeam
{
    public class LineTeamViewComponent : ViewComponent
    {
        private readonly ILineAppService _lineApp;
        public LineTeamViewComponent(ILineAppService lineApp)
        {
            _lineApp = lineApp;
        }
        public async Task<IViewComponentResult> InvokeAsync(string lineCode)
        {
            var result = _lineApp.GetLineTeams(lineCode);
            return View(result);
        }
    }
}
