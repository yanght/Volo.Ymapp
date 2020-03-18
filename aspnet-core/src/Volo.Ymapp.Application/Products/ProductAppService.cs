using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private IRepository<ProductImage, long> repository_productImage;
        public ProductAppService(IRepository<Product, long> repository,
            IRepository<ProductImage, long> _repository_productImage)
       : base(repository)
        {
            repository_productImage = _repository_productImage;
        }

        public override async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            var product = ObjectMapper.Map<CreateProductDto, Product>(input);
            product = await Repository.InsertAsync(product, true);
            if (input.ProductImages.Any())
            {
                foreach (var item in input.ProductImages)
                {
                    await repository_productImage.InsertAsync(new ProductImage()
                    {
                        ImageUrl = item,
                        ProductId = product.Id
                    });
                }
            }
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public override async Task<ProductDto> UpdateAsync(long id, UpdateProductDto input)
        {
            var product = ObjectMapper.Map<UpdateProductDto, Product>(input);
            var productDto = await base.UpdateAsync(id, input);
            await repository_productImage.DeleteAsync(m => m.ProductId == id);
            if (input.ProductImages.Any())
            {
                foreach (var item in input.ProductImages)
                {
                    await repository_productImage.InsertAsync(new ProductImage()
                    {
                        ImageUrl = item,
                        ProductId = product.Id
                    });
                }
            }
            return productDto;
        }


        public async Task<ProductDetailDto> GetDetailAsync(long id)
        {
            var product = await base.GetAsync(id);
            var productDetail = ObjectMapper.Map<ProductDto, ProductDetailDto>(product);
            productDetail.ProductImages = repository_productImage.Where(m => m.ProductId == product.Id).Select(m => m.ImageUrl).ToList();
            return productDetail;
        }
    }
}
