using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Volo.Ymapp.Articles
{
    public interface IArticleRepository : IBasicRepository<Article, Guid>
    {
        Task<List<Article>> GetArticleList(string title,string author,Guid categoryId,DateTime startTime,DateTime endTime,int skipCount,int maxResultCount);
    }
}
