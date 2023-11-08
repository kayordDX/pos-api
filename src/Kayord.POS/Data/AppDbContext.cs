using Kayord.POS.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.POS.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Business> Business => Set<Business>();

}