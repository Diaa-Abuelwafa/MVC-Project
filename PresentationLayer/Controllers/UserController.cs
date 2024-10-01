using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.ViewModels;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> UserManager;

        public UserController(UserManager<ApplicationUser> UserManager)
        {
            this.UserManager = UserManager;
        }

        public async Task<IActionResult> Index(string SearchWord)
        {
            List<UserViewModel> UsersVM = new List<UserViewModel>();
            List<ApplicationUser> Users = new List<ApplicationUser>();

            if (SearchWord == null)
            {
                Users = UserManager.Users.ToList();

                for (int i = 0; i < Users.Count(); ++i)
                {
                    UserViewModel User = new UserViewModel()
                    {
                        Id = Users[i].Id,
                        UserName = Users[i].UserName,
                        Email = Users[i].Email,
                        Roles = UserManager.GetRolesAsync(Users[i]).Result.ToList(),
                        Address = Users[i].Address
                    };

                    UsersVM.Add(User);
                }

                return View(UsersVM);
            }

            Users = UserManager.Users.Where(U => U.Email.ToLower().Contains(SearchWord.ToLower())).ToList();

            for(int i = 0; i < Users.Count(); ++i)
            {
                UserViewModel User = new UserViewModel()
                {
                    Id = Users[i].Id,
                    UserName = Users[i].UserName,
                    Email = Users[i].Email,
                    Roles = UserManager.GetRolesAsync(Users[i]).Result.ToList(),
                    Address = Users[i].Address
                };

                UsersVM.Add(User);
            }

            return View(UsersVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string Id)
        {
            ApplicationUser User = await UserManager.FindByIdAsync(Id);

            UserViewModel UserVM = new UserViewModel()
            {
                Id = User.Id,
                UserName = User.UserName,
                Email = User.Email,
                Roles = UserManager.GetRolesAsync(User).Result.ToList(),
                Address = User.Address
            };

            return View(UserVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            ApplicationUser User = await UserManager.FindByIdAsync(Id);

            UserViewModel UserVM = new UserViewModel()
            {
                Id = User.Id,
                UserName = User.UserName,
                Email = User.Email,
                Roles = UserManager.GetRolesAsync(User).Result.ToList(),
                Address = User.Address
            };

            return View(UserVM);
        }

        [HttpGet]
        public async Task<IActionResult> SaveDelete(string Id)
        {
            ApplicationUser User = await UserManager.FindByIdAsync(Id);

            UserViewModel UserVM = new UserViewModel()
            {
                Id = User.Id,
                UserName = User.UserName,
                Email = User.Email,
                Roles = UserManager.GetRolesAsync(User).Result.ToList(),
                Address = User.Address
            };

            IdentityResult Result = await UserManager.DeleteAsync(User);

            if(Result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View(UserVM);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            ApplicationUser User = await UserManager.FindByIdAsync(Id);

            UserUpdateViewModel UserVM = new UserUpdateViewModel()
            {
                Id = User.Id,
                UserName = User.UserName,
                Email = User.Email,
                Address = User.Address
            };

            return View(UserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserUpdateViewModel Model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser User = await UserManager.FindByIdAsync(Model.Id);

                User.UserName = Model.UserName;
                User.Email = Model.Email;
                User.Address = Model.Address;

                IdentityResult Result = await UserManager.UpdateAsync(User);

                if(Result.Succeeded)
                {
                    return RedirectToAction("Index");
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
