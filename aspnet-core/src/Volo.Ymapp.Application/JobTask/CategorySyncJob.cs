using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;
using Volo.Ymapp.Kh10086;

namespace Volo.Ymapp.JobTask
{
    public class CategorySyncJob : BackgroundJob<CategorySyncArgs>, ITransientDependency
    {
        private ILineAppService _lineApp;
        private ICategoryAppService _categoryApp;
        public CategorySyncJob(ILineAppService lineApp, ICategoryAppService categoryApp)
        {
            _lineApp = lineApp;
            _categoryApp = categoryApp;
        }

        public override void Execute(CategorySyncArgs args)
        {
            List<string> continents = _lineApp.GetContinents();
            continents.ForEach((item) =>
            {
                _categoryApp.CreateAsync(new CreateCategoryDto()
                {
                    Name = item,
                    ParentId = Guid.NewGuid(),
                    Sort = 0,
                    Type = (int)CategoryType.Line
                }).GetAwaiter().GetResult();
            });
        }
    }
}
