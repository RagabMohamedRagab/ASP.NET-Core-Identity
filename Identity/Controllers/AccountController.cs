using Identity.DAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.Controllers {
    public class AccountController : Controller {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ok()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser() { Email = model.Email, UserName = model.Email,City=model.City };
                IdentityResult result=await _userManager.CreateAsync(user, model.Password);
                switch (result.Succeeded)
                {
                    case true:
                        if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                        {
                            return RedirectToAction("ListUsers", "Adminsitration");
                        }
                         await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction(nameof(ok));
                    default:
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, item.Description);
                        }
                        break;
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Link()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse([Bind("Email")]Register model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {model.Email} is already Taken by another user");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RemmberMe, false);
                if (user.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl)&& Url.IsLocalUrl(returnUrl))
                    { 
                        //return LocalRedirect(returnUrl);
                        return Redirect(returnUrl);
                      
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    }
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
