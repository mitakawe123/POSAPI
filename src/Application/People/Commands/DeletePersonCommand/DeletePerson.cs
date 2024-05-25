using POSAPI.Application.Common.Interfaces;
using POSAPI.Domain.Events.PeopleEvents;

namespace POSAPI.Application.People.Commands.DeletePersonCommand;

public record DeletePersonCommand(Guid Id) : IRequest;

public class DeletePersonCommandHandler(IApplicationDbContext context) :
    IRequestHandler<DeletePersonCommand>
{
    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.People
            .Include(p => p.Addresses)
            .ThenInclude(a => a.Phones)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        context.People.Remove(entity);

        entity.AddDomainEvent(new PersonDeletedEvent(entity));

        await context.SaveChangesAsync(cancellationToken);
    }
}
