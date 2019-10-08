using AutoMapper;
using Volo.Ymapp.Books;

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
        }
    }
}
