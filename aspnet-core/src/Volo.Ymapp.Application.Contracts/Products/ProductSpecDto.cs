﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Products
{
    public class ProductSpecDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
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
        public virtual List<ProductPriceDto> ProductPrice { get; set; }
        /// <summary>
        /// 商品库存
        /// </summary>
        public virtual List<ProductStockDto> ProductStocks { get; set; }
        public string ConcurrencyStamp { get ; set; }
    }
}
