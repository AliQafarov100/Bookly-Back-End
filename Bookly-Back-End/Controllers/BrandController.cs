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
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;

        public BrandController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Brands()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Book> books = await _context.Books.ToListAsync();

            BookVM model = new BookVM
            {
                Categories = categories,
                AllBooks = books
            };
            return View(model);
        }
    }
}
