using AutoMapper;
using Store.Application.Common;
using Store.Application.Mapper;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.OrdersQueries.ReadRange;

public record ReadOrdersResponse : ResponseBase, IMapWith<Order>
{

    public List<object> Orders { get; set; } = null!;
    public int Count { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IEnumerable<Order>, ReadOrdersResponse>()
            .ForMember(dest => dest.Orders,
                opt => opt.MapFrom(source => source.Select(o => new
                {
                    o.Id,
                    o.UserId,
                    o.CreationDate,
                    TotalPrice = o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)
                }))).ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count()));

        ;
    }

}