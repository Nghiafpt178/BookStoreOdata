using AutoMapper;
using ODataBookStore.DTOs;
using ODataBookStore.Models;

namespace ODataBookStore.Repository
{
	public class AuthorRepository : IAuthorRepository
	{
        private readonly As2Context _context;
        private readonly IMapper mapper;

		public AuthorRepository(As2Context context, IMapper mapper)
		{
			_context = context;
			this.mapper = mapper;
		}

		public AuthorRespond AddAuthor(AuthorRespond authorRespond)
		{
            Author author = mapper.Map<Author>(authorRespond);
            _context.Authors.Add(author);
            _context.SaveChanges();
            AuthorRespond authorRespond1 = mapper.Map<AuthorRespond>(author);
            return authorRespond1;
        }

		public List<AuthorRespond> DeleteAuthor(string key)
		{
            string res = key.Replace(" ", "");
            var author = _context.Authors.FirstOrDefault(b => b.AuthorId == res);
            if (author != null)
            {
                _context.Remove(author);
                _context.SaveChanges();
                List<Author> authors = _context.Authors.ToList();
                List<AuthorRespond> authorResponds = mapper.Map<List<AuthorRespond>>(authors);
                return authorResponds;
            }
            return null;
        }

		public List<Author> GetAllAuthor()
		{
			return _context.Authors.ToList();
		}

		public AuthorRespond GetAuthorByID(string key)
		{
            string res = key.Replace(" ", "");
            var author = _context.Authors.FirstOrDefault(b => b.AuthorId == res);
            if (author != null)
            {
                AuthorRespond authorRespond = mapper.Map<AuthorRespond>(author);
                return authorRespond;
            }
            return null;
        }

		public AuthorRespond UpdateAuthor(string key, AuthorRespond authorRespond)
		{
            string res = key.Replace(" ", "");
            var author = _context.Authors.FirstOrDefault(b => b.AuthorId == res);
            if (author != null)
            {
                author.LastName = authorRespond.LastName;   
                author.FirstName = authorRespond.FirstName;
                author.Phone = authorRespond.Phone;
                author.Address = authorRespond.Address;
                author.City = authorRespond.City;
                author.State = authorRespond.State;
                author.Zip = authorRespond.Zip;
                author.EmailAddress = authorRespond.EmailAddress;
                _context.Update(author);
                _context.SaveChanges();
                AuthorRespond authorRespond1 = mapper.Map<AuthorRespond>(author);
                return authorRespond1;
            }
            return null;
        }

	}
}
