using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Extensions;
using Bookly_Back_End.Interfaces;
using Bookly_Back_End.Models;
using Bookly_Back_End.Utilities;
using Bookly_Back_End.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderConfirmation _confirmation;

        public OrderController(AppDbContext context,UserManager<AppUser> userManager,IOrderConfirmation confirmation)
        {
            _context = context;
            _userManager = userManager;
            _confirmation = confirmation;
        }
        public async Task<IActionResult> Orders()
        {
            List<Order> orders = await _context.Orders.Include(o => o.City).Include(o => o.AppUser).ToListAsync();
            return View(orders);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Order order = await _context.Orders.Include(o => o.OrderProducts).ThenInclude(o => o.Book.BookImages)
                .ThenInclude(o => o.Book.Discount)
                .Include(o => o.AppUser).Include(o => o.City).FirstOrDefaultAsync(o => o.Id == id);
            return View(order);
        }

        public async Task<IActionResult> Accept(string message, int id)
        {
            Order order = await _context.Orders.Include(o => o.AppUser).FirstOrDefaultAsync(o => o.Id == id);
            AppUser user = await _userManager.FindByEmailAsync(order.AppUser.Email);

            if (order == null) return NotFound();
            order.Status = true;
            order.AdminMessage = message;
            
            _context.SaveChanges();

            _confirmation.Send(id, message);

            return Json(new { status = 200});
        }
        public async Task<IActionResult> Reject(string message,int id)
        {
            Order order = await _context.Orders.Include(o => o.AppUser).FirstOrDefaultAsync(o => o.Id == id);
            AppUser user = await _userManager.FindByEmailAsync(order.AppUser.Email);

            if (order == null) return NotFound();
            order.Status = false;
            order.AdminMessage = message;
            _context.SaveChanges();

            _confirmation.Send(id, message);
            return Json(new { status = 200 });
        }
    }
}
