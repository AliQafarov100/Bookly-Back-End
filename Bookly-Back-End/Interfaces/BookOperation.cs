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
        
        public IQueryable<BookAuthor> BookAuthors => _context.BookAuthors;
        public IQueryable<BookAuthor> GetBookByFilter(string category, string author, string highToLow,
            int? minPrice, int? maxPrice,string language,string format)
        {
            var bookAuthors = _context.BookAuthors.AsQueryable();
            if (category != null)
            {
                bookAuthors = bookAuthors.Where(b => b.Book.Category.Name.Contains(category));
            }
            if (author != null)
            {
                bookAuthors = bookAuthors.Where(b => b.Author.FullName.Contains(author));
            }
            if(language != null)
            {
                bookAuthors = bookAuthors.Where(b => b.Book.Language.Name.Contains(language));
            }
            if(format != null)
            {
                bookAuthors = bookAuthors.Where(b => b.Book.Format.Name.Contains(format));
            }

            if (minPrice != null && maxPrice != null)
            {
                bookAuthors = bookAuthors.Where(b => b.Book.Price >= minPrice && b.Book.Price <= maxPrice);
            }
            switch (highToLow)
            {
                case "high":
                    bookAuthors = bookAuthors.OrderByDescending(b => b.Book.Price);
                    break;
                default:
                    bookAuthors = bookAuthors.AsQueryable();
                    break;
            }
            return (bookAuthors);
        }

        public IQueryable<BookAuthor> GetBookByCategory(string category)
        {
            var books = _context.BookAuthors.AsQueryable();

            if(category != null)
            {
                books = books.Where(b => b.Book.Category.Name.Contains(category));
            }
            return books;
        }

        public IQueryable<BookAuthor> GetBookBySearch(string searching)
        {
            var searchBooks = _context.BookAuthors.AsQueryable();

            if (searchBooks != null)
            {
                searchBooks = searchBooks.Where(sb => sb.Book.Name.Contains(searching) || sb.Author.FullName.Contains(searching));
            }

            return searchBooks;
        }
       
    }
}
