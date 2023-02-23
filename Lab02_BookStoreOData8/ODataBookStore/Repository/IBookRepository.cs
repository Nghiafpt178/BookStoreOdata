using ODataBookStore.DTOs;
using ODataBookStore.Models;

namespace ODataBookStore.Services
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();

        void AddBook(BookRespond bookRespond);    
    }
}
