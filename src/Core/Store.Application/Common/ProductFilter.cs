namespace Store.Application.Common;

[Flags]
public enum ProductFilter
{
    None = 0,
    Name = 1,
    MinPrice = 2,
    MaxPrice = 4,
    AvailableInStock = 8,
    Categories = 32
}