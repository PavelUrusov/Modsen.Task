using AutoMapper;
using Store.Application.Common;
using Store.Application.Mapper;
using Store.Domain.Entities;

namespace Store.Application.CQRS.Queries.CategoryQueries.ReadSingle;

public record ReadCategoryResponse : ResponseBase, IMapWith<Category>
{

    public object Category { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, ReadCategoryResponse>()
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(category => new { category.Id, category.Name, category.Description }));
    }

}