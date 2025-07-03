namespace Kayord.Pos.Integration.Status;

public class StatusTests(App app) : TestBase<App>
{
    [Fact]
    public async Task TestGetStatus()
    {
        var (rsp, res) = await app.ClientAuth.GETAsync<Features.User.GetStatus.Endpoint, Features.User.GetStatus.Response>();

        rsp.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}