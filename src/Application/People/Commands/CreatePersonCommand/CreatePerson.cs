using POSAPI.Application.Common.Interfaces;
using POSAPI.Domain.Entities;
using POSAPI.Domain.Events.PeopleEvents;

namespace POSAPI.Application.People.Commands.CreatePersonCommand;

public record CreatePersonCommand : IRequest<Guid>
{
    public string FullName { get; set; } = string.Empty;

    public ICollection<AddressDTO> Addresses { get; set; } = [];
}

public class CreatePersonCommandHandler(IApplicationDbContext context) :
    IRequestHandler<CreatePersonCommand, Guid>
{
    public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        Person person = new()
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Addresses = request.Addresses.Select(a => new Address
            {
                Street = a.Street,
                City = a.City,
                State = a.State,
                ZipCode = a.ZipCode,
                Country = a.Country,
                Type = a.Type,
                Phones = a.Phones?.Select(p => new Phone
                {
                    PhoneNumber = p.PhoneNumber
                }).ToList() ?? []
            }).ToList()
        };
        
        person.AddDomainEvent(new PersonCreatedEvent(person));

        context.People.Add(person);
        
        await context.SaveChangesAsync(cancellationToken);

        return person.Id;
    }
}
