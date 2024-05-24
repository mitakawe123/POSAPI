using System.ComponentModel.DataAnnotations;

namespace POSAPI.Domain.Entities;
public class Address
{
    [Key]
    public string Id { get; set; } = string.Empty;

    public string AddressLine { get; set; } = string.Empty;

    public AddressType AddressType { get; set; }

    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }

    public ICollection<Phone> Phones { get; set; } = [];
}
