using Volo.Abp.Domain.Entities.Auditing;

namespace Volo.Ymapp.Products
{
    public class ProductImage: FullAuditedAggregateRoot<long>
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
    }
}