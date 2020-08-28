using Tampines_CMS.Domain;
using System;

namespace Tampines_CMS.Persistence.Interface
{
    public interface IUserAccountDao
    {

        UserAccount AuthenticateUser(UserAccount UA);

        int SaveUserLoginLog(UserAccount UA, out Guid SessionId);

        ForgotPassword SaveForgotPassword(ForgotPassword FPP);

        ForgotPassword VerifyForgotPasswordUniqueId(ForgotPassword FP);

        UserAccount UpdatePassword(ForgotPassword FP);

        bool CheckLoginStatus(Guid SessionId, Guid UserGUID);

        int UpdateUserLoginLog(UserAccount UA);

        int ChangePassword(UserAccount user);
    }
}
