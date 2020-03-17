using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Volo.Ymapp.Products
{
    public interface IProductPropertyAppService : IApplicationService
    {
        Task CreateCateporyProperty(CreateCateporyPropertyDto input);

        Task CreatePropertyName(CreatePropertyNameDto input);
        Task CreatePropertyValue(CreatePropertyValueDto input);
        Task DeletePropertyValue(long id);
        Task DeletePropertyName(long id);
        List<PropertyListDto> GetPropertyList(GetPropertyListDto input);

    }
}
