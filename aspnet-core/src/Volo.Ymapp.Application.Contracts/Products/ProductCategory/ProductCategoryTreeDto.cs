using System;
using System.Collections.Generic;
using System.Text;
using Volo.Ymapp.Products;

namespace Volo.Ymapp.Products
{
    public class ProductCategoryTreeDto : ProductCategoryDto
    {
        public List<ProductCategoryTreeDto> Children { get; set; }
    }
}
