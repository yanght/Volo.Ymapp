using KH10086.Service.Common;
using KH10086.Service.Utour;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;

namespace KH10086.Service
{
    public class UTourService
    {
        static ILog Logger { get; set; }
        static DbHelperOleDb op { get; set; }
        public UTourService()
        {
            string connString = ConfigurationManager.AppSettings["connString"];
            Logger = LogManager.GetLogger(typeof(UTourService));
            string connStr = connString;//"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";";
            op = new DbHelperOleDb(connStr); // 初始化实例，需要传入一个连接字符串
        }

        /// <summary>
        /// 添加线路
        /// </summary>
        /// <param name="classId">所属分类</param>
        /// <returns></returns>
        public static bool InsertArticle(int classId, LineItem line, LineDetailModel lineDetail, out int articleId)
        {

            articleId = 0;
            if (classId == 0)
            {
                Logger.Error("分类编号错误");
                return false;
            }
            try
            {

                string productCode = line.team.teamData[0].productCode;
                string teamId = line.team.teamData[0].teamId;
                string startDate = line.team.teamData[0].dateStart;
                List<string> traffics = new List<string>();
                lineDetail.lineDays.ForEach(m =>
                {
                    if (!string.IsNullOrEmpty(m.dayTraffic))
                    {
                        string[] trafficlist = m.dayTraffic.Split(',');
                        for (int i = 0; i < trafficlist.Length; i++)
                        {
                            if (trafficlist[i] != "")
                            {
                                if (!traffics.Contains(trafficlist[i]))
                                {
                                    traffics.Add(trafficlist[i]);
                                }
                            }
                        }
                    }
                });

                var lineStock = UTourApiClient.getRealTimeTeamStockNum(productCode, teamId);
                var linePrice = UTourApiClient.getRealTimeTeamPrice(productCode, teamId);
                var lineinfo = UTourApiClient.getTeamInfoByCodeOrId(productCode, teamId);

                Logger.Info("线路实时信息" + JsonConvert.SerializeObject(lineinfo));

                #region  线路存在则更新线路

                string oldArticle = "select ID from c_article where productCode=@productCode ";
                object obj = op.GetSingle(oldArticle, new OleDbParameter("@productCode", productCode));
                int oldId = obj == null ? 0 : Convert.ToInt32(obj);
                articleId = oldId;
                if (oldId > 0)
                {
                    if (IsDebugger())
                    {
                        Logger.Info($"线路【{line.lineCode},{line.lineName}】已存在，更新行程");
                    }
                    return UpdateArticle(oldId, line, lineDetail);
                }

                #endregion

                string insertArticle = @"insert into c_article ([ID],[ClassID],[Title],[text1],[text2],[text3],[text4],[text5],[text6],[text7],[text8],[text9],[text10],[zy],[Content],[DefaultPicUrl],[Hits],[Updatetime],[link],[orderid],[tj],[bestnew],[hot],[jturl],[public],[hftime],[qzcl],[stitle],[skeywrods],[scontent],[qtpicUrl],[wappicUrl],[qtxx],[productCode],[teamId])
values(@ID, @ClassID, @Title, @text1, @text2, @text3, @text4, @text5, @text6, @text7, @text8, @text9, @text10, @zy,
@Content, @DefaultPicUrl, @Hits, @Updatetime, @link, @orderid, @tj, @bestnew, @hot, @jturl, @public, @hftime, @qzcl, @stitle, @skeywrods, @scontent, @qtpicUrl, @wappicUrl, @qtxx,@productCode,@teamId)";
                int nextArticleId = GetNextArticleId();
                articleId = nextArticleId;
                OleDbParameter[] parms = {
                new OleDbParameter("@ID", OleDbType.Integer){ Value=nextArticleId},//0
                new OleDbParameter("@ClassID", OleDbType.Integer){ Value=classId},//1
                new OleDbParameter("@Title", OleDbType.VarChar){ Value=lineinfo.title},//2
                new OleDbParameter("@text1", OleDbType.VarChar){ Value=linePrice?.priceRetail.ToString("f0")},//3
                new OleDbParameter("@text2", OleDbType.VarChar){ Value=linePrice?.priceRetail.ToString("f0")},//4
                new OleDbParameter("@text3", OleDbType.VarChar){ Value=linePrice?.childPrice.ToString("f0")},//5
                new OleDbParameter("@text4", OleDbType.VarChar){ Value=lineinfo.placeLeave},//6
                new OleDbParameter("@text5", OleDbType.VarChar){ Value=lineDetail.NumDay/* + "天" + lineDetail.NumNight + "晚"*/},//7
                new OleDbParameter("@text6", OleDbType.VarChar){ Value=string.Join("",traffics.ToArray())},//8//交通方式
                new OleDbParameter("@text7", OleDbType.VarChar){ Value=""},//9//套餐类型
                new OleDbParameter("@text8", OleDbType.VarChar){ Value=lineinfo.dateSart},//10 发团日期
                new OleDbParameter("@text9", OleDbType.VarChar){ Value=""},//11
                new OleDbParameter("@text10", OleDbType.VarChar){ Value=""},//12
                new OleDbParameter("@zy", OleDbType.VarChar){ Value=""},//13//产品提示
                new OleDbParameter("@Content", OleDbType.VarChar){ Value=""},//14//服务标准
                new OleDbParameter("@DefaultPicUrl", OleDbType.VarChar){ Value=line.firstLineImg},//15
                new OleDbParameter("@Hits", OleDbType.Integer){ Value=0},//16
                new OleDbParameter("@Updatetime", OleDbType.Date){ Value = DateTime.Now.ToString("yyyy/MM/dd")},//17
                new OleDbParameter("@link", OleDbType.VarChar){ Value=""},//18
                new OleDbParameter("@orderid", OleDbType.Integer){ Value=0},//19
                new OleDbParameter("@tj", OleDbType.Integer){ Value=0},//20
                new OleDbParameter("@bestnew", OleDbType.Integer){ Value=0},//21
                new OleDbParameter("@hot", OleDbType.Integer){ Value=0},//22
                new OleDbParameter("@jturl", OleDbType.VarChar){ Value=""},//23
                new OleDbParameter("@public", OleDbType.Integer){ Value=0},//24
                new OleDbParameter("@hftime", OleDbType.Date){Value = DateTime.Now.ToString("yyyy/MM/dd")},//25
                new OleDbParameter("@qzcl", OleDbType.VarChar){ Value=lineDetail.lineIntros!=null&&lineDetail.lineIntros.Count>0?lineDetail.lineIntros.FirstOrDefault(m => m.orderNum == "7")?.text??"":""},//26
                new OleDbParameter("@stitle", OleDbType.VarChar){ Value=lineinfo.title},//27
                new OleDbParameter("@skeywrods", OleDbType.VarChar){ Value=""},//28
                new OleDbParameter("@scontent", OleDbType.VarChar){ Value=""},//29
                new OleDbParameter("@qtpicUrl", OleDbType.VarChar){ Value=string.Format("*{0}",!string.IsNullOrEmpty(lineDetail.imgCode)? lineDetail.imgCode.Replace(',', '*'):"")},//30
                new OleDbParameter("@wappicUrl", OleDbType.VarChar){ Value=""},//31
                new OleDbParameter("@qtxx", OleDbType.VarChar){ Value=""},//32
                new OleDbParameter("@productCode", OleDbType.VarChar){ Value=productCode},//32
                new OleDbParameter("@teamId", OleDbType.VarChar){ Value=teamId},//32
            };

                var result = op.ExecuteSql(insertArticle, parms);

                if (result > 0)
                {
                    if (IsDebugger())
                    {
                        Logger.Info($"线路【{line.lineCode},{line.lineName}】插入成功");
                    }
                    return UpdateNextArticleId() > 0;
                }
                else
                {
                    Logger.Error($"线路【{line.lineCode},{line.lineName}】插入失败");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"插如线路错误， 错误原因：{ex.ToString()}");
            }
            return false;
        }

