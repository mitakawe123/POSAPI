using Microsoft.AspNetCore.Identity;
using POSAPI.Domain.Entities;

namespace POSAPI.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser
{
    public string UserId { get; set; } = string.Empty;

    public User User { get; set; } = new();
}
