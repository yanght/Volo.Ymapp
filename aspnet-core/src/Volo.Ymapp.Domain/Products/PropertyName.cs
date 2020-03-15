using Volo.Abp.Domain.Entities.Auditing;

namespace Volo.Ymapp.Products
{
    public class PropertyName: FullAuditedAggregateRoot<long>
    {
        /// <summary>
        /// 分类编号
        /// </summary>
        public long CategoryId { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Title { get; set; }
        
    }
}