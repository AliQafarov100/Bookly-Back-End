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
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IQuery _query;

        public BrandController(AppDbContext context,IQuery query)
        {
            _context = context;
            _query = query;
        }
        public async Task<IActionResult> Brands()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Book> books = await _context.Books.ToListAsync();

            BookVM model = new BookVM
            {
                Categories = _query.Categories,
                AllBooks = _query.Books
            };
            return View(model);
        }
    }
}
