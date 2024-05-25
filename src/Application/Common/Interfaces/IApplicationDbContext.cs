using POSAPI.Domain.Entities;

namespace POSAPI.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Address> Addresses { get; }
    DbSet<Phone> Phones { get; }
    DbSet<Person> People { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<List<TEntity>> FromSqlRaw<TEntity>(string sql, params object[] parameters) where TEntity : class;
    Task<List<TEntity>> FromSqlInterpolated<TEntity>(FormattableString sql) where TEntity : class;
    IQueryable<TEntity> SqlQuery<TEntity>(FormattableString sql) where TEntity : class;
}
