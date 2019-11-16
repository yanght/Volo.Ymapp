using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Ymapp.ApiTransfer
{
    public class HttpContextMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 构造 Http 请求中间件
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="cacheService"></param>
        public HttpContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request.Body;
            var response = context.Response.Body;

            string host = "https://tispapi.utourworld.com";

            if (context.Request.Method == "POST")
            {
                HttpClient httpClient = new HttpClient();//http对象
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string requestBody = string.Empty;
                using (var reader = new StreamReader(request))
                {
                    requestBody = await reader.ReadToEndAsync();
                }
                HttpContent content = new StringContent(requestBody);

                var result = await httpClient.PostAsync(host + context.Request.Path, content);

                string tresponse = await result.Content.ReadAsStringAsync();

                using (var writer = new StreamWriter(response, Encoding.UTF8))
                {
                    await writer.WriteAsync(tresponse);
                    await writer.FlushAsync();
                }

                context.Response.Body = response;
            }

            // 不处理任何 request, 直接调用下一个中间件
            await _next.Invoke(context);
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpContextMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpContextMiddleware>();
        }
    }
}
