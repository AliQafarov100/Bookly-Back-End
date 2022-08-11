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
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> About()
        {
            var query = _context.Abouts.AsQueryable();
            var mediaquery = _context.TeamSocialMedias.AsQueryable();
            List<About> abouts = await query.ToListAsync();
            List<Team> teams = await _context.Teams.Include(t => t.Profession).Include(t => t.TeamSocialMedias).ToListAsync();
            List<TeamSocialMedia> socialMedias = await mediaquery.Include(s => s.SocialMedia).ToListAsync();
            AboutVM about = new AboutVM
            {
                Abouts = abouts,
                Teams = teams,
                TeamSocialMedias = socialMedias
            };
            return View(about);
        }
    }
}
