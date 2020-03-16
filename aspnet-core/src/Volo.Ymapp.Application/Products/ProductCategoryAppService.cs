using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Ymapp.Categorys;

namespace Volo.Ymapp.Products
{
    public class ProductCategoryAppService :
         CrudAppService<
             ProductCategory, ProductCategoryDto, long, PagedAndSortedResultRequestDto,
             CreateProductCategoryDto, UpdateProductCategoryDto>,
             IProductCategoryAppService
    {
        public ProductCategoryAppService(IRepository<ProductCategory, long> repository)
       : base(repository)
        {
        }

        /// <summary>
        /// 获取树形分类列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductCategoryTreeDto>> GetCategoryTree()
        {
            var result = await Repository.GetListAsync();
            var list = ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryTreeDto>>(result);
            return GetCategoryTree(0, list);
        }
        private List<ProductCategoryTreeDto> GetCategoryTree(long parentId, List<ProductCategoryTreeDto> list)
        {
            if (list == null || list.Count == 0) return null;
            List<ProductCategoryTreeDto> treeList = new List<ProductCategoryTreeDto>();
            var result = list.Where(m => m.ParentId == parentId).ToList();
            foreach (var item in result)
            {
                item.Children = GetCategoryTree(item.Id, list);
                treeList.Add(item);
            }
            return treeList.Count() == 0 ? null : treeList;
        }

    }
}
