﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public BookRespond AddBook(BookRespond bookRespond)
        {
            Book book = mapper.Map<Book>(bookRespond);
            _context.Books.Add(book);
            _context.SaveChanges();
            BookRespond bookRespond1 = mapper.Map<BookRespond>(book);
            return bookRespond1;    
            
        }

        public List<BookRespond> DeleteBook(string key)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == key.Trim());
            if(book != null)
            {
                _context.Remove(book);
                _context.SaveChanges();
                List<Book> books = _context.Books.ToList();
                List<BookRespond> bookResponds = mapper.Map<List<BookRespond>>(books);
                return bookResponds;
            }
            return null;
        }

        public List<Book> GetAllBooks()
        {
            var books = _context.Books.Include(b => b.Pub).ToList();
            return books;
        }

        public BookRespond GetBookByID(string key)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == key.Trim());
            if(book != null)
            {
                BookRespond bookRespond = mapper.Map<BookRespond>(book);
                return bookRespond;
            }
            return null;
        }

        public BookRespond UpdateBook(string key, BookRespond bookRespond)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == key.Trim());
            if(book != null)
            {
                book.Title = bookRespond.Title; 
                book.Type = bookRespond.Type;
                book.PubId = bookRespond.PubId;
                book.Price = bookRespond.Price;
                book.Advance = bookRespond.Advance;
                book.Royalty = bookRespond.Royalty; 
                book.YtdSales = bookRespond.YtdSales;
                book.Notes = bookRespond.Notes;
                book.PublishedDate = bookRespond.PublishedDate;
                _context.Update(book);
                _context.SaveChanges();
                BookRespond bookRespond1 = mapper.Map<BookRespond>(book);
                return bookRespond1;
            }
            return null;
        }
    }
}
