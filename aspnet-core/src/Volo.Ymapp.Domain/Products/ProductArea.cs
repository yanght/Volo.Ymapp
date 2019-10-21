using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Volo.Ymapp.Products
{
    public class ProductArea : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        public Guid AreaId { get; set; }
    }
}
