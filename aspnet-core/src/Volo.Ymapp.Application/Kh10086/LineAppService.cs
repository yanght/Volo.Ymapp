﻿using AutoMapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Threading;
using Volo.Ymapp.JobTask;
using Volo.Ymapp.Utils;
using System.Linq;

namespace Volo.Ymapp.Kh10086
{
    public class LineAppService : ApplicationService, ILineAppService
    {
        //public ILogger<MyService> Logger { get; set; }

        private readonly IRepository<Line, long> _lineRepository;
        private readonly IRepository<LineDay, long> _lineDayRepository;
        private readonly IRepository<LineDayImage, long> _lineDayImageRepository;
        private readonly IRepository<LineDaySelf, long> _lineDaySelfRepository;
        private readonly IRepository<LineDayShop, long> _lineDayShopRepository;
        private readonly IRepository<LineDayTraffic, long> _lineDayTrafficRepository;
        private readonly IRepository<LineIntro, long> _lineIntroRepository;
        private readonly IRepository<LineRouteDate, long> _lineRouteDateRepository;
        private readonly IRepository<LineTeam, long> _lineTeamRepository;
        public LineAppService(IRepository<Line, long> lineRepository
            , IRepository<LineDay, long> lineDayRepository
            , IRepository<LineDayImage, long> lineDayImageRepository
            , IRepository<LineDaySelf, long> lineDaySelfRepository
            , IRepository<LineDayShop, long> lineDayShopRepository
            , IRepository<LineDayTraffic, long> lineDayTrafficRepository
            , IRepository<LineIntro, long> lineIntroRepository
            , IRepository<LineRouteDate, long> lineRouteDateRepository
            , IRepository<LineTeam, long> lineTeamRepository)
        {
            _lineRepository = lineRepository;
            _lineDayRepository = lineDayRepository;
            _lineDayImageRepository = lineDayImageRepository;
            _lineDaySelfRepository = lineDaySelfRepository;
            _lineDayShopRepository = lineDayShopRepository;
            _lineDayTrafficRepository = lineDayTrafficRepository;
            _lineIntroRepository = lineIntroRepository;
            _lineRouteDateRepository = lineRouteDateRepository;
            _lineTeamRepository = lineTeamRepository;
        }
        public async Task ParseLineData(ParseLineDataDto dto)
        {
            Log.Information($"开始获取解析线路数据。。。");
            string lineListurl = dto.LineListUrl;// "https://tispfile.utourworld.com/upload/op/xml/agentLine/index.xml";
            string lineDetailUrl = dto.LineDetailUrl;//"https://tispfile.utourworld.com/upload/op/xml/agentLine/{0}.xml";
            string response = HttpClientHelper.HttpRequest(lineListurl, encoding: Encoding.GetEncoding("GBK"));
            var doc = new XmlDocument();
            doc.LoadXml(response);
            XmlNodeList nodeList = doc.SelectNodes("routes/line");
            Log.Information($"共获取到{nodeList.Count}条线路");
            var lineList = await GetLineList(lineDetailUrl, nodeList);
            Log.Information($"结束线路数据解析");

            //if (lineList != null && lineList.Count > 0)
            //{
            //    int index = 1;
            //    lineList.ForEach((line) =>
            //   {
            //       AsyncHelper.RunSync(async () =>
            //       {
            //           try
            //           {
            //               Log.Information($"开始入库第{index}条数据");
            //               await InsertLine(line);
            //               Log.Information($"结束入库第{index}条数据");
            //           }
            //           catch (Exception ex)
            //           {
            //               Log.Error($"第{index}条数据入库失败,{ex.ToString()}");
            //           }
            //           index++;
            //       });
            //   });
            //}
        }

        /// <summary>
        /// 解析线路列表
        /// </summary>
        /// <returns></returns>
        private static XmlDocument ParseLineList(string url)
        {
            string response = HttpClientHelper.HttpRequest(url, encoding: Encoding.GetEncoding("GBK"));
            var doc = new XmlDocument();
            doc.LoadXml(response);
            return doc;
        }

        private async Task<List<LineDto>> GetLineList(string url, XmlNodeList nodeList)
        {
            List<LineDto> list = new List<LineDto>();
            if (nodeList == null || nodeList.Count == 0) return list;
            int index = 1;
            foreach (XmlNode node in nodeList)
            {
                if (index > 1) break;
                string lineCode = node.Attributes["lineCode"].Value;
                try
                {
                    Log.Information($"开始解析第{index}条数据,【{lineCode}】");
                    var lineDetail = ParseLineDetail(string.Format(url, lineCode), lineCode);
                    lineDetail.LineTeams = GetLineTeams(node.SelectNodes("team/teamData"));
                    lineDetail.FirstLineImg = node.Attributes["firstLineImg"].Value;
                    list.Add(lineDetail);
                    Log.Information($"结束解析第{index}条数据,【{lineCode}】");
                    AsyncHelper.RunSync(async () =>
                    {
                        try
                        {
                            Log.Information($"开始入库第{index}条数据");
                            await AddOrUpdateLine(lineDetail);
                            Log.Information($"结束入库第{index}条数据");
                        }
                        catch (Exception ex)
                        {
                            Log.Error($"第{index}条数据入库失败,{ex.ToString()}");
                        }
                        index++;
                    });
                }
                catch (Exception ex)
                {
                    Log.Error($"第{index}条数据解析失败,【{lineCode}】,{ex.ToString()}");
                }

                index++;
            }
            return list;
        }

