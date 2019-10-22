using System;
using System.Collections.Generic;
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
        private readonly IProductPriceRepository _productPriceRepository;
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductSpecRepository _productSpecRepository;
        private readonly IProductStockRepository _productStockRepository;
        public ProductManager(IProductRepository productRepository,
            IProductPriceRepository productPriceRepository,
            IProductPictureRepository productPictureRepository,
            IProductSpecRepository productSpecRepository,
            IProductStockRepository productStockRepository
            )
        {
            _productRepository = productRepository;
            _productPriceRepository = productPriceRepository;
            _productPictureRepository = productPictureRepository;
            _productSpecRepository = productSpecRepository;
            _productStockRepository = productStockRepository;
        }

    }
}
