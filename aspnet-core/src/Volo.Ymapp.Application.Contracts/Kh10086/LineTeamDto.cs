﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Kh10086
{
    public class LineTeamDto : AuditedEntityDto<long>, IHasConcurrencyStamp
    {
        public long LineId { get; set; }
        public string TeamId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Function { get; set; }
        public string Continent { get; set; }
        public string PlaceLeave { get; set; }
        public string PlaceReturn { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public int DayNum { get; set; }
        public string AirCompany { get; set; }
        public string AirShortName { get; set; }
        public decimal CustomerPrice { get; set; }
        public decimal AgentPrice { get; set; }
        public decimal ChildPrice { get; set; }
        public decimal SingleRoom { get; set; }
        public decimal OverseasJoinPrice { get; set; }
        public decimal Deposit { get; set; }
        public int PlanNum { get; set; }
        public int FreeNum { get; set; }
        public string WebsiteTags { get; set; }
        /// <summary>
        /// 终端售卖开始日期
        /// </summary>
        public DateTime DateOnline { get; set; }
        public DateTime DateOffline { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string PostersImg { get; set; }
        public string PostersData { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
