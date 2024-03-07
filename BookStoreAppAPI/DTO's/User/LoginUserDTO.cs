using System.ComponentModel.DataAnnotations;

namespace BookStoreAppAPI.DTO_s.UserDTO
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
