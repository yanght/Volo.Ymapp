using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;
using Volo.Ymapp.Dtos;

namespace Volo.Ymapp.Categorys
{
    public interface ICategoryAppService :
        ICrudAppService< //Defines CRUD methods
           CategoryDto, //Used to show books
           Guid, //Primary key of the book entity
           PagedAndSortedResultRequestDto, //Used for paging/sorting on getting a list of books
           CreateCategoryDto, //Used to create a new book
           UpdateCategoryDto> //Used to update a book
    {
        Task<List<TreeDataDto>> GetCategoryTree(GetCategoryTreeDto input);
        CategoryDto GetCategoryByName(string categoryName);
        List<CategoryDto> GetCategoryListByType(CategoryType type);
    }
}
