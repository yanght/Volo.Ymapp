using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.ObjectMapping;
using Volo.Ymapp.Areas;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;
using Volo.Ymapp.Products;

namespace Volo.Ymapp.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string Code { get; protected set; }
        /// <summary>
        /// 分类编码
        /// </summary>
        public Guid CategoryId { get; protected set; }
        /// <summary>
        /// 分类
        /// </summary>
        public virtual Category Category { get; protected set; }
        /// <summary>
        /// 商品状态
        /// </summary>
        public ProductState State { get; protected set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; protected set; }

    }
}
