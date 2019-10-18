using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.Ymapp.Articles
{
    public interface IArticleAppService : ICrudAppService< //Defines CRUD methods
           ArticleDto, //Used to show books
           Guid, //Primary key of the book entity
           PagedAndSortedResultRequestDto, //Used for paging/sorting on getting a list of books
           CreateArticleDto, //Used to create a new book
           UpdateArticleDto> //Used to update a book
    {
        PagedResultDto<ArticleDto> GetArticleList(GetArtticlesDto input);
    }
}
