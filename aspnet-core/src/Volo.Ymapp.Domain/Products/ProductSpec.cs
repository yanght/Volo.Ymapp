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
    }
}
