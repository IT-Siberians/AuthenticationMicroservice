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

        CreateMap<CreatingUserRequest, CreateUserModel>();
        #endregion
        #region UserReadModel=>UserReadResponse
        CreateMap<UserReadModel, UserReadResponse>();
        #endregion
        #region ChangeUsernameRequest=>ChangeUsernameRequest
        CreateMap<ChangeUsernameRequest, ChangeUsernameModel>();
        #endregion
        #region ChangePasswordRequest=>ChangePasswordRequest

        CreateMap<ChangePasswordRequest, ChangePasswordModel>();
        #endregion
        #region ChangeEmailRequest=>MailConfirmationGenerationModel
        CreateMap<ChangeEmailRequest, MailConfirmationGenerationModel>();
        #endregion
        #region VerifyEmailRequest=>VerifyEmailRequest
        CreateMap<VerifyEmailRequest, SetUserEmailModel>();
        #endregion
        #region ChangePasswordRequest=>ValidatePasswordModel
        CreateMap<ChangePasswordRequest, ValidatePasswordModel>()
            .ForMember(dest=>dest.Password,
                opt=>opt.MapFrom(
                    src=>src.OldPassword));
        #endregion
    }
}