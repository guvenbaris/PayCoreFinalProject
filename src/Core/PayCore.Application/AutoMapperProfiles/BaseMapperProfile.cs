using AutoMapper;
using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.Application.AutoMapperProfiles
{
    public class BaseMapperProfile : Profile
    {
        public BaseMapperProfile()
        {
            CreateMap<Container, ContainerModel>().IncludeBase<BaseEntity, BaseModel>().ReverseMap();
            CreateMap<BaseEntity, BaseModel>().ReverseMap();
        }
    }
}
