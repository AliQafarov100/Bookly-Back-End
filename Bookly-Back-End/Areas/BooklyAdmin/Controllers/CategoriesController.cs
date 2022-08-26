using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Bookly_Back_End.Extensions;
using Microsoft.AspNetCore.Hosting;
using Bookly_Back_End.Utilities;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoriesController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page <= 0) return RedirectToAction("Index", "Categories");
            List<Category> categories = await _context.Categories.ToListAsync();

            return View(categories.ToPagedList(page, 5));
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

       
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Photo != null)
                {
                    if (!category.Photo.IsOkay(1))
                    {
                        ModelState.AddModelError("Photo", "Size of image must be more than 1MB");
                        return View();
                    }
                    category.Image = await category.Photo.FileCreate(_env.WebRootPath, @"assets\Image\Category Book");
                }
                else
                {
                    ModelState.AddModelError("", "Please choose image!");
                    return View();
                }

                _context.Add(category);
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            Category existed = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    if(category.Photo == null)
                    {
                        string fileName = existed.Image;
                        _context.Entry(existed).CurrentValues.SetValues(category);
                        existed.Image = fileName;
                    }
                    else
                    {
                        if (!category.Photo.IsOkay(1))
                        {
                            ModelState.AddModelError("Photo", "Size of image mustn't more than 1MB!");
                            return View(existed);
                        }
                        FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\Category Book", existed.Image);
                        _context.Entry(existed).CurrentValues.SetValues(category);
                        existed.Image = await category.Photo.FileCreate(_env.WebRootPath, @"assets\Image\Category Book");
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\Category Book",category.Image);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
