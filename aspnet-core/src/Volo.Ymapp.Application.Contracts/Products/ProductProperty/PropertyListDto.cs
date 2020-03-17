using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Ymapp.Products
{
    public class GetPropertyListDto
    {
        public long CategoryId { get; set; }
    }
    public class PropertyListDto : PropertyNameDto
    {
        public List<PropertyValueDto> PropertyValues { get; set; }
    }
}
