using Store.Domain.Interfaces;

namespace Store.Domain.Entities;

public class OrderItem : IEntity<int>
{
    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;
    public int OrderId { get; set; }

    public virtual Product Product { get; set; } = null!;
    public int ProductId { get; set; }
    public int Id { get; set; }
}