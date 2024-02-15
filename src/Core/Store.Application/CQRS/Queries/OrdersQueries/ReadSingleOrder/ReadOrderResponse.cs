using AutoMapper;
using Store.Application.Common;
using Store.Application.Mapper;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.OrdersQueries.ReadSingleOrder;

public record ReadOrderResponse : ResponseBase, IMapWith<Order>
{

    public object Order { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, ReadOrderResponse>()
            .ForMember(dest => dest.Order,
                opt => opt.MapFrom(source => new
                {
                    source.Id,
                    source.CreationDate,
                    source.UserId,
                    OrderItems = source.OrderItems.Select(oi => new
                        { oi.Id, oi.Quantity, oi.Product.Name, Price = oi.Product.Price * oi.Quantity }),
                    TotalPrice = source.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)
                }));
    }

}