using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSAPI.Domain.Entities;

namespace POSAPI.Infrastructure.Data.Configurations;
public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(p => p.FullName)
            .HasMaxLength(100)
            .IsRequired();
    }
}
