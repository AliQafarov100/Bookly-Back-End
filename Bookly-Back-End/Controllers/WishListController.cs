using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Controllers
{
    public class WishListController : Controller
    {
        private readonly AppDbContext _context;

        public WishListController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> WishList()
        {
            var queryWishList = _context.WishListItems.AsQueryable();
            List<WishListItem> wishLists = await queryWishList.Include(b => b.Book).
                ThenInclude(b => b.Discount).Include(w => w.Book.BookImages).ToListAsync();
            return View(wishLists);
        }
    }
}
