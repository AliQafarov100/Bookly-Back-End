using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class ContactFormController : Controller
    {
        private readonly AppDbContext _context;

        public ContactFormController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page <= 0) return RedirectToAction("Index", "Categories");
            List<ContactForm> forms = await _context.ContactForms.ToListAsync();
            return View(forms.ToPagedList(page, 5));
        }
    }
}
