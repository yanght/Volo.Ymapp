﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Ymapp.Products
{
    public class CreateProductDto : CreateUpdateProductDtoBase
    {
        public List<string> ProductImages { get; set; }
    }
}
