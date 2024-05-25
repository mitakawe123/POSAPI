using System.Reflection;
using POSAPI.Application.Common.Interfaces;
using POSAPI.Domain.Entities;
using POSAPI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POSAPI.Application.Person.Queries;

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

        builder.Entity<PersonSqlResult>().HasNoKey();

        builder.Entity<Person>()
            .HasMany(p => p.Addresses)
            .WithOne(a => a.Person)
            .HasForeignKey(a => a.UserId);

        builder.Entity<Address>()
            .HasMany(a => a.Phones)
            .WithOne(t => t.Address)
            .HasForeignKey(t => t.AddressId);
    }

    public async Task<List<TEntity>> FromSqlRaw<TEntity>(string sql, params object[] parameters) where TEntity : class
    {
        return await Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync();
    }

    //The FromSqlInterpolated method ensures that the interpolated variables are parameterized, mitigating the risk of SQL injection
    public async Task<List<TEntity>> FromSqlInterpolated<TEntity>(FormattableString sql) where TEntity : class
    {
        return await Set<TEntity>().FromSqlInterpolated(sql).ToListAsync();
    }

    public IQueryable<TEntity> SqlQuery<TEntity>(FormattableString sql) where TEntity : class
    {
        return Database.SqlQuery<TEntity>(sql);
    }
}
