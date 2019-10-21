using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Ymapp.Areas;

namespace Volo.Ymapp.Products
{
    public class ProductPrice : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// 商品规格编码
        /// </summary>
        public Guid ProductSpecId { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        public Guid AreaId { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal OrignPrice { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal Price { get; set; }
    }
}
