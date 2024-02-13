using AutoMapper;
using Store.Application.Common;
using Store.Application.Mapper;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.ProductQueries.Read.Range;

public record ReadRangeProductResponse : ResponseBase, IMapWith<IEnumerable<Product>>
{
    public IEnumerable<object> Products { get; set; } = null!;
    public int Quantity { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IEnumerable<Product>, ReadRangeProductResponse>()
            .ForMember(dest => dest.Products,
                opt => opt.MapFrom(src => src.Select(product => new
                {
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.Quantity,
                    Categories = product.Categories.Select(category => new
                    {
                        category.Id,
                        category.Name,
                        category.Description
                    })
                })))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Count()));
    }
}