using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Interfaces;
using Bookly_Back_End.Models;
using Bookly_Back_End.ViewModels;
using Braintree;
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
        private readonly IBookOperation _repository;
        private readonly IQuery _query;


        public BookController(AppDbContext context, UserManager<AppUser> userManager, IBookOperation repository, IQuery query)
        {
            _context = context;
            _userManager = userManager;
            _repository = repository;
            _query = query;
        }
        public async Task<IActionResult> Index(string category, string sortBy,
            string author, int? minimum, int? maximum, string language, string format, int page = 1)
        {

            ViewBag.Author = author;
            var query = _repository.GetBookByFilter(category, author, sortBy, minimum,
                maximum, language, format);

            ViewBag.CurrentMaximum = maximum;
            ViewBag.CurentMinimum = minimum;
            ViewBag.CurentCategory = category;
            ViewBag.CurentLanguage = language;
            ViewBag.CurentFormat = format;
            ViewBag.TotalPage = Math.Ceiling(((decimal)await query.CountAsync()) / 12);
            ViewBag.CurrentPage = page;
            ViewBag.High = sortBy;
            ViewBag.Sort = sortBy;

            if (page <= 0) return RedirectToAction("Index", "Book");

            List<BookAuthor> bookAuthors = await query.Skip((page - 1) * 12)
                 .Include(b => b.Book).ThenInclude(b => b.BookImages).Include(b => b.Book.Discount)
                 .Include(b => b.Author).ToListAsync();

            BookVM model = new BookVM
            {
                Formats = await _query.Formats.ToListAsync(),
                Languages = await _query.Languages.ToListAsync(),
                Categories = await _query.Categories.ToListAsync(),
                Authors = await _query.Authors.ToListAsync(),
                BookAuthors = bookAuthors,
                FilteringPrices = await _query.FilteringPrices.ToListAsync()
            };
            return View(model);
        }

        public async Task<IActionResult> AddBasket(int count, Book books)
        {
            
            Book book = await _context.Books.FirstOrDefaultAsync(b => b.Id == books.Id);
            TempData["Unavailable"] = null;
            
            book.Counter = count;

            _context.SaveChanges();
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
                        Count = book.Counter,
                        Price = book.Price

                    };
                    _context.BasketItems.Add(item);
                }
                else
                {
                    existed.Count += book.Counter;
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
                        Count = book.Counter
                    };
                    
                    basket.Add(cookie);
                   
                    basketStr = JsonConvert.SerializeObject(basket);
                }
                else
                {
                    basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);

                    BasketCookieItemVM existedCookie = basket.FirstOrDefault(c => c.Id == book.Id);
                    if (existedCookie == null)
                    {
                        BasketCookieItemVM cookie = new BasketCookieItemVM
                        {
                            Id = book.Id,
                            Count = book.Counter
                        };

                        basket.Add(cookie);
                    }
                    else
                    {
                        existedCookie.Count += book.Counter;
                    }
                }
                basketStr = JsonConvert.SerializeObject(basket);

                HttpContext.Response.Cookies.Append("Basket", basketStr);

            }

            return Json(new { status = 200 });
        }
        public async Task<IActionResult> RemoveBasket(int id)
        {

            Book book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (User.Identity.IsAuthenticated)
            {
                AppUser existeUser = await _userManager.FindByNameAsync(User.Identity.Name);
                BasketItem existedItem = await _context.BasketItems.FirstOrDefaultAsync(bi => bi.BookId == id);

                if (existedItem != null)
                {
                    _context.BasketItems.RemoveRange(existedItem);
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                string basketStr = HttpContext.Request.Cookies["Basket"];
                List<BasketCookieItemVM> basket;

                if (!string.IsNullOrEmpty(basketStr))
                {
                    basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);
                    BasketCookieItemVM existed = basket.FirstOrDefault(c => c.Id == book.Id);

                    if (existed != null)
                    {
                        basket.Remove(existed);
                    }

                    basketStr = JsonConvert.SerializeObject(basket);
                }
                HttpContext.Response.Cookies.Append("Basket", basketStr);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddToWishList(int? id)
        {
            if (id is null && id == 0) return BadRequest();
            Book book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            if (User.Identity.IsAuthenticated)
            {
                AppUser existedUser = await _userManager.FindByNameAsync(User.Identity.Name);
                WishListItem existed = await _context.WishListItems
                    .FirstOrDefaultAsync(w => w.BookId == book.Id && w.AppUserId == existedUser.Id);
                if (existed == null)
                {
                    WishListItem item = new WishListItem
                    {
                        AppUser = existedUser,
                        Price = book.Price,
                        Book = book,
                        Count = 1
                    };
                    _context.WishListItems.Add(item);
                }
                else
                {

                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveWishList(int? id)
        {
            if (id is null && id == 0) return BadRequest();

            Book book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return NotFound();
            if (User.Identity.IsAuthenticated)
            {
                AppUser existedUser = await _userManager.FindByNameAsync(User.Identity.Name);
                WishListItem existedWishList = await _context.WishListItems
                    .FirstOrDefaultAsync(w => w.AppUserId == existedUser.Id && w.BookId == book.Id);
                if (existedWishList != null)
                {
                    _context.WishListItems.Remove(existedWishList);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
