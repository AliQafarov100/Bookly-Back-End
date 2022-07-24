using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Bookly_Back_End.Utilities;
using Microsoft.AspNetCore.Hosting;
using Bookly_Back_End.Extensions;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    public class AwardsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AwardsController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: BooklyAdmin/Awards
        public async Task<IActionResult> Index()
        {
            return View(await _context.Awards.ToListAsync());
        }

        // GET: BooklyAdmin/Awards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var award = await _context.Awards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (award == null)
            {
                return NotFound();
            }

            return View(award);
        }

        // GET: BooklyAdmin/Awards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BooklyAdmin/Awards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Award award)
        {
            if (ModelState.IsValid)
            {
                if(award.Picture != null)
                {
                    if (!award.Picture.IsOkay(1))
                    {
                        ModelState.AddModelError("Picture", "Size of picture must be less than 1MB");
                        return View();
                    }
                    award.Image = await award.Picture.FileCreate(_env.WebRootPath, @"assets\Image\Awards");
                }
                else
                {
                    ModelState.AddModelError("Picture", "This model with picture!Please choose picture for this model!");
                    return View();
                }

                _context.Add(award);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(award);
        }

        // GET: BooklyAdmin/Awards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var award = await _context.Awards.FindAsync(id);
            if (award == null)
            {
                return NotFound();
            }
            return View(award);
        }

        // POST: BooklyAdmin/Awards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Award award)
        {
            if (id != award.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Award existed = await _context.Awards.FirstOrDefaultAsync(a => a.Id == id);
                try
                {
                    if(award.Picture == null)
                    {
                        string fileName = existed.Image;
                        _context.Entry(existed).CurrentValues.SetValues(award);
                        existed.Image = fileName;
                    }
                    else
                    {
                        if (!award.Picture.IsOkay(1))
                        {
                            ModelState.AddModelError("Picture", "Size of picture must be less than 1MB");
                            return View();
                        }
                        
                        FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\Awards", existed.Image);
                        _context.Entry(existed).CurrentValues.SetValues(award);
                        existed.Image = await award.Picture.FileCreate(_env.WebRootPath, @"assets\Image\Awards");
                    }

                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AwardExists(award.Id))
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
            return View(award);
        }

        // GET: BooklyAdmin/Awards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var award = await _context.Awards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (award == null)
            {
                return NotFound();
            }

            return View(award);
        }

        // POST: BooklyAdmin/Awards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            _context.Awards.Remove(award);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AwardExists(int id)
        {
            return _context.Awards.Any(e => e.Id == id);
        }
    }
}
