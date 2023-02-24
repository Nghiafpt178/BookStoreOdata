using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataBookStore.DTOs;
using ODataBookStore.Models;
using ODataBookStore.Services;

namespace ODataBookStore.Controllers
{
    public class BooksController : ODataController
    {
        private readonly IBookRepository bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            List<Book> bookResponds = bookRepository.GetAllBooks();
            return Ok(bookResponds);
        }

        [EnableQuery]
        public IActionResult Post([FromBody] BookRespond book)
        {
            BookRespond bookRespond = bookRepository.AddBook(book);
            return Ok(new Respond<BookRespond>()
            {
                IsSuccess = true,
                Message = "Create new book success!",
                Data = bookRespond
            });
        }

        [EnableQuery]
        public IActionResult Delete([FromODataUri] string key)
        {
            List<BookRespond> deletedBook = bookRepository.DeleteBook(key);
            if (deletedBook == null)
            {
                return NotFound();
            }
            return Ok(new Respond<List<BookRespond>>()
            {
                IsSuccess = true,
                Message = "Delete book success!",
                Data = deletedBook
            });

        }

        [EnableQuery]
        public IActionResult Get(string key)
        {
            BookRespond bookRespond = bookRepository.GetBookByID(key);
            return Ok(bookRespond);
        }

        [EnableQuery]
        [HttpPut]
        public IActionResult Put([FromODataUri] string key, [FromBody] BookRespond bookRespond)
        {
            BookRespond bookRespond1 = bookRepository.UpdateBook(key, bookRespond);
            if (bookRespond1 == null)
            {
                return NotFound();
            }
            return Ok(new Respond<BookRespond>()
            {
                IsSuccess = true,
                Message = "Update book success!",
                Data = bookRespond1
            });
        }

    }
}
