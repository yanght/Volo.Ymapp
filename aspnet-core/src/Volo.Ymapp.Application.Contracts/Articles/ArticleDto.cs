﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Ymapp.Categorys;

namespace Volo.Ymapp.Articles
{
    public class ArticleDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 分类编号
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public CategoryDto Category { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool Recommend { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        public string PictureUrl { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string MainContent { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
