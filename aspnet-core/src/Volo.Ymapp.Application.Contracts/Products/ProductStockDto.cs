using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Products
{
    public class ProductStockDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
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
        public string ConcurrencyStamp { get ; set; }
    }
}
