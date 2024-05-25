using POSAPI.Domain.Entities;

namespace POSAPI.Application.People.Commands.CreatePersonCommand;
public record PhoneDTO
{
    public string PhoneNumber { get; set; } = string.Empty;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Phone, PhoneDTO>();
        }
    }
}
