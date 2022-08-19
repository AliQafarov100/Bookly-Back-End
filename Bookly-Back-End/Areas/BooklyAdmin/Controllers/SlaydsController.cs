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
using Microsoft.AspNetCore.Authorization;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class SlaydsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlaydsController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: BooklyAdmin/Slayds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Slayds.ToListAsync());
        }

        // GET: BooklyAdmin/Slayds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slayd = await _context.Slayds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slayd == null)
            {
                return NotFound();
            }

            return View(slayd);
        }



        // GET: BooklyAdmin/Slayds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slayd = await _context.Slayds.FindAsync(id);
            if (slayd == null)
            {
                return NotFound();
            }
            return View(slayd);
        }

        // POST: BooklyAdmin/Slayds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Slayd slayd)
        {
            if (id != slayd.Id)
            {
                return NotFound();
            }
            Slayd existed = await _context.Slayds.FindAsync(id);

            if (ModelState.IsValid)
            {
                
                try
                {
                    if (slayd.Photo == null)
                    {
                        string fileName = existed.Image;
                        _context.Entry(existed).CurrentValues.SetValues(slayd);
                        existed.Image = fileName;
                    }
                    else
                    {
                        if (!slayd.Photo.IsOkay(1))
                        {
                            ModelState.AddModelError("Photo", "Size of photo mustn't more than 1MB");
                            return View(existed);
                        }
                        FileExtesion.FileDelete(_env.WebRootPath, @"assets/Image/Slider", existed.Image);
                        _context.Entry(existed).CurrentValues.SetValues(slayd);
                        existed.Image = await slayd.Photo.FileCreate(_env.WebRootPath, @"assets/Image/Slider");
                        
                    }
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlaydExists(slayd.Id))
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
            return View(slayd);
        }



        private bool SlaydExists(int id)
        {
            return _context.Slayds.Any(e => e.Id == id);
        }
    }
}
