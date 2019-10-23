using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.Ymapp.Products
{
    public interface IProductAppService :
         ICrudAppService< //Defines CRUD methods
            ProductDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting on getting a list of books
            CreateProductDto, //Used to create a new book
            UpdateProductDto> //Used to update a book
    {

        PagedResultDto<ProductDto> GetProductList(GetProductListDto input);
    }
}
