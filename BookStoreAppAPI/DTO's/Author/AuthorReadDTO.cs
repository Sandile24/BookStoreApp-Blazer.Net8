using BookStoreAppAPI.Data;
using BookStoreAppAPI.DTO_s;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAppAPI.Models.Author
{
    public class AuthorReadDTO: BaseDTO
    {
       
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }

        //public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
