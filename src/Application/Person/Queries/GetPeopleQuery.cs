using POSAPI.Application.Common.Interfaces;
using POSAPI.Application.Person.Commands.CreatePersonCommand;

namespace POSAPI.Application.Person.Queries;

public record GetPeopleQuery(Guid Id) : IRequest<PersonDTO>
{
    public Guid Id { get; set; } = Id;
}

public class GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper) :
    IRequestHandler<GetPeopleQuery, PersonDTO>
{
    public async Task<PersonDTO> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.People
            .Include(p => p.Addresses)
            .ThenInclude(a => a.Phones)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        var personDto = mapper.Map<PersonDTO>(entity);

        return personDto;
    }
}
