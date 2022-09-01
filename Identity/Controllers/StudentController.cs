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
    }
}
