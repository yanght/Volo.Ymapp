﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Ymapp.Areas;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;

namespace Volo.Ymapp.Products
{
    public class ProductDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public CategoryDto Category { get; set; }
        /// <summary>
        /// 商品状态
        /// </summary>
        public ProductState State { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public  List<ProductPictureDto> ProductPictures { get; set; }
        /// <summary>
        /// 商品区域
        /// </summary>
        public List<AreaDto> Areas { get; set; }
        /// <summary>
        /// 商品规格
        /// </summary>
        public  List<ProductSpecDto> ProductSpecs { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
