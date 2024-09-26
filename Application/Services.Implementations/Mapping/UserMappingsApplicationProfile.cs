using AutoMapper;
using Domain.Entities;
using Services.Contracts;

namespace Services.Implementations.Mapping;

public class UserMappingsApplicationProfile : Profile
{
    public UserMappingsApplicationProfile()
    {
        #region User=>UserModel

        CreateMap<User, UserModel>()
            .ForMember(
                dest => dest.Username,
                opt => opt.MapFrom(
                    src => src.Username.Value))
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(
                    src => src.Email.Value));
        #endregion
    }
}