using System.Reflection;
using Bogus;
using Bogus.Extensions.UnitedStates;
using Humanizer;
using Kayord.Pos.Common.Enums;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kayord.Pos.Data;

public static class AppDbSeed
{
    public static async Task SeedData(AppDbContext context, CancellationToken cancellationToken)
    {
        await context.Database.ExecuteSqlRawAsync("""TRUNCATE TABLE "Business" RESTART IDENTITY CASCADE;""");
        if (!context.Business.Any())
        {
            var staff = new Faker<Staff>()
                .RuleFor(o => o.Name, f => f.Name.FullName())
                .RuleFor(o => o.StaffType, f => f.PickRandom<StaffType>());
            var table = new Faker<Table>()
                .RuleFor(o => o.Name, f => f.Name.JobArea())
                .RuleFor(o => o.Capacity, f => f.Random.Int(1, 14));
            var section = new Faker<Section>()
                .RuleFor(o => o.Name, f => f.Name.FirstName())
                .RuleFor(o => o.Tables, _ => table.GenerateBetween(5, 7));
            var outlet = new Faker<Outlet>()
                .RuleFor(o => o.Name, f => f.Company.CompanyName())
                .RuleFor(o => o.Staff, f => staff.GenerateBetween(10, 12))
                .RuleFor(o => o.Sections, _ => section.GenerateBetween(3, 5));
            var business = new Faker<Business>()
                .RuleFor(o => o.Name, f => f.Company.CompanyName())
                .RuleFor(o => o.Outlets, _ => outlet.GenerateBetween(2, 5));

            var results = business.GenerateBetween(1, 1);
            await context.Business.AddRangeAsync(results);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!context.Role.Any())
        {
            await context.Role.AddAsync(new Role { Name = "Guest", Description = "Guest", RoleId = 1 });
            await context.Role.AddAsync(new Role { Name = "Waiter", Description = "Waiter", RoleId = 2 });
            await context.Role.AddAsync(new Role { Name = "Chef", Description = "Chef", RoleId = 3 });
            await context.Role.AddAsync(new Role { Name = "Manager", Description = "Manager", RoleId = 4 });
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}