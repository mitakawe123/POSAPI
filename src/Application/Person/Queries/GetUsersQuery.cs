using POSAPI.Application.Common.Interfaces;

namespace POSAPI.Application.Person.Queries;

public class GetPeopleQuery : IRequest<IEnumerable<Domain.Entities.Person>>;

public class GetUsersQueryHandler(IApplicationDbContext context) : 
    IRequestHandler<GetPeopleQuery, IEnumerable<Domain.Entities.Person>>
{
    public async Task<IEnumerable<Domain.Entities.Person>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
    {
        return await context.People.ToListAsync(cancellationToken);
    }
}
