using Data.Contexts.Foods.Mappings;
using Domain.Contexts.Foods.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Database;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
    ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    ChangeTracker.AutoDetectChangesEnabled = false;
  }

  public DbSet<Food> Foods { get; set; } = null!;
  public DbSet<Group> Groups { get; set; } = null!;
  public DbSet<Component> Components { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

    foreach (var property in modelBuilder.Model.GetEntityTypes()
      .SelectMany(e => e.GetProperties()
        .Where(p => p.ClrType == typeof(string))))
      property.SetColumnType("varchar(100)");

    foreach (var relationship in modelBuilder.Model.GetEntityTypes()
      .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;


    modelBuilder.ApplyConfiguration(new FoodMap());
    modelBuilder.ApplyConfiguration(new GroupMap());
    modelBuilder.ApplyConfiguration(new ComponentMap());

    base.OnModelCreating(modelBuilder);
  }


  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
    {
      if (entry.State == EntityState.Added)
      {
        entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
      }

      if (entry.State == EntityState.Modified)
      {
        entry.Property("CreatedAt").IsModified = false;
      }
    }
    return base.SaveChangesAsync(cancellationToken);
  }
}