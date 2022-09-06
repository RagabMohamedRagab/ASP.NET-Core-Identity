using Identity.DAL.ViewModel;
using Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Identity.Controllers {
    public class StudentController : Controller {
        private readonly IStudentService _studentService;
        private readonly IFileService _fileService;

        public StudentController(IStudentService studentService, IFileService fileService)
        {
            _studentService = studentService;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            return View(_studentService.GetAllStudents());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Levels=new SelectList(_studentService.GetAllLevels().Select(m=>m.Name));
            ViewBag.Genders = new SelectList(_studentService.GetAllGenders().Select(m => m.Name));
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentVM student)
        {
            if (ModelState.IsValid)
            {
                if (_studentService.Create(student) > 0)
                {
                    _fileService.Create(student.File);
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(student);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            if (_fileService.Remove(_studentService.Find(Id).ImgUrl) && _studentService.Remove(Id) > 0)
            {
                    return View("Done");
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int Id)
        {
            ViewBag.Levels = new SelectList(_studentService.GetAllLevels().Select(m => m.Name));
            ViewBag.Genders = new SelectList(_studentService.GetAllGenders().Select(m => m.Name));
            var student = _studentService.Find(Id);
            if (student!= null)
            {
                return View(student);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Levels()
        {
            return View(_studentService.GetAllLevels());
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
        public IActionResult Genders()
        {
            return View(_studentService.GetAllGenders());
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
                    return RedirectToAction(nameof(Genders));
                }
            }
            return View(gender);
        }
    }
}
