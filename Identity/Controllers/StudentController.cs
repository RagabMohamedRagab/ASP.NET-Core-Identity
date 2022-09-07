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
            ViewBag.Levels = new SelectList(_studentService.GetAllLevels().Select(m => m.Name));
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
        [HttpGet]
        public IActionResult Update(int Id)
        {
            ViewBag.Levels = new SelectList(_studentService.GetAllLevels().Select(m => m.Name));
            ViewBag.Genders = new SelectList(_studentService.GetAllGenders().Select(m => m.Name));
            var student = _studentService.Find(Id);
            if (student != null)
            {
                return View(student);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Update(int Id, StudentVM student)
        {
            if (ModelState.IsValid)
            {
                if (_fileService.Create(student.File) != null || _studentService.Update(Id, student) > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(student);
        }


        [HttpGet]
        public IActionResult UpdateLevel(int Id)
        {
            return View(_studentService.FindLevel(Id));
        }
        [HttpPost]
        public IActionResult UpdateLevel(int Id, LevelVM levelvm)
        {
            if (Id > 0)
            {
                if (ModelState.IsValid)
                {
                    if (_studentService.UpdateLevel(Id, levelvm) > 0)
                    {
                        return RedirectToAction(nameof(Levels));
                    }
                }
            }
            return View(levelvm);
        }

        [HttpGet]
        public IActionResult DeleteLevel(int Id)
        {
            if (Id <= 0)
            {
                return RedirectToAction(nameof(Levels));
            }
            if (_studentService.DeleteLevel(Id) > 0)
            {
                return View("Done");
            }
            return RedirectToAction(nameof(Levels));
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
        public IActionResult Level(LevelVM level)
        {
            if (ModelState.IsValid)
            {
                if (_studentService.Create(level) > 0)
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
        [HttpGet]
        public IActionResult DeleteGender(int Id)
        {
            if (Id <= 0)
            {
                return RedirectToAction(nameof(Genders));
            }
            if (_studentService.Delete(Id) > 0)
            {
                return View("Done");
            }
            return RedirectToAction(nameof(Genders));
        }
        [HttpGet]
        public IActionResult UpdateGender(int Id)
        {
            return View(_studentService.FindbyId(Id));
        }
        [HttpPost]
        public IActionResult UpdateGender(int Id, GenderVM gender)
        {
            if (Id >0)
            {
                if (ModelState.IsValid)
                {
                    if (_studentService.UpdateGender(Id, gender) > 0)
                    {
                        return RedirectToAction(nameof(Genders));
                    }
                }
            }
            return View(gender);
        }
    }
}
