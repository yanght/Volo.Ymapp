using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Volo.Ymapp.kh10086;
using Volo.Ymapp.TaskScheduler.Common;

namespace Volo.Ymapp.TaskScheduler.Utour
{
    public class LineXmlParse
    {
        /// <summary>
        /// 解析线路列表
        /// </summary>
        /// <returns></returns>
        public static List<Line> ParseLineList()
        {
            List<Line> list = new List<Line>();
            string url = "https://tispfile.utourworld.com/upload/op/xml/agentLine/index.xml";
            //string response = HttpClientHelper.HttpRequest(url);
            string response = HttpClientHelper.HttpRequest(url, encoding: Encoding.GetEncoding("GBK"));
            var doc = new XmlDocument();
            doc.LoadXml(response);

            Dictionary<string, string> lineDic = new Dictionary<string, string>();

            var lineList = doc.SelectNodes("routes/line");

            foreach (XmlNode node in lineList)
            {
                string lineCode = node.Attributes["lineCode"].Value;
                XmlNode teamNode = node.SelectSingleNode("team/teamData");
                string teamId = teamNode.Attributes["teamId"].Value;
                list.Add(new Line()
                {
                    LineCode = lineCode,
                    FirstLineImg = node.Attributes["firstLineImg"].Value,
                    CustomTitle= node.Attributes["CustomTitle"].Value,
                });
            }

            return lineDic;
        }

        public static Line ParseLineDetail(string lineCode, string teamId)
        {
            string url = string.Format("https://tispfile.utourworld.com/upload/op/xml/agentLine/{0}.xml", lineCode);
            string response = HttpClientHelper.HttpRequest(url, encoding: Encoding.GetEncoding("GBK"));
            var doc = new XmlDocument();
            doc.LoadXml(response);
            var node = doc.SelectSingleNode("routes/item");
            Line line = new Line()
            {
                Continent = node.Attributes["continent"].Value,
                Country = node.Attributes["country"].Value,
                CustomTitle = "",
                FirstLineImg = "",
                Function = node.Attributes["function"].Value,
                ImgCity = node.Attributes["imgCity"].Value,
                ImgCode = node.Attributes["imgCity"].Value,
                ImgContinent = node.Attributes["imgContinent"].Value,
                ImgCountry = node.Attributes["imgCountry"].Value,
                LineCode = node.Attributes["lineCode"].Value,
                LineType = node.Attributes["lineType"].Value,
                NumDay = int.Parse(node.Attributes["NumDay"].Value),
                NumNight = int.Parse(node.Attributes["NumNight"].Value),
                PlaceLeave = node.Attributes["placeLeave"].Value,
                PlaceReturn = node.Attributes["placeReturn"].Value,
                Sight = node.Attributes["sight"].Value,
                Title = node.Attributes["title"].Value,
                TxtTransitCity = "",
                Visa = node.Attributes["visa"].Value,
            };
            return line;
        }

        public static List<LineIntro> GetLineIntros(XmlNodeList nodeList)
        {
            List<LineIntro> list = new List<LineIntro>();
            if (nodeList == null || nodeList.Count == 0) return list;
            foreach (XmlNode node in nodeList)
            {
                list.Add(new LineIntro()
                {
                    ChannelType = node.Attributes["channelType"].Value,
                    Describe = node.InnerText,
                    Title = node.Attributes["title"].Value,
                    OrderNum = int.Parse(node.Attributes["orderNum"].Value)
                });
            }
            return list;
        }

        public static List<LineDay> GetLineDays(XmlNodeList nodeList)
        {
            List<LineDay> list = new List<LineDay>();
            if (nodeList == null || nodeList.Count == 0) return list;
            foreach (XmlNode node in nodeList)
            {

                var dayNode = node.SelectSingleNode("/itineraryDay");
                list.Add(new LineDay
                {
                    Breakfast = node.Attributes["Breakfast"].Value,
                    DayNumber = int.Parse(node.Attributes["dayNumber"].Value),
                    DayHotel = node.Attributes["dayHotel"].Value,
                    Lunch = node.Attributes["Lunch"].Value,
                    Dinner = node.Attributes["Dinner"].Value,
                    DayTraffic = node.Attributes["dayTraffic"].Value,
                    CityEnglish = dayNode.Attributes["cityEnglish"].Value,
                    Describe = dayNode.SelectSingleNode("/sightIntro").InnerText,
                    TrafficName = dayNode.Attributes["trafficName"].Value,
                    ScityDistance = dayNode.Attributes["scitydistance"].Value,
                });
            }
            return list;
        }

        public static List<LineDayTraffic> GetLineTraffics(XmlNodeList nodeList)
        {
            List<LineDayTraffic> list = new List<LineDayTraffic>();
            if (nodeList == null || nodeList.Count == 0) return list;
            foreach (XmlNode node in nodeList)
            {
                list.Add(new LineDayTraffic()
                {
                    TrafficCo = node.Attributes["TrafficCo"].Value,
                    TrafficTimeEnd = node.Attributes["TrafficTimeEnd"].Value,
                    TrafficTimeStart = node.Attributes["TrafficTimeStart"].Value,
                    TrafficNo = node.Attributes["TrafficNo"].Value,
                });
            }
            return list;
        }

        public static List<LineDayImage> GetLineDayImsges(XmlNodeList nodeList)
        {
            List<LineDayImage> list = new List<LineDayImage>();
            if (nodeList == null || nodeList.Count == 0) return list;
            foreach (XmlNode node in nodeList)
            {
                list.Add(new LineDayImage()
                {
                    City = node.Attributes["City"].Value,
                    Continent = node.Attributes["Continent"].Value,
                    Country = node.Attributes["Country"].Value,
                    ImgCode = node.Attributes["imgCode"].Value,
                    ImgPath = node.Attributes["imgPath"].Value,
                    Sight = node.SelectSingleNode("/sightIntroduce").InnerText
                });
            }
            return list;
        }
    }
}
