using POSAPI.Domain.Entities;
using POSAPI.Domain.Enums;

namespace POSAPI.Application.Person.Commands.CreatePersonCommand;
public record AddressDTO
{
    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string ZipCode { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public AddressType Type { get; set; }

    public ICollection<PhoneDTO>? Phones { get; set; } = [];

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Address, AddressDTO>();
        }
    }
}
