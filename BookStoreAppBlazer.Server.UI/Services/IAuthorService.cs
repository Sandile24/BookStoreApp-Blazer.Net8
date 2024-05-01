using BookStoreAppBlazer.Server.UI.Services.Base;

namespace BookStoreAppBlazer.Server.UI.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadDTO>>> GetAuthors();
    }
}