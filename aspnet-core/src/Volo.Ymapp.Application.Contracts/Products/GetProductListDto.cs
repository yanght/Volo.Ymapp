using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Ymapp.CommonEnum;

namespace Volo.Ymapp.Products
{
    public class GetProductListDto : PagedAndSortedResultRequestDto
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid? CategoryId { get; set; }
        public ProductState? State { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
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
    }
}
