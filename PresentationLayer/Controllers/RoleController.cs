using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.ViewModels;
using System.Security.Principal;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly UserManager<ApplicationUser> UserManager;

        public RoleController(RoleManager<IdentityRole> RoleManager,
                              UserManager<ApplicationUser> UserManager)
        {
            this.RoleManager = RoleManager;
            this.UserManager = UserManager;
        }
        public IActionResult Index(string Word)
        {
            List<RoleViewModel> RolesVM = new List<RoleViewModel>();
            List<IdentityRole> Roles = new List<IdentityRole>();

            if(Word == null)
            {
                Roles = RoleManager.Roles.ToList();

                for(int i = 0; i < Roles.Count; ++i)
                {
                    RoleViewModel Role = new RoleViewModel()
                    {
                        Id = Roles[i].Id,
                        Name = Roles[i].Name
                    };

                    RolesVM.Add(Role);
                }

                return View(RolesVM);
            }

            Roles = RoleManager.Roles.Where(R => R.Name.ToLower().Contains(Word.ToLower())).ToList();

            for (int i = 0; i < Roles.Count; ++i)
            {
                RoleViewModel Role = new RoleViewModel()
                {
                    Id = Roles[i].Id,
                    Name = Roles[i].Name
                };

                RolesVM.Add(Role);
            }

            return View(RolesVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string Id)
        {
            IdentityRole Role = await RoleManager.FindByIdAsync(Id);

            RoleViewModel RoleVM = new RoleViewModel()
            {
                Id = Role.Id,
                Name = Role.Name,
            };

            return View(RoleVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            IdentityRole Role = await RoleManager.FindByIdAsync(Id);

            RoleViewModel RoleVM = new RoleViewModel()
            {
                Id = Role.Id,
                Name = Role.Name,
            };

            return View(RoleVM);
        }

        [HttpGet]
        public async Task<IActionResult> SaveDelete(string Id)
        {
            IdentityRole Role = await RoleManager.FindByIdAsync(Id);

            RoleViewModel RoleVM = new RoleViewModel()
            {
                Id = Role.Id,
                Name = Role.Name,
            };

            IdentityResult Result = await RoleManager.DeleteAsync(Role);

            if(Result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View(RoleVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleCreateViewModel Model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole Role = new IdentityRole() { Name = Model.Name };

                IdentityResult Result = await RoleManager.CreateAsync(Role);

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

        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            IdentityRole Role = await RoleManager.FindByIdAsync(Id);

            RoleViewModel RoleVM = new RoleViewModel()
            {
                Id = Role.Id,
                Name = Role.Name,
            };

            return View(RoleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RoleViewModel Model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole Role = await RoleManager.FindByIdAsync(Model.Id);

                Role.Name = Model.Name;

                IdentityResult Result = await RoleManager.UpdateAsync(Role);

                if(Result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var Error in Result.Errors)
                {
                    ModelState.AddModelError("", Error.Description);
                }
            }

            return View(Model);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemove(string Id)
        {
            List<ApplicationUser> Users = UserManager.Users.ToList();
            List<UserCheckViewModel> UsersVM = new List<UserCheckViewModel>();

            IdentityRole Role = await RoleManager.FindByIdAsync(Id);

            TempData["Role"] = Id;

            for(int i = 0; i < Users.Count(); ++i)
            {
                UserCheckViewModel User = new UserCheckViewModel()
                {
                    Id = Users[i].Id,
                    UserName = Users[i].UserName
                };

                if(await UserManager.IsInRoleAsync(Users[i], Role.Name))
                {
                    User.IsSelected = true;
                }
                else
                {
                    User.IsSelected = false;
                }

                UsersVM.Add(User);
            }

            return View(UsersVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemove(List<UserCheckViewModel> UsersVM)
        {
            List<ApplicationUser> Users = UserManager.Users.ToList();
            IdentityRole Role = await RoleManager.FindByIdAsync(TempData["Role"].ToString());

            if(ModelState.IsValid)
            {
                for (int i = 0; i < Users.Count(); ++i)
                {
                    if (UsersVM[i].IsSelected)
                    {
                        if (!await UserManager.IsInRoleAsync(Users[i], Role.Name))
                        {
                            await UserManager.AddToRoleAsync(Users[i], Role.Name);
                        }
                }
                    else if (!UsersVM[i].IsSelected)
                    {
                        if (await UserManager.IsInRoleAsync(Users[i], Role.Name))
                        {
                            await UserManager.RemoveFromRoleAsync(Users[i], Role.Name);
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            return View(Users);
        }
    }
}
