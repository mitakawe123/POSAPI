using POSAPI.Domain.Entities;

namespace POSAPI.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Address> Addresses { get; }
    DbSet<Phone> Phones { get; }
    DbSet<Domain.Entities.Person> People { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
