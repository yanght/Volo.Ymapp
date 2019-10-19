using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Volo.Ymapp.Articles
{
    public class GetArtticlesDto: PagedAndSortedResultRequestDto
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool? Recommend { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
