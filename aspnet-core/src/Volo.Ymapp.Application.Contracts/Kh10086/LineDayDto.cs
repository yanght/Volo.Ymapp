using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Kh10086
{
    public class LineDayDto : AuditedEntityDto<long>, IHasConcurrencyStamp
    {
        /// <summary>
        /// 线路Id
        /// </summary>
        public long LineId { get; set; }
        /// <summary>
        /// 第几天
        /// </summary>
        public int DayNumber { get; set; }
        /// <summary>
        /// 酒店
        /// 有备注信息数据以“_”分隔符连接,没有就不显示“_”字符
        /// 无 当地三星级 当地三-四星级 当地四星级 当地四-五星级 当地五星级 国际五星级 国际五星级 当地四花 当地五花 特色酒店 国际四星级 舒适酒店 度假酒店 特色农场 
        /// </summary>
        public string DayHotel { get; set; }
        /// <summary>
        /// 早餐
        /// 有备注信息数据以“_”分隔符连接,没有就不显示“_”字符
        /// 无 有 自理
        /// </summary>
        public string Breakfast { get; set; }
        /// <summary>
        /// 午餐
        /// 有备注信息数据以“_”分隔符连接,没有就不显示“_”字符
        /// 无 有 自理
        /// </summary>
        public string Lunch { get; set; }
        /// <summary>
        /// 晚餐
        /// 有备注信息数据以“_”分隔符连接,没有就不显示“_”字符
        /// 无 有 自理
        /// </summary>
        public string Dinner { get; set; }
        /// <summary>
        /// 交通工具
        /// 有备注信息数据以“_”分隔符连接,没有就不显示“_”字符
        /// 无 飞机 汽车 火车 游轮 渡轮 高铁 快艇 内陆飞机 水上飞机 船 邮轮  酒店班车 自驾 
        /// </summary>
        public string DayTraffic { get; set; }
        /// <summary>
        /// 途经城市英文名称
        /// </summary>
        public string CityEnglish { get; set; }
        /// <summary>
        /// 点对点途经城市公里数
        /// </summary>
        public string ScityDistance { get; set; }
        /// <summary>
        /// 每日产品名称
        /// 途经城市+交通工具
        /// </summary>
        public string TrafficName { get; set; }
        /// <summary>
        /// 产品介绍
        /// </summary>
        public string Describe { get; set; }
        public string ConcurrencyStamp { get; set; }

        public List<LineDayTrafficDto> LineDayTraffics { get; set; }
        public List<LineDayImageDto> LineDayImages { get; set; }
        public List<LineDaySelfDto> LineDaySelfs { get; set; }
        public List<LineDayShopDto> LineDayShops { get; set; }
    }

    /// <summary>
    /// 航班节点
    /// </summary>
    public class LineDayTrafficDto : AuditedEntityDto<long>, IHasConcurrencyStamp
    {
        /// <summary>
        /// 线路Id
        /// </summary>
        public long LineId { get; set; }
        /// <summary>
        /// 每日产品节点Id
        /// </summary>
        public long LineDayId { get; set; }
        /// <summary>
        /// 航空公司二字码
        /// </summary>
        public string TrafficCo { get; set; }
        /// <summary>
        /// 航班号
        /// </summary>
        public string TrafficNo { get; set; }
        /// <summary>
        /// 到达时间
        /// </summary>
        public string TrafficTimeEnd { get; set; }
        /// <summary>
        /// 起飞时间
        /// </summary>
        public string TrafficTimeStart { get; set; }
        public string ConcurrencyStamp { get; set; }
    }

    /// <summary>
    /// 自费节点
    /// 当channelType为“自费”的时候才生成    countrynameSel节点
    /// </summary>
    public class LineDaySelfDto : AuditedEntityDto<long>, IHasConcurrencyStamp
    {
        /// <summary>
        /// 线路Id
        /// </summary>
        public long LineId { get; set; }
        /// <summary>
        /// 每日产品节点Id
        /// </summary>
        public long LineDayId { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 自费项目
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 活动内容
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 服务内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
        public string ConcurrencyStamp { get; set; }
    }

    /// <summary>
    /// 商店节点
    /// 当channelType为“商店”的时候才生成countrynameShop节点
    /// </summary>
    public class LineDayShopDto : AuditedEntityDto<long>, IHasConcurrencyStamp
    {
        /// <summary>
        /// 线路Id
        /// </summary>
        public long LineId { get; set; }
        /// <summary>
        /// 每日产品节点Id
        /// </summary>
        public long LineDayId { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 商店名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 主要商品
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 停留时间
        /// </summary>
        public string ActivityTime { get; set; }
        public string ConcurrencyStamp { get; set; }
    }

    /// <summary>
    /// 每日产品配图节点
    /// </summary>
    public class LineDayImageDto : AuditedEntityDto<long>, IHasConcurrencyStamp
    {
        /// <summary>
        /// 线路Id
        /// </summary>
        public long LineId { get; set; }
        /// <summary>
        /// 每日产品节点Id
        /// </summary>
        public long LineDayId { get; set; }
        /// <summary>
        /// 图片编码
        /// </summary>
        public string ImgCode { get; set; }
        /// <summary>
        /// 州名称
        /// </summary>
        public string Continent { get; set; }
        /// <summary>
        /// 国家名称
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string Sight { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgPath { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
