using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public Task AddProductPicture(List<ProductPicture> pictures)
        {
            return DbSet.AddRangeAsync(pictures);
        }

        public async Task DeleteProductPicture(Guid productId)
        {
            await DeleteAsync(m => m.ProductId == productId);
        }

        public async Task UpdateProductPictures(Guid productId, List<ProductPicture> pictures)
        {
            await DeleteProductPicture(productId);
            await AddProductPicture(pictures);
        }
    }
}
