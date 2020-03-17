using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq;
using Volo.Abp;

namespace Volo.Ymapp.Products
{
    public class ProductPropertyAppService : ApplicationService, IProductPropertyAppService
    {

        private IRepository<ProductProperty, long> repository_ProductProperty { get; set; }
        private IRepository<PropertyName, long> repository_PropertyName { get; set; }
        private IRepository<PropertyValue, long> repository_PropertyValue { get; set; }
        public ProductPropertyAppService(
        IRepository<ProductProperty, long> _repository_ProductProperty,
        IRepository<PropertyName, long> _repository_PropertyName,
        IRepository<PropertyValue, long> _repository_PropertyValue
            )
        {
            repository_ProductProperty = _repository_ProductProperty;
            repository_PropertyName = _repository_PropertyName;
            repository_PropertyValue = _repository_PropertyValue;
        }

        public async Task CreateCateporyProperty(CreateCateporyPropertyDto input)
        {
            var propertyName = await repository_PropertyName.InsertAsync(new PropertyName() { CategoryId = input.CategoryId, Title = input.PropertyName }, true);
            var propertyValue = await repository_PropertyValue.InsertAsync(new PropertyValue() { PropertyNameId = propertyName.Id, Value = input.PropertyValue });
        }

        public async Task CreatePropertyName(CreatePropertyNameDto input)
        {
            if (repository_PropertyName.Count(m => m.Title == input.Title) > 0)
            {
                throw new UserFriendlyException("属性名已存在");
            }
            await repository_PropertyName.InsertAsync(new PropertyName() { CategoryId = input.CategoryId, Title = input.Title });
        }

        public async Task CreatePropertyValue(CreatePropertyValueDto input)
        {
            if (repository_PropertyValue.Count(m => m.PropertyNameId == input.PropertyNameId && m.Value == input.Value) > 0)
            {
                throw new UserFriendlyException("此属性值已存在");
            }
            await repository_PropertyValue.InsertAsync(new PropertyValue() { PropertyNameId = input.PropertyNameId, Value = input.Value, ImageUrl = input.ImageUrl });
        }

        public async Task DeletePropertyValue(long id)
        {
            await repository_PropertyValue.DeleteAsync(id);
        }

        public async Task DeletePropertyName(long id)
        {
            await repository_PropertyName.DeleteAsync(id);
        }

        public List<PropertyListDto> GetPropertyList(GetPropertyListDto input)
        {
            List<PropertyListDto> list = new List<PropertyListDto>();
            var propertyNames = repository_PropertyName.Where(m => m.CategoryId == input.CategoryId).ToList();
            foreach (var item in propertyNames)
            {
                var propertyValue = repository_PropertyValue.Where(m => m.PropertyNameId == item.Id).ToList();
                PropertyListDto model = ObjectMapper.Map<PropertyName, PropertyListDto>(item);
                model.PropertyValues = ObjectMapper.Map<List<PropertyValue>, List<PropertyValueDto>>(propertyValue);
                list.Add(model);
            }
            return list;
        }
    }
}
