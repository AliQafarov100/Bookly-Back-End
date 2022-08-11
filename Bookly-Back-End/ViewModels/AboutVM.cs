using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.ViewModels
{
    public class AboutVM
    {
        public List<About> Abouts { get; set; }
        public List<TeamSocialMedia> TeamSocialMedias { get; set; }
        public List<Team> Teams { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
    }
}
