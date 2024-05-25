namespace POSAPI.Domain.Events.TodoItemEvents;

public class TodoItemDeletedEvent(TodoItem item) : BaseEvent
{
    public TodoItem Item { get; } = item;
}
