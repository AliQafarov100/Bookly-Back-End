using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Interfaces
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Book> Books => _context.Books;

        public IQueryable<Book> GetBookByCategory(int? category, int? author)
        {
            var books = _context.Books.AsQueryable();
            var bookAuthors = _context.BookAuthors.AsQueryable();

            foreach (var book in books)
            {
                if (category == book.CategoryId)
                {
                    books = books.Where(b => b.CategoryId == category);
                }
            }

            if (author.HasValue)
            {
                foreach(var authorName in bookAuthors.Where(b => b.AuthorId == author))
                {
                    books = books.Where(b => b.Id == authorName.BookId).AsQueryable();
                }
            }
            return books;
        }
    }
}
