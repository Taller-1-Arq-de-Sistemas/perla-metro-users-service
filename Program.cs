using Microsoft.EntityFrameworkCore;
using PerlaMetroUsersService.Data;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("AppDb");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(cs));

var app = builder.Build();

app.MapGet("/", (DataContext db) => db.Users.ToList());

app.Run();
