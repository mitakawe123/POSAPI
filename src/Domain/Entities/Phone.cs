using System.ComponentModel.DataAnnotations;

namespace POSAPI.Domain.Entities;
public class Phone
{
    [Key]
    public string Id { get; set; } = string.Empty;

    [Required]
    public required string PhoneNumber { get; set; }

    public string AddressId { get; set; } = string.Empty;
    public required Address Address { get; set; }
}
