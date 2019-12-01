using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Volo.Ymapp.Kh10086
{
    public interface ILineAppService : IApplicationService
    {

        Task ParseLineData(ParseLineDataDto dto);

        Task InsertLine(LineDto dto);
    }
}
