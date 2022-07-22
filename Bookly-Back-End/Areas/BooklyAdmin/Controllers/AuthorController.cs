using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Bookly_Back_End.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AuthorController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Author> authors = await _context.Authors.Include(a => a.AuthorAwards).Include(a => a.SocialMedias).ToListAsync();
            return View(authors);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Awards = await _context.Awards.ToListAsync();
            ViewBag.SocialMedia = await _context.SocialMedias.ToListAsync();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(Author author)
        {
            ViewBag.Awards = await _context.Awards.ToListAsync();
            ViewBag.SocialMedia = await _context.SocialMedias.ToListAsync();
            if (!ModelState.IsValid) return View();
            
            if(author.Photo != null)
            {
                if (!author.Photo.IsOkay(1))
                {
                    ModelState.AddModelError("Photo", "Size of image must be more than 1MB");
                    return View();
                }
                author.Image = await author.Photo.FileCreate(_env.WebRootPath, @"assets\Image\AuthorImage");
            }
            else
            {
                ModelState.AddModelError("", "Please choose image!");
                return View();
            }
           

            author.SocialMedias = new List<SocialMedia>();

            foreach(var media in author.SocialMedias)
            {
                SocialMedia socialMedia = new SocialMedia
                {
                    Author = author
                };
                author.SocialMedias.Add(socialMedia);
            };

            author.AuthorAwards = new List<AuthorAward>();

            foreach(var awardId in author.AwardIds)
            {
                AuthorAward authorAward = new AuthorAward
                {
                    AwardId = awardId
                };
                author.AuthorAwards.Add(authorAward);
            }

           await _context.Authors.AddAsync(author);
           await _context.SaveChangesAsync();

           return RedirectToAction(nameof(Index));
        }
    }
}
