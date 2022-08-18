using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Link()
        {
            return View();
        }
    }
}
