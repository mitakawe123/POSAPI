using System.Text;
using POSAPI.Application.Common.Interfaces;
using POSAPI.Domain.Entities;
using POSAPI.Domain.Enums;

namespace POSAPI.Application.Person.Queries;

public record GetPeopleQuery(Guid Id) : IRequest<string>
{
    public Guid Id { get; } = Id;
}

public class GetUsersQueryHandler(IApplicationDbContext context) :
    IRequestHandler<GetPeopleQuery, string>
{
    public async Task<string> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
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

        var personDetails = await context.SqlQuery<PersonSqlResult>(sql).ToListAsync(cancellationToken);

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

        var formattedOutput = FormatPersonData(entity);

        return formattedOutput;
    }

    private static string FormatPersonData(Domain.Entities.Person person)
    {
        var sb = new StringBuilder();
        sb.AppendLine("--------------------");
        sb.AppendLine($"Name: {person.FullName}");

        foreach (var address in person.Addresses)
        {
            if (address.Type == AddressType.Office)
            {
                sb.AppendLine($"Office Address: {address.Street}");
            }
            else
            {
                sb.AppendLine($"Home Address: {address.Street}");
            }

            foreach (var phone in address.Phones)
            {
                sb.AppendLine($"Tel: {phone.PhoneNumber}");
            }
        }

        sb.AppendLine("--------------------");

        return sb.ToString();
    }
}
