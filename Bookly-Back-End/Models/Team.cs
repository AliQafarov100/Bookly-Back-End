using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }
        public List<TeamSocialMedia> TeamSocialMedias { get; set; }
    }
}
