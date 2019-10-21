using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Ymapp.Products;

namespace Volo.Ymapp
{
    public class ProductAppService :
         CrudAppService<
            Product, ProductDto, Guid, PagedAndSortedResultRequestDto,
            CreateProductDto, UpdateProductDto>,
            IProductAppService
    {
        public ProductAppService(IRepository<Product, Guid> repository)
        : base(repository)
        {
        }
    }
}
