namespace POSAPI.Domain.Events.PeopleEvents;
public class PersonDeletedEvent(Person person) : BaseEvent
{
    public Person Person { get; } = person;
}
