using BookStoreAppAPI.DTO_s;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAppAPI.Models.Author
{
    public class AuthorUpdateDTO: BaseDTO
    {

        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }
        [StringLength(350)]
        public string? Bio { get; set; }
    }
}
