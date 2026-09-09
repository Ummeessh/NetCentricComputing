var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseRouting();
app.UseAntiforgery(); // Enforces CSRF Protection

app.MapControllerRoute(name: "default", pattern: "{controller=Security}/{action=Index}/{id?}");
app.Run();