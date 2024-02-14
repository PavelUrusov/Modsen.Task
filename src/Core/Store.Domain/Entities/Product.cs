using Store.Domain.Interfaces;

namespace Store.Domain.Entities;

public class Product : IEntity<int>
{

    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public virtual IEnumerable<Category> Categories { get; set; } = null!;

    public virtual IEnumerable<OrderItem> OrderItems { get; set; } = null!;
    public int Id { get; set; }

}