using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Bookly_Back_End.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }
        //public async Task<IActionResult> Orders()
        //{ 
        //    List<Order> orders = await _context.Orders.Include(o => o.City).Include(o => o.AppUser).ToListAsync();
        //    return View(orders);
        //}
        //public async Task<IActionResult> OrdersDetail(int? id)
        //{
        //    if (id is null && id == 0) return NotFound();
        //    Order order = await _context.Orders.Include(o => o.City).Include(o => o.AppUser).Include(b => b.BasketItems)
        //        .FirstOrDefaultAsync(o => o.Id == id);
        //    List<BasketItem> items = await _context.BasketItems.Include(b => b.Book).Include(b => b.Book.BookImages)
        //        .Include(b => b.Book.Discount).ToListAsync();
        //    if (order == null) return NotFound();

        //    OrderVM model = new OrderVM
        //    {
        //        Order = order,
        //        BasketItems = items
        //    };
        //    return View(model);
        //}
    }
}
