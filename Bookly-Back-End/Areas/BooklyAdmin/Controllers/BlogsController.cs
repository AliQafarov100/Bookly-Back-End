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
using Bookly_Back_End.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class BlogsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogsController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: BooklyAdmin/Blogs
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page <= 0) return RedirectToAction("Index", "Blog");
            List<Blog> blogs = await _context.Blogs.ToListAsync();
            return View(blogs.ToPagedList(page, 5));
        }

        // GET: BooklyAdmin/Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: BooklyAdmin/Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BooklyAdmin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                if(blog.Image != null)
                {
                    if (!blog.Image.IsOkay(1))
                    {
                        ModelState.AddModelError("", "Image mustn't more than 1Mb");
                        return View();
                    }
                    blog.Photo = await blog.Image.FileCreate(_env.WebRootPath, @"assets\Image\Blog");
                }
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: BooklyAdmin/Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: BooklyAdmin/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }
            Blog existed = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    if(blog.Image == null)
                    {
                        string filename = existed.Photo;
                        _context.Entry(existed).CurrentValues.SetValues(blog);
                        existed.Photo = filename;
                    }
                    else
                    {
                        if (!blog.Image.IsOkay(1))
                        {
                            ModelState.AddModelError("Photo", "Image mustn't more than 1Mb");
                            return View();
                        }
                        else
                        {
                            FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\Blog", existed.Photo);
                            _context.Entry(existed).CurrentValues.SetValues(blog);
                            existed.Photo = await blog.Image.FileCreate(_env.WebRootPath, @"assets\Image\Blog");
                        }
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: BooklyAdmin/Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: BooklyAdmin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\Blog", blog.Photo);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
