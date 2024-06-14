using BookStoreAppBlazer.Server.UI.Services.Base;

namespace BookStoreAppBlazer.Server.UI.Services
{
    public interface IBookService
    {

        Task<Response<List<BookReadDTO>>> GetBooks();
        Task<Response<BookDetailsDTO>> GetBook(int id);
        Task<Response<BookUpdateDTO>> GetBookForUpdate(int id);
        Task<Response<int>> CreateBook(BookCreateDTO createDTO);
        Task<Response<int>> EditBook(int id, BookUpdateDTO updateDTO);
        Task<Response<int>> DeleteBook(int id);

    }
}
