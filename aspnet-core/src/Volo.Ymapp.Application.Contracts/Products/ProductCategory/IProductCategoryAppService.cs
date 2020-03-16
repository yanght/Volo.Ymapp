using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Ymapp.Categorys;

namespace Volo.Ymapp.Products
{
    public interface IProductCategoryAppService :
        ICrudAppService< //Defines CRUD methods
           ProductCategoryDto, //Used to show books
           long, //Primary key of the book entity
           PagedAndSortedResultRequestDto, //Used for paging/sorting on getting a list of books
           CreateProductCategoryDto, //Used to create a new book
           UpdateProductCategoryDto> //Used to update a book
    {
        /// <summary>
        /// 获取树形分类列表
        /// </summary>
        /// <returns></returns>
        Task<List<ProductCategoryTreeDto>> GetCategoryTree();
    }

}
