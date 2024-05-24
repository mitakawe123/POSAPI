using System.ComponentModel.DataAnnotations;

namespace POSAPI.Domain.Entities;
public class Person
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    public ICollection<Address> Addresses { get; set; } = [];
}
