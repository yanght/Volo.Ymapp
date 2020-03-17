using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Ymapp.Products;

namespace Volo.Ymapp.AutoMapperProfile
{
    public class ProductPropertyProfile : Profile
    {
        public ProductPropertyProfile()
        {
            CreateMap<PropertyValue, PropertyValueDto>();
            CreateMap<PropertyName, PropertyNameDto>();
            CreateMap<PropertyName, PropertyListDto>();
        }
    }
}
