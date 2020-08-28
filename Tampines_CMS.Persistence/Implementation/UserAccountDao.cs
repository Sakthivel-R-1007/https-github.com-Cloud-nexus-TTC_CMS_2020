using Dapper;
using Tampines_CMS.Domain;
using Tampines_CMS.Persistence.DBConnectionFactory;
using Tampines_CMS.Persistence.Interface;
using System;
using System.Data;
using System.Linq;

namespace Tampines_CMS.Persistence.Implementation
{
    public class UserAccountDao : IUserAccountDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public UserAccountDao()
        {
            this.factory = new DbConnectionFactory("DefaultDB");
        }

        #endregion

        public UserAccount AuthenticateUser(UserAccount UA)
        {
            UserAccount result = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", UA.UserName, dbType: DbType.String);
            param.Add("@Password", UA.Password, dbType: DbType.String);
            param.Add("@SystemIP", UA.SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_AuthenticateUser]";
                result = conn.Query<UserAccount>(sql: SQL,
                                      param: param,
                                      commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }

        public int SaveUserLoginLog(UserAccount UA, out Guid SessionId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserGUID", UA.GUID, dbType: DbType.Guid);
            param.Add("@SystemIP", UA.SystemIp, dbType: DbType.String);
            param.Add("@GUID", dbType: DbType.Guid, direction: ParameterDirection.Output);
            SessionId = Guid.Empty;
            int result = 0;
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveUserLoginLog]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                SessionId = param.Get<Guid>("@GUID");
                conn.Close();
            }
            return result;
        }

        public ForgotPassword SaveForgotPassword(ForgotPassword FPP)
        {
            ForgotPassword result = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Email", FPP.UserAccount.Email, dbType: DbType.String);
            param.Add("@SystemIP", FPP.SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveForgotPassword]";

                result = conn.Query<ForgotPassword, UserAccount, ForgotPassword>(sql: SQL,
                    param: param,
                    map: (FP, U) =>
                    {
                        if (FP != null)
                        {
                            FP.UserAccount = U;
                        }
                        return FP;
                    },
            commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }

        public ForgotPassword VerifyForgotPasswordUniqueId(ForgotPassword FP)
        {
            ForgotPassword result = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@UniqueId", FP.UniqueId, dbType: DbType.Guid);
            param.Add("@Id", FP.UserAccount.GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_VerifyForgotPasswordUniqueId]";
                result = conn.Query<ForgotPassword, UserAccount, ForgotPassword>(sql: SQL,
                    param: param,
                    map: (FPD, U) =>
                    {
                        if (FPD != null)
                        {
                            FPD.UserAccount = U;
                        }
                        return FPD;
                    },
            commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }

        public UserAccount UpdatePassword(ForgotPassword FP)
        {
            UserAccount result = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@UniqueId", FP.UniqueId, dbType: DbType.Guid);
            param.Add("@SystemIP", FP.SystemIp, dbType: DbType.String);
            param.Add("@UserId", FP.UserAccount.GUID, dbType: DbType.Guid);
            param.Add("@Password", FP.UserAccount.Password, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdatePassword]";
                result = conn.Query<UserAccount>(sql: SQL,
                                      param: param,
                                      commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }

        public bool CheckLoginStatus(Guid SessionId, Guid UserGUID)
        {
            DynamicParameters param = new DynamicParameters();
            if (SessionId != Guid.Empty)
                param.Add("@GUID", SessionId, dbType: DbType.Guid);

            if (UserGUID != Guid.Empty)
                param.Add("@UserGUID", UserGUID, dbType: DbType.Guid);

            bool result = false;
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_CheckLoginStatus]";
                var logs = conn.Query<long>(sql: SQL, param: param, commandType: CommandType.StoredProcedure).ToList();
                result = (logs != null && logs.Count() > 0);
            }
            return result;
        }

        public int UpdateUserLoginLog(UserAccount UA)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", UA.SessionId, DbType.Guid);
            param.Add("@UserGUID", UA.GUID, DbType.Guid);
            param.Add("@IsForcedLogOut", UA.LastLoginStatus, dbType: DbType.Boolean);
            param.Add("@SystemIp", UA.SystemIp, dbType: DbType.String);
            int result = 0;
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdateUserLoginLog]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }

        public int ChangePassword(UserAccount user)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", user.GUID, DbType.Guid);
            param.Add("@CurrentPassword", user.Password, DbType.String);
            param.Add("@NewPassword", user.NewPassword, DbType.String);
            param.Add("@SystemIp", user.SystemIp, DbType.String);
            int result = 0;
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_ProfileChangePassword]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }
    }
}
