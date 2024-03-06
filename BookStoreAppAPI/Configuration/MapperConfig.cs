using AutoMapper;
using BookStoreAppAPI.Data;
using BookStoreAppAPI.Models.Author;

namespace BookStoreAppAPI.Configuration
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
            CreateMap<AuthorReadDTO, Author>().ReverseMap();
        }
    }
}
