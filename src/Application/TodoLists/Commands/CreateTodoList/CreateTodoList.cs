using POSAPI.Application.Common.Interfaces;
using POSAPI.Domain.Entities;

namespace POSAPI.Application.TodoLists.Commands.CreateTodoList;

public record CreateTodoListCommand : IRequest<Guid>
{
    public string? Title { get; init; }
}

public class CreateTodoListCommandHandler(IApplicationDbContext context) : 
    IRequestHandler<CreateTodoListCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList { Title = request.Title };

        context.TodoLists.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
