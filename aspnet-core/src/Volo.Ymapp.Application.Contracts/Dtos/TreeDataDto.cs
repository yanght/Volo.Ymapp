using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Ymapp.Dtos
{
    public class TreeDataDto
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public string Key { get; set; }
        public List<TreeDataDto> Children { get; set; }
    }
}
