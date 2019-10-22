using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Ymapp.Products;

namespace Volo.Ymapp.EntityFrameworkCore
{
    public class EfCoreProductPriceRepository : EfCoreRepository<YmappDbContext, ProductPrice, Guid>, IProductPriceRepository
    {
        public EfCoreProductPriceRepository(IDbContextProvider<YmappDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
