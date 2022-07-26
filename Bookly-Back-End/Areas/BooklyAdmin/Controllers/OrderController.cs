﻿using System;
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
using X.PagedList;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderConfirmation _confirmation;

        public OrderController(AppDbContext context, UserManager<AppUser> userManager, IOrderConfirmation confirmation)
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
        public async Task<IActionResult> Edit(int id,string username)
        {
            
            Order order = await _context.Orders.Include(o => o.OrderProducts).ThenInclude(o => o.Book.BookImages)
                .ThenInclude(o => o.Book.Discount)
                .Include(o => o.AppUser).Include(o => o.City).FirstOrDefaultAsync(o => o.Id == id && o.AppUser.UserName == username);
            
            return View(order);
        }

        public async Task<IActionResult> Accept(int id,string message)
        {
            Order order = await _context.Orders.Include(o => o.AppUser).FirstOrDefaultAsync(o => o.Id == id);
            AppUser user = await _userManager.FindByEmailAsync(order.AppUser.Email);

            if (order == null) return NotFound();
            order.Status = true;
            order.AdminMessage = message;
           _context.SaveChanges();


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("tu8cvbxnx@code.edu.az", "Bookly");
            mail.To.Add(new MailAddress(user.Email));

            mail.Subject = "Order";
            mail.Body = $"{message}";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("tu8cvbxnx@code.edu.az", "zkgawykhucvuahin");
            smtp.Send(mail);

            return Json(new { status = 200 });
        }
        public async Task<IActionResult> Reject(int id, string message)
        {
            Order order = await _context.Orders.Include(o => o.AppUser).FirstOrDefaultAsync(o => o.Id == id);
            AppUser user = await _userManager.FindByEmailAsync(order.AppUser.Email);

            if (order == null) return NotFound();
            order.Status = false;
            order.AdminMessage = message;
            _context.SaveChanges();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("tu8cvbxnx@code.edu.az", "Bookly");
            mail.To.Add(new MailAddress(user.Email));

            mail.Subject = "Order";
            mail.Body = $"{message}";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("tu8cvbxnx@code.edu.az", "zkgawykhucvuahin");
            smtp.Send(mail);

            return Json(new { status = 200 });
        }
    }
}
