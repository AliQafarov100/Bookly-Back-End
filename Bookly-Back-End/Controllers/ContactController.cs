using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookly_Back_End.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Contact()
        {
            return View();
        }
        public async Task<IActionResult> Form(ContactForm contact)
        {
            if (!ModelState.IsValid) return View(contact);
            if (contact == null) return View();

            _context.ContactForms.Add(contact);
            TempData["Success"] = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Contact));
        }
    }
}
