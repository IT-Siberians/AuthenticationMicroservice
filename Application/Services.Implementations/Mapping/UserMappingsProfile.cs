using AutoMapper;
using Domain.Entities;
using Services.Contracts;

namespace Services.Implementations.Mapping;
public class UserMappingsProfile : Profile
{
    public UserMappingsProfile()
    {
        #region User=>UserReadModel
        CreateMap<User, UserReadModel>()
            //Id mapping
            .ForMember(
                dest=>dest.Id,
                opt=>opt.MapFrom(
                    src=>src.Id))
            //Username mapping
            .ForMember(
                dest=>dest.Username,
                opt=>opt.MapFrom(
                    src=>src.Username.Value))
            //PasswordHash mapping
            .ForMember(
                dest => dest.PasswordHash,
                opt => opt.MapFrom(
                    src => src.PasswordHash.Value))
            //Email mapping
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(
                    src => src.Email.Value))
            //AccountStatuses mapping
            .ForMember(
                dest => dest.AccountStatus,
                opt => opt.MapFrom(
                    src => src.AccountStatus));
        #endregion

        #region User=>PublicationOfEmailConfirmationModel

        CreateMap<User, PublicationOfEmailConfirmationModel>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(
                    src => src.Id))
            .ForMember(dest => dest.NewEmail,
                opt => opt.MapFrom(
                    src => src.Email.Value));


        #endregion
    }
}