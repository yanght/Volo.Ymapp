using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Ymapp.Products
{
    public class CreateCateporyPropertyDto
    {
        public long CategoryId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }
}
