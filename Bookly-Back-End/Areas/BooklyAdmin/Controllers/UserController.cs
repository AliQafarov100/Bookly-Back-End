﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Bookly_Back_End.Utilities;
using Bookly_Back_End.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookly_Back_End.Areas.BooklyAdmin.Controllers
{
    [Area("BooklyAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signIn;

        public UserController(UserManager<AppUser> userManager,AppDbContext context,SignInManager<AppUser> signIn)
        {
            _userManager = userManager;
            _context = context;
            _signIn = signIn;
        }
        public async Task<IActionResult> Users()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            
            return View(users);
        }
       
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] 
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(login.Username);

            if (user == null) return NotFound();

            if(!user.IsAdmin || !user.IsSuperAdmin)
            {
                ModelState.AddModelError("", "Only admin users can be sign in here!");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signIn.PasswordSignInAsync(user, login.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Incorrect password or username");
                return View();
            }

            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> AppointRole(string id)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(i => i.Id == id);
            if (user == null) return NotFound();
            
            return View(user);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> AppointRole(string id,AppUser user)
        {
            AppUser existed = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (existed == null) return NotFound();
            
            if (user.IsMember)
            {
                await _userManager.AddToRoleAsync(existed, Roles.Member.ToString());
            }
            if (user.IsAdmin)
            {
                await _userManager.AddToRoleAsync(existed, Roles.Admin.ToString());
            }
            if (user.IsSuperAdmin)
            {
                await _userManager.AddToRoleAsync(existed, Roles.SuperAdmin.ToString());
            }
            existed.IsSuperAdmin = user.IsSuperAdmin;
            existed.IsAdmin = user.IsAdmin;
            existed.IsMember = user.IsMember;
            await _userManager.UpdateAsync(existed);
            return RedirectToAction(nameof(Users));
        }

        public async Task<IActionResult> Lockout(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Lockout(AppUser appUser)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == appUser.Id);

            if (user == null) return NotFound();

            if (appUser.IsBlock)
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
            }
            else
            {
                user.LockoutEnd = null;
            }
            user.IsBlock = appUser.IsBlock;
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Users));
        }
    }
}
