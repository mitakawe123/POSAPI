using Microsoft.Extensions.Logging;
using POSAPI.Domain.Events.TodoItemEvents;

namespace POSAPI.Application.TodoItems.EventHandlers;

public class TodoItemCompletedEventHandler(ILogger<TodoItemCompletedEventHandler> logger)
    : INotificationHandler<TodoItemCompletedEvent>
{
    public Task Handle(TodoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("POSAPI Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
