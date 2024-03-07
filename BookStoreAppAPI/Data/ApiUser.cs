using Microsoft.AspNetCore.Identity;

namespace BookStoreAppAPI.Data
{
    public class ApiUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
