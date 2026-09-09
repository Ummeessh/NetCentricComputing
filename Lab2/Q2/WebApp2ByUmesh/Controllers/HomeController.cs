using Microsoft.AspNetCore.Mvc;
using WebApp2ByUmesh.Models;

namespace WebApp2ByUmesh.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyRazorPage()
        {
            ViewBag.CurrentDateTime = DateTime.Now;
            ViewBag.Name = "Umesh";
            ViewBag.RollNo = 20;
            return View();
        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                return View("StudentDetails", student);
            }
            return View(student);
        }
    }
}