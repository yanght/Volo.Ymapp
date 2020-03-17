using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Volo.Ymapp.Products
{
    public class ProductAppService :
       CrudAppService<
           Product, ProductDto, long, PagedAndSortedResultRequestDto,
           CreateProductDto, UpdateProductDto>,
           IProductAppService
    {
        public ProductAppService(IRepository<Product, long> repository)
       : base(repository)
        {

        }

    }
}
