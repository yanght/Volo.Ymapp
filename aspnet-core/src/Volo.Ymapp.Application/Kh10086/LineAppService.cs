using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Threading;
using Volo.Ymapp.Utils;

namespace Volo.Ymapp.Kh10086
{
    public class LineAppService : ApplicationService, ILineAppService
    {
        //public ILogger<MyService> Logger { get; set; }
        private readonly IDistributedCache<LineDto> _cache;
        private readonly IRepository<Line, long> _lineRepository;
        private readonly IRepository<LineDay, long> _lineDayRepository;
        private readonly IRepository<LineDayImage, long> _lineDayImageRepository;
        private readonly IRepository<LineDaySelf, long> _lineDaySelfRepository;
        private readonly IRepository<LineDayShop, long> _lineDayShopRepository;
        private readonly IRepository<LineDayTraffic, long> _lineDayTrafficRepository;
        private readonly IRepository<LineIntro, long> _lineIntroRepository;
        private readonly IRepository<LineRouteDate, long> _lineRouteDateRepository;
        private readonly IRepository<LineTeam, long> _lineTeamRepository;
        public LineAppService(
            IDistributedCache<LineDto> cache
            , IRepository<Line, long> lineRepository
            , IRepository<LineDay, long> lineDayRepository
            , IRepository<LineDayImage, long> lineDayImageRepository
            , IRepository<LineDaySelf, long> lineDaySelfRepository
            , IRepository<LineDayShop, long> lineDayShopRepository
            , IRepository<LineDayTraffic, long> lineDayTrafficRepository
            , IRepository<LineIntro, long> lineIntroRepository
            , IRepository<LineRouteDate, long> lineRouteDateRepository
            , IRepository<LineTeam, long> lineTeamRepository)
        {
            _cache = cache;
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

        /// <summary>
        /// 解析同步线路数据
        /// </summary>
        /// <param name="dto"></param>
        public void ParseLineData(ParseLineDataDto dto)
        {
            Log.Information($"开始获取解析线路数据。。。");
            string lineListurl = dto.LineListUrl;// "https://tispfile.utourworld.com/upload/op/xml/agentLine/index.xml";
            string lineDetailUrl = dto.LineDetailUrl;//"https://tispfile.utourworld.com/upload/op/xml/agentLine/{0}.xml";
            string response = HttpClientHelper.HttpRequest(lineListurl, encoding: Encoding.GetEncoding("GBK"));
            var doc = new XmlDocument();
            doc.LoadXml(response);
            XmlNodeList nodeList = doc.SelectNodes("routes/line");
            Log.Information($"共获取到{nodeList.Count}条线路");
            var lineList = GetLineList(lineDetailUrl, nodeList);
            Log.Information($"结束线路数据解析");
        }
        /// <summary>
        /// 获取线路集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<LineListDto> GetLineList(GetLineListDto input)
        {
            var query = _lineRepository.WhereIf(!string.IsNullOrEmpty(input.Continent), m => m.Continent.Contains(input.Continent))
                .WhereIf(!string.IsNullOrEmpty(input.Country), m => m.Country.Contains(input.Country))
                .WhereIf(input.Recommend.HasValue, m => m.Recommend == input.Recommend.Value)
                .WhereIf(!string.IsNullOrEmpty(input.LineCategoryType), m => m.LineCategoryType == input.LineCategoryType)
                .Where(m => m.DateOffline > DateTime.Now && m.DateStart > DateTime.Now && m.DateOnline < DateTime.Now);

            var count = query.Count();
            var list = query.PageBy(input.SkipCount, input.MaxResultCount)
                       .ToList();
            return new PagedResultDto<LineListDto>(count, ObjectMapper.Map<List<Line>, List<LineListDto>>(list));
        }
        /// <summary>
        /// 获取所有洲的集合
        /// </summary>
        /// <returns></returns>
        public List<string> GetContinents()
        {
            List<string> list = new List<string>();
            var lineContinents = _lineRepository.Select(m => m.Continent).Distinct().ToList();
            foreach (var item in lineContinents)
            {
                if (string.IsNullOrEmpty(item)) continue;
                var arrs = item.Split(',');
                for (int i = 0; i < arrs.Length; i++)
                {
                    if (string.IsNullOrEmpty(arrs[i])) continue;
                    if (!list.Contains(arrs[i]))
                    {
                        list.Add(arrs[i]);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取所有线路国家
        /// </summary>
        /// <returns></returns>
        public List<string> GetCountrys()
        {
            var countryList = _lineRepository.Select(m => m.Country).Distinct().ToList();
            List<string> countrys = new List<string>();
            foreach (var item in countryList)
            {
                var arr = item.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    var name = arr[i];
                    if (!countrys.Contains(name))
                    {
                        countrys.Add(name);
                    }
                }
            }
            return countrys;
        }

        public List<LineTeamDto> GetLineTeams(string lineCode)
        {
            var teamList = _lineTeamRepository.Where(m => m.DateOnline <= DateTime.Now && m.DateOffline > DateTime.Now && m.LineCode == lineCode).ToList();
            return teamList.MapToList<LineTeam, LineTeamDto>().ToList();
        }

        #region 获取线路详情

        public async Task<LineDto> GetLineByLineId(long lineId)
        {
            return await _cache.GetOrAddAsync(
            lineId.ToString(), //Cache key
            async () => await GetLineByLineIdFromDb(lineId),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
            }
        );
        }

        private async Task<LineDto> GetLineByLineIdFromDb(long lineId)
        {
            LineDto model = new LineDto();
            var line = await _lineRepository.FindAsync(lineId);
            model = line.MapTo<Line, LineDto>();
            var lineTeams = _lineTeamRepository.Where(m => m.LineCode == line.LineCode).ToList();
            var lineDays = _lineDayRepository.Where(m => m.LineCode == line.LineCode).ToList();
            var lineRoutes = _lineRouteDateRepository.Where(m => m.LineCode == line.LineCode).ToList();
            var lineIntros = _lineIntroRepository.Where(m => m.LineCode == line.LineCode).ToList();
            model.LineTeams = lineTeams.MapToList<LineTeam, LineTeamDto>().ToList();
            model.LineIntros = lineIntros.MapToList<LineIntro, LineIntroDto>().ToList();
            model.LineRouteDates = lineRoutes.MapToList<LineRouteDate, LineRouteDateDto>().ToList();
            List<LineDayDto> lineDayDtos = new List<LineDayDto>();
            if (lineDays != null && lineDays.Count > 0)
            {
                lineDays.ForEach(item =>
                {
                    var lineDayImages = _lineDayImageRepository.Where(m => m.LineCode == line.LineCode && m.DayNumber == item.DayNumber).ToList();
                    var lineDayTraffics = _lineDayTrafficRepository.Where(m => m.LineCode == line.LineCode && m.DayNumber == item.DayNumber).ToList();
                    var lineDaySelfs = _lineDaySelfRepository.Where(m => m.LineCode == line.LineCode && m.DayNumber == item.DayNumber).ToList();
                    var lineDayShops = _lineDayShopRepository.Where(m => m.LineCode == line.LineCode && m.DayNumber == item.DayNumber).ToList();
                    var lineDay = item.MapTo<LineDay, LineDayDto>();
                    lineDay.LineDayImages = lineDayImages.MapToList<LineDayImage, LineDayImageDto>().ToList();
                    lineDay.LineDayTraffics = lineDayTraffics.MapToList<LineDayTraffic, LineDayTrafficDto>().ToList();
                    lineDay.LineDaySelfs = lineDaySelfs.MapToList<LineDaySelf, LineDaySelfDto>().ToList();
                    lineDay.LineDayShops = lineDayShops.MapToList<LineDayShop, LineDayShopDto>().ToList();
                    lineDayDtos.Add(lineDay);
                });
            }
            model.LineDays = lineDayDtos;
            return model;
        }

        public async Task<LineDto> GetLineByLineCode(string lineCode)
        {
            var line = _lineRepository.SingleOrDefault(m => m.LineCode == lineCode);
            return await _cache.GetOrAddAsync(
            line.Id.ToString(), //Cache key
            async () => await GetLineByLineIdFromDb(line.Id),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
            }
        );
        }

        public async Task<LineDto> GetLineByProductCode(string productCode)
        {
            var lineTeam = _lineTeamRepository.SingleOrDefault(m => m.ProductCode == productCode);
            return await _cache.GetOrAddAsync(
           lineTeam.LineId.ToString(), //Cache key
           async () => await GetLineByLineIdFromDb(lineTeam.LineId),
           () => new DistributedCacheEntryOptions
           {
               AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
           }
       );
        }

        #endregion 

        /// <summary>
        /// 同步线路的最低价格
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        public async Task LinePriceAsync()
        {
            var lines = await _lineRepository.GetListAsync();
            foreach (var line in lines)
            {
                var lineTeams = _lineTeamRepository.Where(m => m.LineCode == line.LineCode);
                var lineTeam = lineTeams.OrderBy(m => m.CustomerPrice).FirstOrDefault();
                line.CustomerPrice = lineTeam.CustomerPrice;
                line.AgentPrice = lineTeam.AgentPrice;
                line.ChildPrice = lineTeam.ChildPrice;
                line.OverseasJoinPrice = lineTeam.OverseasJoinPrice;
                lineTeam = lineTeams.OrderBy(m => m.DateStart).FirstOrDefault();
                line.DateStart = lineTeam.DateStart;
                lineTeam = lineTeams.OrderByDescending(m => m.DateOffline).FirstOrDefault();
                line.DateOffline = lineTeam.DateOffline;
                lineTeam = lineTeams.OrderBy(m => m.DateOnline).FirstOrDefault();
                line.DateOnline = lineTeam.DateOnline;
                await _lineRepository.UpdateAsync(line);
            }
        }

        public async Task LineTeamAsync()
        {
            try
            {
                string host = "http://129.204.184.147:8002/";// _configuration["Utour:apiUrl"];
                string visitCode = "HNKH";// _configuration["Utour:visitCode"];
                string signKey = "1ey53c6a8ebbfe0dyc301cy5c010y1bx";// _configuration["Utour:signKey"];
                string userCode = "AGENT201911151338295";// _configuration["Utour:userCode"];
                string token = "agentApi1024";// _configuration["Utour:token"];
                string accpCode = "530015";// _configuration["Utour:accpCode"];

                UTourApiClient client = new UTourApiClient(host, visitCode, signKey, userCode, token, accpCode);
                while (true)
                {
                    var teamList = _lineTeamRepository.Where(m => m.LastAsyncTime.Date < DateTime.Now.Date).Take(10).ToList();
                    if (teamList != null && teamList.Count > 0)
                    {
                        foreach (var item in teamList)
                        {
                            var teamInfo = client.getTeamInfoByCodeOrId(item.ProductCode, item.TeamId);
                            if (teamInfo != null)
                            {
                                item.ProductCode = teamInfo.productCode;
                                item.ProductName = teamInfo.title;
                                item.Continent = teamInfo.continent;
                                item.PlaceLeave = teamInfo.placeLeave;
                                item.PlaceReturn = teamInfo.placeRetrun;
                                item.DateStart = DateTime.Parse(teamInfo.dateSart);
                                item.DateFinish = DateTime.Parse(teamInfo.dateFinish);
                                item.DateOnline = DateTime.Parse(teamInfo.dateOnline);
                                item.DateOffline = DateTime.Parse(teamInfo.dateOffline);
                                item.AirCompany = teamInfo.airlineCompany;
                            }

                            var teamPrice = client.getRealTimeTeamPrice(item.ProductCode, item.TeamId);
                            if (teamPrice != null)
                            {
                                item.CustomerPrice = teamPrice.priceRetail;
                                item.OverseasJoinPrice = teamPrice.overseasTourPrice;
                                item.ChildPrice = teamPrice.childPrice;
                                item.SingleRoom = teamPrice.singleRoomDifference;
                                item.AgentPrice = teamPrice.tradePrice;
                            }

                            var teamNumber = client.getRealTimeTeamStockNum(item.ProductCode, item.TeamId);
                            if (teamNumber != null)
                            {
                                item.FreeNum = teamNumber.numFree;
                                item.PlanNum = teamNumber.numPlan;
                            }

                            await _lineTeamRepository.UpdateAsync(item);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"同步线路最新信息失败,{ex.ToString()}");
            }
        }

        #region 内部方法
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

        private List<LineDto> GetLineList(string url, XmlNodeList nodeList)
        {
            List<LineDto> list = new List<LineDto>();
            if (nodeList == null || nodeList.Count == 0) return list;
            int index = 1;
            foreach (XmlNode node in nodeList)
            {
                //if (index > 1) break;
                string lineCode = node.Attributes["lineCode"].Value;
                try
                {
                    Log.Information($"开始解析第{index}条数据,【{lineCode}】");
                    var lineDetail = ParseLineDetail(string.Format(url, lineCode));
                    lineDetail.LineTeams = GetLineTeams(node.SelectNodes("team/teamData"));
                    lineDetail.FirstLineImg = node.Attributes["firstLineImg"].Value;
                    list.Add(lineDetail);
                    Log.Information($"结束解析第{index}条数据,【{lineCode}】");
                    AsyncHelper.RunSync(async () =>
                    {
                        try
                        {
                            Log.Information($"开始入库第{index}条数据");
                            await InsertOrUpdateLine(lineDetail);
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
                    PostersData = node.SelectSingleNode("postersData").InnerText,
                    PostersImg = node.SelectSingleNode("postersData").Attributes["postersImg"].Value,
                });
            }
            return list;
        }

        private static LineDto ParseLineDetail(string url)
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
                    LineDaySelfs = GetLineDaySelfs(node.SelectNodes("countrynameSelf/self")),
                    LineDayShops = GetLineDayShops(node.SelectNodes("countrynameShop/shop")),
                    LineRouteDates = GetLineRouteDates(node.SelectNodes("routeDates/routeDate"))
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

        private static List<LineDaySelfDto> GetLineDaySelfs(XmlNodeList nodeList)
        {
            List<LineDaySelfDto> list = new List<LineDaySelfDto>();
            if (nodeList == null || nodeList.Count == 0) return list;
            foreach (XmlNode node in nodeList)
            {
                list.Add(new LineDaySelfDto()
                {
                    CountryName = node.Attributes["countryname"].Value,
                    CityName = node.Attributes["cityname"].Value,
                    Content = node.Attributes["content"].Value,
                    Intro = node.Attributes["intro"].Value,
                    Name = node.Attributes["name"].Value,
                    Price = decimal.Parse(node.Attributes["price"].Value),

                });
            }
            return list;
        }

        private static List<LineDayShopDto> GetLineDayShops(XmlNodeList nodeList)
        {
            List<LineDayShopDto> list = new List<LineDayShopDto>();
            if (nodeList == null || nodeList.Count == 0) return list;
            foreach (XmlNode node in nodeList)
            {
                list.Add(new LineDayShopDto()
                {
                    CountryName = node.Attributes["countryname"].Value,
                    CityName = node.Attributes["cityname"].Value,
                    ActivityTime = node.Attributes["activityTime"].Value,
                    Intro = node.Attributes["intro"].Value,
                    Name = node.Attributes["name"].Value,
                });
            }
            return list;
        }

        private static List<LineRouteDateDto> GetLineRouteDates(XmlNodeList nodeList)
        {
            List<LineRouteDateDto> list = new List<LineRouteDateDto>();
            if (nodeList == null || nodeList.Count == 0) return list;
            foreach (XmlNode node in nodeList)
            {
                list.Add(new LineRouteDateDto()
                {
                    AdultPrice = decimal.Parse(node.Attributes["adultPrice"].Value),
                    ChildPrice = decimal.Parse(node.Attributes["childPrice"].Value),
                    AgentPrice = decimal.Parse(node.Attributes["agentPrice"].Value),
                    DateFinish = DateTime.Parse(node.Attributes["dateFinish"].Value),
                    DateOffline = DateTime.Parse(node.Attributes["dateOffline"].Value),
                    DateStart = DateTime.Parse(node.Attributes["datestart"].Value),
                    Deposit = decimal.Parse(node.Attributes["deposit"].Value),
                    JieShouRiQi = DateTime.Parse(node.Attributes["JieShouRiQi"].Value),
                    OverseasJoinPrice = decimal.Parse(node.Attributes["overseasJoinPrice"].Value),
                    FreeNum = int.Parse(node.Attributes["freeNum"].Value),
                    PlanNum = int.Parse(node.Attributes["planNum"].Value),
                    ProductCode = node.Attributes["productCode"].Value,
                    RetainCount = int.Parse(node.Attributes["retainCount"].Value),
                    SingleRoom = decimal.Parse(node.Attributes["singleRoom"].Value),
                    TeamId = node.Attributes["teamId"].Value,
                    WebsiteTags = node.Attributes["websiteTags"].Value,
                }); ;
            }
            return list;
        }

        private async Task InsertOrUpdateLine(LineDto dto)
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

            if (dto.LineRouteDates != null && dto.LineRouteDates.Count > 0)
            {
                dto.LineRouteDates.ForEach(async (m) =>
                {
                    var lineRoute = m.MapTo<LineRouteDateDto, LineRouteDate>();
                    var model = _lineRouteDateRepository.FirstOrDefault(item => item.LineCode == dto.LineCode && item.ProductCode == m.ProductCode);
                    if (model == null)
                    {
                        lineRoute.LineId = line.Id;
                        lineRoute.LineCode = line.LineCode;
                        await _lineRouteDateRepository.InsertAsync(lineRoute);
                    }
                    else
                    {
                        await _lineRouteDateRepository.UpdateAsync(model);
                    }
                });
            }
        }

        #endregion
    }
}
