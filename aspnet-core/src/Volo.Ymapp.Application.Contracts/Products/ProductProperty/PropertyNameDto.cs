using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Products
{
    public class PropertyNameDto : AuditedEntityDto<long>, IHasConcurrencyStamp
    {
        public long CategoryId { get; set; }
        public string Title { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
