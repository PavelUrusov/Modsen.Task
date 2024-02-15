using Microsoft.AspNetCore.Identity;

namespace Store.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public int Level { get; set; }
}