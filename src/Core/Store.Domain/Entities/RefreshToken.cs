using Store.Domain.Interfaces;

namespace Store.Domain.Entities;

public class RefreshToken : IEntity<long>
{

    public string Token { get; set; } = null!;
    public DateTime ExpiryOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedByIp { get; set; } = null!;
    public DateTime? RevokedOn { get; set; }
    public string? RevokedByIp { get; set; }

    public virtual User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public long Id { get; set; }

}