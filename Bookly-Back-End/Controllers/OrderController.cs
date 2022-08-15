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

        public OrderController(AppDbContext context, UserManager<AppUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        public async Task<IActionResult> Checkout()
        {
            ViewBag.Cities = await _context.Cities.ToListAsync();
            Country country = await _context.Countries.Include(c => c.Cities).FirstOrDefaultAsync();
            AppUser user = await _manager.FindByNameAsync(User.Identity.Name);
            OrderVM model = new OrderVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                BasketItems = _context.BasketItems.Include(b => b.Book).ThenInclude(b => b.BookImages).Include(b => b.Book.Discount)
                .Where(b => b.AppUserId == user.Id).ToList(),
                Country = country,
               
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderVM orderVM)
        {
        
            ViewBag.Cities = await _context.Cities.ToListAsync();
            AppUser user = await _manager.FindByNameAsync(User.Identity.Name);
            Country country = await _context.Countries.Include(c => c.Cities).FirstOrDefaultAsync();
            OrderVM model = new OrderVM
            {
                FirstName = orderVM.FirstName,
                LastName = orderVM.LastName,
                UserName = orderVM.UserName,
                Email = orderVM.Email,
                BasketItems = _context.BasketItems.Include(b => b.Book).ThenInclude(b => b.BookImages).Include(b => b.Book.Discount)
                .Where(b => b.AppUserId == user.Id).ToList(),
                Country = country,
                
            };
            if (!ModelState.IsValid) return View(model);

            TempData["Success"] = false;
            
            if (model.BasketItems.Count == 0) return RedirectToAction("Index", "Home");

            Order order = new Order
            {
                Address = orderVM.Address,
                TotalPrice = 0,
                CityId = orderVM.Country.CityId,
                AppUserId = user.Id,
                OrderDate = DateTime.Now,
                Message = orderVM.Message
            };

            int counter = default;
            foreach (BasketItem item in model.BasketItems)
            {
                order.TotalPrice += item.Book.DiscountId == null ? item.Count * item.Book.Price : (item.Book.Price - ((item.Book.Price * item.Book.Discount.DiscountPercent) / 100)) * item.Count;
                OrderProduct product = new OrderProduct
                {
                    Name = item.Book.Name,
                    Price = item.Book.DiscountId == null ? item.Count * item.Book.Price : item.Book.Price - ((item.Book.Price * item.Book.Discount.DiscountPercent) / 100),
                    AppUserId = user.Id,
                    BookId = item.Book.Id,
                    Order = order
                };
                counter += item.Count;
                _context.OrderProducts.Add(product);
            }
            ViewBag.bookCount = counter;
            _context.BasketItems.RemoveRange(model.BasketItems);
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            TempData["Success"] = true;

            return RedirectToAction("Index", "Home");
        }
    }
}
