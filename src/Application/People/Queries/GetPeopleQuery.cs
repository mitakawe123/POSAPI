using System.Text;
using POSAPI.Application.Common.Interfaces;
using POSAPI.Domain.Entities;
using POSAPI.Domain.Enums;

namespace POSAPI.Application.People.Queries;

public record GetPersonQuery(Guid Id) : IRequest<string>
{
    public Guid Id { get; } = Id;
}

public class GetUsersQueryHandler(IApplicationDbContext context) :
    IRequestHandler<GetPersonQuery, string>
{
    public async Task<string> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        // This is the LINQ version of the below code
        /*var entity = await context.People
            .Include(p => p.Addresses)
            .ThenInclude(a => a.Phones)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);*/

        FormattableString sql =
            $"""
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
              left join public."Phones" as ph on ph."AddressId" = a."Id"
              where pe."Id" = {request.Id}
              """;

        var personDetails = await context
            .SqlQuery<PersonSqlResult>(sql)
            .ToListAsync(cancellationToken);

        var personDict = personDetails
            .GroupBy(x => x.PersonId)
            .ToDictionary(
                g => g.Key,
                g => new Person
                {
                    Id = g.Key,
                    FullName = g.First().FullName,
                    Addresses = g.GroupBy(a => a.AddressId)
                    .Select(ag =>
                    {
                        var a = ag.First();
                        return new Address
                        {
                            Id = ag.Key,
                            Street = a.Street,
                            City = a.City,
                            State = a.State,
                            ZipCode = a.ZipCode,
                            Country = a.Country,
                            Type = a.Type,
                            Phones = a.PhoneId is not null ? ag
                                .Select(p => new Phone
                                {
                                    Id = p.PhoneId ?? Guid.Empty,
                                    PhoneNumber = p.PhoneNumber ?? string.Empty
                                }).ToList() : []
                        };
                    }).ToList()
                });

        var entity = personDict.Values.ToList().FirstOrDefault();

        Guard.Against.NotFound(request.Id, entity);

        var formattedOutput = FormatPersonData(entity);

        return formattedOutput;
    }

    private static string FormatPersonData(Person person)
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
