using Microsoft.AspNetCore.Mvc;
using WebApp10ByUmesh.Models;

namespace WebApp10ByUmesh.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignUp() => View();

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            ViewBag.Message = "Form submitted successfully!";
            return View(model);
        }
    }
}