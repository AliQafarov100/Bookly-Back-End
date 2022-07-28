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
    public class DetailController : Controller
    {
        private readonly AppDbContext _context;

        public DetailController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Details(int id)
        {
            Book book = await _context.Books.Include(b => b.BookImages).Include(b => b.BookFormats)
                .Include(b => b.BookLanguages).Include(b => b.BookAuthors).Include(c => c.Category).FirstOrDefaultAsync(b => b.Id == id);
            List<Discount> discounts = await _context.Discounts.ToListAsync();
            List<BookAuthor> bookAuthors = await _context.BookAuthors.ToListAsync();
            List<Author> authors = await _context.Authors.Include(a => a.BookAuthors).ToListAsync();
            List<Format> formats = await _context.Formats.Include(f => f.BookFormats).ToListAsync();
            List<BookFormat> bookFormats = await _context.BookFormats.ToListAsync();
            List<Language> languages = await _context.Languages.Include(l => l.BookLanguages).ToListAsync();
            List<BookLanguage> bookLanguages = await _context.BookLanguages.ToListAsync();
            BookVM model = new BookVM
            {
                Book = book,
                Discounts = discounts,
                BookAuthors = bookAuthors,
                Authors = authors,
                BookFormats = bookFormats,
                BookLanguages = bookLanguages
            };
            return View(model);
        }
    }
}
