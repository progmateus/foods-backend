using Domain.Contexts.Foods.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Contexts.Foods.Mappings;

public class GroupMap : IEntityTypeConfiguration<Group>
{
  public void Configure(EntityTypeBuilder<Group> builder)
  {
    builder.ToTable("Groups");

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Name)
        .HasColumnName("Name")
        .HasColumnType("VARCHAR")
        .HasMaxLength(200)
        .IsRequired(false);
  }
}