using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Volo.Ymapp.Kh10086
{
    public class LineTeam : FullAuditedAggregateRoot<long>
    {
        public long LineId { get; set; }
        /// <summary>
        /// 线路编号
        /// </summary>
        public string LineCode { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public string TeamId { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public string Function { get; set; }
        /// <summary>
        /// 目的地大洲
        /// </summary>
        public string Continent { get; set; }
        /// <summary>
        /// 出发地城市
        /// </summary>
        public string PlaceLeave { get; set; }
        /// <summary>
        /// 返回城市
        /// </summary>
        public string PlaceReturn { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime DateStart { get; set; }
        /// <summary>
        /// 回团日期
        /// </summary>
        public DateTime DateFinish { get; set; }
        /// <summary>
        /// 本团天数
        /// </summary>
        public int DayNum { get; set; }
        /// <summary>
        /// 航空公司中文
        /// </summary>
        public string AirCompany { get; set; }
        /// <summary>
        /// 航空公司2字码
        /// </summary>
        public string AirShortName { get; set; }
        /// <summary>
        /// 直客价
        /// </summary>
        public decimal CustomerPrice { get; set; }
        /// <summary>
        /// 同业价
        /// </summary>
        public decimal AgentPrice { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildPrice { get; set; }
        /// <summary>
        /// 单房差
        /// </summary>
        public decimal SingleRoom { get; set; }
        /// <summary>
        /// 境外参团价
        /// </summary>
        public decimal OverseasJoinPrice { get; set; }
        /// <summary>
        /// 订金
        /// </summary>
        public decimal Deposit { get; set; }
        /// <summary>
        /// 预收数
        /// </summary>
        public int PlanNum { get; set; }
        /// <summary>
        /// 余位数
        /// </summary>
        public int FreeNum { get; set; }
        /// <summary>
        /// 团队标签
        /// </summary>
        public string WebsiteTags { get; set; }
        /// <summary>
        /// 截止下单日期，即下架日期
        /// </summary>
        public DateTime DateOffline { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string PostersImg { get; set; }
        public string PostersData { get; set; }
    }
}
