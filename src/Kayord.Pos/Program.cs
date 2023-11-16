global using FastEndpoints;
using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
string tokenSigningKey = builder.Configuration.GetValue<string>("SigningKey") ?? string.Empty;
builder.Services.ConfigureAuth(tokenSigningKey);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
    // options.EnableSensitiveDataLogging();
});

var app = builder.Build();
app.UseFastEndpoints();
app.Run();