        /// <summary>
        /// 更新线路
        /// </summary>
        /// <returns></returns>
        public static bool UpdateArticle(int articleId, LineItem line, LineDetailModel lineDetail)
        {
            if (articleId == 0)
            {
                Logger.Error("线路编号错误");
                return false;
            }
            try
            {
                string productCode = line.team.teamData[0].productCode;
                string teamId = line.team.teamData[0].teamId;
                string startDate = line.team.teamData[0].dateStart;
                List<string> traffics = new List<string>();
                lineDetail.lineDays.ForEach(m =>
                {
                    if (!string.IsNullOrEmpty(m.dayTraffic))
                    {
                        string[] trafficlist = m.dayTraffic.Split(',');
                        for (int i = 0; i < trafficlist.Length; i++)
                        {
                            if (trafficlist[i] != "")
                            {
                                if (!trafficlist.Contains(trafficlist[i]))
                                {
                                    traffics.Add(trafficlist[i]);
                                }
                            }
                        }
                    }
                });

                var lineStock = UTourApiClient.getRealTimeTeamStockNum(productCode, teamId);
                var linePrice = UTourApiClient.getRealTimeTeamPrice(productCode, teamId);
                //Logger.Info($"lineStock:{JsonConvert.SerializeObject(lineStock)},linePrice:{JsonConvert.SerializeObject(linePrice)}");
                string updatesql = @"update c_article set 
 [Title] = @Title, [text1] = @text1, [text2] = @text2, [text3] = @text3, [text4] = @text4, [text5] = @text5, [text6] = @text6,
 [text8] = @text8,  [DefaultPicUrl] = @DefaultPicUrl, [Updatetime] = @Updatetime, [hftime]=@hftime, 
[qzcl]=@qzcl, [stitle]=@stitle, [qtpicUrl]=@qtpicUrl
 where ID=@ID";

                OleDbParameter[] parms = {
                new OleDbParameter("@Title", OleDbType.VarChar){ Value=lineDetail.title},//0
                new OleDbParameter("@text1", OleDbType.VarChar){ Value=linePrice?.priceRetail.ToString("f0")},//3
                new OleDbParameter("@text2", OleDbType.VarChar){ Value=linePrice?.priceRetail.ToString("f0")},//4
                new OleDbParameter("@text3", OleDbType.VarChar){ Value=linePrice?.childPrice.ToString("f0")},//5
                new OleDbParameter("@text4", OleDbType.VarChar){ Value=lineDetail.placeLeave},//6
                new OleDbParameter("@text5", OleDbType.VarChar){ Value=lineDetail.NumDay/* + "天" + lineDetail.NumNight + "晚"*/},//7
                new OleDbParameter("@text6", OleDbType.VarChar){ Value=string.Join("",traffics.ToArray())},//8//交通方式
                //new OleDbParameter("@text7", OleDbType.VarChar){ Value=""},//9//套餐类型
                new OleDbParameter("@text8", OleDbType.VarChar){ Value=startDate},//10 发团日期
                //new OleDbParameter("@text9", OleDbType.VarChar){ Value=""},//9
                //new OleDbParameter("@text10", OleDbType.VarChar){ Value=""},//10
                //new OleDbParameter("@zy", OleDbType.VarChar){ Value=""},//11
               // new OleDbParameter("@Content", OleDbType.VarChar){ Value=""},//12
                new OleDbParameter("@DefaultPicUrl", OleDbType.VarChar){ Value=line.firstLineImg},//13
                new OleDbParameter("@Updatetime", OleDbType.Date){ Value=DateTime.Now.ToString("yyyy/MM/dd")},//14
                //new OleDbParameter("@link", OleDbType.VarChar){ Value=""},//15
                //new OleDbParameter("@jturl", OleDbType.VarChar){ Value=""},//16
                new OleDbParameter("@hftime", OleDbType.Date){ Value=DateTime.Now.ToString("yyyy/MM/dd")},//17
                new OleDbParameter("@qzcl", OleDbType.VarChar){ Value=lineDetail.lineIntros!=null&&lineDetail.lineIntros.Count>0? lineDetail.lineIntros.FirstOrDefault(m => m.orderNum == "4")?.text:""},//18
                new OleDbParameter("@stitle", OleDbType.VarChar){ Value=lineDetail.title},//19              
                //new OleDbParameter("@skeywrods", OleDbType.VarChar){ Value=""},//20
                //new OleDbParameter("@scontent", OleDbType.VarChar){ Value=""},//21
                new OleDbParameter("@qtpicUrl", OleDbType.VarChar){ Value=string.Format("*{0}",string.IsNullOrEmpty(lineDetail.imgCode)?"": lineDetail.imgCode.Replace(',', '*'))},//22
                //new OleDbParameter("@wappicUrl", OleDbType.VarChar){ Value=""},//23
                //new OleDbParameter("@qtxx", OleDbType.VarChar){ Value=""},//24
                new OleDbParameter("@ID", OleDbType.Integer){ Value=articleId},//24
            };

                int result = op.ExecuteSql(updatesql, parms);
                if (result > 0)
                {
                    if (IsDebugger())
                    {
                        Logger.Info($"线路【{line.lineCode},{line.lineName}】更新成功");
                    }
                    return true;
                }
                else
                {
                    Logger.Info($"线路【{line.lineCode},{line.lineName}】更新失败");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"更新线路错误， 错误原因：{ ex.ToString()}");
            }
            return false;
        }

