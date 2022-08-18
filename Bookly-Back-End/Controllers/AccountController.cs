using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            string link = Url.Action(nameof(VerifyEmail), "Account", new { email = appUser.Email, token }, Request.Scheme,
                Request.Host.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("tu8cvbxnx@code.edu.az", "Bookly Company");
            mail.To.Add(new MailAddress(appUser.Email));
            mail.Subject = "Verify Email";

            string body = string.Empty;

            using (StreamReader reader = new StreamReader(@"wwwroot/assets/template/verifyemail.html"))
            {
                body = reader.ReadToEnd();
            }
            mail.Body = body.Replace("{{link}}", link);
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.Credentials = new NetworkCredential("tu8cvbxnx@code.edu.az", "zkgawykhucvuahin");
            smtp.Send(mail);
            TempData["Verify"] = true;
            await _userManager.AddToRoleAsync(appUser, Roles.Member.ToString());

            await _signIn.SignInAsync(appUser, true);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();
            await _userManager.ConfirmEmailAsync(user, token);

            await _signIn.SignInAsync(user, true);
            TempData["Verified"] = true;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> ForgotPassword(AccountVM account)
        {
            AppUser user = await _userManager.FindByEmailAsync(account.AppUser.Email);
            if (user == null) return BadRequest();

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action(nameof(ResetPassword),"Account",new { email = user.Email,token},Request.Scheme,
                Request.Host.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("tu8cvbxnx@code.edu.az", "Bookly");
            mail.To.Add(new MailAddress(user.Email));

            mail.Subject = "Reset Password";
            mail.Body = $"<a href='{link}'>Please click here to reset your password</a>";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.Credentials = new NetworkCredential("tu8cvbxnx@code.edu.az", "zkgawykhucvuahin");
            smtp.Send(mail);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ResetPassword(string email,string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();

            AccountVM model = new AccountVM
            {
                AppUser = user,
                Token = token
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ResetPassword(AccountVM account)
        {
           
            AppUser user = await _userManager.FindByEmailAsync(account.AppUser.Email);
            AccountVM model = new AccountVM
            {
                AppUser = user,
                Token = account.Token
            };

            if (!ModelState.IsValid) return View(model);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, account.Token, account.Password);
            if (!result.Succeeded)
            {
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user == null) return NotFound();
            IList<string> roles = await _userManager.GetRolesAsync(user);

            string role = roles.FirstOrDefault(r => r.ToLower().Trim() == Roles.Member.ToString().ToLower().Trim());

            if (user.IsBlock)
            {
                ModelState.AddModelError("", "This user is blocked!Please contact with admin!");
                return View();
            }
            if (login.RememberMe)
            {
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signIn
                    .PasswordSignInAsync(user,login.Password, true, true);
                if (!signInResult.Succeeded)
                {
                    if (signInResult.IsLockedOut)
                    {
                        ModelState.AddModelError("", "You are is blocked for 5 minutes!");
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
