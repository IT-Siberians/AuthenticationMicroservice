using AutoMapper;
using Services.Contracts;
using WebApiAuthenticate.Models;

namespace WebApiAuthenticate.Mapping;
public class UserMappingsProfile : Profile
{
    public UserMappingsProfile()
    {
        #region CreatingUserModel=>CreateUserDto
        CreateMap<CreatingUserModel, CreateUserDto>()
            //Username mapping
            .ForMember(
                dest => dest.Username,
                opt => opt.MapFrom(
                    src => src.Username))
            //Password mapping
            .ForMember(
                dest => dest.Password,
                opt => opt.MapFrom(
                    src => src.Password))
            //Email mapping
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(
                    src => src.Email));
        #endregion
        #region UserReadDto=>UserResponseModel
        CreateMap<UserReadDto, UserResponseModel>()
            //Id mapping
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(
                    src => src.Id))
            //Username mapping
            .ForMember(
                dest => dest.Username,
                opt => opt.MapFrom(
                    src => src.Username))
            //PasswordHash mapping
            .ForMember(
                dest => dest.PasswordHash,
                opt => opt.MapFrom(
                    src => src.PasswordHash))
            //Email mapping
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(
                    src => src.Email))
            //AccountStatus mapping
            .ForMember(
                dest => dest.AccountStatus,
                opt => opt.MapFrom(
                    src => src.AccountStatus));
        #endregion
        #region ChangeUsernameModel=>ChangeUsernameDto
        CreateMap<ChangeUsernameModel, ChangeUsernameDto>()
            //Id mapping
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(
                    src => src.Id))
            //Username mapping
            .ForMember(
                dest => dest.Username,
                opt => opt.MapFrom(
                    src => src.Username));
        #endregion
        #region ChangePasswordModel=>ChangePasswordDto
        CreateMap<ChangePasswordModel, ChangePasswordDto>()
            //Id mapping
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(
                    src => src.Id))
            //Password mapping
            .ForMember(
                dest => dest.Password,
                opt => opt.MapFrom(
                    src => src.Password))
            //NewPassword mapping
            .ForMember(
                dest => dest.NewPassword,
                opt => opt.MapFrom(
                    src => src.NewPassword));
        #endregion
        #region ChangeEmailModel=>PublicationOfEmailConfirmationDto
        CreateMap<ChangeEmailModel, PublicationOfEmailConfirmationDto>()
            //Id mapping
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(
                    src => src.Id))
            //NewEmail mapping
            .ForMember(
                dest => dest.NewEmail,
                opt => opt.MapFrom(
                    src => src.NewEmail));
        #endregion
        #region VerifyEmailModel=>VerifyEmailDto
        CreateMap<VerifyEmailModel, VerifyEmailDto>()
            //Id mapping
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(
                    src => src.Id))
            //NewEmail mapping
            .ForMember(
                dest => dest.NewEmail,
                opt => opt.MapFrom(
                    src => src.NewEmail));
        #endregion
    }
}