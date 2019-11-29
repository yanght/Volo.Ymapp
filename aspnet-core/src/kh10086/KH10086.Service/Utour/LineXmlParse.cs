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
    public class LineXmlParse
    {
        /// <summary>
        /// 解析线路列表
        /// </summary>
        /// <returns></returns>
        public static LineModel ParseLineList()
        {
            string url = "https://tispfile.utourworld.com/upload/op/xml/agentLine/index.xml";
            //string response = HttpClientHelper.HttpRequest(url);
            string response = HttpClientHelper.HttpRequest(url);
            var doc = new XmlDocument();
            doc.LoadXml(response);
            var lineList = doc.SelectNodes("routes/line");
            LineModel routes = new LineModel() { line = GetLineItemList(lineList) };
            return routes;
        }
        /// <summary>
        /// 获取线路集合
        /// </summary>
        /// <param name="nodelList"></param>
        /// <returns></returns>
        private static List<LineItem> GetLineItemList(XmlNodeList nodelList)
        {
            List<LineItem> lineList = new List<LineItem>();
            foreach (XmlNode line in nodelList)
            {
                LineItem lineItem = new LineItem()
                {
                    city = line.Attributes["lineCode"].Value,
                    continent = line.Attributes["continent"].Value,
                    country = line.Attributes["country"].Value,
                    firstLineImg = line.Attributes["firstLineImg"].Value,
                    lineCode = line.Attributes["lineCode"].Value,
                    lineName = line.Attributes["lineName"].Value,
                    numDay = line.Attributes["numDay"].Value,
                    numNight = line.Attributes["numNight"].Value,
                    team = new Team()
                    {
                        teamData = GetTeamDataList(line.SelectNodes("team/teamData"))
                    }
                };
                lineList.Add(lineItem);
            };

            return lineList;
        }
        /// <summary>
        /// 团队数据
        /// </summary>
        /// <param name="nodelList"></param>
        /// <returns></returns>
        private static List<TeamData> GetTeamDataList(XmlNodeList nodelList)
        {
            List<TeamData> teamDataList = new List<TeamData>();
            foreach (XmlNode node in nodelList)
            {
                TeamData teamData = new TeamData()
                {
                    airCompany = node.Attributes["airCompany"].Value,
                    agentPrice = node.Attributes["agentPrice"].Value,
                    airShortName = node.Attributes["airShortName"].Value,
                    childPrice = node.Attributes["childPrice"].Value,
                    continent = node.Attributes["continent"].Value,
                    customerPrice = node.Attributes["customerPrice"].Value,
                    dateFinish = node.Attributes["dateFinish"].Value,
                    dateOffline = node.Attributes["dateOffline"].Value,
                    dateStart = node.Attributes["dateStart"].Value,
                    dayNum = node.Attributes["dayNum"].Value,
                    deposit = node.Attributes["deposit"].Value,
                    deptCode = node.Attributes["deptCode"].Value,
                    deptName = node.Attributes["deptName"].Value,
                    freeNum = node.Attributes["freeNum"].Value,
                    function = node.Attributes["function"].Value,
                    overseasJoinPrice = node.Attributes["overseasJoinPrice"].Value,
                    placeLeave = node.Attributes["placeLeave"].Value,
                    placeReturn = node.Attributes["placeReturn"].Value,
                    planNum = node.Attributes["planNum"].Value,
                    postersData = GetPostersData(node.SelectSingleNode("postersData")),
                    productCode = node.Attributes["productCode"].Value,
                    productName = node.Attributes["productName"].Value,
                    singleRoom = node.Attributes["singleRoom"].Value,
                    teamId = node.Attributes["teamId"].Value,
                };
                teamDataList.Add(teamData);
            }
            return teamDataList;
        }
        private static PostersData GetPostersData(XmlNode node)
        {
            return new PostersData()
            {
                postersImg = node.Attributes["postersImg"].Value,
                text = node.InnerText
            };
        }
    }
}
