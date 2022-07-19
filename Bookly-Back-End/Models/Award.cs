using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class Award
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public List<AuthorAward> AuthorAwards { get; set; }

    }
}
