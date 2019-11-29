using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using KH10086.Service.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KH10086.Service.Utour
{
    public static class UTourApiClient
    {
        private static readonly string host = ConfigurationManager.AppSettings["UtourApiUrl"];
        private static readonly string visitCode = ConfigurationManager.AppSettings["visitCode"];
        private static readonly string signKey = ConfigurationManager.AppSettings["signKey"];
        private static readonly string userCode = ConfigurationManager.AppSettings["userCode"];
        private static readonly string token = ConfigurationManager.AppSettings["token"];
        private static readonly string accpCode = ConfigurationManager.AppSettings["accpCode"];

        /// <summary>
        /// 分销用户公共验证
        /// </summary>
        private static readonly string getUserLoginVerificationUrl = "api/ag/agentForeignApi/getUserLoginVerification";
        /// <summary>
        /// 产品列表
        /// </summary>
        private static readonly string productListUrl = "upload/op/xml/agentLine/index.xml";
        /// <summary>
        /// 产品详情
        /// </summary>
        private static readonly string productDetailUrl = "upload/op/xml/agentLine/{0}.xml";
        /// <summary>
        /// 查询产品实时信息接口
        /// </summary>
        private static readonly string getTeamInfoByCodeOrIdUrl = "api/ag/agentForeignApi/getTeamInfoByCodeOrId";
        /// <summary>
        /// 实时库存查询接口
        /// </summary>
        private static readonly string getRealTimeTeamStockNumUrl = "api/ag/agentForeignApi/getRealTimeTeamStockNum";
        /// <summary>
        /// 实时价格查询接口
        /// </summary>
        private static readonly string getRealTimeTeamPriceUrl = "api/ag/agentForeignApi/getRealTimeTeamPrice";

        /// <summary>
        /// 分销外部创建订单接口
        /// </summary>
        private static readonly string createAgentOrderApiUrl = "api/ag/agentForeignApi/createAgentOrderApi";
        /// <summary>
        /// 使用编码查询订单状态
        /// </summary>
        private static readonly string getOrderInfoByOrderCodeUrl = "api/ag/agentForeignApi/getOrderInfoByOrderCode";


        public static string getUserLoginVerification()
        {
            RequestModel request = new RequestModel();
            request.visitCode = visitCode;
            request.key = MD5Help.Get32MD5(request.date + signKey).ToLower();
            request.param = new { userCode = userCode, token = token, objStr = MD5Help.Get32MD5($"{userCode}|{token}") };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.HttpPost(host + getUserLoginVerificationUrl, jsondata, "application/json");
            return response;
        }

        /// <summary>
        /// 查询产品实时信息接口
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public static ProductDetail getTeamInfoByCodeOrId(string productCode, string teamId)
        {
            RequestModel request = new RequestModel();
            request.visitCode = visitCode;
            request.key = MD5Help.Get32MD5(request.date + signKey).ToLower();
            request.param = new { userCode = userCode, token = token, objStr = MD5Help.Get32MD5($"{userCode}|{token}"), productCode = productCode, teamId = teamId };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.HttpPost(host + getTeamInfoByCodeOrIdUrl, jsondata, "application/json");
            JObject obj = JObject.Parse(response);
            if (obj["code"].ToString() == "0")
            {
                var data = obj["data"];
                return JsonConvert.DeserializeObject<ProductDetail>(data.ToString());
            }
            return null;
        }

        public static LineStock getRealTimeTeamStockNum(string productCode, string teamId)
        {
            RequestModel request = new RequestModel();
            request.visitCode = visitCode;
            request.key = MD5Help.Get32MD5(request.date + signKey).ToLower();
            request.param = new { userCode = userCode, token = token, objStr = MD5Help.Get32MD5($"{userCode}|{token}"), productCode = productCode, teamId = teamId };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.HttpPost(host + getRealTimeTeamStockNumUrl, jsondata, "application/json");
            JObject obj = JObject.Parse(response);
            if (obj["code"].ToString() == "0")
            {
                var data = obj["data"];
                return JsonConvert.DeserializeObject<List<LineStock>>(data.ToString()).FirstOrDefault();
            }
            return null;
        }

        public static LinePrice getRealTimeTeamPrice(string productCode, string teamId)
        {
            RequestModel request = new RequestModel();
            request.visitCode = visitCode;
            request.key = MD5Help.Get32MD5(request.date + signKey).ToLower();
            request.param = new { userCode = userCode, token = token, objStr = MD5Help.Get32MD5($"{userCode}|{token}"), productCode = productCode, teamId = teamId };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.HttpPost(host + getRealTimeTeamPriceUrl, jsondata, "application/json");
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
        public static string createAgentOrderApi(string teamId, string userId, string custName, string sex, string orderDetail)
        {

            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string key = MD5Help.Get32MD5(date + signKey).ToLower();
            var request = new
            {
                date = date,
                key = key,
                visitCode = visitCode,
                userCode = userCode,
                token = token,
                objStr = MD5Help.Get32MD5($"{userCode}|{token}"),
                teamId = teamId,
                userId = userId,
                custName = custName,
                sex = sex,
                accpCode = accpCode,
                orderDetail = orderDetail
            };

            var jsondata = JsonConvert.SerializeObject(request);

            string response = HttpClientHelper.HttpPost(host + createAgentOrderApiUrl, jsondata, "application/json");

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
        public static OrderState getOrderInfoByOrderCode(string orderCode)
        {
            RequestModel request = new RequestModel();
            request.visitCode = visitCode;
            request.key = MD5Help.Get32MD5(request.date + signKey).ToLower();
            request.param = new { userCode = userCode, token = token, objStr = MD5Help.Get32MD5($"{userCode}|{token}"), orderCode = orderCode };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.HttpPost(host + getOrderInfoByOrderCodeUrl, jsondata, "application/json");
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
        public double priceRetail { get; set; }
        /// <summary>
        /// 同业价-零售价
        /// </summary>
        public double tradePrice { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public double childPrice { get; set; }
        /// <summary>
        /// 境外参团价-零售价
        /// </summary>
        public double overseasTourPrice { get; set; }
        /// <summary>
        /// 单间差
        /// </summary>
        public double singleRoomDifference { get; set; }
    }

    /// <summary>
    /// 实时库存
    /// </summary>
    public class LineStock
    {
        /// <summary>
        /// 余位数 （当前余位大于9时，返回最大可收人数为9）
        /// </summary>
        public string numFree { get; set; }
        /// <summary>
        /// 预收数
        /// </summary>
        public string numPlan { get; set; }
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
