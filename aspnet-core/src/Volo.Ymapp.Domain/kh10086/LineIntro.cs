using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Volo.Ymapp.kh10086
{
    public class LineIntro : FullAuditedAggregateRoot<long>
    {
        /// <summary>
        /// 线路Id
        /// </summary>
        public long LineId { get; set; }
        /// <summary>
        /// 模块名称
        /// 贴心好礼 行程特色 酒店安排 团队餐食 交通工具 相关网站 保险条款 最低成团人数 服务项目 不含项目 服务标准说明 温馨提示 购物补充说明 自费项目补充说明 活动图片附加条款 促销语 子行程电商名称 签证须知 预定须知 行程亮点 一般精包配图 秒杀精包配图 每单购买人数 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 模块类型和位置 
        /// 产品之前：封面图，,产品前 
        /// 产品之后：产品后，自费，商店
        /// </summary>
        public string ChannelType { get; set; }
        /// <summary>
        /// 模块文本数据
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 排序号 也可作为识别号
        /// </summary>
        public int OrderNum { get; set; }
    }
}
