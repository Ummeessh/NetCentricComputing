using WebApp3ByUmesh.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register lifetimes
builder.Services.AddTransient<ITransientService, ExampleService>();
builder.Services.AddScoped<IScopedService, ExampleService>();
builder.Services.AddSingleton<ISingletonService, ExampleService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();