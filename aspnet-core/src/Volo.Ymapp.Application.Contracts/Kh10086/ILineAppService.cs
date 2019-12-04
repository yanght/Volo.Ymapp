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
        void ParseLineData(ParseLineDataDto dto);

        Task InsertOrUpdateLine(LineDto dto);

        Task<LineDto> GetLineByLineId(long lineId);
        Task<LineDto> GetLineByLineCode(string lineCode);
        Task<LineDto> GetLineByProductCode(string productCode);
    }
}
