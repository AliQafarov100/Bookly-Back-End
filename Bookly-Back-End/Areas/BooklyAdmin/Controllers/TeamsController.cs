using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Extensions;
using Bookly_Back_End.Interfaces;
using Bookly_Back_End.Models;
using Bookly_Back_End.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class TeamsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICrudOperation _crud;
        private readonly IWebHostEnvironment _env;

        public TeamsController(AppDbContext context,ICrudOperation crud,IWebHostEnvironment env)
        {
            _context = context;
            _crud = crud;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var team = _crud.Teams.AsQueryable();
            List<Team> teams = await team.Include(t => t.Profession).Include(t => t.TeamSocialMedias).ToListAsync();
            return View(teams);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.SocialMedias = await _context.SocialMedias.ToListAsync();
            ViewBag.Profession = await _context.Professions.ToListAsync();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(Team team)
        {
            ViewBag.SocialMedias = await _context.SocialMedias.ToListAsync();
            ViewBag.Profession = await _context.Professions.ToListAsync();
            if (!ModelState.IsValid) return View();

            _crud.Create(team);
            if (!team.Photo.IsOkay(1))
            {
                ModelState.AddModelError("", "Size of image mustn't more than 1mb");
                return View();
            }

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Read(int id)
        {
            Team team = await _context.Teams.Include(t => t.TeamSocialMedias).Include(t => t.Profession)
                .FirstOrDefaultAsync(t => t.Id == id);
            return View(team);
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.SocialMedias = await _context.SocialMedias.ToListAsync();
            ViewBag.Profession = await _context.Professions.ToListAsync();
            Team team = await _context.Teams.FirstOrDefaultAsync(i => i.Id == id);

            if (team == null) return NotFound();

            return View(team);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Update(Team team,int id)
        {
            ViewBag.SocialMedias = await _context.SocialMedias.ToListAsync();
            ViewBag.Profession = await _context.Professions.ToListAsync();

            Team existedTeam = await _context.Teams.Include(t => t.Profession).Include(t => t.TeamSocialMedias)
                 .FirstOrDefaultAsync(t => t.Id == id);

            _crud.Update(team,id);
            if (!team.Photo.IsOkay(1))
            {
                ModelState.AddModelError("", "Size of image mustn't more than 1mb");
                return View(existedTeam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Team team = await _context.Teams.Include(t => t.Profession).FirstOrDefaultAsync(t => t.Id == id);
            if (team == null) return NotFound();

            return View(team);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {

            Team existed = await _context.Teams.FirstOrDefaultAsync(i => i.Id == id);

            if (existed == null) return NotFound();

            FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\Team", existed.Image);
            _context.Teams.Remove(existed);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
