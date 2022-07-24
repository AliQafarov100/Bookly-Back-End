using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookly_Back_End.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }
}
