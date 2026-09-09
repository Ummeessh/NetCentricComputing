using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace WebApp13ByUmesh.Controllers;

public class SecurityController : Controller
{
    public IActionResult Index() => View();

    // 1. XSS Demonstration
    public IActionResult Xss(string input)
    {
        ViewBag.UserInput = input ?? "<script>alert('XSS Attack!');</script>";
        return View();
    }

    // 2. SQL Injection & Parameterized Query Fix (SqlClient)
    public IActionResult SqlInjection(string username)
    {
        username ??= "admin' OR '1'='1";
        using var conn = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;");

        // Vulnerable (Unparameterized String Concatenation)
        using var unsafeCmd = conn.CreateCommand();
        unsafeCmd.CommandText = $"SELECT * FROM Users WHERE Name = '{username}'";

        // Safe (Parameterized Query using SqlClient)
        using var safeCmd = conn.CreateCommand();
        safeCmd.CommandText = "SELECT * FROM Users WHERE Name = @Name";
        safeCmd.Parameters.AddWithValue("@Name", username);

        ViewBag.UnsafeQuery = unsafeCmd.CommandText;
        ViewBag.SafeQuery = safeCmd.CommandText;
        return View();
    }

    // 3. CSRF Protection
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CsrfSubmit(string data) => Content($"Form submitted securely with Anti-Forgery token: {data}");

    // 4. Open Redirect & Prevention
    public IActionResult OpenRedirect(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction("Index");
    }
}