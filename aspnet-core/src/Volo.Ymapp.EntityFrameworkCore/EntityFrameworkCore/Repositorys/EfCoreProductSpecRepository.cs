using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Ymapp.Products;

namespace Volo.Ymapp.EntityFrameworkCore
{
    public class EfCoreProductSpecRepository : EfCoreRepository<YmappDbContext, ProductSpec, Guid>, IProductSpecRepository
    {
        public EfCoreProductSpecRepository(IDbContextProvider<YmappDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
