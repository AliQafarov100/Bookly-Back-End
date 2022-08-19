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

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class SupportsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SupportsController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: BooklyAdmin/Supports
        public async Task<IActionResult> Index()
        {
            return View(await _context.Supports.ToListAsync());
        }

        // GET: BooklyAdmin/Supports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var support = await _context.Supports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (support == null)
            {
                return NotFound();
            }

            return View(support);
        }

        // GET: BooklyAdmin/Supports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BooklyAdmin/Supports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Support support)
        {
            if (ModelState.IsValid)
            {
                if(support.Photo != null)
                {
                    if (!support.Photo.IsOkay(1))
                    {
                        ModelState.AddModelError("Photo", "Size of piscture mustn't more than 1MB!");
                        return View();
                    }
                    support.Image = await support.Photo.FileCreate(_env.WebRootPath, @"assets/Image/Others");
                }
                else
                {
                    ModelState.AddModelError("Photo", "This model has image,please choose image!");
                    return View();
                }
                _context.Add(support);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(support);
        }

        // GET: BooklyAdmin/Supports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var support = await _context.Supports.FindAsync(id);
            if (support == null)
            {
                return NotFound();
            }
            return View(support);
        }

        // POST: BooklyAdmin/Supports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Support support)
        {
            if (id != support.Id)
            {
                return NotFound();
            }

            Support existed = await _context.Supports.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    if(support.Photo == null)
                    {
                        string imageFile = existed.Image;
                        _context.Entry(existed).CurrentValues.SetValues(support);
                        existed.Image = imageFile;
                    }
                    else
                    {
                        if (!support.Photo.IsOkay(1))
                        {
                            ModelState.AddModelError("Photo", "Size of piscture mustn't more than 1MB!");
                            return View();
                        }
                        FileExtesion.FileDelete(_env.WebRootPath, @"assets/Image/Others", existed.Image);
                        _context.Entry(existed).CurrentValues.SetValues(support);
                        existed.Image = await support.Photo.FileCreate(_env.WebRootPath, @"assets/Image/Others");
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupportExists(support.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(support);
        }

        // GET: BooklyAdmin/Supports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var support = await _context.Supports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (support == null)
            {
                return NotFound();
            }

            return View(support);
        }

        // POST: BooklyAdmin/Supports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var support = await _context.Supports.FindAsync(id);
            FileExtesion.FileDelete(_env.WebRootPath, @"assets/Image/Others", support.Image);
            _context.Supports.Remove(support);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupportExists(int id)
        {
            return _context.Supports.Any(e => e.Id == id);
        }
    }
}
