using System.Collections.Generic;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.Dtos;
using Volo.Ymapp.Kh10086;

namespace KH10086.WebApp.Models.Home
{
    public class HomeViewModel
    {
        /// <summary>
        /// 线路类型
        /// </summary>
        public List<CategoryDto> LineTypeList { get; set; }
        /// <summary>
        /// 线路国家
        /// </summary>
        public List<CategoryDto> LineCountryList { get; set; }
        /// <summary>
        /// 推荐列表
        /// </summary>
        public List<LineListDto> RecommendList { get; set; }
    }
}
