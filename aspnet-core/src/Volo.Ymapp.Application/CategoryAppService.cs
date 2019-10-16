using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.Dtos;

namespace Volo.Ymapp
{
    public class CategoryAppService :
        CrudAppService<
           Category, CategoryDto, Guid, PagedAndSortedResultRequestDto,
           CreateCategoryDto, UpdateCategoryDto>,
           ICategoryAppService
    {
        public CategoryAppService(IRepository<Category, Guid> repository)
        : base(repository)
        {
        }

        public async Task<List<TreeDataDto>> GetCategoryTree(GetCategoryTreeDto input)
        {
            var list = await GetListAsync(new PagedAndSortedResultRequestDto() { SkipCount = 0, MaxResultCount = int.MaxValue });

            return CategoryToTreeData(list.Items.ToList());
        }

        private List<TreeDataDto> CategoryToTreeData(List<CategoryDto> categorys)
        {
            List<TreeDataDto> tree = new List<TreeDataDto>();
            foreach (var item in categorys.Where(m => m.ParentId == Guid.Empty))
            {
                var children = categorys.Where(m => m.ParentId == item.Id).ToList();
                tree.Add(new TreeDataDto()
                {
                    Title = item.Name,
                    Value = item.Id.ToString(),
                    Key = item.Id.ToString(),
                    Children = CategoryToTreeData(children)
                });
            }
            return tree;
        }
    }
}
