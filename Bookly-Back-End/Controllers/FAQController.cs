using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Controllers
{
    public class FAQController : Controller
    {
        private readonly AppDbContext _context;

        public FAQController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> FAQ()
        {
            List<FAQ> fAQs = await _context.FAQs.ToListAsync();
            return View(fAQs);
        }
    }
}
