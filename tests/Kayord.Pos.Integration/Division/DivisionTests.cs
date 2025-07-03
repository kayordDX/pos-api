namespace Kayord.Pos.Integration.Division;

public class DivisionTests(App app) : TestBase<App>
{
    [Fact, Priority(1)]
    public async Task CreateDivision()
    {
        var (rsp, res) = await app.ClientAuth.POSTAsync<Features.Division.Create.Endpoint, Features.Division.Create.Request, Entities.Division>(
            new()
            {
                DivisionTypeId = 1,
                Name = "Test Division",
                OutletId = 1
            });

        rsp.IsSuccessStatusCode.ShouldBeTrue();
        res.DivisionId.ShouldBe(1);
    }
}