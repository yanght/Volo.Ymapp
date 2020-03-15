using System;
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
        /// 商品描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal OrignalPrice { get; set; }
        /// <summary>
        /// 现价（成人价）
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildrenPrice { get; set; }
        /// <summary>
        /// 出发地
        /// </summary>
        public string PlaceLeave { get; set; }
        /// <summary>
        /// 返回地
        /// </summary>
        public string PlaceReturn { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int DayNumber { get; set; }
        /// <summary>
        /// 晚数
        /// </summary>
        public int NightNumber { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
