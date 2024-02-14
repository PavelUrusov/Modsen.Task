using AutoMapper;
using Store.Application.Common;
using Store.Application.Mapper;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.ProductQueries.ReadRange;

public record ReadProductsResponse : ResponseBase, IMapWith<IEnumerable<Product>>
{

    public IEnumerable<object> Products { get; set; } = null!;
    public int Count { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IEnumerable<Product>, ReadProductsResponse>()
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
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count()));
    }

}