using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.Dtos;

namespace KH10086.WebApp.Models.Partial
{
    public class NavigationViewModel
    {
        public List<TreeDataDto> CountryCategorys { get; set; }
        /// <summary>
        /// 线路类型
        /// </summary>
        public List<CategoryDto> LineTypeList { get; set; }
    }
}
