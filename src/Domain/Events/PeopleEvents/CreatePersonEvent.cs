namespace POSAPI.Domain.Events.PeopleEvents;

public class PersonCreatedEvent(Person person) : BaseEvent
{
    public Person Person { get; } = person;
}
