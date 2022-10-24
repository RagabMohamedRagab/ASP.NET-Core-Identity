using Identity.DAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers {
    [Authorize(Roles ="Admin")]
    public class AdminsitrationController:Controller{
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminsitrationController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string Id)
        {
            var user=await _userManager.FindByIdAsync(Id);
            if(user == null)
            {
                ViewBag.ErrorMessage = "User Not Found";
                return View("NotFound");
            }
            var RolesUser =await _userManager.GetRolesAsync(user);
            var ClaimsUser =await _userManager.GetClaimsAsync(user);
            EditUserVM vm = new EditUserVM()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                 Roles=RolesUser.ToList(),
                 Claims=ClaimsUser.Select(b=>b.Value).ToList(),
                city=user.City,
            };
            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVM userVM)
        {
            var user =await _userManager.FindByIdAsync(userVM.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = "User Not Found";
                return View("NotFound");
            }
            else
            {
                user.Email = userVM.Email;
                user.UserName = userVM.UserName;
                user.City = userVM.city;
                IdentityResult result =await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ListUsers));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }
                return View(userVM);
            }

        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var user =await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = "User Not Found";
                return View("NotFound");
            }
            IdentityResult result =await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ListUsers));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            return RedirectToAction(nameof(ListUsers));
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
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Role Not Found";
                return View("NotFound");
            }
            else
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ListRoles));
                }
                else
                {
                    foreach(IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, item.Description);
                    }
                    return RedirectToAction(nameof(ListRoles));
                }
            }
         
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
        [HttpGet]
        public async  Task<IActionResult> EditUserRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role =await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role {roleId}  Not Found";
                return View("NotFound");
            }
         IList<UserRoleVM> userRoles=new List<UserRoleVM>();
            foreach (var user in _userManager.Users.ToList())
            {  
                UserRoleVM vM = new UserRoleVM()
                    {
                        RoleId = roleId,
                        UserId = user.Id,
                        UserName = user.UserName,
                    };
                if(await _userManager.IsInRoleAsync(user, role.Name)) {
                    vM.IsSelelected = true;
                }
                else
                {
                    vM.IsSelelected = false;
                }
                userRoles.Add(vM);
            }
            return View(userRoles);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserRole(List<UserRoleVM> userRoles,string roleId) {
            var role =await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role {roleId}  Not Found";
                return View("NotFound");
            }
            for (int i = 0; i < userRoles.Count; i++) {
                var user = await _userManager.FindByIdAsync(userRoles[i].UserId);
                if (user == null) {
                    ViewBag.ErrorMessage = $"User {userRoles[i].UserName}  Not Found";
                    return View("NotFound");
                }
                IdentityResult result = null;
                if (userRoles[i].IsSelelected&& !(await _userManager.IsInRoleAsync(user,role.Name)))
                {
                    result =await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if(!userRoles[i].IsSelelected&&await _userManager.IsInRoleAsync(user,role.Name)) {
                    result=await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < userRoles.Count)
                    {
                        continue;
                    }
                    else {
                        return RedirectToAction(nameof(EditRole), new { id = roleId });
                    }
                   
                }
            }
            return RedirectToAction(nameof(EditRole), new { id = roleId });
        }
    }
}
