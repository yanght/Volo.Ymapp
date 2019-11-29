using KH10086.Service.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KH10086.Service.Utour
{
    public class LineDetailXmlParse
    {
        public static LineDetailModel ParseLineDetail(string lineCode)
        {
            string url = string.Format("https://tispfile.utourworld.com/upload/op/xml/agentLine/{0}.xml", lineCode);
            string response = HttpClientHelper.HttpRequest(url);
            //HttpClientHelper.HttpRequest(url);
            var doc = new XmlDocument();
            doc.LoadXml(response);
            var node = doc.SelectSingleNode("routes/item");
            return GetLineDetail(node);
        }
        private static LineDetailModel GetLineDetail(XmlNode node)
        {
            LineDetailModel model = new LineDetailModel()
            {
                Continent = node.Attributes["Continent"].Value,
                Country = node.Attributes["Country"].Value,
                customTitle = node.Attributes["customTitle"].Value,
                function = node.Attributes["function"].Value,
                imgCity = node.Attributes["imgCity"].Value,
                imgCode = node.Attributes["imgCode"].Value,
                imgContinent = node.Attributes["imgContinent"].Value,
                imgCountry = node.Attributes["imgCountry"].Value,
                lineCode = node.Attributes["lineCode"].Value,
                lineDays = GetItineraryDays(node.SelectNodes("lineDays/itineraryDays")),
                lineIntros = GetLineIntro(node.SelectNodes("lineIntros/lineIntro")),
                lineType = node.Attributes["lineType"].Value,
                NumDay = node.Attributes["NumDay"].Value,
                NumNight = node.Attributes["NumNight"].Value,
                placeLeave = node.Attributes["placeLeave"].Value,
                placeReturn = node.Attributes["placeReturn"].Value,
                routeDates = GetRouteDate(node.SelectNodes("routeDates/routeDate")),
                Sight = node.Attributes["Sight"].Value,
                title = node.Attributes["title"].Value,
                txtTransitCity = node.Attributes["txtTransitCity"].Value,
                Visa = node.Attributes["Visa"].Value,
            };
            return model;
        }
        private static List<RouteDate> GetRouteDate(XmlNodeList nodeList)
        {
            List<RouteDate> list = new List<RouteDate>();
            foreach (XmlNode node in nodeList)
            {
                var routeDate = new RouteDate()
                {
                    agentPrice = node.Attributes["agentPrice"].Value,
                    childPrice = node.Attributes["childPrice"].Value,
                    dateFinish = node.Attributes["dateFinish"].Value,
                    dateOffline = node.Attributes["dateOffline"].Value,
                    datestart = node.Attributes["datestart"].Value,
                    deposit = node.Attributes["deposit"].Value,
                    freeNum = node.Attributes["freeNum"].Value,
                    JieShouRiQi = node.Attributes["JieShouRiQi"].Value,
                    memberAwardId = node.Attributes["memberAwardId"].Value,
                    memberAwardRate = node.Attributes["memberAwardRate"].Value,
                    memberCostId = node.Attributes["memberCostId"].Value,
                    memberCostRate = node.Attributes["memberCostRate"].Value,
                    memberDiscountRate = node.Attributes["memberDiscountRate"].Value,
                    overseasJoinPrice = node.Attributes["overseasJoinPrice"].Value,
                    payCustNum = node.Attributes["payCustNum"].Value,
                    planNum = node.Attributes["planNum"].Value,
                    policyAmuont = node.Attributes["policyAmuont"].Value,
                    policyCondition = node.Attributes["policyCondition"].Value,
                    policyEndDate = node.Attributes["policyEndDate"].Value,
                    policyStarDate = node.Attributes["policyStarDate"].Value,
                    policyTitle = node.Attributes["policyTitle"].Value,
                    productCode = node.Attributes["productCode"].Value,
                    retainCount = node.Attributes["retainCount"].Value,
                    singleRoom = node.Attributes["singleRoom"].Value,
                    specailEndDate = node.Attributes["specailEndDate"].Value,
                    specialPrice = node.Attributes["specialPrice"].Value,
                    specialStarDate = node.Attributes["specialStarDate"].Value,
                    supplierData = node.Attributes["supplierData"].Value,
                    teamId = node.Attributes["teamId"].Value,
                    transportTags = node.Attributes["transportTags"].Value,
                    UCoin = node.Attributes["UCoin"].Value,
                    unpayCustNum = node.Attributes["unpayCustNum"].Value,
                    websiteTags = node.Attributes["websiteTags"].Value,
                    ztBuMen = node.Attributes["ztBuMen"].Value,
                    ztRen = node.Attributes["ztRen"].Value,
                };
                list.Add(routeDate);
            }
            return list;
        }
        private static List<ItineraryDays> GetItineraryDays(XmlNodeList nodeList)
        {
            List<ItineraryDays> list = new List<ItineraryDays>();
            foreach (XmlNode node in nodeList)
            {
                var nodel = new ItineraryDays()
                {
                    Breakfast = node.Attributes["Breakfast"].Value,
                    countrynameSelf = new CountrynameSelf(),
                    countrynameShop = new CountrynameShop(),
                    dayHotel = node.Attributes["dayHotel"].Value,
                    dayNumber = node.Attributes["dayNumber"].Value,
                    dayTraffic = node.Attributes["dayTraffic"].Value,
                    Dinner = node.Attributes["Dinner"].Value,
                    img = GetImgUrl(node.SelectNodes("img/imgUrl")),
                    itineraryDay = GetItineraryDay(node.SelectSingleNode("itineraryDay")),
                    Lunch = node.Attributes["Lunch"].Value,
                    traffics = GetTraffics(node.SelectNodes("traffics/traffic")),
                };
                list.Add(nodel);
            }
            return list;
        }
        private static List<Traffic> GetTraffics(XmlNodeList nodelist)
        {
            List<Traffic> list = new List<Traffic>();
            foreach (XmlNode node in nodelist)
            {
                list.Add(new Traffic()
                {
                    TrafficCo = node.Attributes["TrafficCo"].Value,
                    TrafficNo = node.Attributes["TrafficNo"].Value,
                    TrafficTimeEnd = node.Attributes["TrafficTimeEnd"].Value,
                    TrafficTimeStart = node.Attributes["TrafficTimeStart"].Value,
                });
            }
            return list;
        }
        private static List<ImgUrl> GetImgUrl(XmlNodeList nodeist)
        {
            List<ImgUrl> list = new List<ImgUrl>();
            foreach (XmlNode node in nodeist)
            {
                list.Add(new ImgUrl()
                {
                    City = node.Attributes["City"].Value,
                    Continent = node.Attributes["Continent"].Value,
                    Country = node.Attributes["Country"].Value,
                    imgCode = node.Attributes["imgCode"].Value,
                    imgPath = node.Attributes["imgPath"].Value,
                    Sight = node.Attributes["Sight"].Value,
                    sightIntroduce = new SightIntroduce()
                    {
                        text = node.InnerText
                    },
                });
            }
            return list;
        }
        private static List<LineIntro> GetLineIntro(XmlNodeList nodelist)
        {
            List<LineIntro> list = new List<LineIntro>();
            foreach (XmlNode node in nodelist)
            {
                list.Add(new LineIntro()
                {
                    channelType = node.Attributes["channelType"].Value,
                    orderNum = node.Attributes["orderNum"].Value,
                    text = node.InnerText,
                    title = node.Attributes["title"].Value,
                });
            }
            return list;
        }
        private static ItineraryDay GetItineraryDay(XmlNode node)
        {
            return new ItineraryDay()
            {
                cityEnglish = node.Attributes["cityEnglish"].Value,
                scitydistance = node.Attributes["scitydistance"].Value,
                trafficName = node.Attributes["trafficName"].Value,
                text = node.SelectSingleNode("sightIntro").InnerText,
            };
        }
    }
}
