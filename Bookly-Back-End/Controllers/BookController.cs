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
using Newtonsoft.Json;

namespace Bookly_Back_End.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBookOperation _repository;

        public BookController(AppDbContext context,UserManager<AppUser> userManager,IBookOperation repository)
        {
            _context = context;
            _userManager = userManager;
            _repository = repository;
        }
        public async Task<IActionResult> Index(string category,string highToLow,
            string author,int? minimum,int? maximum, string language,string format,int page = 1)
        {
            ViewBag.Author = author;
            var query = _repository.GetBookByFilter(category,author,highToLow,minimum,
                maximum,language,format);

            ViewBag.CurrentMaximum = maximum;
            ViewBag.CurentMinimum = minimum;
            ViewBag.CurentCategory = category;
            ViewBag.CurentLanguage = language;
            ViewBag.CurentFormat = format;
            ViewBag.TotalPage = Math.Ceiling(((decimal)await query.CountAsync()) / 12);
            ViewBag.CurrentPage = page;
            ViewBag.High = highToLow;
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Book> books = await _context.Books.Include(i => i.BookImages)
                .Include(a => a.BookAuthors).ToListAsync();
            List<Format> formats = await _context.Formats.ToListAsync();
            List<Language> languages = await _context.Languages.ToListAsync();
            List<Author> authors = await _context.Authors.Include(a => a.BookAuthors).ToListAsync();
            List<Book> anotherBooks = await _context.Books.Include(i => i.BookImages).
                Include(a => a.BookAuthors).ToListAsync();
            List<BookAuthor> bookAuthors = await query.Skip((page - 1) * 12).ToListAsync();
            List<Discount> discounts = await _context.Discounts.ToListAsync();
            List<FilteringPrice> filteringPrices = await _context.FilteringPrices.ToListAsync();
            
            
            BookVM model = new BookVM
            {
                Formats = formats,
                Languages = languages,
                Categories = categories,
                Authors = authors,
                BookAuthors = bookAuthors,
                AllBooks = books,
                AnotherBooks = anotherBooks,
                Discounts = discounts,
                FilteringPrices = filteringPrices
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

        public async Task<IActionResult> RemoveBasket(int? id)
        {
            if (id is null && id == 0) return NotFound();
            Book book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (User.Identity.IsAuthenticated)
            {
                AppUser existeUser = await _userManager.FindByNameAsync(User.Identity.Name);
                BasketItem existedItem = await _context.BasketItems.FirstOrDefaultAsync(bi => bi.BookId == id);

                if (existedItem != null && existedItem.Count > 1)
                {
                    existedItem.Count--;
                }
                else if (existedItem != null && existedItem.Count == 1)
                {
                    _context.BasketItems.Remove(existedItem);
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

                    if (existed != null && existed.Count > 1)
                    {
                        existed.Count--;
                    }
                    else if (existed != null && existed.Count == 1)
                    {
                        basket.Remove(existed);
                    }

                    basketStr = JsonConvert.SerializeObject(basket);
                }
                HttpContext.Response.Cookies.Append("Basket", basketStr);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
