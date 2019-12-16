using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Ymapp.Utils;

namespace Volo.Ymapp.Kh10086
{
    public class UTourApiClient
    {
        public static string Host { get; set; }
        public static string VisitCode { get; set; }
        public static string SignKey { get; set; }
        public static string UserCode { get; set; }
        public static string Token { get; set; }
        public static string AccpCode { get; set; }
        public UTourApiClient(string host, string visitCode, string signKey, string userCode, string token, string accpCode)
        {

            Host = host;
            VisitCode = visitCode;
            SignKey = signKey;
            UserCode = userCode;
            Token = token;
            AccpCode = accpCode;
        }

        //private static readonly string host = ConfigurationManager.AppSettings["UtourApiUrl"];
        //private static readonly string visitCode = ConfigurationManager.AppSettings["visitCode"];
        //private static readonly string signKey = ConfigurationManager.AppSettings["signKey"];
        //private static readonly string userCode = ConfigurationManager.AppSettings["userCode"];
        //private static readonly string token = ConfigurationManager.AppSettings["token"];
        //private static readonly string accpCode = ConfigurationManager.AppSettings["accpCode"];


        /// <summary>
        /// 分销用户公共验证
        /// </summary>
        private const string getUserLoginVerificationUrl = "api/ag/agentForeignApi/getUserLoginVerification";
        /// <summary>
        /// 产品列表
        /// </summary>
        /// <summary>
        /// 查询产品实时信息接口
        /// </summary>
        private const string getTeamInfoByCodeOrIdUrl = "api/ag/agentForeignApi/getTeamInfoByCodeOrId";
        /// <summary>
        /// 实时库存查询接口
        /// </summary>
        private const string getRealTimeTeamStockNumUrl = "api/ag/agentForeignApi/getRealTimeTeamStockNum";
        /// <summary>
        /// 实时价格查询接口
        /// </summary>
        private const string getRealTimeTeamPriceUrl = "api/ag/agentForeignApi/getRealTimeTeamPrice";

        /// <summary>
        /// 分销外部创建订单接口
        /// </summary>
        private const string createAgentOrderApiUrl = "api/ag/agentForeignApi/createAgentOrderApi";
        /// <summary>
        /// 使用编码查询订单状态
        /// </summary>
        private const string getOrderInfoByOrderCodeUrl = "api/ag/agentForeignApi/getOrderInfoByOrderCode";


        public  string getUserLoginVerification()
        {
            RequestModel request = new RequestModel();
            request.visitCode = VisitCode;
            request.key = MD5Help.Get32MD5(request.date + SignKey).ToLower();
            request.param = new { userCode = UserCode, token = Token, objStr = MD5Help.Get32MD5($"{UserCode}|{Token}") };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.HttpPost(Host + getUserLoginVerificationUrl, jsondata, "application/json");
            return response;
        }

        /// <summary>
        /// 查询产品实时信息接口
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public  ProductDetail getTeamInfoByCodeOrId(string productCode, string teamId)
        {
            RequestModel request = new RequestModel();
            request.visitCode = VisitCode;
            request.key = MD5Help.Get32MD5(request.date + SignKey).ToLower();
            request.param = new { userCode = UserCode, token = Token, objStr = MD5Help.Get32MD5($"{UserCode}|{Token}"), productCode = productCode, teamId = teamId };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.HttpPost(Host + getTeamInfoByCodeOrIdUrl, jsondata, "application/json");
            JObject obj = JObject.Parse(response);
            if (obj["code"].ToString() == "0")
            {
                var data = obj["data"];
                return JsonConvert.DeserializeObject<ProductDetail>(data.ToString());
            }
            return null;
        }

        public  LineStock getRealTimeTeamStockNum(string productCode, string teamId)
        {
            RequestModel request = new RequestModel();
            request.visitCode = VisitCode;
            request.key = MD5Help.Get32MD5(request.date + SignKey).ToLower();
            request.param = new { userCode = UserCode, token = Token, objStr = MD5Help.Get32MD5($"{UserCode}|{Token}"), productCode = productCode, teamId = teamId };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.HttpPost(Host + getRealTimeTeamStockNumUrl, jsondata, "application/json");
            JObject obj = JObject.Parse(response);
            if (obj["code"].ToString() == "0")
            {
                var data = obj["data"];
                return JsonConvert.DeserializeObject<List<LineStock>>(data.ToString()).FirstOrDefault();
            }
            return null;
        }

        public  LinePrice getRealTimeTeamPrice(string productCode, string teamId)
        {
            RequestModel request = new RequestModel();
            request.visitCode = VisitCode;
            request.key = MD5Help.Get32MD5(request.date + SignKey).ToLower();
            request.param = new { userCode = UserCode, token = Token, objStr = MD5Help.Get32MD5($"{UserCode}|{Token}"), productCode = productCode, teamId = teamId };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.HttpPost(Host + getRealTimeTeamPriceUrl, jsondata, "application/json");
            JObject obj = JObject.Parse(response);
            if (obj["code"].ToString() == "0")
            {
                var data = obj["data"];
                return JsonConvert.DeserializeObject<List<LinePrice>>(data.ToString()).FirstOrDefault();
            }
            return null;
        }

