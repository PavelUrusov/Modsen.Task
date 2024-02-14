namespace Store.WebApi.Common.Dtos;

public class UpdateProductDto
{

    public string? Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
    public IEnumerable<int>? NewCategoryIds { get; set; } = null!;

}