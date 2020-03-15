using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Products
{
    public class ProductCategoryDto : AuditedEntityDto<long>, IHasConcurrencyStamp
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 上级分类Id
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
