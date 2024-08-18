using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations.Mapping;

public class GuestMappingsProfile : Profile
{
    public GuestMappingsProfile(IPasswordHasher hasher)
    {
        CreateMap<CreateUserModel, Guest>()
            .ConstructUsing(src => new Guest(src.Username, hasher.GenerateHashPassword(src.Password), src.Email));
    }
}