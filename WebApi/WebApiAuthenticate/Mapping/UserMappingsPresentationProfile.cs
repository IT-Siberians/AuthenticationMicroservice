using AutoMapper;
using Services.Contracts;
using System.Globalization;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.Responses;

namespace WebApiAuthenticate.Mapping;

public class UserMappingsPresentationProfile : Profile
{
    public UserMappingsPresentationProfile()
    {
        CreateMap<CreatingUserRequest, CreateUserModel>()
            .ForMember(
                dest => dest.Username,
                opt => opt.MapFrom(
                    src => src.Username.Trim().ToLower()))
            .ForMember(
                dest => dest.Firstname,
                opt => opt.MapFrom(
                    src =>
                        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.FirstName.Trim())))
            .ForMember(
                dest => dest.Lastname,
                opt => opt.MapFrom(
                    src =>
                        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.Lastname.Trim())))
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(
                    src => src.Email.Trim().ToLower()));

        CreateMap<UserModel, UserInfoResponse>();

        CreateMap<ChangePasswordRequest, ChangePasswordModel>();

        CreateMap<ConfirmEmailRequest, EmailConfirmationModel>()
            .ForMember(
                dest => dest.NewEmail,
                opt => opt.MapFrom(
                    src => src.NewEmail.Trim().ToLower()));

        CreateMap<ChangePasswordRequest, ValidatePasswordModel>()
            .ForMember(
                dest => dest.Password,
                opt => opt.MapFrom(
                    src => src.OldPassword));
    }
}