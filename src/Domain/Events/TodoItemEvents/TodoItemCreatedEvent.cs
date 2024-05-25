namespace POSAPI.Domain.Events.TodoItemEvents;

public class TodoItemCreatedEvent(TodoItem item) : BaseEvent
{
    public TodoItem Item { get; } = item;
}