        private static List<LineTeamDto> GetLineTeams(XmlNodeList nodeList)
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
                    DateFinish = node.Attributes["dateFinish"].Value,
                    DateOffline = node.Attributes["dateOffline"].Value,
                    DateStart = node.Attributes["dateStart"].Value,
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
                    PostersData = node.SelectSingleNode("postersData").InnerText,
                    PostersImg = node.SelectSingleNode("postersData").Attributes["postersImg"].Value,
                });
            }
            return list;
        }

        private static LineDto ParseLineDetail(string url, string lineCode)
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

        private static LineDto GetLine(XmlNode node)
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

        private static List<LineIntroDto> GetLineIntros(XmlNodeList nodeList)
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

        private static List<LineDayDto> GetLineDays(XmlNodeList nodeList)
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
                    LineDayTraffics = GetLineTraffics(node.SelectNodes("traffics/traffic")),
                });
            }
            return list;
        }

        private static List<LineDayTrafficDto> GetLineTraffics(XmlNodeList nodeList)
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

        private static List<LineDayImageDto> GetLineDayImsges(XmlNodeList nodeList)
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

        //public async Task InsertLine(LineDto dto)
        //{
        //    var line = await _lineRepository.InsertAsync(new Line()
        //    {
        //        Continent = dto.Continent,
        //        Country = dto.Country,
        //        CustomTitle = dto.CustomTitle,
        //        FirstLineImg = dto.FirstLineImg,
        //        Function = dto.Function,
        //        ImgCity = dto.ImgCity,
        //        ImgCode = dto.ImgCode,
        //        ImgContinent = dto.ImgContinent,
        //        ImgCountry = dto.ImgCountry,
        //        LineCode = dto.LineCode,
        //        LineType = dto.LineType,
        //        NumDay = dto.NumDay,
        //        NumNight = dto.NumNight,
        //        PlaceLeave = dto.PlaceLeave,
        //        PlaceReturn = dto.PlaceReturn,
        //        Sight = dto.Sight,
        //        Title = dto.Title,
        //        TxtTransitCity = dto.TxtTransitCity,
        //        Visa = dto.Visa,
        //    });

        //    if (dto.LineIntros != null & dto.LineIntros.Count > 0)
        //    {
        //        dto.LineIntros.ForEach(async (m) =>
        //        {
        //            var lineIntro = m.MapTo<LineIntroDto, LineIntro>();
        //            lineIntro.LineId = line.Id;
        //            lineIntro.LineCode = line.LineCode;
        //            await _lineIntroRepository.InsertAsync(lineIntro);
        //        });
        //    }

        //    if (dto.LineTeams != null && dto.LineTeams.Count > 0)
        //    {
        //        dto.LineTeams.ForEach(async (m) =>
        //        {
        //            var lineTeam = m.MapTo<LineTeamDto, LineTeam>();
        //            lineTeam.LineId = line.Id;
        //            lineTeam.LineCode = line.LineCode;
        //            await _lineTeamRepository.InsertAsync(lineTeam);
        //        });
        //    }

        //    if (dto.LineDays != null && dto.LineDays.Count > 0)
        //    {
        //        dto.LineDays.ForEach(async (m) =>
        //        {
        //            var lineDay = await _lineDayRepository.InsertAsync(new LineDay()
        //            {
        //                LineCode = dto.LineCode,
        //                Breakfast = m.Breakfast,
        //                CityEnglish = m.CityEnglish,
        //                DayHotel = m.DayHotel,
        //                DayNumber = m.DayNumber,
        //                DayTraffic = m.DayTraffic,
        //                Describe = m.Describe,
        //                Dinner = m.Dinner,
        //                LineId = line.Id,
        //                Lunch = m.Lunch,
        //                ScityDistance = m.ScityDistance,
        //                TrafficName = m.TrafficName,
        //            });

        //            if (m.LineDayImages != null && m.LineDayImages.Count > 0)
        //            {
        //                m.LineDayImages.ForEach(async (img) =>
        //                {
        //                    var image = img.MapTo<LineDayImageDto, LineDayImage>();
        //                    image.LineDayId = lineDay.Id;
        //                    image.LineId = line.Id;
        //                    image.LineCode = dto.LineCode;
        //                    image.DayNumber = m.DayNumber;
        //                    await _lineDayImageRepository.InsertAsync(image);
        //                });
        //            }

        //            if (m.LineDaySelfs != null && m.LineDaySelfs.Count > 0)
        //            {
        //                m.LineDaySelfs.ForEach(async (self) =>
        //                {
        //                    var daySelf = self.MapTo<LineDaySelfDto, LineDaySelf>();
        //                    daySelf.LineId = line.Id;
        //                    daySelf.LineDayId = lineDay.Id;
        //                    daySelf.LineCode = dto.LineCode;
        //                    daySelf.DayNumber = m.DayNumber;
        //                    await _lineDaySelfRepository.InsertAsync(daySelf);
        //                });
        //            }

        //            if (m.LineDayShops != null && m.LineDayShops.Count > 0)
        //            {
        //                m.LineDayShops.ForEach(async (shop) =>
        //                {
        //                    var dayShop = shop.MapTo<LineDayShopDto, LineDayShop>();
        //                    dayShop.LineId = line.Id;
        //                    dayShop.LineDayId = lineDay.Id;
        //                    dayShop.LineCode = dto.LineCode;
        //                    dayShop.DayNumber = m.DayNumber;
        //                    await _lineDayShopRepository.InsertAsync(dayShop);
        //                });
        //            }

        //            if (m.LineDayTraffics != null && m.LineDayTraffics.Count > 0)
        //            {
        //                m.LineDayTraffics.ForEach(async (traffic) =>
        //                {
        //                    var dayTraffic = traffic.MapTo<LineDayTrafficDto, LineDayTraffic>();
        //                    dayTraffic.LineId = line.Id;
        //                    dayTraffic.LineDayId = lineDay.Id;
        //                    dayTraffic.LineCode = dto.LineCode;
        //                    dayTraffic.DayNumber = m.DayNumber;
        //                    await _lineDayTrafficRepository.InsertAsync(dayTraffic);
        //                });
        //            }

        //        });
        //    }
        //}

        public async Task AddOrUpdateLine(LineDto dto)
        {
            var line = _lineRepository.FirstOrDefault(m => m.LineCode == dto.LineCode);
            if (line == null)
            {
                line = await _lineRepository.InsertAsync(new Line()
                {
                    Continent = dto.Continent,
                    Country = dto.Country,
                    CustomTitle = dto.CustomTitle,
                    FirstLineImg = dto.FirstLineImg,
                    Function = dto.Function,
                    ImgCity = dto.ImgCity,
                    ImgCode = dto.ImgCode,
                    ImgContinent = dto.ImgContinent,
                    ImgCountry = dto.ImgCountry,
                    LineCode = dto.LineCode,
                    LineType = dto.LineType,
                    NumDay = dto.NumDay,
                    NumNight = dto.NumNight,
                    PlaceLeave = dto.PlaceLeave,
                    PlaceReturn = dto.PlaceReturn,
                    Sight = dto.Sight,
                    Title = dto.Title,
                    TxtTransitCity = dto.TxtTransitCity,
                    Visa = dto.Visa,
                });
            }
            else
            {
                Log.Information($"线路【{line.LineCode}】已存在，更新线路");
                line.Continent = dto.Continent;
                line.Country = dto.Country;
                line.CustomTitle = dto.CustomTitle;
                line.FirstLineImg = dto.FirstLineImg;
                line.Function = dto.Function;
                line.ImgCity = dto.ImgCity;
                line.ImgCode = dto.ImgCode;
                line.ImgContinent = dto.ImgContinent;
                line.ImgCountry = dto.ImgCountry;
                line.LineCode = dto.LineCode;
                line.LineType = dto.LineType;
                line.NumDay = dto.NumDay;
                line.NumNight = dto.NumNight;
                line.PlaceLeave = dto.PlaceLeave;
                line.PlaceReturn = dto.PlaceReturn;
                line.Sight = dto.Sight;
                line.Title = dto.Title;
                line.TxtTransitCity = dto.TxtTransitCity;
                line.Visa = dto.Visa;
                await _lineRepository.UpdateAsync(line);
            }

            if (dto.LineIntros != null & dto.LineIntros.Count > 0)
            {
                dto.LineIntros.ForEach(async (m) =>
                {
                    var lineIntro = m.MapTo<LineIntroDto, LineIntro>();
                    var model = _lineIntroRepository.FirstOrDefault(item => item.LineCode == dto.LineCode && item.OrderNum == m.OrderNum);
                    if (model == null)
                    {
                        lineIntro.LineId = line.Id;
                        lineIntro.LineCode = line.LineCode;
                        await _lineIntroRepository.InsertAsync(lineIntro);
                    }
                    else
                    {
                        await _lineIntroRepository.UpdateAsync(model);
                    }
                });
            }

            if (dto.LineTeams != null && dto.LineTeams.Count > 0)
            {
                dto.LineTeams.ForEach(async (m) =>
                {
                    var lineTeam = m.MapTo<LineTeamDto, LineTeam>();
                    var model = _lineTeamRepository.FirstOrDefault(item => item.LineCode == dto.LineCode && item.ProductCode == m.ProductCode);
                    if (model == null)
                    {
                        lineTeam.LineId = line.Id;
                        lineTeam.LineCode = line.LineCode;
                        await _lineTeamRepository.InsertAsync(lineTeam);
                    }
                    else
                    {
                        await _lineTeamRepository.UpdateAsync(model);
                    }
                });
            }

            if (dto.LineDays != null && dto.LineDays.Count > 0)
            {
                dto.LineDays.ForEach(async (m) =>
                {
                    var lineDay = m.MapTo<LineDayDto, LineDay>();
                    var model = _lineDayRepository.FirstOrDefault(item => item.LineCode == dto.LineCode && item.DayNumber == m.DayNumber);
                    if (model == null)
                    {
                        lineDay.LineId = line.Id;
                        lineDay.LineCode = line.LineCode;
                        await _lineDayRepository.InsertAsync(lineDay);
                    }
                    else
                    {
                        await _lineDayRepository.UpdateAsync(model);
                    }

                    if (m.LineDayImages != null && m.LineDayImages.Count > 0)
                    {
                        m.LineDayImages.ForEach(async (img) =>
                        {
                            var image = img.MapTo<LineDayImageDto, LineDayImage>();
                            var model = _lineDayImageRepository.FirstOrDefault(item => item.LineCode == dto.LineCode && item.DayNumber == m.DayNumber);
                            if (model == null)
                            {
                                image.LineDayId = lineDay.Id;
                                image.LineId = line.Id;
                                image.LineCode = dto.LineCode;
                                image.DayNumber = m.DayNumber;
                                await _lineDayImageRepository.InsertAsync(image);
                            }
                            else
                            {
                                await _lineDayImageRepository.UpdateAsync(model);
                            }
                        });
                    }

                    if (m.LineDaySelfs != null && m.LineDaySelfs.Count > 0)
                    {
                        m.LineDaySelfs.ForEach(async (self) =>
                        {
                            var daySelf = self.MapTo<LineDaySelfDto, LineDaySelf>();
                            var model = _lineDaySelfRepository.FirstOrDefault(item => item.LineCode == dto.LineCode && item.DayNumber == m.DayNumber);
                            if (model == null)
                            {
                                daySelf.LineId = line.Id;
                                daySelf.LineDayId = lineDay.Id;
                                daySelf.LineCode = dto.LineCode;
                                daySelf.DayNumber = m.DayNumber;
                                await _lineDaySelfRepository.InsertAsync(daySelf);
                            }
                            else
                            {
                                await _lineDaySelfRepository.UpdateAsync(model);
                            }
                        });
                    }

                    if (m.LineDayShops != null && m.LineDayShops.Count > 0)
                    {
                        m.LineDayShops.ForEach(async (shop) =>
                        {
                            var dayShop = shop.MapTo<LineDayShopDto, LineDayShop>();
                            var model = _lineDayShopRepository.FirstOrDefault(item => item.LineCode == dto.LineCode && item.DayNumber == m.DayNumber);
                            if (model == null)
                            {
                                dayShop.LineId = line.Id;
                                dayShop.LineDayId = lineDay.Id;
                                dayShop.LineCode = dto.LineCode;
                                dayShop.DayNumber = m.DayNumber;
                                await _lineDayShopRepository.InsertAsync(dayShop);
                            }
                            else
                            {
                                await _lineDayShopRepository.UpdateAsync(model);
                            }
                        });
                    }

                    if (m.LineDayTraffics != null && m.LineDayTraffics.Count > 0)
                    {
                        m.LineDayTraffics.ForEach(async (traffic) =>
                        {
                            var dayTraffic = traffic.MapTo<LineDayTrafficDto, LineDayTraffic>();
                            var model = _lineDayTrafficRepository.FirstOrDefault(item => item.LineCode == dto.LineCode && item.DayNumber == m.DayNumber);
                            if (model == null)
                            {
                                dayTraffic.LineId = line.Id;
                                dayTraffic.LineDayId = lineDay.Id;
                                dayTraffic.LineCode = dto.LineCode;
                                dayTraffic.DayNumber = m.DayNumber;
                                await _lineDayTrafficRepository.InsertAsync(dayTraffic);
                            }
                            else
                            {
                                await _lineDayTrafficRepository.UpdateAsync(model);
                            }
                        });
                    }

                });
            }
        }
    }
}
