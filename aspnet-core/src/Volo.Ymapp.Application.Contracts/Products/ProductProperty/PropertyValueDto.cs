using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Products
{
    public class PropertyValueDto : AuditedEntityDto<long>, IHasConcurrencyStamp
    {
        public long PropertyNameId { get; set; }
        public string Value { get; set; }
        public string ImageUrl { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
