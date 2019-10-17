using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Ymapp.Dtos
{
    public class TreeDataDto
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 存储值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<TreeDataDto> Children { get; set; }
    }
}
