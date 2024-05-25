using POSAPI.Application.Common.Interfaces;
using POSAPI.Domain.Entities;

namespace POSAPI.Application.Person.Commands.UpdatePersonCommand;
public record UpdatePersonCommand : IRequest
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public ICollection<Address>? Addresses { get; set; } = [];
}

public class UpdatePersonCommandHandler(IApplicationDbContext context) :
    IRequestHandler<UpdatePersonCommand>
{
    public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        //here I have an error I don't check the nested addresses and phones id's 
        var entity = await context.People
            .Include(p => p.Addresses)
            .ThenInclude(a => a.Phones)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.FullName = request.FullName;

        if (request.Addresses is not null && request.Addresses.Count is not 0)
            entity.Addresses = request.Addresses;

        await context.SaveChangesAsync(cancellationToken);
    }
}
