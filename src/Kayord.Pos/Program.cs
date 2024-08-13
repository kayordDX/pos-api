global using FastEndpoints;
using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Extensions.Cors;
using Kayord.Pos.Common.Extensions.Health;
using Kayord.Pos.Common.Extensions.Host;
using Kayord.Pos.Hubs;
using Kayord.Pos.Services;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddLoggingConfiguration(builder.Configuration);
builder.Services.ConfigureApi();
builder.Services.ConfigureConfig(builder.Configuration);
builder.Services.ConfigureGeneral(builder.Configuration);
builder.Services.ConfigureFirebase(builder.Environment);
builder.Services.ConfigureHealth(builder.Configuration);
builder.Services.ConfigureHalo(builder.Configuration);
builder.Services.ConfigureWhatsapp(builder.Configuration);

// builder.Services.AddStackExchangeRedisCache(o =>
// {
//     o.Configuration = builder.Configuration.GetConnectionString("Redis");
//     o.InstanceName = "redisTesting";
// });

var corsSection = builder.Configuration.GetSection("Cors");
builder.Services.ConfigureCors(corsSection.Get<string[]>() ?? [""]);

builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.ConfigureEF(builder.Configuration);

builder.Services.AddHostedService<MigratorHostedService>();
builder.Services.AddSingleton<CurrentUserService>();
builder.Services.AddTransient<IEmailSender, EmailService>();
builder.Services.AddSingleton<IUserIdProvider, UserProvider>();
builder.Services.AddTransient<NotificationService>();

builder.Services
    .AddSignalR()
    .AddStackExchangeRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

app.UseCorsKayord();
app.UseApi();
app.UseHealth();
app.MapHub<KayordHub>("/hub");
app.Run();