using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Products
{
    public class ProductDto : AuditedEntityDto<long>, IHasConcurrencyStamp
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public string ProuctCode { get; set; }
        /// <summary>
        /// 商品分类
        /// </summary>
        public long CategoryId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 商品主图
        /// </summary>
        public string MainPicture { get; set; }
        /// <summary>
        /// 商品状态
        /// </summary>
        public int Status { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
