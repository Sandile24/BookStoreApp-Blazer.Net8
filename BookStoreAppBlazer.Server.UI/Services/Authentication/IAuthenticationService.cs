using BookStoreAppBlazer.Server.UI.Services.Base;

namespace BookStoreAppBlazer.Server.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDTO loginModel);
        public Task Logout();
    }
}
