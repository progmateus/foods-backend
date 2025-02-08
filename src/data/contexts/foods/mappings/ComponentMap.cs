using Domain.Contexts.Foods.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Contexts.Foods.Mappings;

public class ComponentMap : IEntityTypeConfiguration<Component>
{
  public void Configure(EntityTypeBuilder<Component> builder)
  {
    builder.ToTable("Components");

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Name)
      .HasColumnName("Name")
      .HasColumnType("VARCHAR")
      .HasMaxLength(200)
      .IsRequired(false);

    builder.Property(x => x.Unit)
      .HasColumnName("Unit")
      .HasColumnType("VARCHAR")
      .HasMaxLength(100)
      .IsRequired(false);

    builder.Property(x => x.Value)
      .HasColumnName("Value")
      .HasColumnType("VARCHAR")
      .HasMaxLength(100)
      .IsRequired(false);

    builder.HasOne(x => x.Food)
      .WithMany(f => f.Components)
      .HasForeignKey("FoodId")
      .HasPrincipalKey(f => f.Id)
      .OnDelete(DeleteBehavior.Cascade);
  }
}