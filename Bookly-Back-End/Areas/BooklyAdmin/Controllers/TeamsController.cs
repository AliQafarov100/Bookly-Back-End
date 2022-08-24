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
        private readonly IWebHostEnvironment _env;

        public TeamsController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var team = _context.Teams.AsQueryable();
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

            if (team.Photo != null)
            {
                if (team.Photo.IsOkay(1))
                {
                    team.Image = await team.Photo.FileCreate(_env.WebRootPath, @"assets\Image\Team");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please choose image!");
                return View();
            }

            team.TeamSocialMedias = new List<TeamSocialMedia>();
            foreach (var socialId in team.SocialMediaIds)
            {
                TeamSocialMedia media = new TeamSocialMedia
                {
                    SocialMediaId = socialId
                };
                team.TeamSocialMedias.Add(media);
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
            Team team = await _context.Teams.Include(t => t.TeamSocialMedias)
                .Include(t => t.Profession).FirstOrDefaultAsync(i => i.Id == id);

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

            if (team.Photo == null)
            {
                string fileName = existedTeam.Image;
                _context.Entry(existedTeam).CurrentValues.SetValues(team);
                existedTeam.Image = fileName;
            }
            else
            {
                if (!team.Photo.IsOkay(1))
                {
                    ModelState.AddModelError("Photo", "Image mustn't size of more than 1mb!");
                    return View();    
                }

                FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\Team", existedTeam.Image);
                _context.Entry(existedTeam).CurrentValues.SetValues(team);
                existedTeam.Image = await team.Photo.FileCreate(_env.WebRootPath, @"assets\Image\Team");
            }

            List<TeamSocialMedia> removeable = existedTeam.TeamSocialMedias.Where(t => !team.SocialMediaIds.Contains(t.Id)).ToList();
            existedTeam.TeamSocialMedias.RemoveAll(ri => removeable.Any(i => i.Id == ri.Id));
            foreach (var mediaId in team.SocialMediaIds)
            {
                TeamSocialMedia media = new TeamSocialMedia
                {
                    TeamId = existedTeam.Id,
                    SocialMediaId = mediaId
                };
                existedTeam.TeamSocialMedias.Add(media);
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
