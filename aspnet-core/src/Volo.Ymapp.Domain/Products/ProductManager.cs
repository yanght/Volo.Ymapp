using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Ymapp.Areas;

namespace Volo.Ymapp.Products
{
    public class ProductManager : IDomainService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductAreaRepository _productAreaRepository;
        private readonly IProductPriceRepository _productPriceRepository;
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductSpecRepository _productSpecRepository;
        private readonly IProductStockRepository _productStockRepository;
        public ProductManager(IProductRepository productRepository,
            IProductAreaRepository productAreaRepository,
            IProductPriceRepository productPriceRepository,
            IProductPictureRepository productPictureRepository,
            IProductSpecRepository productSpecRepository,
            IProductStockRepository productStockRepository
            )
        {
            _productRepository = productRepository;
            _productAreaRepository = productAreaRepository;
            _productPriceRepository = productPriceRepository;
            _productPictureRepository = productPictureRepository;
            _productSpecRepository = productSpecRepository;
            _productStockRepository = productStockRepository;
        }

        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> CreateProduct(Product product)
        {
            var prouctInfo = await _productRepository.InsertAsync(product);

            foreach (var item in product.ProductAreas)
            {
                await _productAreaRepository.InsertAsync(item);
            }

            foreach (var item in product.ProductPictures)
            {
                await _productPictureRepository.InsertAsync(item);
            }
            return prouctInfo;
        }

        public async Task<Product> UpdateProduct(Guid id, Product product)
        {
            var model = await _productRepository.FindAsync(id);

            await _productPictureRepository.UpdateProductPictures(model.Id, product.ProductPictures);

            model.SetCategoryId(product.CategoryId);
            model.SetCode(product.Code);
            model.SetDescription(product.Description);
            model.SetName(product.Name);
            model.SetState(product.State);
            model.SetProductAreas(product.ProductAreas.Select(m => m.Id).ToList());
            model.SetProductPictures(product.ProductPictures.Select(m => m.PictureUrl).ToList());

            return await _productRepository.UpdateAsync(model);
        }
    }
}
