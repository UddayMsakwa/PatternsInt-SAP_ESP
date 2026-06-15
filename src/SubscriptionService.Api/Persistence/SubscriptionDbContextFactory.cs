using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SubscriptionService.Api.Persistence;

public sealed class SubscriptionDbContextFactory : IDesignTimeDbContextFactory<SubscriptionDbContext>
{
    public SubscriptionDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SubscriptionDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5434;Database=subscriptiondb;Username=postgres;Password=postgres");

        return new SubscriptionDbContext(optionsBuilder.Options);
    }
}