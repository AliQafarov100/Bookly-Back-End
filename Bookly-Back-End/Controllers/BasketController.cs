using System;
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
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Cart()
        {

            return View();
        }
        public async Task<IActionResult> UpdateCart(int id, int count)
        {
            Book book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            TempData["Unavailable"] = null;
            book.Counter = count;

            _context.SaveChanges();

            if (book == null) return NotFound();
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                BasketItem existed = await _context.BasketItems.
                    FirstOrDefaultAsync(bi => bi.AppUserId == user.Id && bi.BookId == book.Id);

                existed.Count += book.Counter;

                await _context.SaveChangesAsync();
            }
            else
            {
                string basketStr = HttpContext.Request.Cookies["Basket"];

                List<BasketCookieItemVM> basket;
                basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);

                BasketCookieItemVM existedCookie = basket.FirstOrDefault(c => c.Id == book.Id);

                existedCookie.Count = book.Counter;
                //books.Stock -= books.Counter;
                //_context.SaveChanges();
                //book.Stock = books.Stock;

                //if (book.Stock == 0)
                //{
                //    TempData["Unavailable"] = true;
                //}

                basketStr = JsonConvert.SerializeObject(basket);

                HttpContext.Response.Cookies.Append("Basket", basketStr);

            }


            return Json(new { status = 200 });
        }
    }
}
