using Identity.DAL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.Controllers {
    public class AdminsitrationController:Controller{
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminsitrationController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
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
                    return RedirectToAction(nameof(Ok), "Account");
                }
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                
            }
            return View(model);
        }
    }
}
