using AutoMapper;
using Services.Contracts;
using WebApiAuthenticate.Models;

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
        #region ChangeEmailRequest=>PublicationOfEmailConfirmationModel
        CreateMap<ChangeEmailRequest, PublicationOfEmailConfirmationModel>();
        #endregion
        #region VerifyEmailRequest=>VerifyEmailRequest
        CreateMap<VerifyEmailRequest, VerifyEmailModel>();
        #endregion
        #region ChangePasswordRequest=>ValidatePasswordModel
        CreateMap<ChangePasswordRequest, ValidatePasswordModel>();
        #endregion
    }
}