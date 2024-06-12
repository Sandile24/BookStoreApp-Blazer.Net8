using BookStoreAppBlazer.Server.UI.Services.Base;

namespace BookStoreAppBlazer.Server.UI.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadDTO>>> GetAuthors();
        Task<Response<AuthorDetailsDTO>> GetAuthor(int id);
        Task<Response<AuthorUpdateDTO>> GetAuthorForUpdate(int id);
        Task<Response<int>> CreateAuthor(AuthorCreateDTO createDTO);
        Task<Response<int>> EditAuthor(int id, AuthorUpdateDTO updateDTO);
        Task<Response<int>> DeleteAuthor(int id);

    }
}