using AutoMapper;
using ODataBookStore.DTOs;
using ODataBookStore.Models;

namespace ODataBookStore.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly As2Context _context;
        private readonly IMapper mapper;

        public BookRepository(As2Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public void AddBook(BookRespond bookRespond)
        {
            Book book = mapper.Map<Book>(bookRespond);
            _context.Books.Add(book);
            _context.SaveChanges();
            
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }
    }
}
