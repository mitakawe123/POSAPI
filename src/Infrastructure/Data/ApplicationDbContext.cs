using System.Reflection;
using POSAPI.Application.Common.Interfaces;
using POSAPI.Domain.Entities;
using POSAPI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace POSAPI.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public DbSet<Person> People => Set<Person>();

    public DbSet<Address> Addresses => Set<Address>();

    public DbSet<Phone> Phones => Set<Phone>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<Person>()
            .HasMany(p => p.Addresses)
            .WithOne(a => a.Person)
            .HasForeignKey(a => a.UserId);

        builder.Entity<Address>()
            .HasMany(a => a.Phones)
            .WithOne(t => t.Address)
            .HasForeignKey(t => t.AddressId);
    }
}
