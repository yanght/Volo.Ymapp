using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Ymapp.Categorys;

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
    }
}
