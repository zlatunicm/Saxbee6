using Microsoft.EntityFrameworkCore;
using Saxbee6.Api.Data.Models;

namespace Saxbee6.Api.Data;

public class SaxBeeContext : DbContext
{
    public SaxBeeContext(DbContextOptions<SaxBeeContext> options) : base(options) { }

    public DbSet<Apiary> Apiaries => Set<Apiary>();
    public DbSet<Hive> Hives => Set<Hive>();
    public DbSet<Queen> Queens => Set<Queen>();
    public DbSet<Inspection> Inspections => Set<Inspection>();
    public DbSet<Treatment> Treatments => Set<Treatment>();
    public DbSet<Harvest> Harvests => Set<Harvest>();
    public DbSet<Feeding> Feedings => Set<Feeding>();
    public DbSet<BeeTask> Tasks => Set<BeeTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hive>()
            .HasOne(h => h.Queen)
            .WithOne(q => q.Hive)
            .HasForeignKey<Queen>(q => q.HiveId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Hive>()
            .HasMany(h => h.Inspections)
            .WithOne(i => i.Hive)
            .HasForeignKey(i => i.HiveId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Hive>()
            .HasMany(h => h.Treatments)
            .WithOne(t => t.Hive)
            .HasForeignKey(t => t.HiveId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Hive>()
            .HasMany(h => h.Harvests)
            .WithOne(h => h.Hive)
            .HasForeignKey(h => h.HiveId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Hive>()
            .HasMany(h => h.Feedings)
            .WithOne(f => f.Hive)
            .HasForeignKey(f => f.HiveId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Apiary>()
            .HasMany(a => a.Hives)
            .WithOne(h => h.Apiary)
            .HasForeignKey(h => h.ApiaryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed data
        modelBuilder.Entity<Apiary>().HasData(
            new Apiary { Id = 1, Name = "Main Apiary", Description = "Primary apiary location", Location = "Backyard", CreatedAt = new DateTime(2024, 1, 1) }
        );
        modelBuilder.Entity<Hive>().HasData(
            new Hive { Id = 1, Name = "Hive #1", ApiaryId = 1, Type = HiveType.Langstroth, Status = HiveStatus.Active, EstablishedDate = new DateTime(2024, 3, 1), Color = "Yellow" },
            new Hive { Id = 2, Name = "Hive #2", ApiaryId = 1, Type = HiveType.Langstroth, Status = HiveStatus.Active, EstablishedDate = new DateTime(2024, 3, 15), Color = "Blue" }
        );
    }
}
