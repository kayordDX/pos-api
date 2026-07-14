using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Integration.StockDivision;

[Collection(nameof(AppCollection))]
public class StockDivisionTests(App app) : TestBase<App>
{
    [Fact]
    public async Task GetAllStockDivisions_FiltersDeleted()
    {
        // Arrange
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var outlet = await dbContext.Outlet.FirstAsync();
        var suffix = Guid.NewGuid().ToString("N");
        var activeDivisionName = $"Active Stock Division {suffix}";
        var deletedDivisionName = $"Deleted Stock Division {suffix}";

        var activeDivision = new Kayord.Pos.Entities.Division
        {
            DivisionName = activeDivisionName,
            OutletId = outlet.Id,
            DivisionTypeId = 0,
            IsDeleted = false
        };
        dbContext.Division.Add(activeDivision);

        var deletedDivision = new Kayord.Pos.Entities.Division
        {
            DivisionName = deletedDivisionName,
            OutletId = outlet.Id,
            DivisionTypeId = 0,
            IsDeleted = true
        };
        dbContext.Division.Add(deletedDivision);
        await dbContext.SaveChangesAsync();

        // Act
        var (rsp, res) = await app.ClientAuth.GETAsync<Kayord.Pos.Features.Stock.Division.GetAll.Endpoint, Kayord.Pos.Features.Stock.Division.GetAll.Request, List<Kayord.Pos.Entities.Division>>(
            new() { OutletId = outlet.Id });

        // Assert
        rsp.IsSuccessStatusCode.ShouldBeTrue();
        res.ShouldNotContain(d => d.DivisionName == deletedDivisionName);
        res.ShouldContain(d => d.DivisionName == activeDivisionName);
    }
}
