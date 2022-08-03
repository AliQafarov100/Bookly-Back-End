using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Interfaces
{
    public class BookOperation : IBookOperation
    {
        private readonly AppDbContext _context;

        public BookOperation(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Book> Books => _context.Books;

        public IQueryable<Book> GetBookByCategory(int? category, int? author,string highToLow)
        {
            var books = _context.Books.AsQueryable();
            var bookAuthors = _context.BookAuthors.AsQueryable();
            
            if (category != null)
            {
                books = books.Where(b => b.CategoryId == category);
            }
            if (author != null)
            {
                books = books.Where(b => b.BookAuthors.Contains(bookAuthors.FirstOrDefault(a => a.AuthorId == author)));
            }
            switch (highToLow)
            {
                case "high":
                    books = books.OrderByDescending(b => b.Price);
                    break;
                default:
                    books = books.AsQueryable();
                    break;
            }
            return books;
        }

        public IQueryable<Book> GetBookBySearch(string searching)
        {
            var searchBooks = _context.Books.AsQueryable();

            if(searchBooks != null)
            {
                searchBooks = searchBooks.Where(sb => sb.Name.Contains(searching));
            }

            return searchBooks;
        }
    }
}
