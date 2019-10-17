using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Categorys
{
    public class CreateUpdateCategoryDtoBase
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        public int Type { get; set; } = 0;

        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid ParentId { get; set; }
        [Required]
        public int Sort { get; set; }
    }
}
