namespace Integration.Division;

[Collection(nameof(AppCollection))]
public class DivisionTests(App app) : TestBase<App>
{
    [Fact, Priority(1)]
    public async Task CreateDivision()
    {
        var (rsp, res) = await app.ClientAuth.POSTAsync<Kayord.Pos.Features.Division.Create.Endpoint, Kayord.Pos.Features.Division.Create.Request, Kayord.Pos.Entities.Division>(
            new()
            {
                DivisionTypeId = 1,
                Name = "Test Division",
                OutletId = 99
            });

        // This is false because you need to be a manager for this to pass.
        rsp.IsSuccessStatusCode.ShouldBeFalse();
        // res.DivisionId.ShouldBe(1);
    }
}