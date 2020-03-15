using Volo.Abp.Domain.Entities.Auditing;

namespace Volo.Ymapp.Products
{
    public class ProductCategory: FullAuditedAggregateRoot<long>
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 上级分类Id
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}