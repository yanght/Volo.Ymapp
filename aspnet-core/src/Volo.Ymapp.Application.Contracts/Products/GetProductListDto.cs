using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Ymapp.CommonEnum;

namespace Volo.Ymapp.Products
{
    public class GetProductListDto : PagedAndSortedResultRequestDto
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid? CategoryId { get; set; }
        public ProductState? State { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
