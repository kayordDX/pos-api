using DotNet.Testcontainers.Builders;
using Kayord.Pos.Services;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Integration;

// Define a collection that all integration tests belong to - ensures single fixture instance
[CollectionDefinition(nameof(AppCollection))]
public class AppCollection : ICollectionFixture<App>;

public class App : AppFixture<Program>
{
    private PostgreSqlContainer? postgreSqlContainer;
    public HttpClient ClientAuth = new HttpClient();

    protected override async ValueTask PreSetupAsync()
    {
        postgreSqlContainer = new PostgreSqlBuilder("postgres:18")
            .WithDatabase("db")
            .WithUsername("db")
            .WithPassword("db")
            .WithPortBinding(15432, 5432)
            .Build();

        var redis = new ContainerBuilder("docker.io/bitnami/redis:latest")
            .WithPortBinding(16379, 6379)
            .WithEnvironment("REDIS_PASSWORD", "4qWF6jAcW6e9PCeW")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(6379))
            .Build();

        await postgreSqlContainer.StartAsync();
        await redis.StartAsync();
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
