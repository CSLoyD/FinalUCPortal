using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UCPortal.DTO.Request;
using UCPortal.DTO.Response;

namespace UCPortal.BusinessLogic.Login
{
    public interface ILoginManagement
    {
        LoginResponse CheckIfUserExist(LoginRequest loginRequest);
        NotificationResponse GetNotifications(NotificationRequest notificationRequest);
        VerifyEmailResponse CheckifUserIsVerified(VerifyEmailRequest verifyEmailRequest);      
        ResetPasswordResponse ResetPassword(ResetPasswordRequest resetPasswordRequest);
        ChangePasswordResponse ChangePassword(ChangePasswordRequest changePassword);
        EmailCheckResponse CheckEmail(EmailCheckRequest emailCheckRequest);
        ValidateEmailResponse ValidateEmail(ValidateEmailRequest validateEmailRequest);
        ReadNotifResponse ReadNotification(ReadNotifRequest readNotifRequest);
        CreateAccountResponse CreateAccount(CreateAccountRequest createAccountRequest);
        UpdateEmailResponse UpdateEmail(UpdateEmailRequest updateEmailRequest);
    }
}
