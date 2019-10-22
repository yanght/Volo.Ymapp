using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Ymapp.Areas;
using Volo.Ymapp.Products;

namespace Volo.Ymapp.EntityFrameworkCore.Repositorys
{
    public class EfCoreProductAreaRepository : EfCoreRepository<YmappDbContext, ProductArea, Guid>, IProductAreaRepository
    {
        public EfCoreProductAreaRepository(IDbContextProvider<YmappDbContext> dbContextProvider)
          : base(dbContextProvider)
        {
        }
    }
}
