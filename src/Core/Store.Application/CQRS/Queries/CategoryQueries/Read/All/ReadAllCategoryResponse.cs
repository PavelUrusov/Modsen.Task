using AutoMapper;
using Store.Application.Common;
using Store.Application.Mapper;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.CategoryQueries.Read.All;

public record ReadAllCategoryResponse : ResponseBase, IMapWith<IEnumerable<Category>>
{
    public IEnumerable<object> Categories { get; set; } = new List<object>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IEnumerable<Category>, ReadAllCategoryResponse>()
            .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.Select(category => new { category.Id, category.Name })));
    }
}