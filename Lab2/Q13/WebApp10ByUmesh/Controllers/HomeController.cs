using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp10ByUmesh.Controllers;

[Authorize] // Requires authentication for all actions in this controller by default
public class HomeController : Controller
{
    [AllowAnonymous] // Overrides controller-level attribute to allow public access
    public IActionResult Index()
    {
        return Content("Public Page - Access Granted via [AllowAnonymous]");
    }

    public IActionResult SecureData()
    {
        return Content("Protected Page - Access Granted via [Authorize]");
    }
}