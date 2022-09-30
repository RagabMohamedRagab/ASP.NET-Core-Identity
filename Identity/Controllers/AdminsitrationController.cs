using Identity.DAL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers {
    public class AdminsitrationController:Controller{
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminsitrationController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public ActionResult Create() { 
          return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleVM model) {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ListRoles));
                }
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
             return View(_roleManager.Roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
           var role=await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Role User Not Found";
                return View("NotFound");
            }
            EditRoleVM model = new()
            {
                RoleName = role.Name,
                Id = role.Id
            };
            foreach (var item in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(item, role.Name)) {
                    model.Users.Add(item.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleVM model) 
        {
            var role =await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Role User Not Found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
              var result=  await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ListRoles));
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }
                return View(model);
            }
        }
    }
}
