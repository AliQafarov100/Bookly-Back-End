using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Extensions;
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
            List<Author> authors = await _context.Authors.Include(a => a.AuthorAwards).Include(s => s.AuthorSocialMedias).ToListAsync();
            return View(authors);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Awards = await _context.Awards.ToListAsync();
            ViewBag.SocialMedias = await _context.SocialMedias.ToListAsync();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(Author author)
        {
            ViewBag.Awards = await _context.Awards.ToListAsync();
            ViewBag.SocialMedias = await _context.SocialMedias.ToListAsync();
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

            if (!author.IsBest)
            {
                author.IsBest = false;
            }
            
            author.AuthorAwards = new List<AuthorAward>();

            foreach(var awardId in author.AwardIds)
            {
                AuthorAward authorAward = new AuthorAward
                {
                    AwardId = awardId
                };
                author.AuthorAwards.Add(authorAward);
            }

            author.AuthorSocialMedias = new List<AuthorSocialMedia>();

            foreach(var mediaId in author.SocialMediaIds)
            {
                AuthorSocialMedia media = new AuthorSocialMedia
                {
                    SocialMediaId = mediaId
                };
                author.AuthorSocialMedias.Add(media);
            }

           await _context.Authors.AddAsync(author);
           await _context.SaveChangesAsync();

           return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Read(int id)
        {
            Author author = await _context.Authors.Include(a => a.AuthorAwards).Include(s => s.AuthorSocialMedias).
                FirstOrDefaultAsync(a => a.Id == id);
            return View(author);
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Awards = await _context.Awards.ToListAsync();
            ViewBag.SocialMedias = await _context.SocialMedias.ToListAsync();
            Author author = await _context.Authors.Include(a => a.AuthorAwards).Include(s => s.AuthorSocialMedias).
                FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return NotFound();
            return View(author);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Update(Author author,int id)
        {
            ViewBag.Awards = await _context.Awards.ToListAsync();
            ViewBag.SocialMedias = await _context.SocialMedias.ToListAsync();
            Author existedAuthor = await _context.Authors.Include(a => a.AuthorAwards).Include(a => a.AuthorSocialMedias).
                FirstOrDefaultAsync(a => a.Id == id);

            if (existedAuthor == null) return NotFound();


            if(author.Photo == null)
            {
                if (!author.IsBest)
                {
                    author.IsBest = false;
                }

                string filename = existedAuthor.Image;
                _context.Entry(existedAuthor).CurrentValues.SetValues(author);
                existedAuthor.Image = filename;
            }
            else
            {
                if (!author.Photo.IsOkay(1))
                {
                    ModelState.AddModelError("Photo", "Size of image mustn't more than 1MB!");
                    return View(existedAuthor);
                }
                if (!author.IsBest)
                {
                    author.IsBest = false;
                }
                FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\AuthorImage", existedAuthor.Image);
                _context.Entry(existedAuthor).CurrentValues.SetValues(author);
                existedAuthor.Image = await author.Photo.FileCreate(_env.WebRootPath, @"assets\Image\AuthorImage");
                
            }

            
            List<AuthorAward> removableAward = existedAuthor.AuthorAwards.Where(a => !author.AwardIds.Contains(a.Id)).ToList();

            existedAuthor.AuthorAwards.RemoveAll(aw => removableAward.Any(r => r.Id == aw.Id));

            foreach(var awardId in author.AwardIds)
            {
                AuthorAward authorAward = new AuthorAward
                {
                    AuthorId = existedAuthor.Id,
                    AwardId = awardId
                };
                existedAuthor.AuthorAwards.Add(authorAward);
            }

            List<AuthorSocialMedia> removableSocialMedia = existedAuthor.AuthorSocialMedias.Where(s => !author.SocialMediaIds.Contains(s.Id)).ToList();

            existedAuthor.AuthorSocialMedias.RemoveAll(rs => removableSocialMedia.Any(r => r.Id == rs.Id));

            foreach(var mediaId in author.SocialMediaIds)
            {
                AuthorSocialMedia socialMedia = new AuthorSocialMedia
                {
                    AuthorId = existedAuthor.Id,
                    SocialMediaId = mediaId
                };
                existedAuthor.AuthorSocialMedias.Add(socialMedia);
            }
           
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            

            Author author = await _context.Authors.FindAsync(id);

            if (author == null) return NotFound();

            return View(author);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirm(int id)
        {
            ViewBag.Awards = await _context.Awards.ToListAsync();
            ViewBag.SocialMedias = await _context.SocialMedias.ToListAsync();
            Author existedAuthor = await _context.Authors.Include(a => a.AuthorAwards).Include(a => a.AuthorSocialMedias).
                FirstOrDefaultAsync(a => a.Id == id);

            if (existedAuthor == null) return NotFound();
            FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\Category Book", existedAuthor.Image);
            _context.Remove(existedAuthor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
