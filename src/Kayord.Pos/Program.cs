global using FastEndpoints;
using Kayord.Pos.Common.Extensions;
using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
string tokenSigningKey = builder.Configuration.GetValue<string>("SigningKey") ?? string.Empty;
builder.Services.ConfigureAuth(tokenSigningKey);

builder.Services.ConfigureEF(builder.Configuration);

builder.Services.AddHostedService<MigratorHostedService>();

var app = builder.Build();
app.UseFastEndpoints();
app.Run();
