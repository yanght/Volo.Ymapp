﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Volo.Ymapp.Kh10086
{
    /// <summary>
    /// 线路
    /// </summary>
    public class Line : FullAuditedAggregateRoot<long>
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

        public decimal CustomerPrice { get; set; }
        public decimal AgentPrice { get; set; }
        public decimal ChildPrice { get; set; }
        public decimal SingleRoom { get; set; }
        public decimal OverseasJoinPrice { get; set; }
        public decimal Deposit { get; set; }
        /// <summary>
        /// 最早出团日期
        /// </summary>
        public DateTime DateStart { get; set; }
        /// <summary>
        /// 最早终端售卖开始日期
        /// </summary>
        public DateTime DateOnline { get; set; }
        /// <summary>
        /// 最晚截止下单日期，即下架日期
        /// </summary>
        public DateTime DateOffline { get; set; }

        /// <summary>
        /// 分类编号
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public int Recommend { get; set; }
        /// <summary>
        /// 线路分类类型  多个以,分隔
        /// </summary>
        public string LineCategoryType { get; set; }
    }
}
