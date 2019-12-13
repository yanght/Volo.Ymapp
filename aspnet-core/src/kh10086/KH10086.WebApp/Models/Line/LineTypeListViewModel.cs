using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.Kh10086;

namespace KH10086.WebApp.Models.Line
{
    public class LineTypeListViewModel
    {
        public CategoryDto Category { get; set; }
        public long TotalCount { get; set; }
        public List<LineListDto> Lines { get; set; }
    }
}
