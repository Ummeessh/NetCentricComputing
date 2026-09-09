var builder = WebApplication.CreateBuilder(args);

// Add MVC services (includes TempData support)
builder.Services.AddControllersWithViews();

// 1. Add Memory Cache service
builder.Services.AddMemoryCache();

// 2. Add Session state services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable Session Middleware before Authorization
app.UseSession();

// 3. Custom Middleware to set HttpContext.Items for the request lifecycle
app.Use(async (context, next) =>
{
    context.Items["RequestItem"] = $"Item set at {DateTime.Now:HH:mm:ss}";
    await next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=State}/{action=Index}/{id?}");

app.Run();