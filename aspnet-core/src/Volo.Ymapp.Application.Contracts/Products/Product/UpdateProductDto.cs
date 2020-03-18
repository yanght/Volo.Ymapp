using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Products
{
    public class UpdateProductDto : CreateUpdateProductDtoBase, IHasConcurrencyStamp
    {
        public List<string> ProductImages { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
