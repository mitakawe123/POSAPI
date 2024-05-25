using Microsoft.Extensions.Logging;
using POSAPI.Domain.Events.PeopleEvents;

namespace POSAPI.Application.Person.EventHandlers;
public class PersonDeletedEventHandler(ILogger<PersonDeletedEventHandler> logger) :
    INotificationHandler<PersonDeletedEvent>
{
    public Task Handle(PersonDeletedEvent notification, CancellationToken cancellationToken)
    {
        //for now just log some info 
        logger.LogInformation("POSAPI Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
