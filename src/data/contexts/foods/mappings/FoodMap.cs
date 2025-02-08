using Domain.Contexts.Foods.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Contexts.Foods.Mappings;

public class FoodMap : IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        builder.ToTable("Foods");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(x => x.Code)
            .HasColumnName("Code")
            .HasColumnType("VARCHAR")
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(x => x.ScientificName)
            .HasColumnName("ScientificName")
            .HasColumnType("VARCHAR")
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(x => x.Brand)
            .HasColumnName("Brand")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100)
            .IsRequired(false);


        builder.HasOne(x => x.Group)
            .WithMany(g => g.Foods)
            .HasForeignKey("FoodId")
            .HasPrincipalKey(f => f.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}