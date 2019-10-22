using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Ymapp.Products;

namespace Volo.Ymapp.EntityFrameworkCore
{
    public class EfCoreProductPictureRepository : EfCoreRepository<YmappDbContext, ProductPicture, Guid>, IProductPictureRepository
    {
        public EfCoreProductPictureRepository(IDbContextProvider<YmappDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
