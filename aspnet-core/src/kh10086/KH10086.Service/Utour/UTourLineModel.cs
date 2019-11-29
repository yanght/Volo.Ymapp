using System;
using System.Collections.Generic;

namespace KH10086.Service.Utour
{
    public class CDataBaseModel
    {
        public string text { get; set; }
    }
    public class PostersData : CDataBaseModel
    {
        public string postersImg { get; set; }
    }
    public class TeamData
    {
        /// <summary>
        /// 
        /// </summary>
        public string teamId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 流光岁月璀璨俄罗斯（臻美双城+金银环小镇）6晚8日
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 大众常规
        /// </summary>
        public string function { get; set; }
        /// <summary>
        /// 亚洲
        /// </summary>
        public string continent { get; set; }
        /// <summary>
        /// 上海
        /// </summary>
        public string placeLeave { get; set; }
        /// <summary>
        /// 上海
        /// </summary>
        public string placeReturn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dateStart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dateFinish { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dayNum { get; set; }
        /// <summary>
        /// 中国国际航空公司
        /// </summary>
        public string airCompany { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string airShortName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string customerPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string agentPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string childPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string singleRoom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string overseasJoinPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deposit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string planNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string freeNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dateOffline { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deptCode { get; set; }
        /// <summary>
        /// 上海欧洲电商组
        /// </summary>
        public string deptName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PostersData postersData { get; set; }
    }
    public class Team
    {
        /// <summary>
        /// 
        /// </summary>
        public List<TeamData> teamData { get; set; }
    }
    public class LineItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string lineCode { get; set; }
        /// <summary>
        /// 流光岁月璀璨俄罗斯（臻美双城+金银环小镇）
        /// </summary>
        public string lineName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string numDay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string numNight { get; set; }
        /// <summary>
        /// 欧洲
        /// </summary>
        public string continent { get; set; }
        /// <summary>
        /// 韩国,俄罗斯
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 莫斯科,上海,首尔,谢尔盖耶夫镇,圣彼得堡,诺夫哥罗德
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string firstLineImg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Team team { get; set; }
    }
    public class LineModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<LineItem> line { get; set; }
    }

    /// <summary>
    /// 线路详情
    /// </summary>
    public class LineDetailModel
    {
        public string lineCode { get; set; }
        public string title { get; set; }
        public string customTitle { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public string txtTransitCity { get; set; }
        public string Sight { get; set; }
        public string lineType { get; set; }
        public string NumNight { get; set; }
        public string NumDay { get; set; }
        public string Visa { get; set; }
        public string imgCode { get; set; }
        public string imgContinent { get; set; }
        public string imgCountry { get; set; }
        public string imgCity { get; set; }
        public string placeLeave { get; set; }
        public string placeReturn { get; set; }
        public string function { get; set; }
        public List<LineIntro> lineIntros { get; set; }
        public List<ItineraryDays> lineDays { get; set; }
        public List<RouteDate> routeDates { get; set; }
    }
    public class LineIntro : CDataBaseModel
    {
        public string title { get; set; }
        public string channelType { get; set; }
        public string orderNum { get; set; }
    }
    public class RouteDate
    {
        public string teamId { get; set; }
        public string productCode { get; set; }
        public string datestart { get; set; }
        public string dateFinish { get; set; }
        public string agentPrice { get; set; }
        public string JieShouRiQi { get; set; }
        public string childPrice { get; set; }
        public string deposit { get; set; }
        public string websiteTags { get; set; }
        public string planNum { get; set; }
        public string singleRoom { get; set; }
        public string overseasJoinPrice { get; set; }
        public string retainCount { get; set; }
        public string freeNum { get; set; }
        public string dateOffline { get; set; }
        public string supplierData { get; set; }
        public string memberCostRate { get; set; }
        public string memberAwardRate { get; set; }
        public string memberDiscountRate { get; set; }
        public string memberCostId { get; set; }
        public string memberAwardId { get; set; }
        public string UCoin { get; set; }
        public string transportTags { get; set; }
        public string ztBuMen { get; set; }
        public string ztRen { get; set; }
        public string payCustNum { get; set; }
        public string unpayCustNum { get; set; }
        public string policyTitle { get; set; }
        public string policyAmuont { get; set; }
        public string policyCondition { get; set; }
        public string policyStarDate { get; set; }
        public string policyEndDate { get; set; }
        public string specialPrice { get; set; }
        public string specialStarDate { get; set; }
        public string specailEndDate { get; set; }
    }
    public class ItineraryDays
    {
        public string dayNumber { get; set; }
        public string dayHotel { get; set; }
        public string Breakfast { get; set; }
        public string Lunch { get; set; }
        public string Dinner { get; set; }
        public string dayTraffic { get; set; }
        public ItineraryDay itineraryDay { get; set; }
        public List<Traffic> traffics { get; set; }
        public List<ImgUrl> img { get; set; }
        public CountrynameSelf countrynameSelf { get; set; }
        public CountrynameShop countrynameShop { get; set; }
    }
    public class ItineraryDay: CDataBaseModel
    {
        public string cityEnglish { get; set; }
        public string scitydistance { get; set; }
        public string trafficName { get; set; }
    }
    public class SightIntro : CDataBaseModel
    {

    }
    public class Traffic
    {
        public string TrafficCo { get; set; }
        public string TrafficNo { get; set; }
        public string TrafficTimeEnd { get; set; }
        public string TrafficTimeStart { get; set; }
    }
    public class SightIntroduce : CDataBaseModel
    {

    }
    public class ImgUrl
    {
        public string imgCode { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Sight { get; set; }
        public string imgPath { get; set; }
        public SightIntroduce sightIntroduce { get; set; }
    }
    public class CountrynameSelf
    {

    }
    public class CountrynameShop
    {

    }
}
