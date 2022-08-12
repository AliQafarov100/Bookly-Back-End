using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Extensions;
using Bookly_Back_End.Models;
using Bookly_Back_End.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Interfaces
{
    public class CrudOperation : ICrudOperation
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CrudOperation(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IEnumerable<Team> Teams => _context.Teams;

        public async void Create(Team team)
        {
            if(team.Photo != null)
            {
                if (team.Photo.IsOkay(1))
                {
                    team.Image = await team.Photo.FileCreate(_env.WebRootPath, @"assets\Image\Team");
                }
            }
            team.TeamSocialMedias = new List<TeamSocialMedia>();
            foreach(var socialId in team.SocialMediaIds)
            {
                TeamSocialMedia media = new TeamSocialMedia
                {
                    SocialMediaId = socialId
                };
                team.TeamSocialMedias.Add(media);
            }
        }
        public async void Update(Team team, int id)
        {
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
                if (team.Photo.IsOkay(1))
                {
                    FileExtesion.FileDelete(_env.WebRootPath, @"assets\Image\Team", existedTeam.Image);
                    _context.Entry(existedTeam).CurrentValues.SetValues(team);
                    existedTeam.Image = await team.Photo.FileCreate(_env.WebRootPath, @"assets\Image\Team");
                }
            }

            List<TeamSocialMedia> removeable = existedTeam.TeamSocialMedias.Where(t => !team.SocialMediaIds.Contains(t.Id)).ToList();
            existedTeam.TeamSocialMedias.RemoveAll(ri => removeable.Any(i => i.Id == ri.Id));
            foreach(var mediaId in team.SocialMediaIds)
            {
                TeamSocialMedia media = new TeamSocialMedia
                {
                    SocialMediaId = mediaId
                };
                team.TeamSocialMedias.Add(media);
            }
           await _context.SaveChangesAsync();
        }
    }
}
