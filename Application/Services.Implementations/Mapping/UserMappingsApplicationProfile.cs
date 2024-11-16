using AutoMapper;
using Domain.Entities;
using Services.Contracts;

namespace Services.Implementations.Mapping;

public class UserMappingsApplicationProfile : Profile
{
    public UserMappingsApplicationProfile()
    {
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
                        src.Email.Value))
            .ForCtorParam(
                nameof(UserModel.Firstname),
                opt =>
                    opt.MapFrom(src =>
                        src.Firstname.Value))
            .ForCtorParam(
                nameof(UserModel.Lastname),
                opt =>
                    opt.MapFrom(
                        src =>
                            src.Lastname.Value));
    }
}