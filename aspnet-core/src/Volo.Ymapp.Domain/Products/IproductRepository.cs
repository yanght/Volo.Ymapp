using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Volo.Ymapp.Products
{
    public interface IProductRepository : IBasicRepository<Product, Guid>
    {
    }
}
