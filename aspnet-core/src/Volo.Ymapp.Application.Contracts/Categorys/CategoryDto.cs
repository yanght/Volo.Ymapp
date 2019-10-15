using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Volo.Ymapp.Categorys
{
    public class CategoryDto : AuditedEntityDto<Guid>
    {
        public CategoryType Type { get; set; }
        public string Name { get; set; }
        public Guid ParentId { get; set; }
        public int Sort { get; set; }
    }
}
