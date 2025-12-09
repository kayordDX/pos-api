namespace Integration.Status;

[Collection(nameof(AppCollection))]
public class StatusTests(App app) : TestBase<App>
{
    [Fact]
    public async Task TestGetStatus()
    {
        var (rsp, res) = await app.ClientAuth.GETAsync<Kayord.Pos.Features.User.GetStatus.Endpoint, Kayord.Pos.Features.User.GetStatus.Response>();

        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}