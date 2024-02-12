using AutoMapper;
using Store.Application.Common;
using Store.Application.Mapper;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.CategoryQueries.Read.Single;

public record ReadSingleCategoryResponse : ResponseBase, IMapWith<Category>
{
    public object Category { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, ReadSingleCategoryResponse>()
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(category => new { category.Id, category.Name, category.Description }));
    }
}