using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Bookly_Back_End.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _context.Books.AsQueryable();
            ViewBag.TotalPage = Math.Ceiling(((decimal)await query.CountAsync()) / 9);
            ViewBag.CurrentPage = page;
            List<Format> formats = await _context.Formats.ToListAsync();
            List<Language> languages = await _context.Languages.ToListAsync();
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Author> authors = await _context.Authors.ToListAsync();
            List<Book> books = await query.Include(b => b.BookImages).Skip((page - 1) * 9).ToListAsync();
            List<BookAuthor> bookAuthors = await _context.BookAuthors.ToListAsync();
            List<Discount> discounts = await _context.Discounts.ToListAsync();

            BookVM model = new BookVM
            {
                Formats = formats,
                Languages = languages,
                Categories = categories,
                Authors = authors,
                BookAuthors = bookAuthors,
                Books = books,
                Discounts = discounts
            };
            return View(model);
        }
    }
}
