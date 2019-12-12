using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Kh10086
{
    public class GetLineListDto : PagedAndSortedResultRequestDto
    {
        public Guid CategoryId { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public string LineCategoryType { get; set; }
        public int? Recommend { get; set; }
    }

    public class LineListDto : AuditedEntityDto<long>
    {
        /// <summary>
        /// 行程编号
        /// </summary>
        public string LineCode { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomTitle { get; set; }
        /// <summary>
        /// 大洲名称序列
        /// </summary>
        public string Continent { get; set; }
        /// <summary>
        /// 国家名称序列
        /// </summary>
        public string Country { get; set; }
        public string TxtTransitCity { get; set; }
        /// <summary>
        /// 景点名称序列
        /// </summary>
        public string Sight { get; set; }
        /// <summary>
        /// 所属类型
        /// </summary>
        public string LineType { get; set; }
        /// <summary>
        /// 晚数
        /// </summary>
        public int NumNight { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int NumDay { get; set; }
        /// <summary>
        /// 签证名称 序列格式:签证id|签证名称,
        /// </summary>
        public string Visa { get; set; }
        /// <summary>
        /// 多张产品配图 图片地址”,”号分割
        /// </summary>
        public string ImgCode { get; set; }
        /// <summary>
        /// 多张产品配图  图片所属大洲”,”号分割
        /// </summary>
        public string ImgContinent { get; set; }
        /// <summary>
        /// 多张产品配图 图片所属国家”,”号分割
        /// </summary>
        public string ImgCountry { get; set; }
        /// <summary>
        /// 多张产品配图 图片所属国家”,”号分割
        /// </summary>
        public string ImgCity { get; set; }
        /// <summary>
        /// 出发城市
        /// </summary>
        public string PlaceLeave { get; set; }
        /// <summary>
        /// 返回城市
        /// </summary>
        public string PlaceReturn { get; set; }
        /// <summary>
        /// 产品类型
        /// 大众常规 奇迹旅行 境外参团 特价 自由行
        /// </summary>
        public string Function { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FirstLineImg { get; set; }
    }
}
