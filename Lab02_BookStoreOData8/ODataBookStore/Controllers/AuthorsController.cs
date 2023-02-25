using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataBookStore.DTOs;
using ODataBookStore.Models;
using ODataBookStore.Repository;
using ODataBookStore.Services;

namespace ODataBookStore.Controllers
{
	public class AuthorsController : ODataController
    {
        private readonly IAuthorRepository authorRepository;

		public AuthorsController(IAuthorRepository authorRepository)
		{
			this.authorRepository = authorRepository;
		}

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            List<Author> authors = authorRepository.GetAllAuthor();
            return Ok(authors);
        }

        [EnableQuery]
        public IActionResult Post([FromBody] AuthorRespond author)
        {
            AuthorRespond authorRespond = authorRepository.AddAuthor(author);
            return Ok(new Respond<AuthorRespond>()
            {
                IsSuccess = true,
                Message = "Create new author success!",
                Data = authorRespond
            });
        }

        [EnableQuery]
        public IActionResult Get(string key)
        {
            AuthorRespond authorRespond = authorRepository.GetAuthorByID(key);
            return Ok(authorRespond);
        }

        [EnableQuery]
        [HttpPut]
        public IActionResult Put([FromODataUri] string key, [FromBody] AuthorRespond authorRespond)
        {
            AuthorRespond authorRespond1 = authorRepository.UpdateAuthor(key, authorRespond);
            if (authorRespond1 == null)
            {
                return NotFound();
            }
            return Ok(new Respond<AuthorRespond>()
            {
                IsSuccess = true,
                Message = "Update book success!",
                Data = authorRespond1
            });
        }
        [EnableQuery]
        [HttpDelete]
        public IActionResult Delete([FromODataUri] string key)
        {
            List<AuthorRespond> deletedAuthors = authorRepository.DeleteAuthor(key);
            if (deletedAuthors == null)
            {
                return NotFound();
            }
            return Ok(new Respond<List<AuthorRespond>>()
            {
                IsSuccess = true,
                Message = "Delete book success!",
                Data = deletedAuthors
            });

        }
    }
}
