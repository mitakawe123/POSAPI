using POSAPI.Application.Common.Interfaces;
using POSAPI.Domain.Entities;
using POSAPI.Domain.Events.TodoItemEvents;

namespace POSAPI.Application.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand : IRequest<Guid>
{
    public int ListId { get; init; }

    public string? Title { get; init; }
}

public class CreateTodoItemCommandHandler(IApplicationDbContext context) : 
    IRequestHandler<CreateTodoItemCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItem
        {
            ListId = request.ListId,
            Title = request.Title,
            Done = false
        };

        entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        context.TodoItems.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
