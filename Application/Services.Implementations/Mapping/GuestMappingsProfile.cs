using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations.Mapping;
public class GuestMappingsProfile : Profile
{
    public GuestMappingsProfile(IPasswordHasher hasher) // учесть что инжектится hasher в аутомаппер
    {
        CreateMap<CreateUserDto, Guest>()
            .ConstructUsing(src => new Guest(src.Username, hasher.GenerateHashPassword(src.Password), src.Email));
    }
}