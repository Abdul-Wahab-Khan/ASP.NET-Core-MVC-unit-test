using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using mvc_test.DbContexts;
using mvc_test.Models.Claims;
using mvc_test.Models.Users;
using mvc_test.Other;

namespace mvc_test.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestDbContext context;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;

        public AccountController(TestDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var model = new DisplayUsersViewModel
            {
                Users = context.Users.ToList()
            };
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if(user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Cannot Log in right now");
                return Unauthorized();
            }

            ModelState.AddModelError("", "You dont have any account yet");
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AppUser
            {
                UserName = model.UserName,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "An error occured");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AssignClaims(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return Content("User Not found");
            var model = new UserClaimViewModel()
            {
                UserId = userId,
                AssignedClaims = await userManager.GetClaimsAsync(user),
                Claims = TestClaims.Claims
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignClaims(UserClaimViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            var claim = new Claim(model.Claim, model.Claim);

            var result = await userManager.AddClaimAsync(user, claim);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            return View(model);
        }
    }

}
