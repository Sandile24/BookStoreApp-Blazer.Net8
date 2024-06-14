using AutoMapper;
using BookStoreAppBlazer.Server.UI.Services.Base;

namespace BookStoreAppBlazer.Server.UI.Configurations
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorReadDTO,AuthorUpdateDTO>().ReverseMap();
            CreateMap<BookDetailsDTO, BookUpdateDTO>().ReverseMap();
        }
    }
}
