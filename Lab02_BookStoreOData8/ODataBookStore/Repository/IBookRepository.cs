using ODataBookStore.DTOs;
using ODataBookStore.Models;

namespace ODataBookStore.Services
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        BookRespond AddBook(BookRespond bookRespond);    
        List<BookRespond> DeleteBook(string key);

        BookRespond GetBookByID(string key);

        BookRespond UpdateBook(string key, BookRespond bookRespond);

    }
}
