using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp10ByUmesh.Controllers;

public class AccountController : Controller
{
    [AllowAnonymous]
    public IActionResult Login()
    {
        return Content("Login Page - Intercepted unauthenticated access to /Home/SecureData");
    }
}   