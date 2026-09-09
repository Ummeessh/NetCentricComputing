using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp11ByUmesh.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    // This endpoint is accessible without authentication
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        // User was redirected here because they were not authenticated
        return Content(
            "Login Endpoint - Redirected because user is not authenticated."
        );
    }

    // Separate endpoint to actually log in as Admin
    [AllowAnonymous]
    public async Task<IActionResult> AdminLogin()
    {
        var user = new IdentityUser
        {
            UserName = "admin@test.com",
            Email = "admin@test.com"
        };

        var customClaims = new[]
        {
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("Permission", "Manage")
        };

        await _signInManager.SignInWithClaimsAsync(
            user,
            isPersistent: false,
            customClaims
        );

        return Content(
            "Logged in successfully as Admin with " +
            "'Permission=Manage' claim! " +
            "Now try /Home/AdminOnly or /Home/ManageResources."
        );
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return Content("Logged out successfully.");
    }
}
