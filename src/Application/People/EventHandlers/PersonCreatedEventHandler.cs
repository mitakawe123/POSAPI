using Microsoft.Extensions.Logging;
using POSAPI.Domain.Events.PeopleEvents;

namespace POSAPI.Application.People.EventHandlers;
public class PersonCreatedEventHandler(ILogger<PersonCreatedEventHandler> logger) : 
    INotificationHandler<PersonCreatedEvent>
{
    public Task Handle(PersonCreatedEvent notification, CancellationToken cancellationToken)
    {
        //for now just log some info 
        logger.LogInformation("POSAPI Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
