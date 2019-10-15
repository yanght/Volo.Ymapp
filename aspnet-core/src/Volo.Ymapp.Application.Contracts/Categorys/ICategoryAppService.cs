using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Ymapp.Categorys;

namespace Volo.Ymapp.Categorys
{
    public interface ICategoryAppService:
        ICrudAppService< //Defines CRUD methods
           CategoryDto, //Used to show books
           Guid, //Primary key of the book entity
           PagedAndSortedResultRequestDto, //Used for paging/sorting on getting a list of books
           CreateUpdateCategoryDto, //Used to create a new book
           CreateUpdateCategoryDto> //Used to update a book
    {

    }
}
