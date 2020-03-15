using Volo.Abp.Domain.Entities.Auditing;

namespace Volo.Ymapp.Products
{
    public class PropertyValue: FullAuditedAggregateRoot<long>
    {
        /// <summary>
        /// 属性名称编号
        /// </summary>
        public long PropertyNameId { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 属性图片
        /// </summary>
        public string ImageUrl { get; set; }
    }
}