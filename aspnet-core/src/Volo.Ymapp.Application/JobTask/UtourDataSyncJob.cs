using System;
using Newtonsoft.Json;
using Serilog;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Ymapp.Kh10086;

namespace Volo.Ymapp.JobTask
{
    public class UtourDataSyncJob : BackgroundJob<UtourDataParseArgs>, ITransientDependency
    {
        private ILineAppService _lineApp;
        public UtourDataSyncJob(ILineAppService lineApp)
        {
            _lineApp = lineApp;
        }
        public override void Execute(UtourDataParseArgs args)
        {
            try
            {
                Log.Information($"开始执行优耐得数据同步任务。。。,参数:{JsonConvert.SerializeObject(args)}");
                _lineApp.ParseLineData(new ParseLineDataDto()
                {
                    LineDetailUrl = args.LineDetailUrl,
                    LineListUrl = args.LineListUrl,
                    UtourApiUrl = args.UtourApiUrl
                }).GetAwaiter().GetResult();
                Log.Information($"结束执行优耐得数据同步任务。。。");
            }
            catch (Exception ex)
            {
                Log.Error($"线路数据同步失败，{ex.ToString()}");
            }
        }
    }
}
