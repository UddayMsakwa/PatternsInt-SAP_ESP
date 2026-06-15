using Microsoft.EntityFrameworkCore;
using SubscriptionService.Api.Domain;

namespace SubscriptionService.Api.Persistence;

public sealed class SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options) : DbContext(options)
{
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.ToTable("subscriptions");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.FollowerAccountNumber).HasMaxLength(50).IsRequired();
            entity.Property(x => x.TraderAccountNumber).HasMaxLength(50).IsRequired();
            entity.Property(x => x.CopyRatio).HasPrecision(18, 4);
            entity.Property(x => x.CreatedAtUtc).IsRequired();

            entity.HasIndex(x => new
            {
                x.FollowerUserId,
                x.FollowerAccountNumber,
                x.TraderId,
                x.TraderAccountNumber
            }).IsUnique();
        });

        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.ToTable("outbox_messages");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.MessageType).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Payload).HasColumnType("text").IsRequired();
            entity.Property(x => x.OccurredAtUtc).IsRequired();
            entity.Property(x => x.Error).HasColumnType("text");
        });
    }
}