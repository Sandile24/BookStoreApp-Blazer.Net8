using Blazored.LocalStorage;
using BookStoreAppBlazer.Server.UI.Services.Base;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreAppBlazer.Server.UI.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;
        public AuthorService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            _client = client;
        }

        public async Task<Response<List<AuthorReadDTO>>> GetAuthors()
        {
            Response<List<AuthorReadDTO>> response;

            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsAllAsync();
                response = new Response<List<AuthorReadDTO>>
                {
                    Data = data.ToList(),
                    Success= true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<List<AuthorReadDTO>>(exception);
            }

            return response;
        }
    }
}
