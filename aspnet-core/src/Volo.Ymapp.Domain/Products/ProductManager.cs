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
            return prouctInfo;
        }

        public async Task<Product> UpdateProduct(Guid id, Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }
    }
}
