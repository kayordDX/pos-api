global using FastEndpoints;
using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Hubs;
using Kayord.Pos.Services;
using KayordKit.Extensions.Api;
using KayordKit.Extensions.Cors;
using KayordKit.Extensions.Health;
using KayordKit.Extensions.Host;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddLoggingConfiguration(builder.Configuration);
builder.Services.ConfigureApi();
builder.Services.ConfigureConfig(builder.Configuration);
builder.Services.ConfigureHealth(builder.Configuration);
builder.Services.ConfigureHalo(builder.Configuration);

// builder.Services.AddStackExchangeRedisCache(o =>
// {
//     o.Configuration = builder.Configuration.GetConnectionString("Redis");
//     o.InstanceName = "redisTesting";
// });
builder.Services.AddSignalR().AddStackExchangeRedis(builder.Configuration.GetConnectionString("Redis")!);

var corsSection = builder.Configuration.GetSection("Cors");
builder.Services.ConfigureCors(corsSection.Get<string[]>() ?? [""]);

builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.ConfigureEF(builder.Configuration);

builder.Services.AddHostedService<MigratorHostedService>();
builder.Services.AddSingleton<CurrentUserService>();

var app = builder.Build();

app.UseApi();
app.MapHub<NotificationHub>("/notify");
app.UseCorsKayord();
app.UseHealth();
app.Run();