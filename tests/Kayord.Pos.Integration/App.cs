using Kayord.Pos.Services;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Kayord.Pos.Integration;

public class App : AppFixture<Program>
{
    private PostgreSqlContainer? postgreSqlContainer;
    public HttpClient ClientAuth = new HttpClient();

    protected override async ValueTask PreSetupAsync()
    {
        postgreSqlContainer = new PostgreSqlBuilder()
            .WithImage("postgres:17")
            .WithDatabase("db")
            .WithUsername("db")
            .WithPassword("db")
            .Build();

        await postgreSqlContainer.StartAsync();
    }

    protected override async ValueTask SetupAsync()
    {
        var userService = Services.GetRequiredService<UserService>();
        var apiKey = await userService.GetIdToken("92jlIC3p9uUavQOw5Pf5bX61ck13");
        var adminClient = CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", apiKey.IdToken);
        });
        ClientAuth = adminClient;
    }

    // protected override void ConfigureApp(IWebHostBuilder a)
    // {
    //     // do host builder config here
    // }

    // protected override void ConfigureServices(IServiceCollection s)
    // {
    //     // do test service registration here
    // }

    protected override ValueTask TearDownAsync()
    {
        ClientAuth.Dispose();
        return ValueTask.CompletedTask;
    }
}