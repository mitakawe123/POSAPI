using POSAPI.Application.Common.Interfaces;
using POSAPI.Application.Person.Commands.CreatePersonCommand;
using POSAPI.Domain.Entities;

namespace POSAPI.Application.Person.Queries;

public record GetPeopleQuery(Guid Id) : IRequest<PersonDTO>
{
    public Guid Id { get; } = Id;
}

public class GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper) :
    IRequestHandler<GetPeopleQuery, PersonDTO>
{
    public async Task<PersonDTO> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
    {
        // This is the LINQ version of the below code
        /*var entity = await context.People
            .Include(p => p.Addresses)
            .ThenInclude(a => a.Phones)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);*/

        FormattableString sql = $"""
                                  
                                              select
                                                  pe."Id" as PersonId,
                                                  pe."FullName",
                                                  a."Id" as AddressId,
                                                  a."Street",
                                                  a."City",
                                                  a."State",
                                                  a."ZipCode",
                                                  a."Country",
                                                  a."Type",
                                                  ph."Id" as PhoneId,
                                                  ph."PhoneNumber"
                                              from public."People" as pe
                                              join public."Addresses" as a on pe."Id" = a."UserId"
                                              join public."Phones" as ph on ph."AddressId" = a."Id"
                                              where pe."Id" = {request.Id}
                                  """;

        var personDetails = await context.FromSqlInterpolated<PersonSqlResult>(sql).ConfigureAwait(false);

        var personDict = personDetails
            .GroupBy(x => x.PersonId)
            .ToDictionary(g => g.Key, g => new Domain.Entities.Person
            {
                Id = g.Key,
                FullName = g.First().FullName,
                Addresses = g.GroupBy(a => a.AddressId)
                    .Select(ag => new Address
                    {
                        Id = ag.Key,
                        Street = ag.First().Street,
                        City = ag.First().City,
                        State = ag.First().State,
                        ZipCode = ag.First().ZipCode,
                        Country = ag.First().Country,
                        Type = ag.First().Type,
                        Phones = ag.Select(p => new Phone
                        {
                            Id = p.PhoneId,
                            PhoneNumber = p.PhoneNumber
                        }).ToList()
                    }).ToList()
            });

        var entity = personDict.Values.ToList().FirstOrDefault();

        Guard.Against.NotFound(request.Id, entity);

        var personDto = mapper.Map<PersonDTO>(entity);

        return personDto;
    }
}
