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
    public class BestController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBookOperation _repository;

        public BestController(AppDbContext context, IBookOperation repository)
        {
            _context = context;
            _repository = repository;
        }
        public async Task<IActionResult> Best(string category, string highToLow,
            string author, int? minimum, int? maximum, string language, string format, int page = 1)
        {

            ViewBag.Author = author;
            var query = _repository.GetBookByFilter(category, author, highToLow, minimum,
                maximum, language, format);

            ViewBag.CurrentMaximum = maximum;
            ViewBag.CurentMinimum = minimum;
            ViewBag.CurentCategory = category;
            ViewBag.CurentLanguage = language;
            ViewBag.CurentFormat = format;
            ViewBag.TotalPage = Math.Ceiling(((decimal)await query.CountAsync()) / 12);
            ViewBag.CurrentPage = page;
            ViewBag.High = highToLow;
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Book> books = await _context.Books.Include(i => i.BookImages)
                .Include(a => a.BookAuthors).ToListAsync();
            List<Format> formats = await _context.Formats.ToListAsync();
            List<Language> languages = await _context.Languages.ToListAsync();
            List<Author> authors = await _context.Authors.Include(a => a.BookAuthors).ToListAsync();
            List<Book> anotherBooks = await _context.Books.Include(i => i.BookImages).
                Include(a => a.BookAuthors).ToListAsync();
            List<BookAuthor> bookAuthors = await query.Skip((page - 1) * 12).Where(b => b.Book.IsBest == true).ToListAsync();
            List<Discount> discounts = await _context.Discounts.ToListAsync();
            List<FilteringPrice> filteringPrices = await _context.FilteringPrices.ToListAsync();


            BookVM model = new BookVM
            {
                Formats = formats,
                Languages = languages,
                Categories = categories,
                Authors = authors,
                BookAuthors = bookAuthors,
                AllBooks = books,
                AnotherBooks = anotherBooks,
                Discounts = discounts,
                FilteringPrices = filteringPrices
            };
            return View(model);
        }
    }
}
