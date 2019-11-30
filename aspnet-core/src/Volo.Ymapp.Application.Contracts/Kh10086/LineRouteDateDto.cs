using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Kh10086
{
    public class LineRouteDateDto : AuditedEntityDto<long>, IHasConcurrencyStamp
    {
        /// <summary>
        /// 线路Id
        /// </summary>
        public long LineId { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public string TeamId { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime DateStart { get; set; }
        /// <summary>
        /// 回团时间
        /// </summary>
        public DateTime DateFinish { get; set; }
        /// <summary>
        /// 同业价格
        /// </summary>
        public decimal AgentPrice { get; set; }
        /// <summary>
        /// 出票日期
        /// </summary>
        public DateTime? JieShouRiQi { get; set; }
        /// <summary>
        /// 直客儿童参考价格
        /// </summary>
        public decimal ChildPrice { get; set; }
        /// <summary>
        /// 直客成人参考价
        /// </summary>
        public decimal AdultPrice { get; set; }
        /// <summary>
        /// 订金
        /// </summary>
        public decimal Deposit { get; set; }
        /// <summary>
        /// 团队标签
        /// </summary>
        public string WebsiteTags { get; set; }
        /// <summary>
        /// 成团人数
        /// </summary>
        public int PlanNum { get; set; }
        /// <summary>
        /// 单房差
        /// </summary>
        public decimal SingleRoom { get; set; }
        /// <summary>
        /// 境外参团价
        /// </summary>
        public decimal OverseasJoinPrice { get; set; }
        public string RetainCount { get; set; }
        /// <summary>
        /// 余位
        /// </summary>
        public int FreeNum { get; set; }
        /// <summary>
        /// 下架日期
        /// </summary>
        public DateTime DateOffline { get; set; }
        public string ConcurrencyStamp { get; set; }

        #region 暂不解析
        /*
        public string SupplierData { get; set; }
        public string MemberCostRate { get; set; }
        public string MemberAwardRate { get; set; }
        public string MemberDiscountRate { get; set; }
        public string MemberCostId { get; set; }
        public string MemberAwardId { get; set; }
        public string UCoin { get; set; }
        public string TransportTags { get; set; }
        public string ZtBuMen { get; set; }
        public string ZtRen { get; set; }
        public string PayCustNum { get; set; }
        public string UnpayCustNum { get; set; }
        public string PolicyTitle { get; set; }
        public string PolicyAmuont { get; set; }
        public string PolicyCondition { get; set; }
        public string PolicyStarDate { get; set; }
        public string PolicyEndDate { get; set; }
        public string SpecialPrice { get; set; }
        public string SpecialStarDate { get; set; }
        public string SpecailEndDate { get; set; }
        */
        #endregion
    }
}