        /// <summary>
        /// 添加行程
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static bool InsertXc(int articleId, ItineraryDays day)
        {
            try
            {
                #region 行程存在则更新行程 
                string oldxcsql = "select id from xcap where pid=@pid and orederid=@orederid";
                object obj = op.GetSingle(oldxcsql, new OleDbParameter("@pid", articleId), new OleDbParameter("@orederid", day.dayNumber));
                int oldId = obj == null ? 0 : Convert.ToInt32(obj);
                if (oldId > 0)
                {
                    if (IsDebugger())
                    {
                        Logger.Info($"行程【{day.dayNumber}】已存在，更新行程");
                    }
                    return UpdateXc(oldId, day);
                }
                #endregion

                #region 行程和交通方式
                string xc1 = "", fs1 = "", xc2 = "", fs2 = "", xc3 = "", fs3 = "", xc4 = "";
                var traffics = day.itineraryDay.trafficName.Split('-');
                //法国小镇-汽车-卢塞恩-未知-因特拉肯-汽车-因特拉肯
                if (traffics.Length == 1)
                {
                    xc1 = traffics[0];
                }
                if (traffics.Length == 2)
                {
                    xc1 = traffics[0];
                    fs1 = GetTrafficId(traffics[1]);
                }
                if (traffics.Length == 3)
                {
                    xc1 = traffics[0];
                    fs1 = GetTrafficId(traffics[1]);
                    xc2 = traffics[2];
                }
                if (traffics.Length == 4)
                {
                    xc1 = traffics[0];
                    fs1 = GetTrafficId(traffics[1]);
                    xc2 = traffics[2];
                    fs2 = GetTrafficId(traffics[3]);
                }
                if (traffics.Length == 5)
                {
                    xc1 = traffics[0];
                    fs1 = GetTrafficId(traffics[1]);
                    xc2 = traffics[2];
                    fs2 = GetTrafficId(traffics[3]);
                    xc3 = traffics[4];
                }
                if (traffics.Length == 6)
                {
                    xc1 = traffics[0];
                    fs1 = GetTrafficId(traffics[1]);
                    xc2 = traffics[2];
                    fs2 = GetTrafficId(traffics[3]);
                    xc3 = traffics[4];
                    fs3 = GetTrafficId(traffics[5]);
                }
                if (traffics.Length == 7)
                {
                    xc1 = traffics[0];
                    fs1 = GetTrafficId(traffics[1]);
                    xc2 = traffics[2];
                    fs2 = GetTrafficId(traffics[3]);
                    xc3 = traffics[4];
                    fs3 = GetTrafficId(traffics[5]);
                    xc4 = traffics[6];
                }
                #endregion

                string inserrtXc = @"insert into xcap([xc1],[fs1],[xc2],[fs2],[xc3],[fs3],[xc4],[yc],[zs],[orederid],[pid],[tjtime],[xcsm],[zgts])
values(@xc1,@fs1,@xc2,@fs2,@xc3,@fs3,@xc4,@yc,@zs,@orederid,@pid,@tjtime,@xcsm,@zgts)";
                OleDbParameter[] parms = {
                new OleDbParameter("@xc1", OleDbType.VarChar){ Value=xc1},//0
                new OleDbParameter("@fs1", OleDbType.VarChar){ Value=fs1},//0
                new OleDbParameter("@xc2", OleDbType.VarChar){ Value=xc2},//0
                new OleDbParameter("@fs2", OleDbType.VarChar){ Value=fs2},//0
                new OleDbParameter("@xc3", OleDbType.VarChar){ Value=xc3},//0
                new OleDbParameter("@fs3", OleDbType.VarChar){ Value=fs3},//0
                new OleDbParameter("@xc4", OleDbType.VarChar){ Value=xc4},//0
                new OleDbParameter("@yc", OleDbType.VarChar){ Value=string.Format("早:{0},中:{1}，晚:{2}",GetFoodDesc(day.Breakfast),GetFoodDesc(day.Lunch),GetFoodDesc(day.Dinner))},//0
                new OleDbParameter("@zs", OleDbType.VarChar){ Value=day.dayHotel},//0
                new OleDbParameter("@orederid", OleDbType.Integer){ Value=day.dayNumber},//0
                new OleDbParameter("@pid", OleDbType.VarChar){ Value=articleId},//0
                new OleDbParameter("@tjtime", OleDbType.Date){ Value=DateTime.Now.ToString("yyyy/MM/dd")},//0
                new OleDbParameter("@xcsm", OleDbType.VarChar){ Value=day.itineraryDay.text},//0
                new OleDbParameter("@zgts", OleDbType.Integer){ Value=0},//0
            };
                var result = op.ExecuteSql(inserrtXc, parms);
                if (result > 0)
                {
                    if (IsDebugger())
                    {
                        Logger.Info($"行程【{day.dayNumber}】插入成功");
                    }
                    return true;
                }
                else
                {
                    Logger.Error($"行程【{day.dayNumber}】插入失败");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"插入行程错误， 错误原因：{ex.ToString()}");
            }
            return false;
        }

