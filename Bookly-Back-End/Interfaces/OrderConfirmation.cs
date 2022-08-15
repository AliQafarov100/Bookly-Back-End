using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Interfaces
{
    public class OrderConfirmation : IOrderConfirmation
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderConfirmation(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async void Send(int id, string message)
        {
            Order order = await _context.Orders.Include(o => o.AppUser).FirstOrDefaultAsync(o => o.Id == id);
            AppUser user = await _userManager.FindByEmailAsync(order.AppUser.Email);

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
        }
    }
}
