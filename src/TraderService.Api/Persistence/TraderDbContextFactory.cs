using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TraderService.Api.Persistence;

public sealed class TraderDbContextFactory : IDesignTimeDbContextFactory<TraderDbContext>
{
    public TraderDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TraderDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=traderdb;Username=postgres;Password=postgres");

        return new TraderDbContext(optionsBuilder.Options);
    }
}