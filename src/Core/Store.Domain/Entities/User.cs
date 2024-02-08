using Microsoft.AspNetCore.Identity;
using Store.Domain.Interfaces;

namespace Store.Domain.Entities;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
    public virtual IEnumerable<Order> Orders { get; set; } = null!;
}