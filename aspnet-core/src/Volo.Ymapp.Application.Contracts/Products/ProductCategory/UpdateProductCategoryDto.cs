﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Products
{
    public class UpdateProductCategoryDto : CreateUpdateProductCategoryDtoBase, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}
