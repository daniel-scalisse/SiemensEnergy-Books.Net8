using AutoMapper;
using Books_Api.ViewModels;
using Books_Business.Modules.Authors;
using Books_Business.Modules.Books;
using Books_Business.Modules.Genders;

namespace Books_Api.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Author, AuthorInput>().ReverseMap();
            CreateMap<Book, BookInput>().ReverseMap();
            CreateMap<Gender, GenderInput>().ReverseMap();
        }
    }
}