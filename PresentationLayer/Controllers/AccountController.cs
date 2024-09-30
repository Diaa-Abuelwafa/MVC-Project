using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Controllers.Helpers;
using PresentationLayer.Models.ViewModels;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        public AccountController(UserManager<ApplicationUser> UserManager,
                                 SignInManager<ApplicationUser> SignInManager)
        {
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel Model)
        {
            if(ModelState.IsValid == true)
            {
                // Check For Unique UserName
                ApplicationUser User = await UserManager.FindByNameAsync(Model.UserName);

                if(User != null)
                {
                    ModelState.AddModelError("UserName", "This UserName Already Taken");
                    return View(Model);
                }

                User = null;

                // Check For Unique Email
                User = await UserManager.FindByEmailAsync(Model.Email);

                if(User != null)
                {
                    ModelState.AddModelError("Email", "This Email Already Taken");
                    return View(Model);
                }

                User = null;

                // Mapping
                User = new ApplicationUser();
                User.UserName = Model.UserName;
                User.Email = Model.Email;
                User.Address = Model.Address;
                User.IsAgree = Model.IsAgree;

                IdentityResult Result = await UserManager.CreateAsync(User, Model.Password);

                if(Result.Succeeded)
                {
                    // Make A SignIn Operation For This Account
                    await SignInManager.SignInAsync(User, false);

                    return RedirectToAction("Login", "Account");
                }

                foreach(var Error in Result.Errors)
                {
                    ModelState.AddModelError("", Error.Description);
                }

                return View(Model);
            }

            return View(Model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser User = await UserManager.FindByNameAsync(Model.UserName);

                if(User != null)
                {
                    bool Found = await UserManager.CheckPasswordAsync(User, Model.Password);

                    if(Found)
                    {
                        await SignInManager.SignInAsync(User, Model.RememberMe);

                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid Login");
            return View(Model);
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchEmail(ForgetPasswordViewModel Model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser User = await UserManager.FindByEmailAsync(Model.Email);

                if(User != null)
                {
                    // Send Email
                    EmailViewModel Email = new EmailViewModel();
                    Email.To = User.Email;
                    Email.Subject = "ResetPassword";

                    // Make The Body Of The Email Address
                    var Token = await UserManager.GeneratePasswordResetTokenAsync(User);
                    var url = Url.Action("ResetPassword", "Account", new { Email = User.Email, Token = Token }, Request.Scheme);
                    Email.Body = url;

                    // Send The Email Address
                    EmailHelper Helper = new EmailHelper();
                    Helper.SendEmail(Email);

                    // Return To View Check Your Inbox
                    return View("CheckInbox");
                }

                ModelState.AddModelError("Email", "This Email Address Not Exsist");
            }

            return View(Model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string Email, string Token)
        {
            TempData["Email"] = Email;
            TempData["Token"] = Token;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel Model)
        {
            if(ModelState.IsValid)
            {
                string? Token = TempData["Token"]?.ToString();
                string? Email = TempData["Email"]?.ToString();

                ApplicationUser User = await UserManager.FindByEmailAsync(Email);

                IdentityResult Result = await UserManager.ResetPasswordAsync(User, Token, Model.NewPassword);

                if(Result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }

                foreach(var Error in Result.Errors)
                {
                    ModelState.AddModelError("", Error.Description);
                }
            }

            return View(Model);
        }
    }
}
