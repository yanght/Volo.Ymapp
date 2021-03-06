﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Ymapp.Products
{
    public class CreateUpdateProductDtoBase
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
    }
}
