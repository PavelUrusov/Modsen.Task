using AutoMapper;
using Store.Application.Common;
using Store.Application.Mapper;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.OrdersQueries.ReadUserOrders;

public record ReadUserOrdersResponse : ResponseBase, IMapWith<IEnumerable<Order>>
{

    public List<object> Orders { get; set; } = null!;
    public int Count { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IEnumerable<Order>, ReadUserOrdersResponse>()
            .ForMember(dest => dest.Orders,
                opt => opt.MapFrom(source => source.Select(o => new
                {
                    o.Id,
                    o.CreationDate,
                    OrderItems = o.OrderItems.Select(oi => new
                        { oi.Id, oi.Quantity, oi.Product.Name, Price = oi.Product.Price * oi.Quantity }),
                    TotalPrice = o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)
                })))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(source => source.Count()));
    }

}