using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Ymapp.Areas;

namespace Volo.Ymapp.Products
{
    public class ProductSpec : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// 规格名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        public Guid AreaId { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public virtual List<ProductPrice> ProductPrice { get; set; }
        /// <summary>
        /// 商品库存
        /// </summary>
        public virtual List<ProductStock> ProductStocks { get; set; }
    }
}
