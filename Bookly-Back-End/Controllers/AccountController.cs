﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.Models;
using Bookly_Back_End.Utilities;
using Bookly_Back_End.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bookly_Back_End.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signIn;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signIn,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signIn = signIn;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = new AppUser
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.Username
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, register.Password);

            if (!result.Succeeded)
            {
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(appUser, Roles.Member.ToString());
            await _signIn.SignInAsync(appUser, false);

            return RedirectToAction("Index", "Home");

        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user == null) return NotFound();
            IList<string> roles = await _userManager.GetRolesAsync(user);

            string role = roles.FirstOrDefault(r => r.ToLower().Trim() == Roles.Member.ToString().ToLower().Trim());

            if (login.RememberMe)
            {
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signIn
                    .PasswordSignInAsync(user,login.Password, true, true);
                if (!signInResult.Succeeded)
                {
                    if (signInResult.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Count of wrong enter is 3!You are blocked on 5 minutes!");
                        return View();
                    }
                    ModelState.AddModelError("", "Username or email incorrect!");
                    return View();
                }
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signIn
                    .PasswordSignInAsync(user, login.Password, false, true);
                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Count of wrong enter is 3!You are blocked on 5 minutes!");
                        return View();
                    }
                }
                ModelState.AddModelError("", "Username or email incorrect!");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();    

            EditUserVM edit = new EditUserVM
            {
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Username = user.UserName,
                Email = user.Email
            };

            return View(edit);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(EditUserVM user)
        {
            AppUser existed = await _userManager.FindByNameAsync(User.Identity.Name);

            EditUserVM edit = new EditUserVM
            {
                Firstname = existed.FirstName,
                Lastname = existed.LastName,
                Username = existed.UserName,
                Email = existed.Email
            };

            if (!ModelState.IsValid) return View();

            bool result = user.Password == null && user.ConfirmPassword == null && user.CurrentPassword != null;

            if(user.Email == null || user.Email != existed.Email)
            {
                ModelState.AddModelError("", "You cannot chage email address!");
                return View(edit);
            }

            if (result)
            {
                existed.FirstName = user.Firstname;
                existed.LastName = user.Lastname;
                existed.UserName = user.Username;
                await _userManager.UpdateAsync(existed);
            }
            else
            {
                existed.FirstName = user.Firstname;
                existed.LastName = user.Lastname;
                existed.UserName = user.Username;
                if(user.CurrentPassword == user.Password)
                {
                    ModelState.AddModelError("", "You cannot change with the same password");
                    return View();
                }

                IdentityResult resultEdit = await _userManager.ChangePasswordAsync(existed, user.CurrentPassword, user.Password);
                if (!resultEdit.Succeeded)
                {
                    foreach(IdentityError error in resultEdit.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(edit);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Member.ToString() });
            await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() });
            await _roleManager.CreateAsync(new IdentityRole { Name = Roles.SuperAdmin.ToString() });
        }
    }
}
