using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Common.Extensions.Cors;
using Kayord.Pos.Common.Extensions.Health;
using Kayord.Pos.Common.Extensions.Host;
using Kayord.Pos.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddLoggingConfiguration(builder.Configuration);
builder.Services.ConfigureApi();
builder.Services.ConfigureRedis(builder.Configuration);
builder.Services.ConfigureConfig(builder.Configuration);
builder.Services.ConfigurePrint();

builder.Services.ConfigureFirebase(builder.Environment);
builder.Services.ConfigureHealth(builder.Configuration);
builder.Services.ConfigureHalo(builder.Configuration);
builder.Services.ConfigureWhatsapp(builder.Configuration);
builder.Services.ConfigureAI(builder.Configuration);

var corsSection = builder.Configuration.GetSection("Cors");
builder.Services.ConfigureCors(corsSection.Get<string[]>() ?? [""]);

builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.ConfigureEF(builder.Configuration, builder.Environment);

builder.Services.ConfigureGeneral(builder.Configuration);

builder.Services
    .AddSignalR()
    .AddStackExchangeRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

app.UseCorsKayord();
app.UseApi();
app.UseHealth();
app.MapHub<KayordHub>("/hub");
app.Run();

public partial class Program { }