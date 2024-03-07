using System.ComponentModel.DataAnnotations;

namespace BookStoreAppAPI.DTO_s.UserDTO
{
    public class UserDTO: LoginUserDTO
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
