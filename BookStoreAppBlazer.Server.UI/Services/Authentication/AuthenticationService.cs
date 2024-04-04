using Blazored.LocalStorage;
using BookStoreAppBlazer.Server.UI.Providers;
using BookStoreAppBlazer.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreAppBlazer.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _stateProvider;
        public AuthenticationService(IClient httpClient,
            ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorageService;
            _stateProvider = authenticationStateProvider;
        }
        public async Task<bool> AuthenticateAsync(LoginUserDTO loginModel)
        {
            var response =await _httpClient.LoginAsync(loginModel);
            //Store Token
            await _localStorage.SetItemAsync("accessToken",response.Token);
            //Change auth state of the app
           await ((ApiAuthenticationStateProvider)_stateProvider).LoggedIn();
            return true;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)_stateProvider).LoggedOut();
        }
    }
}
