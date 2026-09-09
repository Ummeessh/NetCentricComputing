using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp12ByUmesh.Controllers;

public class AdminController : Controller
{
    private readonly RoleManager<IdentityRole> _rm;
    private readonly UserManager<IdentityUser> _um;

    public AdminController(RoleManager<IdentityRole> rm, UserManager<IdentityUser> um)
    {
        _rm = rm;
        _um = um;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _um.FindByEmailAsync("user@test.com");
        ViewBag.Roles = _rm.Roles.ToList();
        ViewBag.User = user;
        ViewBag.UserRoles = user != null ? await _um.GetRolesAsync(user) : new List<string>();
        ViewBag.UserClaims = user != null ? await _um.GetClaimsAsync(user) : new List<Claim>();
        return View();
    }

    // Role Management (Add / Edit / Delete)
    [HttpPost]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        if (!string.IsNullOrEmpty(roleName)) await _rm.CreateAsync(new IdentityRole(roleName));
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> EditRole(string id, string newName)
    {
        var role = await _rm.FindByIdAsync(id);
        if (role != null) { role.Name = newName; await _rm.UpdateAsync(role); }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var role = await _rm.FindByIdAsync(id);
        if (role != null) await _rm.DeleteAsync(role);
        return RedirectToAction("Index");
    }

    // Assign / Revoke Roles & Claims to User
    [HttpPost]
    public async Task<IActionResult> AssignRole(string userId, string roleName)
    {
        var user = await _um.FindByIdAsync(userId);
        if (user != null) await _um.AddToRoleAsync(user, roleName);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> RevokeRole(string userId, string roleName)
    {
        var user = await _um.FindByIdAsync(userId);
        if (user != null) await _um.RemoveFromRoleAsync(user, roleName);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddClaim(string userId, string type, string value)
    {
        var user = await _um.FindByIdAsync(userId);
        if (user != null) await _um.AddClaimAsync(user, new Claim(type, value));
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveClaim(string userId, string type, string value)
    {
        var user = await _um.FindByIdAsync(userId);
        if (user != null) await _um.RemoveClaimAsync(user, new Claim(type, value));
        return RedirectToAction("Index");
    }

    // Test Authorization Action
    [Authorize(Policy = "TestPolicy")]
    public IActionResult TestAuth() => Content("Authorization SUCCESS: User has 'Admin' role AND 'Permission=Manage' claim.");
}