using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppUser> _role;

        public UserController(UserManager<AppUser> userManager,RoleManager<AppUser> role)
        {
            _userManager = userManager;
            _role = role;
        }
        public async Task<IActionResult> Users()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> AppointRole(string id)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(i => i.Id == id);
            if (user == null) return NotFound();

            return View(user);
        }
    }
}
