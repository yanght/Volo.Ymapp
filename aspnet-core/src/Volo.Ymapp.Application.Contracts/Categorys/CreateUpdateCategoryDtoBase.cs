using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Categorys
{
    public class CreateUpdateCategoryDtoBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public int Type { get; set; } = 0;
        /// <summary>
        /// 上级分类编号
        /// </summary>
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid ParentId { get; set; }
        /// <summary>
        /// 分类图片
        /// </summary>
        public string PictureUrl { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        [DefaultValue(0)]
        public int IsRecommend { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }
    }
}
