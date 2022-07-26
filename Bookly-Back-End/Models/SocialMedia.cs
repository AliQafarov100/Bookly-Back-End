﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class SocialMedia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public List<AuthorSocialMedia> AuthorSocialMedias { get; set; }
        public List<TeamSocialMedia> TeamSocialMedias { get; set; }
    }
}
