using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
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
        private readonly ProductManager _productManager;
        public ProductAppService(IRepository<Product, Guid> repository, ProductManager productManager)
        : base(repository)
        {
            _productManager = productManager;
        }
        public PagedResultDto<ProductDto> GetProductList(GetProductListDto input)
        {
            var query = Repository.WithDetails(m => m.Category);            
            var count = query.Count();
            var list = query.PageBy(input.SkipCount, input.MaxResultCount)
                       .ToList();

            return new PagedResultDto<ProductDto>(count, ObjectMapper.Map<List<Product>, List<ProductDto>>(list));
        }
    }
}
