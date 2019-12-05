using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Ymapp.Kh10086;

namespace Volo.Ymapp.JobTask
{
    public class BackgroundServiceService : ApplicationService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;

        public BackgroundServiceService(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
        }

        /// <summary>
        /// 添加优耐德数据解析任务
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task ParseUtourDataAsync(ParseLineDataDto dto)
        {
            await _backgroundJobManager.EnqueueAsync(
            new UtourDataParseArgs
            {
                LineDetailUrl = dto.LineDetailUrl,
                UtourApiUrl = dto.UtourApiUrl,
                LineListUrl = dto.LineListUrl
            }
        );
        }

        /// <summary>
        /// 同步线路分类
        /// </summary>
        /// <returns></returns>
        public async Task CategorySync()
        {
            await _backgroundJobManager.EnqueueAsync(new CategorySyncArgs()
            {

            });
        }
    }
}
