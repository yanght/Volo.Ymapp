using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Ymapp.CommonEnum;

namespace Volo.Ymapp.Categorys
{
    public class CategoryDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        /// <summary>
        /// 分类
        /// </summary>
        public CategoryType Type { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 上级分类编号
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 分类图片
        /// </summary>
        public string PictureUrl { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public int IsRecommend { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
