using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Volo.Ymapp.Kh10086;
using Volo.Ymapp.TaskScheduler.Common;

namespace Volo.Ymapp.TaskScheduler.Utour
{
    public class LineXmlParse
    {
        /// <summary>
        /// 解析线路列表
        /// </summary>
        /// <returns></returns>
        public static XmlDocument ParseLineList(string url)
        {
            string response = HttpClientHelper.HttpRequest(url, encoding: Encoding.GetEncoding("GBK"));
            var doc = new XmlDocument();
            doc.LoadXml(response);
            return doc;
        }

        public static List<LineDto> GetLine(string url, XmlNodeList nodeList)
        {
            List<LineDto> list = new List<LineDto>();
            if (nodeList == null || nodeList.Count == 0) return list;
            int index = 0;
            foreach (XmlNode node in nodeList)
            {
                index++;
                string lineCode = node.Attributes["lineCode"].Value;
                url = string.Format(url, lineCode);
                var lineDetail = ParseLineDetail(url, lineCode);
                lineDetail.LineTeams = GetLineTeams(node.SelectNodes("team/teamData"));
                lineDetail.FirstLineImg = node.Attributes["firstLineImg"].Value;
                lineDetail.LineDays = GetLineDays(node.SelectNodes("lineDays/itineraryDays"));
                list.Add(lineDetail);
                Log.Information($"第{index}条线路解析完毕");
            }
            return list;
        }

        public static List<LineTeamDto> GetLineTeams(XmlNodeList nodeList)
        {
            List<LineTeamDto> list = new List<LineTeamDto>();
            if (nodeList == null || nodeList.Count == 0) return list;
            foreach (XmlNode node in nodeList)
            {
                list.Add(new LineTeamDto()
                {
                    AgentPrice = decimal.Parse(node.Attributes["agentPrice"].Value),
                    AirCompany = node.Attributes["airCompany"].Value,
                    AirShortName = node.Attributes["airShortName"].Value,
                    ChildPrice = decimal.Parse(node.Attributes["childPrice"].Value),
                    Continent = node.Attributes["continent"].Value,
                    CustomerPrice = decimal.Parse(node.Attributes["customerPrice"].Value),
                    DateFinish = DateTime.Parse(node.Attributes["dateFinish"].Value),
                    DateOffline = DateTime.Parse(node.Attributes["dateOffline"].Value),
                    DateStart = DateTime.Parse(node.Attributes["dateStart"].Value),
                    DayNum = int.Parse(node.Attributes["dayNum"].Value),
                    Deposit = decimal.Parse(node.Attributes["deposit"].Value),
                    DeptCode = node.Attributes["deptCode"].Value,
                    DeptName = node.Attributes["deptName"].Value,
                    FreeNum = int.Parse(node.Attributes["freeNum"].Value),
                    Function = node.Attributes["function"].Value,
                    OverseasJoinPrice = decimal.Parse(node.Attributes["overseasJoinPrice"].Value),
                    PlaceLeave = node.Attributes["placeLeave"].Value,
                    PlaceReturn = node.Attributes["placeReturn"].Value,
                    PlanNum = int.Parse(node.Attributes["planNum"].Value),
                    ProductCode = node.Attributes["productCode"].Value,
                    ProductName = node.Attributes["productName"].Value,
                    SingleRoom = decimal.Parse(node.Attributes["singleRoom"].Value),
                    TeamId = node.Attributes["teamId"].Value,
                    WebsiteTags = node.Attributes["websiteTags"].Value,
                    PostersData = node.SelectSingleNode("/postersData ").InnerText,
                    PostersImg = node.SelectSingleNode("/postersData ").Attributes["postersImg"].Value,
                });
            }
            return list;
        }

        public static LineDto ParseLineDetail(string url, string lineCode)
        {
            string response = HttpClientHelper.HttpRequest(url, encoding: Encoding.GetEncoding("GBK"));
            var doc = new XmlDocument();
            doc.LoadXml(response);
            var node = doc.SelectSingleNode("routes/item");
            var line = GetLine(node);
            line.LineIntros = GetLineIntros(node.SelectNodes("lineIntros/lineIntro"));
            line.LineDays = GetLineDays(node.SelectNodes("lineDays/itineraryDays"));
            return line;
        }

