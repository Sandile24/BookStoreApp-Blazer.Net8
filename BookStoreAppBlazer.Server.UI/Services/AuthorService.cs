using AutoMapper;
using Blazored.LocalStorage;
using BookStoreAppBlazer.Server.UI.Services.Base;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreAppBlazer.Server.UI.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;
        private readonly ILocalStorageService _localStorage;
        public AuthorService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
        {
            _client = client;
            _mapper= mapper;
            _localStorage = localStorage;
        }
        public async Task<Response<int>> CreateAuthor(AuthorCreateDTO createDTO)
        {
            Response<int> response = new Response<int> { Success = true };
            try
            {
                await GetBearerToken();
                await _client.AuthorsPOSTAsync(createDTO);
            }
            catch ( ApiException exception)
            {
                response = ConvertApiException<int>(exception);
            }
            return response;
        }
        public async Task<Response<AuthorDetailsDTO>> GetAuthor(int id)
        {
            Response<AuthorDetailsDTO> response = new Response<AuthorDetailsDTO> { Success = true };
            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsGETAsync(id);
                response = new Response<AuthorDetailsDTO>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<AuthorDetailsDTO>(exception);
            }
            return response;
        }
        public async Task<Response<AuthorUpdateDTO>> GetAuthorForUpdate(int id)
        {
            Response<AuthorUpdateDTO> response;
            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsGETAsync(id); // Await the data retrieval
                response = new Response<AuthorUpdateDTO>
                {
                    Data = _mapper.Map<AuthorUpdateDTO>(data),
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<AuthorUpdateDTO>(exception);
            }
            return response;
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
        public async Task<Response<int>> EditAuthor(int id, AuthorUpdateDTO updateDTO)
        {
            Response<int> response = new Response<int> { Success = true };
            try
            {
                await GetBearerToken();
                await _client.AuthorsPUTAsync(id, updateDTO);
                response.Success = true;
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<int>(exception);
            }
            return response;
        }

        public async Task<Response<int>> DeleteAuthor(int id)
        {
            Response<int> response = new Response<int> { Success = true };
            try
            {
                await GetBearerToken();
                await _client.AuthorsDELETEAsync(id);
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<int>(exception);
            }
            return response;
        }
    }
}
