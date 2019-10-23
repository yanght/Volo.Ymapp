using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Volo.Ymapp.Products
{
    public interface IProductPictureRepository : IBasicRepository<ProductPicture, Guid>
    {
        Task DeleteProductPicture(Guid productId);
        Task AddProductPicture(List<ProductPicture> pictures);

        Task UpdateProductPictures(Guid productId, List<ProductPicture> pictures);
    }
}
