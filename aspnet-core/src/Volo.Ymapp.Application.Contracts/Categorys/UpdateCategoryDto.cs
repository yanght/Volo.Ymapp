using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Categorys
{
    public class UpdateCategoryDto : CreateUpdateCategoryDtoBase, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}
