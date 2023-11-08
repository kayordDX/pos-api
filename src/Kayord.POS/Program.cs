global using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();

// builder.Services.AddEntityFrameworkNpgsql()

var app = builder.Build();
app.UseFastEndpoints();
app.Run();
