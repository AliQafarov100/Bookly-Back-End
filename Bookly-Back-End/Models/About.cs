using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class About
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string SubTitle { get; set; }
        public string SecondSubTitle { get; set; }
    }
}
