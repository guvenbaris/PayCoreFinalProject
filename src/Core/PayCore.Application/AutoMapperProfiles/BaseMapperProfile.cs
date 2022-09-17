using AutoMapper;
using PayCore.Application.Dtos.Auth;
using PayCore.Application.Models;
using PayCore.Application.ViewModel.User;
using PayCore.Domain.Entities;

namespace PayCore.Application.AutoMapperProfiles
{
    public class BaseMapperProfile : Profile
    {
        public BaseMapperProfile()
        {
            CreateMap<BaseEntity, BaseModel>().ReverseMap();

            CreateMap<CategoryEntity, CategoryModel>().IncludeBase<BaseEntity, BaseModel>().ReverseMap();
            CreateMap<UserEntity, UserModel>().IncludeBase<BaseEntity, BaseModel>().ReverseMap();
            CreateMap<OfferEntity, OfferModel>().IncludeBase<BaseEntity, BaseModel>();

            CreateMap<UserEntity, UserViewModel>();
            CreateMap<UserModel, UserViewModel>().ReverseMap();
            CreateMap<ProductEntity, ProductModel>().IncludeAllDerived();

            CreateMap<OfferModel, OfferEntity>()
                .ForMember(x => x.Product, source => source.MapFrom(src => src))
                .ForMember(x=>x.User,source => source.MapFrom(src => src));

            CreateMap<OfferModel, ProductEntity>()
                .ForMember(x => x.Id, source => source.MapFrom(src => src.ProductId));

            CreateMap<OfferModel, UserEntity>()
                .ForMember(x => x.Id, source => source.MapFrom(src => src.UserId));
        }
    }
}