        /// <summary>
        /// 更新行程
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static bool UpdateXc(int id, ItineraryDays day)
        {
            try
            {

                #region 行程和交通方式
                string xc1 = "", fs1 = "", xc2 = "", fs2 = "", xc3 = "", fs3 = "", xc4 = "";
                var traffics = day.itineraryDay.trafficName.Split('-');
                //法国小镇-汽车-卢塞恩-未知-因特拉肯-汽车-因特拉肯
                if (traffics.Length == 1)
                {
                    xc1 = traffics[0];
                }
                if (traffics.Length == 2)
                {
                    xc1 = traffics[0];
                    fs1 = GetTrafficId(traffics[1]);
                }
                if (traffics.Length == 3)
                {
                    xc1 = traffics[0];
                    fs1 = GetTrafficId(traffics[1]);
                    xc2 = traffics[2];
                }
                if (traffics.Length == 4)
                {
                    xc1 = traffics[0];
                    fs1 = GetTrafficId(traffics[1]);
                    xc2 = traffics[2];
                    fs2 = GetTrafficId(traffics[3]);
                }
                if (traffics.Length == 5)
                {
                    xc1 = traffics[0];
                    fs1 = GetTrafficId(traffics[1]);
                    xc2 = traffics[2];
                    fs2 = GetTrafficId(traffics[3]);
                    xc3 = traffics[4];
                }
                if (traffics.Length == 6)
                {
                    xc1 = traffics[0];
                    fs1 = GetTrafficId(traffics[1]);
                    xc2 = traffics[2];
                    fs2 = GetTrafficId(traffics[3]);
                    xc3 = traffics[4];
                    fs3 = GetTrafficId(traffics[5]);
                }
                if (traffics.Length == 7)
                {
                    xc1 = traffics[0];
                    fs1 = GetTrafficId(traffics[1]);
                    xc2 = traffics[2];
                    fs2 = GetTrafficId(traffics[3]);
                    xc3 = traffics[4];
                    fs3 = GetTrafficId(traffics[5]);
                    xc4 = traffics[6];
                }
                #endregion

                string updatexc = "update xcap set [xc1]=@xc1,[fs1]=@fs1,[xc2]=@xc2,[fs2]=@fs2,[xc3]=@xc3,[fs3]=@fs3,[xc4]=@xc4,[yc]=@yc,[zs]=@zs,[tjtime]=@tjtime,[xcsm]=@xcsm where id=@id";
                OleDbParameter[] parms = {
                new OleDbParameter("@xc1", OleDbType.VarChar){ Value=xc1},//0
                new OleDbParameter("@fs1", OleDbType.VarChar){ Value=fs1},//0
                new OleDbParameter("@xc2", OleDbType.VarChar){ Value=xc2},//0
                new OleDbParameter("@fs2", OleDbType.VarChar){ Value=fs2},//0
                new OleDbParameter("@xc3", OleDbType.VarChar){ Value=xc3},//0
                new OleDbParameter("@fs3", OleDbType.VarChar){ Value=fs3},//0
                new OleDbParameter("@xc4", OleDbType.VarChar){ Value=xc4},//0
                new OleDbParameter("@yc", OleDbType.VarChar){ Value=string.Format("早:{0},中:{1}，晚:{2}",GetFoodDesc(day.Breakfast),GetFoodDesc(day.Lunch),GetFoodDesc(day.Dinner))},//0
                new OleDbParameter("@zs", OleDbType.VarChar){ Value=day.dayHotel},//0
                //new OleDbParameter("@orederid", OleDbType.Integer){ Value=day.dayNumber},//0
                //new OleDbParameter("@pid", OleDbType.VarChar){ Value=articleId},//0
                new OleDbParameter("@tjtime", OleDbType.Date){ Value=DateTime.Now.ToString("yyyy/MM/dd")},//0
                new OleDbParameter("@xcsm", OleDbType.VarChar){ Value=day.itineraryDay.text},//0
                //new OleDbParameter("@zgts", OleDbType.Integer){ Value=0},//0
                new OleDbParameter("@id", OleDbType.Integer){ Value=id},//0
            };
                var result = op.ExecuteSql(updatexc, parms);
                if (result > 0)
                {
                    if (IsDebugger())
                    {
                        Logger.Info($"行程【{day.dayNumber}】更新成功");
                    }
                    return true;
                }
                else
                {
                    Logger.Error($"行程【{day.dayNumber}】更新失败");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"更新行程错误， 错误原因：{ ex.ToString()}");
            }
            return false;
        }

