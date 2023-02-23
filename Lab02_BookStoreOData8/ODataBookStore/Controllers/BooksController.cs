using Microsoft.AspNetCore.Mvc;
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
            return Ok(bookRepository.GetAllBooks());
        }

        [EnableQuery]
        public IActionResult Post([FromBody] BookRespond book)
        {
            bookRepository.AddBook(book);
            return Created(book);
        }
    }
}
