using Testcontainers.PostgreSql;

namespace Kayord.Pos.Integration;

public class App : AppFixture<Program>
{
    private PostgreSqlContainer? postgreSqlContainer;

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

    // protected override Task SetupAsync()
    // {
    //     // place one-time setup code here
    // }

    // protected override void ConfigureApp(IWebHostBuilder a)
    // {
    //     // do host builder config here
    // }

    // protected override void ConfigureServices(IServiceCollection s)
    // {
    //     // do test service registration here
    // }

    // protected override Task TearDownAsync()
    // {
    //     // do cleanups here
    // }
}