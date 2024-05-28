namespace POSAPI.Domain.Entities;
public class Person : BaseAuditableEntity
{
    public string FullName { get; set; } = string.Empty;

    public ICollection<Address> Addresses { get; set; } = [];
}