        /// <summary>
        /// 分销外部创建订单接口
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="userId"></param>
        /// <param name="custName"></param>
        /// <param name="sex"></param>
        /// <param name="accpCode"></param>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        public  string createAgentOrderApi(string teamId, string userId, string custName, string sex, string orderDetail)
        {

            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string key = MD5Help.Get32MD5(date + SignKey).ToLower();
            var request = new
            {
                date = date,
                key = key,
                visitCode = VisitCode,
                userCode = UserCode,
                token = Token,
                objStr = MD5Help.Get32MD5($"{UserCode}|{Token}"),
                teamId = teamId,
                userId = userId,
                custName = custName,
                sex = sex,
                accpCode = AccpCode,
                orderDetail = orderDetail
            };

            var jsondata = JsonConvert.SerializeObject(request);

            string response = HttpClientHelper.HttpPost(Host + createAgentOrderApiUrl, jsondata, "application/json");

            JObject obj = JObject.Parse(response);
            if (obj["code"].ToString() == "0")
            {
                var data = obj["data"];
                return data.ToString().Split('|')[1];
            }
            return string.Empty;
        }

        /// <summary>
        /// 使用编码查询订单状态
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public  OrderState getOrderInfoByOrderCode(string orderCode)
        {
            RequestModel request = new RequestModel();
            request.visitCode = VisitCode;
            request.key = MD5Help.Get32MD5(request.date + SignKey).ToLower();
            request.param = new { userCode = UserCode, token = Token, objStr = MD5Help.Get32MD5($"{UserCode}|{Token}"), orderCode = orderCode };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.HttpPost(Host + getOrderInfoByOrderCodeUrl, jsondata, "application/json");
            JObject obj = JObject.Parse(response);
            if (obj["code"].ToString() == "0")
            {
                var data = obj["data"];
                return JsonConvert.DeserializeObject<OrderState>(data.ToString());
            }
            return null;
        }

    }

    /// <summary>
    /// 实时价格
    /// </summary>
    public class LinePrice
    {
        /// <summary>
        /// 零售价
        /// </summary>
        public decimal priceRetail { get; set; }
        /// <summary>
        /// 同业价-零售价
        /// </summary>
        public decimal tradePrice { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal childPrice { get; set; }
        /// <summary>
        /// 境外参团价-零售价
        /// </summary>
        public decimal overseasTourPrice { get; set; }
        /// <summary>
        /// 单间差
        /// </summary>
        public decimal singleRoomDifference { get; set; }
    }

    /// <summary>
    /// 实时库存
    /// </summary>
    public class LineStock
    {
        /// <summary>
        /// 余位数 （当前余位大于9时，返回最大可收人数为9）
        /// </summary>
        public int numFree { get; set; }
        /// <summary>
        /// 预收数
        /// </summary>
        public int numPlan { get; set; }
    }

    public class ProductDetail
    {
        /// <summary>
        /// 团队Id
        /// </summary>
        public string teamId { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 行程目的地
        /// </summary>
        public string continent { get; set; }
        /// <summary>
        /// 出发地
        /// </summary>
        public string placeLeave { get; set; }
        /// <summary>
        /// 返回地
        /// </summary>
        public string placeRetrun { get; set; }
        /// <summary>
        /// 服务开始日期
        /// </summary>
        public string dateSart { get; set; }
        /// <summary>
        /// 服务结束日期
        /// </summary>
        public string dateFinish { get; set; }
        /// <summary>
        /// 终端售卖开始日期
        /// </summary>
        public string dateOnline { get; set; }
        /// <summary>
        /// 终端售卖结束日期
        /// </summary>
        public string dateOffline { get; set; }
        /// <summary>
        /// 航空公司
        /// </summary>
        public string airlineCompany { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string CompName { get; set; }

    }
    public class RequestModel
    {
        public string visitCode { get; set; }
        public string key { get; set; }
        public string date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public object param { get; set; }
    }
    public class CreateOrderRequest : RequestModel
    {
        public string teamId { get; set; }
        public string userId { get; set; }
        public string custName { get; set; }
        public string sex { get; set; }
        public string accpCode { get; set; }
        public string orderDetail { get; set; }
    }

    public class OrderState
    {
        /// <summary>
        /// 创建订单时间
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 订单编码
        /// </summary>
        public string orderCode { get; set; }
        /// <summary>
        /// 订单状态0：已取消，1：已确认，2：占位中
        /// </summary>
        public string orderState { get; set; }
    }
}
