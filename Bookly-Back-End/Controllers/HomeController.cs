using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Interfaces;
using Bookly_Back_End.Models;
using Bookly_Back_End.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBookOperation _repository;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(AppDbContext context,IBookOperation repository,UserManager<AppUser> userManager)
        {
            _context = context;
            _repository = repository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string category)
        {
            var query = _repository.GetBookByCategory(category);
            Slayd slayd = await _context.Slayds.FirstOrDefaultAsync();
            List<Support> supports = await _context.Supports.ToListAsync();
            Festival festival = await _context.Festivals.FirstOrDefaultAsync();
            Offer offer = await _context.Offers.FirstOrDefaultAsync();
            Gift gift = await _context.Gifts.FirstOrDefaultAsync();
            List<Sponsor> sponsors = await _context.Sponsors.ToListAsync();
            List<Book> AllBooks = await _context.Books.Include(i => i.BookImages).
                Include(a => a.BookAuthors).ToListAsync();
            List<Book> anotherBooks = await _context.Books.Include(i => i.BookImages).
                Include(a => a.BookAuthors).ToListAsync();
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Author> authors = await _context.Authors.Include(b => b.BookAuthors).Include(a => a.AuthorAwards).ToListAsync();
            List<BookAuthor> bookAuthors = await query.Include(a => a.Author).ToListAsync();
            List<Award> awards = await _context.Awards.ToListAsync();
            List<AuthorAward> authorAwards = await _context.AuthorAwards.ToListAsync();
            List<SocialMedia> socialMedias = await _context.SocialMedias.ToListAsync();
            List<AuthorSocialMedia> authorSocialMedias = await _context.AuthorSocialMedias.ToListAsync();
            List<Discount> discounts = await _context.Discounts.ToListAsync();
            List<Blog> blogs = await _context.Blogs.ToListAsync();
          
            HomeVM model = new HomeVM
            {
                Slayd = slayd,
                Supports = supports,
                Festival = festival,
                Offer = offer,
                Gift = gift,
                Sponsors = sponsors,
                AllBooks = AllBooks,
                AnotherBooks = anotherBooks,
                Authors = authors,   
                Categories = categories,
                BookAuthors = bookAuthors,
                Awards = awards,
                AuthorAwards = authorAwards,
                SocialMedias = socialMedias,
                AuthorSocialMedias = authorSocialMedias,
                Discounts = discounts,
                Blogs = blogs
            };
            return View(model);
        }
    }
}
