var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/api/greet", () => new { Message = "Hello, World!" });

app.Run();