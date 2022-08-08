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

namespace Bookly_Back_End.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _manager;

        public OrderController(AppDbContext context,UserManager<AppUser> manager)
        {
            _context = context;
            _manager = manager;
        }
        public async Task<IActionResult> Checkout()
        {
            ViewBag.Cities = await _context.Cities.ToListAsync();
            AppUser user = await _manager.FindByNameAsync(User.Identity.Name);
            List<BasketItem> items = await _context.BasketItems.Include(b => b.AppUser)
                .Include(b => b.Book).Include(b => b.Book.Discount).Where(b => b.AppUserId == user.Id).ToListAsync();
            List<Book> books = await _context.Books.Include(i => i.BookImages).
               Include(a => a.BookAuthors).ToListAsync();
            List<City> cities = await _context.Cities.ToListAsync();
            List<Delivery> deliveries = await _context.Deliveries.ToListAsync();
            Country country = await _context.Countries.FirstOrDefaultAsync();
 
            OrderVM model = new OrderVM
            {
                Cities = cities,
                Deliveries = deliveries,
                Country = country,
                BasketItems = items,
                Books = books
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _manager.FindByNameAsync(User.Identity.Name);

            List<BasketItem> items = await _context.BasketItems.Include(b => b.AppUser)
                .Include(b => b.Book).Include(b => b.Book.Discount).Where(b => b.AppUserId == user.Id).ToListAsync();
            
            ViewBag.Cities = await _context.Cities.ToListAsync();

            order.Status = null;
            order.TotalPrice = default;
            order.BasketItems = items;
            order.AppUser = user;
            order.OrderDate = DateTime.Now;
            foreach (var item in items)
            {
                if(item.Book.DiscountId != null)
                {
                    order.TotalPrice += (item.Price - ((item.Price * item.Book.Discount.DiscountPercent) / 100)) * item.Count;
                }
                else
                {
                    order.TotalPrice += item.Price * item.Count;
                }
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
