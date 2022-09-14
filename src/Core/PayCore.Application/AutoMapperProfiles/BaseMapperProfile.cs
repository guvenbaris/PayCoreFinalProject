using AutoMapper;
using PayCore.Application.Models;
using PayCore.Domain.Entities;

namespace PayCore.Application.AutoMapperProfiles
{
    public class BaseMapperProfile : Profile
    {
        public BaseMapperProfile()
        {
            CreateMap<ManagerEntity, ManagerModel>().IncludeBase<BaseEntity, BaseModel>().ReverseMap();
            CreateMap<ApartmentEntity, ApartmentModel>().IncludeBase<BaseEntity, BaseModel>().ReverseMap();
            CreateMap<UserEntity, UserModel>().IncludeBase<BaseEntity, BaseModel>().ReverseMap();
            CreateMap<BaseEntity, BaseModel>().ReverseMap();
        }
    }
}
