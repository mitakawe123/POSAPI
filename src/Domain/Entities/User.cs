using System.ComponentModel.DataAnnotations;

namespace POSAPI.Domain.Entities;
public class User
{
    [Key] 
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    public ICollection<Address> Addresses { get; set; } = [];
}
