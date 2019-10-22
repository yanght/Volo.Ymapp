using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Ymapp.Articles;

namespace Volo.Ymapp.EntityFrameworkCore
{
    public class EfCoreArticleRepository : EfCoreRepository<YmappDbContext, Article, Guid>, IArticleRepository
    {
        public EfCoreArticleRepository(IDbContextProvider<YmappDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }

        public virtual async Task<List<Article>> GetArticleList(string title, string author, Guid categoryId, DateTime startTime, DateTime endTime, int skipCount, int maxResultCount)
        {
            return await DbSet
                   .WhereIf(!title.IsNullOrWhiteSpace(), m => m.Title.Contains(title))
                   .WhereIf(!author.IsNullOrWhiteSpace(), m => m.Author.Contains(author))
                   .WhereIf(!categoryId.Equals(Guid.Empty), m => m.CategoryId == categoryId)
                   .WhereIf(startTime != null, m => m.CreationTime > startTime)
                   .WhereIf(endTime != null, m => m.CreationTime < endTime)
                   .PageBy(skipCount, maxResultCount)
                   .ToListAsync();

        }
    }
}
