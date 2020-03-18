using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Ymapp.Products;

namespace Volo.Ymapp.AutoMapperProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<ProductDto, ProductDetailDto>();
        }
    }
}
