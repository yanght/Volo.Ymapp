using Volo.Abp.Domain.Entities.Auditing;

namespace Volo.Ymapp.Products
{
    public class ProductSku: FullAuditedAggregateRoot<long>
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// SKU编码
        /// </summary>
        public string SkuCode { get;set; }
        /// <summary>
        /// 属性键值对：propertyname:propertyvalue 关联两张表的ID
        /// </summary>
        public string Properties { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal OrignalPrice { get; set; }
        /// <summary>
        /// 现价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

    }
}