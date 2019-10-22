using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Ymapp.Products;

using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace Volo.Ymapp.EntityFrameworkCore
{
    public class EfCoreProductRepository : EfCoreRepository<YmappDbContext, Product, Guid>, IProductRepository
    {
        public EfCoreProductRepository(IDbContextProvider<YmappDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
