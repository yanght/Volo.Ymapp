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
        public string TeamId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Function { get; set; }
        public string Continent { get; set; }
        public string PlaceLeave { get; set; }
        public string PlaceReturn { get; set; }
        public string DateStart { get; set; }
        public string DateFinish { get; set; }
        public int DayNum { get; set; }
        public string AirCompany { get; set; }
        public string AirShortName { get; set; }
        public decimal CustomerPrice { get; set; }
        public decimal AgentPrice { get; set; }
        public decimal ChildPrice { get; set; }
        public decimal SingleRoom { get; set; }
        public decimal OverseasJoinPrice { get; set; }
        public decimal Deposit { get; set; }
        public decimal PlanNum { get; set; }
        public decimal FreeNum { get; set; }
        public string WebsiteTags { get; set; }
        public string DateOffline { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string PostersImg { get; set; }
        public string PostersData { get; set; }
    }
}
