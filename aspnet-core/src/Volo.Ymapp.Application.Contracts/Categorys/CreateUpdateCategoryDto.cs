using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Volo.Ymapp.Categorys
{
    public class CreateUpdateCategoryDto
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
