using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Ymapp.Articles;

namespace Volo.Ymapp
{
    public class ArticleAppService :
        CrudAppService<
            Article, ArticleDto, Guid, PagedAndSortedResultRequestDto,
            CreateArticleDto, UpdateArticleDto>,
            IArticleAppService
    {
        public ArticleAppService(IRepository<Article, Guid> repository)
       : base(repository)
        {

        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<ArticleDto> GetArticleList(GetArtticlesDto input)
        {
            var query = Repository.WithDetails(m => m.Category)
                   .WhereIf(!input.Title.IsNullOrWhiteSpace(), m => m.Title.Contains(input.Title))
                   .WhereIf(!input.Author.IsNullOrWhiteSpace(), m => m.Author.Contains(input.Author))
                   .WhereIf(!input.CategoryId.Equals(Guid.Empty), m => m.CategoryId == input.CategoryId)
            .WhereIf(input.StartTime != null, m => m.CreationTime > input.StartTime);
            //.WhereIf(input.EndTime != null, m => m.CreationTime < input.EndTime);

            var count = query.Count();
            var list = query.PageBy(input.SkipCount, input.MaxResultCount)
                       .ToList();
            return new PagedResultDto<ArticleDto>(count, ObjectMapper.Map<List<Article>, List<ArticleDto>>(list));

        }
    }
}
