using POSAPI.Application.Common.Interfaces;
using POSAPI.Domain.Entities;

namespace POSAPI.Application.People.Commands.UpdatePersonCommand;
public record UpdatePersonCommand : IRequest
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public ICollection<Address> Addresses { get; set; } = [];
}

public class UpdatePersonCommandHandler(IApplicationDbContext context) :
    IRequestHandler<UpdatePersonCommand>
{
    public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.People
            .Include(p => p.Addresses)
            .ThenInclude(a => a.Phones)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.FullName = request.FullName;
        entity.Addresses = request.Addresses;

        await context.SaveChangesAsync(cancellationToken);
    }
}
