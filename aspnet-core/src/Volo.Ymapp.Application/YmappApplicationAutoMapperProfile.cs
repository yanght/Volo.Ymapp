using AutoMapper;
using Volo.Ymapp.Articles;
using Volo.Ymapp.Books;
using Volo.Ymapp.Categorys;
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

            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            CreateMap<ProductPicture, ProductPictureDto>();
            CreateMap<ProductSpec, ProductSpecDto>();
            CreateMap<ProductPrice, ProductPriceDto>();
            CreateMap<ProductStock, ProductStockDto>();
        }
    }
}