        public static LineDto GetLine(XmlNode node)
        {
            if (node == null) return null;
            LineDto line = new LineDto()
            {
                LineCode = node.Attributes["lineCode"].Value,
                Title = node.Attributes["title"].Value,
                CustomTitle = "",//node.Attributes["customerTitle"].Value,
                Continent = node.Attributes["Continent"].Value,
                Country = node.Attributes["Country"].Value,
                TxtTransitCity = node.Attributes["txtTransitCity"].Value,
                Sight = node.Attributes["Sight"].Value,
                NumDay = int.Parse(node.Attributes["NumDay"].Value),
                NumNight = int.Parse(node.Attributes["NumNight"].Value),
                Visa = "",//node.Attributes["visa"].Value,
                ImgContinent = node.Attributes["imgContinent"].Value,
                ImgCountry = node.Attributes["imgCountry"].Value,
                ImgCity = node.Attributes["imgCity"].Value,
                PlaceLeave = node.Attributes["placeLeave"].Value,
                PlaceReturn = node.Attributes["placeReturn"].Value,
                Function = node.Attributes["function"].Value,
                LineType = node.Attributes["lineType"].Value,
                ImgCode = node.Attributes["imgCode"].Value,
            };
            return line;
        }

        public static List<LineIntroDto> GetLineIntros(XmlNodeList nodeList)
        {
            List<LineIntroDto> list = new List<LineIntroDto>();
            if (nodeList == null || nodeList.Count == 0) return list;
            foreach (XmlNode node in nodeList)
            {
                list.Add(new LineIntroDto()
                {
                    ChannelType = node.Attributes["channelType"].Value,
                    Describe = node.InnerText,
                    Title = node.Attributes["title"].Value,
                    OrderNum = int.Parse(node.Attributes["orderNum"].Value)
                });
            }
            return list;
        }

        public static List<LineDayDto> GetLineDays(XmlNodeList nodeList)
        {
            List<LineDayDto> list = new List<LineDayDto>();
            if (nodeList == null || nodeList.Count == 0) return list;
            foreach (XmlNode node in nodeList)
            {
                var dayNode = node.SelectSingleNode("itineraryDay");
                list.Add(new LineDayDto
                {
                    Breakfast = node.Attributes["Breakfast"].Value,
                    DayNumber = int.Parse(node.Attributes["dayNumber"].Value),
                    DayHotel = node.Attributes["dayHotel"].Value,
                    Lunch = node.Attributes["Lunch"].Value,
                    Dinner = node.Attributes["Dinner"].Value,
                    DayTraffic = node.Attributes["dayTraffic"].Value,
                    CityEnglish = dayNode.Attributes["cityEnglish"].Value,
                    Describe = dayNode.SelectSingleNode("sightIntro").InnerText,
                    TrafficName = dayNode.Attributes["trafficName"].Value,
                    ScityDistance = dayNode.Attributes["scitydistance"].Value,
                    LineDayImages = GetLineDayImsges(node.SelectNodes("img/imgUrl")),
                    LineDayTraffics = GetLineTraffics(node.SelectNodes("traffics/traffic ")),
                });
            }
            return list;
        }

        public static List<LineDayTrafficDto> GetLineTraffics(XmlNodeList nodeList)
        {
            List<LineDayTrafficDto> list = new List<LineDayTrafficDto>();
            if (nodeList == null || nodeList.Count == 0) return list;
            foreach (XmlNode node in nodeList)
            {
                list.Add(new LineDayTrafficDto()
                {
                    TrafficCo = node.Attributes["TrafficCo"].Value,
                    TrafficTimeEnd = node.Attributes["TrafficTimeEnd"].Value,
                    TrafficTimeStart = node.Attributes["TrafficTimeStart"].Value,
                    TrafficNo = node.Attributes["TrafficNo"].Value,
                });
            }
            return list;
        }

        public static List<LineDayImageDto> GetLineDayImsges(XmlNodeList nodeList)
        {
            List<LineDayImageDto> list = new List<LineDayImageDto>();
            if (nodeList == null || nodeList.Count == 0) return list;
            foreach (XmlNode node in nodeList)
            {
                list.Add(new LineDayImageDto()
                {
                    City = node.Attributes["City"].Value,
                    Continent = node.Attributes["Continent"].Value,
                    Country = node.Attributes["Country"].Value,
                    ImgCode = node.Attributes["imgCode"].Value,
                    ImgPath = node.Attributes["imgPath"].Value,
                    Sight = node.SelectSingleNode("sightIntroduce").InnerText
                });
            }
            return list;
        }
    }
}
