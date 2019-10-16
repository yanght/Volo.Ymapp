using System;
using System.Collections.Generic;
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
        public CategoryType Type { get; set; } = CategoryType.Undefined;
        [Required]
        public Guid ParentId { get; set; }
        [Required]
        public int Sort { get; set; }
    }
}
