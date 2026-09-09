using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp11ByUmesh.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => Content("Public Page");

    // 1. Role-Based Authorization
    [Authorize(Roles = "Admin")]
    public IActionResult AdminOnly() => Content("Admin Role Access Granted");

    // 2. Policy-Based Authorization (Checks for Permission Claim)
    [Authorize(Policy = "CanManage")]
    public IActionResult ManageResources() => Content("Authorized to Create, Update, Delete Resources");
}