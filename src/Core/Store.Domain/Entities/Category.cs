using Store.Domain.Interfaces;

namespace Store.Domain.Entities;

public class Category: IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual IEnumerable<Product> Products { get; set; } = null!;
}