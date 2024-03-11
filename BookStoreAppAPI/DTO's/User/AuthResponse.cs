namespace BookStoreAppAPI.DTO_s.User
{
    public class AuthResponse
    {
        //This class references the GenareteToken method for claims and token
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
