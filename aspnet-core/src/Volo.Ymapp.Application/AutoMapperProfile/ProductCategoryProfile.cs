using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Ymapp.Products;

namespace Volo.Ymapp.AutoMapperProfile
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<ProductCategory, ProductCategoryTreeDto>();
            CreateMap<CreateProductCategoryDto, ProductCategory>();
            CreateMap<UpdateProductCategoryDto, ProductCategory>();
        }
    }
}