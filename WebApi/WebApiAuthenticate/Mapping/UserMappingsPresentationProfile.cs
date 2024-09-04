using AutoMapper;
using Services.Contracts;
using WebApiAuthenticate.Requests;
using WebApiAuthenticate.Responses;

namespace WebApiAuthenticate.Mapping;

public class UserMappingsPresentationProfile : Profile
{
    public UserMappingsPresentationProfile()
    {
        #region CreatingUserRequest=>CreateUserModel

        CreateMap<CreatingUserRequest, CreateUserModel>()
            .ForMember(
                dest=>dest.Username,
                opt=>opt.MapFrom(
                    src=>src.Username.Trim().ToLower()))
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(
                    src => src.Email.Trim().ToLower()));
        #endregion
        #region UserModel=>UserResponse
        CreateMap<UserModel, UserResponse>();
        #endregion
        #region ChangePasswordRequest=>ChangePasswordRequest

        CreateMap<ChangePasswordRequest, ChangePasswordModel>();
        #endregion
        #region VerifyEmailRequestWithId=>VerifyEmailRequestWithId
        CreateMap<VerifyEmailRequestWithId, SetUserEmailModel>()
            .ForMember(
                dest => dest.NewEmail,
                opt => opt.MapFrom(
                    src => src.NewEmail.Trim().ToLower()));
        #endregion
        #region ChangePasswordRequest=>ValidatePasswordModel
        CreateMap<ChangePasswordRequest, ValidatePasswordModel>()
            .ForMember(dest=>dest.Password,
                opt=>opt.MapFrom(
                    src=>src.OldPassword));
        #endregion
    }
}