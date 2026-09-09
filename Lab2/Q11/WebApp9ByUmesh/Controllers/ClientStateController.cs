using Microsoft.AspNetCore.Mvc;

namespace WebApp9ByUmesh.Controllers
{
    public class ClientStateController : Controller
    {
        // GET: /ClientState/Index?userName=Jayson
        public IActionResult Index(string? userName)
        {
            // 1. Query String Processing
            ViewBag.QueryStringValue = string.IsNullOrEmpty(userName) ? "No query string provided" : userName;

            // 2. Cookie Processing (Read existing cookie)
            string? userCookie = Request.Cookies["UserPreference"];
            ViewBag.CookieValue = string.IsNullOrEmpty(userCookie) ? "No cookie set yet" : userCookie;

            // 3. Hidden Field Initial State
            ViewBag.HiddenFieldValue = "FormStep1_Data";

            return View();
        }

        [HttpPost]
        public IActionResult SetCookie(string cookieValue)
        {
            // 1. Writing Cookie to Client Browser (Expires in 7 days)
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true
            };
            Response.Cookies.Append("UserPreference", cookieValue, options);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ProcessHiddenField(string hiddenFieldData)
        {
            // 3. Reading Hidden Field submitted from Form
            ViewBag.SubmittedHiddenField = hiddenFieldData;
            ViewBag.QueryStringValue = "Preserved";
            ViewBag.CookieValue = Request.Cookies["UserPreference"] ?? "No cookie set yet";
            ViewBag.HiddenFieldValue = hiddenFieldData;

            return View("Index");
        }
    }
}