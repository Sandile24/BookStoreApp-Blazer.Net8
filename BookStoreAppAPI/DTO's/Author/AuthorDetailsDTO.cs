using BookStoreAppAPI.DTO_s.Book;
using BookStoreAppAPI.Models.Author;

namespace BookStoreAppAPI.Models.Author
{
    public class AuthorDetailsDTO: AuthorReadDTO
    {
        public List<BookReadDTO>? Books { get; set; } 
    }
}
