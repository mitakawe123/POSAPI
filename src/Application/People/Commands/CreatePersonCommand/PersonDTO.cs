using POSAPI.Domain.Entities;

namespace POSAPI.Application.People.Commands.CreatePersonCommand;
public record PersonDTO
{
    public string FullName { get; set; } = string.Empty;

    public ICollection<AddressDTO> Addresses { get; set; } = [];

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Person, PersonDTO>();
        }
    }
}
