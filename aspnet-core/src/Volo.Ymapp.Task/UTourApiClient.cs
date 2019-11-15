using System;
using Newtonsoft.Json;

namespace Volo.Ymapp.Task
{
    public static class UTourApiClient
    {
        private static readonly string host = "https://tispapitest.utourworld.com/";
        private static readonly string visitCode = "HMYD";
        private static readonly string signKey = "ff316yyeafaxd53ba033x06x11e7xade";
        private static readonly string userCode = "AGENT201908091643194";
        private static readonly string token = "agentTest ";

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


        public static string getUserLoginVerification()
        {
            RequestModel request = new RequestModel();
            request.visitCode = visitCode;
            request.key = MD5Help.Get16MD5(request.date + signKey).ToLower();
            request.param = new { userCode = visitCode, token = token, objStr = MD5Help.Get32MD5($"{userCode}|{token}") };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.Post(host + getUserLoginVerificationUrl, jsondata);
            return response;
        }

        public static string productList(string productCode, string teamId)
        {
            RequestModel request = new RequestModel();
            request.visitCode = visitCode;
            request.key = MD5Help.Get16MD5(request.date + signKey).ToLower();
            request.param = new { userCode = visitCode, token = token, objStr = MD5Help.Get32MD5($"{userCode}|{token}"), productCode = productCode, teamId = teamId };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.Post(host + getTeamInfoByCodeOrIdUrl, jsondata);
            return response;
        }

        public static string getRealTimeTeamStockNum(string productCode, string teamId)
        {
            RequestModel request = new RequestModel();
            request.visitCode = visitCode;
            request.key = MD5Help.Get16MD5(request.date + signKey).ToLower();
            request.param = new { userCode = visitCode, token = token, objStr = MD5Help.Get32MD5($"{userCode}|{token}"), productCode = productCode, teamId = teamId };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.Post(host + getRealTimeTeamStockNumUrl, jsondata);
            return response;
        }

        public static string getRealTimeTeamPrice(string productCode, string teamId)
        {
            RequestModel request = new RequestModel();
            request.visitCode = visitCode;
            request.key = MD5Help.Get16MD5(request.date + signKey).ToLower();
            request.param = new { userCode = visitCode, token = token, objStr = MD5Help.Get32MD5($"{userCode}|{token}"), productCode = productCode, teamId = teamId };
            var jsondata = JsonConvert.SerializeObject(request);
            string response = HttpClientHelper.Post(host + getRealTimeTeamPriceUrl, jsondata);
            return response;
        }


    }
}
