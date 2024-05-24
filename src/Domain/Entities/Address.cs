using System.ComponentModel.DataAnnotations;

namespace POSAPI.Domain.Entities;
public class Address
{
    [Key]
    public Guid Id { get; set; }

    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;
    
    public string State { get; set; } = string.Empty;
    
    public string ZipCode { get; set; } = string.Empty;
    
    public string Country { get; set; } = string.Empty;
    
    public AddressType Type { get; set; }
    
    public Guid UserId { get; set; }
    
    public Person Person { get; set; } = new();
    
    public ICollection<Phone> Phones { get; set; } = [];
}
