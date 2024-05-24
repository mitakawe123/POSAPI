using System.ComponentModel.DataAnnotations;

namespace POSAPI.Domain.Entities;
public class Phone
{
    [Key]
    public Guid Id { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public Guid AddressId { get; set; }

    public Address Address { get; set; } = new();
}
