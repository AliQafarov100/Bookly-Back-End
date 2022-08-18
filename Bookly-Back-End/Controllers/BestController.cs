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
        private readonly IQuery _query;

        public BestController(AppDbContext context, IBookOperation repository,IQuery query)
        {
            _context = context;
            _repository = repository;
            _query = query;
        }
        public async Task<IActionResult> Best(string category, string sortBy,
            string author, int? minimum, int? maximum, string language, string format, int page = 1)
        {
            var query = _repository.GetBookByFilter(category, author, sortBy, minimum,
                maximum, language, format);

            ViewBag.Author = author;
            ViewBag.CurrentMaximum = maximum;
            ViewBag.CurentMinimum = minimum;
            ViewBag.CurentCategory = category;
            ViewBag.CurentLanguage = language;
            ViewBag.CurentFormat = format;
            ViewBag.TotalPage = Math.Ceiling(((decimal)await query.CountAsync()) / 12);
            ViewBag.CurrentPage = page;
            ViewBag.High = sortBy;

            List<BookAuthor> bookAuthors = await query.Skip((page - 1) * 12)
                .Include(b => b.Book).ThenInclude(b => b.BookImages).Include(b => b.Book.Discount)
                .Include(b => b.Author).Where(b => b.Book.IsBest == true).ToListAsync();

            BookVM model = new BookVM
            {
                Formats = await _query.Formats.ToListAsync(),
                Languages = await _query.Languages.ToListAsync(),
                Categories = await _query.Categories.ToListAsync(),
                Authors = await _query.Authors.ToListAsync(),
                BookAuthors = bookAuthors,
                FilteringPrices = await _query.FilteringPrices.ToListAsync()
            };
            return View(model);
        }
    }
}
