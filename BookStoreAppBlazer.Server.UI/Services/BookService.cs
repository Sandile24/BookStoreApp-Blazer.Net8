using AutoMapper;
using Blazored.LocalStorage;
using BookStoreAppBlazer.Server.UI.Services.Base;

namespace BookStoreAppBlazer.Server.UI.Services
{
    public class BookService : BaseHttpService, IBookService
    {

        private readonly IClient _client;
        private readonly IMapper _mapper;
        private readonly ILocalStorageService _localStorage;
        public BookService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
        {
            _client = client;
            _mapper = mapper;
            _localStorage = localStorage;
        }

        public async Task<Response<int>> CreateBook(BookCreateDTO createDTO)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
               await _client.BooksPOSTAsync(createDTO);
                response.Success = true;
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<int>(exception);
            }
            return (response);
        }

        public async Task<Response<int>> DeleteBook(int id)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await _client.BooksDELETEAsync(id);
                response.Success = true;
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<int>(exception);
            }
            return (response);
            
        }

        public async Task<Response<int>> EditBook(int id, BookUpdateDTO updateDTO)
        {
            Response<int> response = new Response<int> { Success = true };
            try
            {
                await GetBearerToken();
                await _client.BooksPUTAsync(id, updateDTO);
                response.Success = true;
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<int>(exception);
            }
            return response;

        }

        public async Task<Response<BookDetailsDTO>> GetBook(int id)
        {
            Response<BookDetailsDTO> response = new Response<BookDetailsDTO> { Success = true };
            try
            {
                await GetBearerToken();
                var data = await _client.BooksGETAsync(id);
                response = new Response<BookDetailsDTO>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<BookDetailsDTO>(exception);
            }
            return response;
        }

        public async Task<Response<BookUpdateDTO>> GetBookForUpdate(int id)
        {
            Response<BookUpdateDTO> response;
            try
            {
                await GetBearerToken();
                var data = await _client.BooksGETAsync(id); // Await the data retrieval
                response = new Response<BookUpdateDTO>
                {
                    Data = _mapper.Map<BookUpdateDTO>(data),
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<BookUpdateDTO>(exception);
            }
            return response;
        }

        public async Task<Response<List<BookReadDTO>>> GetBooks()
        {
          Response<List<BookReadDTO>> response = new();
            try
            {
                await GetBearerToken();
                var data = await _client.BooksAllAsync();
                response = new Response<List<BookReadDTO>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<List<BookReadDTO>>(exception);
            }
            return response;
        }
    }
}
