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
using Stripe;

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
        public async Task<IActionResult> Increase(int id)
        {
            Book book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
           
            if (book == null) return NotFound();
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                BasketItem existed = await _context.BasketItems.
                    FirstOrDefaultAsync(bi => bi.AppUserId == user.Id && bi.BookId == book.Id);

                if (existed.Count == existed.Book.Stock)
                {
                    existed.Count = existed.Book.Stock;
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
                basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);

                BasketCookieItemVM existedCookie = basket.FirstOrDefault(c => c.Id == book.Id);

                if (existedCookie.Count == book.Stock)
                {
                    existedCookie.Count = book.Stock;
                }
                else
                {
                    existedCookie.Count++;
                }
                basketStr = JsonConvert.SerializeObject(basket);

                HttpContext.Response.Cookies.Append("Basket", basketStr);

            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Cart","Basket");
        }

        public async Task<IActionResult> Decrease(int id)
        {
            Book book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                BasketItem existed = await _context.BasketItems.
                    FirstOrDefaultAsync(bi => bi.AppUserId == user.Id && bi.BookId == book.Id);

                if(existed.Count == 1)
                {
                    existed.Count = 1;
                }
                else
                {
                    existed.Count--;
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                string basketStr = HttpContext.Request.Cookies["Basket"];

                List<BasketCookieItemVM> basket;
                basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);

                BasketCookieItemVM existedCookie = basket.FirstOrDefault(c => c.Id == book.Id);

                if (existedCookie.Count == 1)
                {
                    existedCookie.Count = 1;
                }
                else
                {
                    existedCookie.Count--;
                }
                basketStr = JsonConvert.SerializeObject(basket);

                HttpContext.Response.Cookies.Append("Basket", basketStr);

            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Cart", "Basket");
        }

        [TempData]
        public string TotalAmount { get; set; }
        public async Task<IActionResult> PayOperation(int id)
        {
            
            Book book = await _context.Books.Include(b => b.Discount).FirstOrDefaultAsync(b => b.Id == id);
           
            ViewBag.Product = book;
            ViewBag.DollarAmount = book.Price;
            ViewBag.Total = Math.Round(ViewBag.DollarAmount, 2) * 100;
            ViewBag.Total = Convert.ToInt64(ViewBag.Total);
            long total = ViewBag.total;
            TotalAmount = total.ToString();

            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Proccesing(int id,string stripeToken, string stripeEmail)
        {
            AppUser user = await _userManager.FindByEmailAsync(stripeEmail);
            Book book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            var optionCust = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = user.FirstName,
                Phone = "0706448906"
            };
            var serviceCust = new CustomerService();
            Customer customer = serviceCust.Create(optionCust);
            var optionCharge = new ChargeCreateOptions
            {
                Amount = Convert.ToInt64(TempData["TotalAmount"]),
                Currency = "USD",
                Description = "Book purchase",
                Source = stripeToken,
                ReceiptEmail = stripeEmail
            };

            var service = new ChargeService();
            Charge charge = service.Create(optionCharge);
            if(charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                ViewBag.AmountPaid = Convert.ToDecimal(charge.Amount) % 100 / 100 + (charge.Amount) / 100;
                ViewBag.BalanceTxId = BalanceTransactionId;
                ViewBag.Customer = customer.Name;
            }
            TempData["Success Pay"] = true;
            return RedirectToAction("Index","Home");
        }
    }
}
