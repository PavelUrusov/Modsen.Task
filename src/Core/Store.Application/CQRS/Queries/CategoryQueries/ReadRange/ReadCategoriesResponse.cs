using AutoMapper;
using Store.Application.Common;
using Store.Application.Mapper;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.CategoryQueries.ReadRange;

public record ReadCategoriesResponse : ResponseBase, IMapWith<IEnumerable<Category>>
{

    public IEnumerable<object> Categories { get; set; } = null!;
    public int Count { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IEnumerable<Category>, ReadCategoriesResponse>()
            .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.Select(category => new { category.Id, category.Name })))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count()));
    }

}