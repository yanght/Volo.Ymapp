using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using Volo.Ymapp.Products;

namespace Volo.Ymapp.Products
{
    public interface IProductAreaRepository : IBasicRepository<ProductArea, Guid>
    {
    }
}
