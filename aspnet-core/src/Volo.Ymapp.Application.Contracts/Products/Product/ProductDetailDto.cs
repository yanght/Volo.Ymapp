using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Ymapp.Products
{
    public class ProductDetailDto : ProductDto
    {
        /// <summary>
        /// 商品图片
        /// </summary>
        public List<string> ProductImages { get; set; }
    }
}
