using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Ymapp.Areas;
using Volo.Ymapp.CommonEnum;

namespace Volo.Ymapp.Products
{
    public class CreateUpdateProductCategoryDtoBase
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
    }
}
