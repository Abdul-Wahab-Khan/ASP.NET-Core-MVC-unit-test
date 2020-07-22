using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_test.DbContexts;
using mvc_test.Models.Roles;

namespace mvc_test.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var model = new DisplayRolesViewModel
            {
                Roles = roleManager.Roles.ToList()
            };


            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var role = new IdentityRole
            {
                Name = model.Name
            };

            var result = await roleManager.CreateAsync(role);

            if (result.Succeeded)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Cannot Create Role Now");
            return View(model);
        }

        public async Task<IActionResult> AssignUser(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
                return Content("Not Found");
            var model = new UserRoleViewModel
            {
                RoleId = role.Id,
            };

            var users = userManager.Users.ToList();

            foreach (var user in users)
            {
                if (!await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.NotAssignedUsers.Add(user);
                }
                else
                {
                    model.AssignedUsers.Add(user);
                }
            }



            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> AssignUser(UserRoleViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            var role = await roleManager.FindByIdAsync(model.RoleId);

            var result = await userManager.AddToRoleAsync(user, role.Name);

            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "An error occured");
            return View(model);
        }
    }
}
