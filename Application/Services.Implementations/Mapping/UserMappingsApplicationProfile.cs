using AutoMapper;
using Domain.Entities;
using Otus.QueueDto.User;
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
                nameof(UserModel.FirstName),
                opt =>
                    opt.MapFrom(src =>
                        src.FirstName.Value))
            .ForCtorParam(
                nameof(UserModel.LastName),
                opt =>
                    opt.MapFrom(
                        src =>
                            src.LastName.Value));

        CreateMap<UserModel, UserSignUpEvent>()
            .ForCtorParam(
                nameof(UserSignUpEvent.Id),
                opt =>
                    opt.MapFrom(src =>
                        src.Id))
            .ForCtorParam(
                nameof(UserSignUpEvent.Email),
                opt =>
                    opt.MapFrom(src =>
                        src.Email))
            .ForCtorParam(
                nameof(UserSignUpEvent.FirstName),
                opt =>
                    opt.MapFrom(src =>
                        src.FirstName))
            .ForCtorParam(
                nameof(UserSignUpEvent.LastName),
                opt =>
                    opt.MapFrom(
                        src =>
                            src.LastName))
            .ForCtorParam(
                nameof(UserSignUpEvent.Username),
                opt =>
                    opt.MapFrom(
                        src =>
                            src.Username));

        CreateMap<UserModel, EmailChangedEvent>()
            .ForCtorParam(
                nameof(EmailChangedEvent.Id),
                opt =>
                    opt.MapFrom(src =>
                        src.Id))
            .ForCtorParam(
                nameof(EmailChangedEvent.ChangedEmail),
                opt =>
                    opt.MapFrom(src =>
                        src.Email));
    }
}