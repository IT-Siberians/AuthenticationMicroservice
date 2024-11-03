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
            .ForCtorParam(
                nameof(UserModel.Username),
                opt => 
                    opt.MapFrom(src => 
                        src.Username.Value))
            .ForCtorParam(
                nameof(UserModel.Email),
                opt => 
                    opt.MapFrom(src =>
                        src.Email.Value));

        #endregion
    }
}