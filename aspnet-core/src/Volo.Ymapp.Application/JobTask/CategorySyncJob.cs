using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;
using Volo.Ymapp.Kh10086;

namespace Volo.Ymapp.JobTask
{
    public class CategorySyncJob : BackgroundJob<CategorySyncArgs>, ITransientDependency
    {
        private ILineAppService _lineApp;
        private ICategoryAppService _categoryApp;
        private IRepository<Category, Guid> _categoryRepository;
        public CategorySyncJob(ILineAppService lineApp, ICategoryAppService categoryApp
            , IRepository<Category, Guid> categoryRepository)
        {
            _lineApp = lineApp;
            _categoryApp = categoryApp;
            _categoryRepository = categoryRepository;
        }

        public override void Execute(CategorySyncArgs args)
        {
            List<string> continents = _lineApp.GetContinents();
            continents.ForEach((item) =>
            {
                if (_categoryRepository.Where(m => m.Name == item).Count() == 0)
                {
                    _categoryApp.CreateAsync(new CreateCategoryDto()
                    {
                        Name = item.Trim(),
                        ParentId = Guid.Empty,
                        Sort = 0,
                        Type = (int)CategoryType.Line
                    }).GetAwaiter().GetResult();
                }
            });
        }
    }
}
