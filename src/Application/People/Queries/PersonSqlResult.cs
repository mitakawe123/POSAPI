using POSAPI.Domain.Enums;

namespace POSAPI.Application.People.Queries;
public record PersonSqlResult
{
    public Guid PersonId { get; set; }

    public string FullName { get; set; } = string.Empty;
    
    public Guid AddressId { get; set; }

    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string ZipCode { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public AddressType Type { get; set; }

    public Guid PhoneId { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;
}

