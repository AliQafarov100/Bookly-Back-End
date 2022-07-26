﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Extensions;
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
        private readonly IQuery _query;

        public HomeController(AppDbContext context, IBookOperation repository, 
            UserManager<AppUser> userManager,IQuery query)
        {
            _context = context;
            _repository = repository;
            _userManager = userManager;
            _query = query;
        }
        public async Task<IActionResult> Index(string category,int count)
        {
            var query = _repository.GetBookByCategory(category);
            List<BookAuthor> bookAuthors = await query.Include(a => a.Author).Include(b => b.Book)
                .ThenInclude(b => b.BookImages).Include(b => b.Book.Discount).ToListAsync();

            HomeVM model = new HomeVM
            {
                Slayd = await _query.Slayds.FirstOrDefaultAsync(),
                Supports = _query.Supports,
                Festival = await _query.Festivals.FirstOrDefaultAsync(),
                Offer = await _query.Offers.FirstOrDefaultAsync(),
                Gift = await _query.Gifts.FirstOrDefaultAsync(),
                Sponsors = _query.Sponsors,
                Author = await _query.Authors.Include(b => b.AuthorAwards).ThenInclude(b => b.Award)
                .Include(b => b.AuthorSocialMedias).ThenInclude(a => a.SocialMedia).FirstOrDefaultAsync(a => a.IsBest == true),
                Categories = _query.Categories,
                BookAuthors = await query.ToListAsync(),
                Blogs = _query.Blogs,
                AnotherBooks = _context.Books.Include(b => b.BookImages).Include(b => b.BookAuthors)
                .Include(b => b.Discount).AsQueryable()
            };
            return View(model);
        }
    }
}
