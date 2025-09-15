using PerlaMetroUsersService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Bind to PORT env var if provided (e.g., Render)
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddWebApp(builder.Configuration);

var app = builder.Build();

app.UseWebApp();

// Seed only in Development or when explicitly enabled
if (app.Environment.IsDevelopment() ||
    string.Equals(Environment.GetEnvironmentVariable("SEED_ON_STARTUP"), "true", StringComparison.OrdinalIgnoreCase))
{
    AppSeedService.SeedDatabase(app);
}

app.Run();
