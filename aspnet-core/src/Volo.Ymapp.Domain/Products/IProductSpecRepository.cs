﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace Volo.Ymapp.Products
{
    public interface IProductSpecRepository : IBasicRepository<ProductSpec, Guid>
    {
    }
}