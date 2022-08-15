using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Bookly_Back_End.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsMember { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public List<Order> Orders { get; set; }
        public List<WishListItem> WishListItems { get; set; }
    }
}
