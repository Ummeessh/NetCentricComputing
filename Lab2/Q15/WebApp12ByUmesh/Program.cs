using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp12ByUmesh.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase("AdminDb"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TestPolicy", policy => policy.RequireRole("Admin").RequireClaim("Permission", "Manage"));
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed initial test user
using (var scope = app.Services.CreateScope())
{
    var um = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    if (await um.FindByEmailAsync("user@test.com") == null)
    {
        await um.CreateAsync(new IdentityUser { UserName = "user@test.com", Email = "user@test.com" }, "Pass123!");
    }
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Admin}/{action=Index}/{id?}");

app.Run();