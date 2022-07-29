﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Bookly_Back_End.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Bookly_Back_End.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BookController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _context.Books.AsQueryable();
            ViewBag.TotalPage = Math.Ceiling(((decimal)await query.CountAsync()) / 9);
            ViewBag.CurrentPage = page;
            List<Format> formats = await _context.Formats.ToListAsync();
            List<Language> languages = await _context.Languages.ToListAsync();
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Author> authors = await _context.Authors.ToListAsync();
            List<Book> books = await query.Include(b => b.BookImages).Skip((page - 1) * 9).ToListAsync();
            List<BookAuthor> bookAuthors = await _context.BookAuthors.ToListAsync();
            List<Discount> discounts = await _context.Discounts.ToListAsync();

            BookVM model = new BookVM
            {
                Formats = formats,
                Languages = languages,
                Categories = categories,
                Authors = authors,
                BookAuthors = bookAuthors,
                Books = books,
                Discounts = discounts
            };
            return View(model);
        }

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null && id == 0)
            {
                return NotFound();
            }
            Book book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return NotFound();
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                BasketItem existed = await _context.BasketItems.
                    FirstOrDefaultAsync(bi => bi.AppUserId == user.Id && bi.BookId == book.Id);
                if (existed == null)
                {
                    BasketItem item = new BasketItem
                    {
                        Book = book,
                        AppUser = user,
                        Count = 1,
                        Price = book.Price

                    };
                    _context.BasketItems.Add(item);
                }
                else
                {
                    existed.Count++;
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                string basketStr = HttpContext.Request.Cookies["Basket"];

                List<BasketCookieItemVM> basket;
                if (string.IsNullOrEmpty(basketStr))
                {
                    basket = new List<BasketCookieItemVM>();
                    BasketCookieItemVM cookie = new BasketCookieItemVM
                    {
                        Id = book.Id,
                        Count = 1
                    };
                    basket.Add(cookie);
                    basketStr = JsonConvert.SerializeObject(basket);
                }
                else
                {
                    basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);

                    BasketCookieItemVM existedCookie = basket.FirstOrDefault(c => c.Id == book.Id);
                    if(existedCookie == null)
                    {
                        BasketCookieItemVM cookie = new BasketCookieItemVM
                        {
                            Id = book.Id,
                            Count = 1
                        };

                        basket.Add(cookie);
                    }
                    else
                    {
                        existedCookie.Count++;
                    }
                    
                }
                basketStr = JsonConvert.SerializeObject(basket);

                HttpContext.Response.Cookies.Append("Basket", basketStr);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}