using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Volo.Ymapp.Products
{
    public class ProductPropertyAppService : IProductPropertyAppService
    {

        protected IRepository<ProductProperty, long> repository_ProductProperty { get; set; }
        public ProductPropertyAppService(
        IRepository<ProductProperty, long> _repository_ProductProperty
            )
        {
            repository_ProductProperty = _repository_ProductProperty;
        }

    }
}
