using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Ymapp.Areas;

namespace Volo.Ymapp.Products
{
    public class ProductStock : FullAuditedAggregateRoot<Guid>
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
        /// 库存
        /// </summary>
        public int Stock { get; set; }
    }
}
