using AutoMapper;
using BookStoreAppAPI.Data;
using BookStoreAppAPI.DTO_s.Book;
using BookStoreAppAPI.Models.Author;

namespace BookStoreAppAPI.Configuration
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            //These are for mapping Authors necessary details
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
            CreateMap<AuthorReadDTO, Author>().ReverseMap();
            //These are DTO's for mapping Book
            CreateMap<BookCreateDTO, Book>().ReverseMap();
            //Here I mapped the author fullname after including an author to the book.
            CreateMap<Book,BookReadDTO>()
                .ForMember(a=>a.AuthorName, a=>a.MapFrom(map=>$"{map.Author!.FirstName} {map.Author.LastName}"))
                .ReverseMap();
            CreateMap<Book, BookDetailsDTO>()
                .ForMember(a => a.AuthorName, a => a.MapFrom(map => $"{map.Author!.FirstName} {map.Author.LastName}"))
                .ReverseMap();
            CreateMap<BookUpdateDTO, Book>().ReverseMap();
        }
    }
}
