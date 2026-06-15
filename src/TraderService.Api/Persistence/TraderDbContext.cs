using Microsoft.EntityFrameworkCore;
using TraderService.Api.Domain;

namespace TraderService.Api.Persistence;

public sealed class TraderDbContext(DbContextOptions<TraderDbContext> options) : DbContext(options)
{
    public DbSet<Trader> Traders => Set<Trader>();
    public DbSet<TradeSignal> TradeSignals => Set<TradeSignal>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trader>(entity =>
        {
            entity.ToTable("traders");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.Property(x => x.AccountNumber).HasMaxLength(50).IsRequired();
            entity.HasIndex(x => x.AccountNumber).IsUnique();
            entity.Property(x => x.CreatedAtUtc).IsRequired();
        });

        modelBuilder.Entity<TradeSignal>(entity =>
        {
            entity.ToTable("trade_signals");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.TraderAccountNumber).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Symbol).HasMaxLength(20).IsRequired();
            entity.Property(x => x.Side).HasMaxLength(10).IsRequired();
            entity.Property(x => x.Quantity).HasPrecision(18, 4);
            entity.Property(x => x.Price).HasPrecision(18, 4);
            entity.Property(x => x.SignalTimestampUtc).IsRequired();
            entity.Property(x => x.CreatedAtUtc).IsRequired();

            entity.HasOne(x => x.Trader)
                .WithMany()
                .HasForeignKey(x => x.TraderId)
                .OnDelete(DeleteBehavior.Restrict);
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