        /// <summary>
        /// 获取下一个线路编号
        /// </summary>
        /// <returns></returns>
        private static int GetNextArticleId()
        {
            string sql = "select tid+2 from d_table where tname=@tname";
            object result = op.GetSingle(sql, new OleDbParameter("@tname", "c_article"));
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// 更新下一个线路编号
        /// </summary>
        /// <returns></returns>
        private static int UpdateNextArticleId()
        {
            string sql = "update d_table set tid=tid+1 where tname=@tname";
            return op.ExecuteSql(sql, new OleDbParameter("@tname", "c_article"));
        }

        private static string GetTrafficId(string trafficName)
        {
            if (trafficName == "汽车")
                return "42";
            if (trafficName == "飞机")
                return "44";
            return "42";
        }
        private static string GetFoodDesc(string food)
        {
            if (string.IsNullOrEmpty(food))
            {
                return food;
            }
            else
            {
                if (food.Split('_').Length == 1)
                {
                    return food;
                }
                else
                {
                    return food.Split('_')[1];
                }
            }
        }

        /// <summary>
        /// 是否开启调试
        /// </summary>
        /// <returns></returns>
        private static bool IsDebugger()
        {
            string debugger = ConfigurationManager.AppSettings["Debugger"];
            if (!string.IsNullOrEmpty(debugger))
            {
                if (debugger.ToLower() == "true")
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 同步优耐得线路数据
        /// </summary>
        /// <returns></returns>
        public bool DataSync(int classId)
        {
            try
            {
                LineModel lines = LineXmlParse.ParseLineList();
                if (IsDebugger())
                {
                    Logger.Info($"共获取到{lines.line.Count}条线路，开始解析入库。。。");
                }
                int index = 0;
                if (lines != null && lines.line != null && lines.line.Count > 0)
                {
                    foreach (var item in lines.line)
                    {
                        index++;
                        if (IsDebugger())
                        {
                            Logger.Info($"开始同步第{index}条数据，线路编号:{item.lineCode}");
                        }
                        int articleId = 0;
                        try
                        {
                            var lineDetailModel = LineDetailXmlParse.ParseLineDetail(item.lineCode);
                            if (InsertArticle(classId, item, lineDetailModel, out articleId))
                            {
                                if (lineDetailModel != null && lineDetailModel.lineDays != null && lineDetailModel.lineDays.Count > 0)
                                {
                                    foreach (var day in lineDetailModel.lineDays)
                                    {
                                        InsertXc(articleId, day);
                                    }
                                }
                            }
                            if (IsDebugger())
                            {
                                Logger.Info($"结束同步第{index}条数据，线路编号:{item.lineCode}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error($"线路获取失败，线路编号:{item.lineCode},失败原因{ex.ToString()}");
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"远程获取数据同步失败,错误信息{ex.ToString()}");
            }
            return false;
        }
    }
}
