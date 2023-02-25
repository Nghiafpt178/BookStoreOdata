using ODataBookStore.DTOs;
using ODataBookStore.Models;

namespace ODataBookStore.Repository
{
	public interface IAuthorRepository
	{
        List<Author> GetAllAuthor();
        AuthorRespond AddAuthor(AuthorRespond authorRespond);
        List<AuthorRespond> DeleteAuthor(string key);

        AuthorRespond GetAuthorByID(string key);

        AuthorRespond UpdateAuthor(string key, AuthorRespond authorRespond);
    }
}
