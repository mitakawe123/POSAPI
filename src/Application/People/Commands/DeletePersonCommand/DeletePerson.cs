using POSAPI.Application.Common.Interfaces;
using POSAPI.Domain.Entities;
using POSAPI.Domain.Events.PeopleEvents;

namespace POSAPI.Application.People.Commands.DeletePersonCommand;

public record DeletePersonCommand(Guid Id) : IRequest;

public class DeletePersonCommandHandler(IApplicationDbContext context) :
    IRequestHandler<DeletePersonCommand>
{
    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        int countOfPeopleDeleted = await context.People
            .Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken);

        if (countOfPeopleDeleted is 0)
        {
            throw new NotFoundException($"Person with Id {request.Id} not found.", request.Id.ToString());
        }

        Person person = new() { Id = request.Id };
        person.AddDomainEvent(new PersonDeletedEvent(person));
    }
}
