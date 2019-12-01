using System;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Serilog;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Ymapp.Kh10086;
using Volo.Ymapp.TaskScheduler.Utour;

namespace Volo.Ymapp.TaskScheduler.Jobs
{
    public class ParseLineJob : IJob,ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private ILineAppService _lineApp;

        public ParseLineJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _lineApp = _serviceProvider.GetService<ILineAppService>();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            Log.Information("线路解析任务开始...");
            try
            {
                string url = "https://tispfile.utourworld.com/upload/op/xml/agentLine/index.xml";
                string linedetail = "https://tispfile.utourworld.com/upload/op/xml/agentLine/{0}.xml";
                XmlDocument doc = LineXmlParse.ParseLineList(url);
                var lineList = LineXmlParse.GetLine(linedetail, doc.SelectNodes("routes/line"));
                if (lineList != null && lineList.Count > 0)
                {
                    lineList.ForEach(async (line) =>
                    {
                        await _lineApp.InsertLine(line);
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Information($"线路解析任务失败,{ex.ToString()}");
            }
            Log.Information("线路解析任务结束...");
            await Task.FromResult(true);
        }
    }
}
