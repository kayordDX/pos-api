using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Integration.RoleDivision;

[Collection(nameof(AppCollection))]
public class RoleDivisionTests(App app) : TestBase<App>
{
    [Fact]
    public async Task CreateRoleDivision_Duplicate_ReturnsNoContent()
    {
        // Arrange - Create the initial link
        var (rsp1, _) = await app.ClientAuth.POSTAsync<Kayord.Pos.Features.Role.Division.Create.Endpoint, Kayord.Pos.Features.Role.Division.Create.Request, Kayord.Pos.Entities.Division>(
            new()
            {
                DivisionId = 1,
                RoleId = 9999
            });
        rsp1.IsSuccessStatusCode.ShouldBeTrue();

        // Act - Try to create the same link again
        var (rsp2, _) = await app.ClientAuth.POSTAsync<Kayord.Pos.Features.Role.Division.Create.Endpoint, Kayord.Pos.Features.Role.Division.Create.Request, Kayord.Pos.Entities.Division>(
            new()
            {
                DivisionId = 1,
                RoleId = 9999
            });

        // Assert - Should return 204 NoContent for duplicate
        rsp2.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task CreateRoleDivision_UniqueConstraint_Throws_OnDuplicate()
    {
        // Direct DbContext test to verify the unique constraint
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var link1 = new Kayord.Pos.Entities.RoleDivision
        {
            DivisionId = 1,
            RoleId = 8888
        };
        dbContext.RoleDivision.Add(link1);
        await dbContext.SaveChangesAsync();

        var link2 = new Kayord.Pos.Entities.RoleDivision
        {
            DivisionId = 1,
            RoleId = 8888
        };
        dbContext.RoleDivision.Add(link2);
        await Should.ThrowAsync<DbUpdateException>(() => dbContext.SaveChangesAsync());
    }
}
