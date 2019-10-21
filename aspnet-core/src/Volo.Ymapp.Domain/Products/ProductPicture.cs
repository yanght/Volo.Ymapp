using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Ymapp.CommonEnum;

namespace Volo.Ymapp.Products
{
    public class ProductPicture : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        public string PictureUrl { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// 商品图片类型
        /// </summary>
        public ProductPictureType Type { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
