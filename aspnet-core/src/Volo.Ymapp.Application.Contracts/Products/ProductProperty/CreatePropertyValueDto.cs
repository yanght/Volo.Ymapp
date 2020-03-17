using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Ymapp.Products
{
    public class CreatePropertyValueDto
    {
        public long PropertyNameId { get; set; }
        public string Value { get; set; }
        public string ImageUrl { get; set; }
    }
}
