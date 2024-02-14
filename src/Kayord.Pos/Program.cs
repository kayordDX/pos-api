global using FastEndpoints;
using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Services;
using KayordKit.Extensions.Api;
using KayordKit.Extensions.Cors;
using KayordKit.Extensions.Health;
using KayordKit.Extensions.Host;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddLoggingConfiguration(builder.Configuration);
builder.Services.ConfigureApi();
builder.Services.ConfigureConfig(builder.Configuration);
builder.Services.ConfigureHealth(builder.Configuration);
builder.Services.ConfigureHalo(builder.Configuration);

// TODO: Move url to config
builder.Services.ConfigureCors(["http://localhost:5173"]);

builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.ConfigureEF(builder.Configuration);

builder.Services.AddHostedService<MigratorHostedService>();
builder.Services.AddSingleton<CurrentUserService>();

var app = builder.Build();

app.UseApi();
app.UseCorsKayord();
app.UseHealth();
app.Run();