using Identity.DAL.ViewModel;
using Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Identity.Controllers {
    public class StudentController : Controller {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            return View(_studentService.GetAllStudents());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Levels=new SelectList(_studentService.GetAllLevels().Select(m=>m.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentVM student)
        {
            if (ModelState.IsValid)
            {
                if (_studentService.Create(student) > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(student);
        }
        public IActionResult Levels()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Level()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Level(LevelVM level)
        {
            if (ModelState.IsValid)
            {
                if(_studentService.Create(level) > 0)
                {
                    return RedirectToAction(nameof(Levels));
                }
            }
            return View(level);
        }
        [HttpGet]
        public IActionResult Gender()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Gender(GenderVM gender)
        {
            if (ModelState.IsValid)
            {
                if (_studentService.Create(gender) > 0)
                {
                    return RedirectToAction(nameof(Levels));
                }
            }
            return View(gender);
        }
    }
}
