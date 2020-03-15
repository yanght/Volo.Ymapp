using System.Reflection.Emit;
using Volo.Abp.Domain.Entities.Auditing;

namespace Volo.Ymapp.Products
{
    public class ProductProperty: FullAuditedAggregateRoot<long>
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// 属性名称Id
        /// </summary>
        public long PropertyNameId { get; set; }
        /// <summary>
        /// 属性值Id
        /// </summary>
        public long PropertyValueId { get; set; }
    }
}