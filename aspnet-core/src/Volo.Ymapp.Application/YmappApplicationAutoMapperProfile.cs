using AutoMapper;
using Volo.Ymapp.Areas;
using Volo.Ymapp.Articles;
using Volo.Ymapp.Books;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.Kh10086;
using Volo.Ymapp.Products;

namespace Volo.Ymapp
{
    public class YmappApplicationAutoMapperProfile : Profile
    {
        public YmappApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Book, BookDto>();
            CreateMap<CreateUpdateBookDto, Book>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<Article, ArticleDto>();
            CreateMap<CreateArticleDto, Article>();
            CreateMap<UpdateArticleDto, Article>();

            CreateMap<Area, AreaDto>();
            CreateMap<AreaDto, Area>();

            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            CreateMap<ProductArea, ProductAreaDto>();
            CreateMap<ProductAreaDto, ProductArea>();

            CreateMap<ProductPicture, ProductPictureDto>();
            CreateMap<ProductPictureDto, ProductPicture>();
           
            CreateMap<ProductSpec, ProductSpecDto>();
            CreateMap<ProductSpecDto, ProductSpec>();

            CreateMap<ProductPrice, ProductPriceDto>();
            CreateMap<ProductPriceDto, ProductPrice>();

            CreateMap<ProductStock, ProductStockDto>();
            CreateMap<ProductStockDto, ProductStock>();

            CreateMap<LineTeam, LineTeamDto>();
            CreateMap<LineTeamDto, LineTeam>();

            CreateMap<Line, LineDto>();
            CreateMap<LineDto, Line>();

            CreateMap<LineDay, LineDayDto>();
            CreateMap<LineDayDto, LineDay>();

            CreateMap<LineDayImage, LineDayImageDto>();
            CreateMap<LineDayImageDto, LineDayImage>();

            CreateMap<LineDaySelf, LineDaySelfDto>();
            CreateMap<LineDaySelfDto, LineDaySelf>();

            CreateMap<LineDayShop, LineDayShopDto>();
            CreateMap<LineDayShopDto, LineDayShop>();

            CreateMap<LineDayTraffic, LineDayTrafficDto>();
            CreateMap<LineDayTrafficDto, LineDayTraffic>();

            CreateMap<LineRouteDate, LineRouteDateDto>();
            CreateMap<LineRouteDateDto, LineRouteDate>();

            CreateMap<LineIntro, LineIntroDto>();
            CreateMap<LineIntroDto, LineIntro>();
        }
    }
}
