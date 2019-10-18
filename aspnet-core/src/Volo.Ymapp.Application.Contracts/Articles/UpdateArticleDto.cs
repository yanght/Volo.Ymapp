using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Volo.Ymapp.Articles
{
    public class UpdateArticleDto : CreateUpdateArticleBaseDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}
