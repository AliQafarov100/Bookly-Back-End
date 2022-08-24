using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;

namespace Bookly_Back_End.ViewModels
{
    public class DashBoardVM
    {
        public List<Order> Orders { get; set; }
        public List<Book> Books { get; set; }
        public List<AppUser> AppUsers { get; set; }
        public int VisitorCount { get; set; }
    }
}
