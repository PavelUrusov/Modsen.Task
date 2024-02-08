using AutoMapper;

namespace Store.Application.Mapper;

public interface IMapWith<T>
{
    void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}