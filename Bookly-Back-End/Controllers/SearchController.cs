using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Interfaces;
using Bookly_Back_End.Models;
using Bookly_Back_End.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBookOperation _repository;

        public SearchController(AppDbContext context,IBookOperation repository)
        {
            _context = context;
            _repository = repository;
        }
        public async Task<IActionResult> Search(string searching)
        {
            ViewBag.Search = searching;
            var query = _repository.GetBookBySearch(searching);
            List<Book> books = await _context.Books.Include(i => i.BookImages)
               .Include(a => a.BookAuthors).ToListAsync();
            List<BookAuthor> bookAuthors = await query.ToListAsync();
            List<Author> authors = await _context.Authors.Include(a => a.BookAuthors).ToListAsync();
            List<Discount> discounts = await _context.Discounts.ToListAsync();

            BookVM model = new BookVM
            {
                BookAuthors = bookAuthors,
                AllBooks = books,
                Discounts = discounts,
                Authors = authors
            };
            return View(model);
        }
    }
}
