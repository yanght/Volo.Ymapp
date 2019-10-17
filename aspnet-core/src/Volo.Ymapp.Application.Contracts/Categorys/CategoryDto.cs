using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Categorys
{
    public class CategoryDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public CategoryType Type { get; set; }
        public string Name { get; set; }
        public Guid ParentId { get; set; }
        public int Sort { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
