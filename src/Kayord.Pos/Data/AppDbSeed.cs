using System.Reflection;
using Bogus;
using Humanizer;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kayord.Pos.Data;

public static class AppDbSeed
{
    public static async Task SeedData(AppDbContext context, CancellationToken cancellationToken)
    {
        // TODO: Only run in dev
        if (!context.Business.Any())
        {
            var table = new Faker<Table>().RuleFor(o => o.Name, f => f.Lorem.Word().Pascalize());
            var section = new Faker<Section>()
                .RuleFor(o => o.Name, f => f.Lorem.Word().Pascalize())
                .RuleFor(o => o.Tables, _ => table.GenerateBetween(10, 10));
            var outlet = new Faker<Outlet>()
                .RuleFor(o => o.Name, f => f.Lorem.Word().Pascalize())
                .RuleFor(o => o.Sections, _ => section.GenerateBetween(10, 10));
            var business = new Faker<Business>()
                .RuleFor(o => o.Name, f => f.Lorem.Word().Pascalize())
                .RuleFor(o => o.Outlets, _ => outlet.GenerateBetween(10, 20));
            var results = business.GenerateBetween(2, 5);
            await context.Business.AddRangeAsync(results);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}