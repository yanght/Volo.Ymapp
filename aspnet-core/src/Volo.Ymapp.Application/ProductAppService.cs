using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
        public ProductAppService(IRepository<Product, Guid> repository
            , ProductManager productManager)
        : base(repository)
        {
            _productManager = productManager;
        }
        public PagedResultDto<ProductDto> GetProductList(GetProductListDto input)
        {
            var query = Repository.WithDetails(m => m.Category)
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), m => m.Name.Contains(input.Name))
                .WhereIf(input.CategoryId.HasValue, m => m.CategoryId == input.CategoryId)
                .WhereIf(!input.Code.IsNullOrWhiteSpace(), m => m.Code == input.Code)
                .WhereIf(input.State.HasValue, m => m.State == input.State)
                .WhereIf(input.StartTime.HasValue, m => m.CreationTime > input.StartTime)
                .WhereIf(input.EndTime.HasValue, m => m.CreationTime < input.EndTime);

            var count = query.Count();
            var list = query.PageBy(input.SkipCount, input.MaxResultCount)
                       .ToList();

            return new PagedResultDto<ProductDto>(count, ObjectMapper.Map<List<Product>, List<ProductDto>>(list));
        }

        public override async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            var product = new Product(input.Name, input.Code, input.CategoryId, input.Description);
            product.SetProductAreas(input.ProductAreas);
            product.SetProductPictures(input.ProductPictures);
            var model = await _productManager.CreateProduct(product);
            return ObjectMapper.Map<Product, ProductDto>(model);
        }

        public override Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input)
        {
            var product = new Product(input.Name, input.Code, input.CategoryId, input.Description);

        }
    }
}
