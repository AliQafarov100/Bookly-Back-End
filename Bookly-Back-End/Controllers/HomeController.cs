using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Bookly_Back_End.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            Slayd slayd = await _context.Slayds.FirstOrDefaultAsync();
            List<Support> supports = await _context.Supports.ToListAsync();
            Festival festival = await _context.Festivals.FirstOrDefaultAsync();
            Offer offer = await _context.Offers.FirstOrDefaultAsync();
            Gift gift = await _context.Gifts.FirstOrDefaultAsync();
            List<Sponsor> sponsors = await _context.Sponsors.ToListAsync();
            HomeVM model = new HomeVM
            {
                Slayd = slayd,
                Supports = supports,
                Festival = festival,
                Offer = offer,
                Gift = gift,
                Sponsors = sponsors
            };
            return View(model);
        }
    }
}
