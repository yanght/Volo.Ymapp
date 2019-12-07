using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;
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


        /// <summary>
        /// 获取树形分类列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<TreeDataDto>> GetCategoryTree(GetCategoryTreeDto input)
        {
            var result = Repository.WhereIf(input.Type != CategoryType.Undefined, m => m.Type == input.Type).ToList();
            //var list = await GetListAsync(new PagedAndSortedResultRequestDto() { SkipCount = 0, MaxResultCount = int.MaxValue });
            var list = result.MapToList<Category, CategoryDto>().ToList();
            return GetCategoryTree(Guid.Empty, list);
        }


        private List<TreeDataDto> GetCategoryTree(Guid parentId, List<CategoryDto> list)
        {
            if (list == null || list.Count == 0) return null;
            List<TreeDataDto> treeList = new List<TreeDataDto>();
            var result = list.Where(m => m.ParentId == parentId).ToList();
            foreach (var item in result)
            {
                TreeDataDto model = new TreeDataDto()
                {
                    Title = item.Name,
                    Value = item.Id.ToString(),
                    Key = item.Id.ToString(),
                    Children = GetCategoryTree(item.Id, list)
                };
                treeList.Add(model);
            }
            return treeList.Count() == 0 ? null : treeList;
        }
    }
}
