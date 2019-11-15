using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Ymapp.Areas;
using Volo.Ymapp.CommonEnum;

namespace Volo.Ymapp.Products
{
    public class CreateUpdateProductDtoBase
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        [StringLength(20)]
        public string Code { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>
        [Required]
        public Guid CategoryId { get; set; }
        /// <summary>
        /// 商品状态
        /// </summary>
        public ProductState State { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }
    }
}
