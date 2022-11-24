using DotNetOpenAuth.InfoCard;
using Identity.DAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
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
                       var token= await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        //var confirmEmailToken = Url.Action("ConfirmEmail", "Account",
                        //                        new { userid = user.Id, token = token }, Request.Scheme);
                        await _userManager.ConfirmEmailAsync(user, token);
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
        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IActionResult> ConfirmEmail(string userid,string token)
        //{
        //    if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(token))
        //    {
        //        ViewBag.Error = "PLZ Enter Try Again";
        //        return View("NotFound");
        //    }
        //    var user=await _userManager.FindByIdAsync(userid);
        //    if (user == null)
        //    {
        //        ViewBag.Error = "Can't Found user";
        //        return View("NotFound");
        //    }
        //    var tokenuser = await _userManager.ConfirmEmailAsync(user, token);
        //    if (tokenuser.Succeeded)
        //    {
        //        return View();
        //    }
        //    ViewBag.Error = "try Again";
        //    return View("NotFound");
        //}
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
        public async Task<IActionResult> Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            LoginVM model = new LoginVM()
            {
                ExternalLogin = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
                ReturnUrl = returnUrl
            };
            return View(model);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider,string returnUrl)
        {
            var redirecturl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl = returnUrl });
            var properties=_signInManager.ConfigureExternalAuthenticationProperties(provider, redirecturl);

            return new ChallengeResult(provider, properties);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnurl=null,string remoteError=null)
        {
            returnurl = returnurl ?? Url.Content("~/");
            LoginVM model = new LoginVM()
            {
                ExternalLogin = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
                ReturnUrl = returnurl
            };
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"external Provider {remoteError}");
                return View(nameof(Login), model);
            }
            var info =await _signInManager.GetExternalLoginInfoAsync();
            if(info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading External Information");
                return View(nameof(Login), model);
            }
            var signInUser = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent:false,bypassTwoFactor:true);
            if(signInUser.Succeeded)
            {
                return LocalRedirect(returnurl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    var user =await _userManager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new ApplicationUser()
                        {
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };
                      await  _userManager.CreateAsync(user);
                    }
                  await  _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent:false);
                    return LocalRedirect(returnurl);
                }
                ViewBag.Error = "PLZ Contact With Support ragab.rego.com";
                return View("NotFound");
            }
        }

    }
}
