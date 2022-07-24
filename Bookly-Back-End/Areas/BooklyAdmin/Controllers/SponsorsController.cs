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

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    public class SponsorsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SponsorsController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: BooklyAdmin/Sponsors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sponsors.ToListAsync());
        }

        // GET: BooklyAdmin/Sponsors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return View(sponsor);
        }

        // GET: BooklyAdmin/Sponsors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BooklyAdmin/Sponsors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sponsor sponsor)
        {
            if (ModelState.IsValid)
            {
                if(sponsor.Photo != null)
                {
                    if (!sponsor.Photo.IsOkay(1))
                    {
                        ModelState.AddModelError("Photo", "Size of image mustn't more than 1Mb!");
                        return View();
                    }

                    sponsor.Image = await sponsor.Photo.FileCreate(_env.WebRootPath, @"assets/Image/Sponsors");
                }
                else
                {
                    ModelState.AddModelError("Photo", "This is model with image please choose image!");
                    return View();
                }
                _context.Add(sponsor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sponsor);
        }

        // GET: BooklyAdmin/Sponsors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }
            return View(sponsor);
        }

        // POST: BooklyAdmin/Sponsors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Sponsor sponsor)
        {
            if (id != sponsor.Id)
            {
                return NotFound();
            }
            Sponsor existed = await _context.Sponsors.FindAsync(id);

            if (ModelState.IsValid)
            {
                try
                {
                    if(sponsor.Photo == null)
                    {
                        string fileName = existed.Image;
                        _context.Entry(existed).CurrentValues.SetValues(sponsor);
                        existed.Image = fileName;
                    }
                    else
                    {
                        if (!sponsor.Photo.IsOkay(1))
                        {
                            ModelState.AddModelError("Photo", "Size of image mustn't more than 1Mb!");
                            return View(existed);
                        }
                        FileExtesion.FileDelete(_env.WebRootPath, @"assets/Image/Sponsors", existed.Image);
                        _context.Entry(existed).CurrentValues.SetValues(sponsor);
                        existed.Image = await sponsor.Photo.FileCreate(_env.WebRootPath, @"assets/Image/Sponsors");
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponsorExists(sponsor.Id))
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
            return View(sponsor);
        }

        // GET: BooklyAdmin/Sponsors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return View(sponsor);
        }

        // POST: BooklyAdmin/Sponsors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sponsor = await _context.Sponsors.FindAsync(id);
            _context.Sponsors.Remove(sponsor);
            FileExtesion.FileDelete(_env.WebRootPath, @"assets/Image/Sponsors", sponsor.Image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SponsorExists(int id)
        {
            return _context.Sponsors.Any(e => e.Id == id);
        }
    }
}
