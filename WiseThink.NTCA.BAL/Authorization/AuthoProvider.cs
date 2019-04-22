using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WiseThink.NTCA.DataEntity;
using System.Data;
using WiseThink.NTCA.DAL;
using Org.BouncyCastle.Asn1.Ocsp;

namespace WiseThink.NTCA.BAL.Authorization
{
    public class AuthoProvider
    {
        public static string User
        {
            get
            {
                if (GetLoggedInUser() != null)
                {
                    return GetLoggedInUser().UserName;
                }
                else
                {
                    return "";
                }
            }
        }
        public static DataSet IsUserActive(string userName)
        {
            DataSet ds = DataAccess.Instance.ExecuteDataSet(StoredProcedure.spIsUserInactive, System.Data.CommandType.StoredProcedure,
                    new List<ICommanParameter> 
                {
                    new CommanParameter{ Name="@UserName", Type= System.Data.DbType.String, value= userName}
                }
            );
            return ds;
        }
        public UserInfo AuthenticateUser(string userName, string password, bool IsPresistent = false)
        {
            UserInfo uinfo = new UserInfo();
            try
            {
                // Authenticating User with username and encrypted password. (Zahir)
                DataSet ds = DataAccess.Instance.ExecuteDataSet(StoredProcedure.spLogin, System.Data.CommandType.StoredProcedure,
                     new List<ICommanParameter> 
                {
                    new CommanParameter{ Name="@UserName", Type= System.Data.DbType.String, value= userName},
                    new CommanParameter{ Name="@Pwd", Type= System.Data.DbType.String, value= password}
                }
             );
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    uinfo.UserName = dr[DataBaseFields.UserName].GetEmptyOrString();
                    string roleUser = dr[DataBaseFields.RoleName].ToString();
                    uinfo.Roles.Add(roleUser.ConvertToEnum<Role>());
                    uinfo.isFirstLogin = Convert.ToBoolean(dr[DataBaseFields.isFirstLogin].ToString());
                    uinfo.Password = dr[DataBaseFields.Password].ToString();

                    //if User exists then the login time entry is being inserted in the database. (Zahir)

                    int n = DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spInsertLoginTime, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                    {
                        new CommanParameter{Name="@UserName",Type=System.Data.DbType.String,value=uinfo.UserName},
                        new CommanParameter{Name="@LoginTime", Type=System.Data.DbType.DateTime, value=System.DateTime.Now}
                    });

                    SetLoggedInUser(uinfo); // Calling the function to store current user in session. (Zahir)
                    AuthoCookie.CreateAuthoCookie();
                    SessionHijacking.RegenrateSessionId();
                    SessionHijacking.SetSessionHijachingSession();
                    return uinfo;
                }
                else
                {
                    //if user does not exists then the unsuccessfull login entry in being stored in the database. (Zahir)

                    int n = DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spInsertUnsuccessfulLoginDetails, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                    {
                        new CommanParameter{Name="@UserName",Type=System.Data.DbType.String,value=userName},
                        new CommanParameter{Name="@loginDate", Type=System.Data.DbType.DateTime, value=System.DateTime.Now.ToShortDateString()},
                        new CommanParameter{Name="@lock_time", Type=System.Data.DbType.DateTime, value=System.DateTime.Now},
                        new CommanParameter{Name="@isLocked", Type=System.Data.DbType.Byte, value=0}
                    });
                    return null;
                }

            }
            catch
            {
                throw;
            }

        }

        public static UserInfo GetLoggedInUser()
        {
            // Returns the current user logged in. (Zahir)
            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["UserInfo"] != null)
                {
                    return HttpContext.Current.Session["UserInfo"] as UserInfo;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        static void SetLoggedInUser(UserInfo user)
        {
            HttpContext.Current.Session["UserInfo"] = user; // Sets Current User in session. (Zahir)
        }

        public static bool IsLoggedIn
        {
            get
            {
                return (GetLoggedInUser() != null) && AuthoCookie.IsValidAuthoCookie();
            }
        }
        public static List<Role> LoggedInRoles
        {
            get
            {
                UserInfo lInfo = GetLoggedInUser();
                if (lInfo == null)
                {
                    return null;
                }
                else
                {
                    return lInfo.Roles;
                }
            }
        }

        public static void LogOut()
        {
            if (HttpContext.Current.Session["UserInfo"] != null)
            {
                UserInfo uInfo = HttpContext.Current.Session["UserInfo"] as UserInfo;

                // if Session is not null then here we will update the logout time for the user who was currently logged in. (Zahir)

                int n = DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateLogoutTime, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                    {
                        new CommanParameter{Name="@UserName",Type=System.Data.DbType.String,value=uInfo.UserName},
                        new CommanParameter{Name="@LoginOutTime", Type=System.Data.DbType.DateTime, value=System.DateTime.Now}
                    });
            }
            HttpContext.Current.Session["UserInfo"] = null;
            AuthoCookie.ClearAuthoCookie();
            SessionHijacking.RegenrateSessionId();
            SessionHijacking.ClearSession();
            HttpContext.Current.Session.Abandon();
            //HttpContext.Current.Response.Redirect("~/Login.aspx?logout=true");
            HttpContext.Current.Application["currentUser"] = null;
        }

        public static DataSet IsUserLocked(string userName)
        {
            // Used to check and return the data of user who is trying to loggin is locked or not. (Zahir) 

            DataSet ds = DataAccess.Instance.ExecuteDataSet(StoredProcedure.spIsUserLocked, System.Data.CommandType.StoredProcedure,
                     new List<ICommanParameter> 
                {
                    new CommanParameter{ Name="@UserName", Type= System.Data.DbType.String, value= userName},
                    new CommanParameter{ Name="@loginDate", Type= System.Data.DbType.DateTime, value= System.DateTime.Now.ToShortDateString()}
                }
             );
            return ds;
        }
        /// <summary>
        /// Method: IsUserInactive
        /// Description: Used to check and return the data of user who is trying to loggin is Active or Inactive.
        /// Date: 22 Sep, 2015
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static DataSet IsUserInactive(string userName)
        {
            DataSet ds = DataAccess.Instance.ExecuteDataSet(StoredProcedure.spIsUserInactive, System.Data.CommandType.StoredProcedure,
                     new List<ICommanParameter> 
                {
                    new CommanParameter{ Name="@UserName", Type= System.Data.DbType.String, value= userName}
                }
             );
            return ds;
        }

        public static DataSet IsUserExists(string name)
        {
            // Used to check whether user exists when user request for new password from the forgot password page. (Zahir)

            DataSet ds = DataAccess.Instance.ExecuteDataSet(StoredProcedure.spIsUserExists, System.Data.CommandType.StoredProcedure,
                     new List<ICommanParameter> 
                {
                    new CommanParameter{ Name="@UserName", Type= System.Data.DbType.String, value= name}
                }
             );

            return ds;
        }
        public static int DeleteLoginErrorInfo(string userName)
        {
            // Used to delete the user entry from UnsuccessfullLoginTable when User is unlock and allowed to login after the locktime period pass. (Zahir)

            int n = DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spDeleteLoginErrorInfo, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                    {
                        new CommanParameter{Name="@UserName",Type=System.Data.DbType.String,value=userName},
                        new CommanParameter{Name="@loginDate", Type=System.Data.DbType.DateTime, value=System.DateTime.Now.ToShortDateString()},
                        
                    });

            return n;
        }

        public static void UpdateTemporaryPassword(string userName, string pwd, string flag)
        {
            // Used to update password Whenever the new password is send to user and also user change password from create password page. (Zahir)

            DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateTemporaryPassword, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                    {
                        new CommanParameter{Name="@UserName",Type=System.Data.DbType.String,value=userName},
                        new CommanParameter{Name="@Password", Type=System.Data.DbType.String, value=pwd},
                        new CommanParameter{Name="@flag", Type=System.Data.DbType.String, value=flag},
                    });
        }

        public int StoreIpAddress(UserInfo UserIPaddressDetails)
        {
            //DateTime dt = UserIPaddressDetails.LoginDateTime;
            //string s = dt.ToString("yyyy-MM-dd HH:mm:ss");
            //DateTime oDate = DateTime.ParseExact(s, "yyyy-MM-dd HH:mm:ss", null);
            //UserIPaddressDetails.LoginDateTime = oDate;
            string DateTime = "";
            List<ICommanParameter> parameterList = new List<ICommanParameter> {
             new CommanParameter{Name = DataBaseFields.UserName,Type=DbType.String, value=UserIPaddressDetails.UserName },
             new CommanParameter{Name = DataBaseFields.IPAddress,Type=DbType.String, value=UserIPaddressDetails.IPAddress },
             new CommanParameter{Name = DataBaseFields.LoginDateTime,Type=DbType.String, value= UserIPaddressDetails.LoginDateTime!=null?(UserIPaddressDetails.LoginDateTime.ToString("yyyy-MM-dd h:mm:ss tt")):DateTime},
             new CommanParameter{Name = DataBaseFields.LogoutDateTime,Type=DbType.String, value= UserIPaddressDetails.LogOutDateTime!=null?(Convert.ToDateTime(UserIPaddressDetails.LogOutDateTime)).ToString("yyyy-MM-dd h:mm:ss tt"):DateTime},
             new CommanParameter{Name = DataBaseFields.BrowserType,Type=DbType.String, value=UserIPaddressDetails.BrowserType },
             new CommanParameter{Name = DataBaseFields.AuditPageVisited,Type=DbType.String, value=UserIPaddressDetails.AuditPageVisited },
             new CommanParameter{Name = DataBaseFields.AuditPageReferer,Type=DbType.String, value= UserIPaddressDetails.AuditPageReferer},
             new CommanParameter{Name = DataBaseFields.ActionPerformed,Type=DbType.String, value= UserIPaddressDetails.ActionPerformed},
             new CommanParameter{Name = DataBaseFields.ActionDate,Type=DbType.String, value=UserIPaddressDetails.ActionDate.ToString("yyyy-MM-dd h:mm:ss tt")},
             new CommanParameter{Name = DataBaseFields.ModuleName,Type=DbType.String, value=UserIPaddressDetails.ModuleName },
             new CommanParameter{Name = DataBaseFields.LoginStatus,Type=DbType.String, value= UserIPaddressDetails.LoginStatus}       
           };
            return DataAccess.Instance.ExecuteNonQuery(StoredProcedure.InsertAuditTrailInfo, CommandType.StoredProcedure, parameterList);
        }

        public List<UserInfo> GetAuditTrailDetails()
        {
            DataTable dtAuditTrail = DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetAuditTrailInfo, CommandType.StoredProcedure).Tables[0];
            List<UserInfo> auditTrailInfo = new List<UserInfo>();
            if (dtAuditTrail.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAuditTrail.Rows)
                {
                    UserInfo userInfo = new UserInfo
                    {
                        //AuditTrailId= Convert.ToInt64(dr["Id"]),
                        AuditTrailId = Convert.ToInt64(dr["RowNumber"]),
                        UserName = dr["UserName"].GetEmptyOrString(),
                        LoginDateTime = Convert.ToDateTime(dr["LoginDateTime"]),
                        ActionPerformed = dr["ActionPerformed"].GetEmptyOrString(),
                        IPAddress = dr["IPAddress"].GetEmptyOrString(),
                        LoginStatus = dr["LoginStatus"].GetEmptyOrString(),
                        LogOutDateTime = dr["LogOutDateTime"] as DateTime?,
                        ModuleName = dr["ModuleName"].GetEmptyOrString(),
                        ActionDate = Convert.ToDateTime(dr["ActionDatetime"] as DateTime?)
                    };
                    auditTrailInfo.Add(userInfo);
                }
            }
            return auditTrailInfo;
        }
        public List<UserInfo> GetLoginHistory()
        {
            DataTable dtLoginHistory = DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetLoginHistory, CommandType.StoredProcedure).Tables[0];
            List<UserInfo> loginHistory = new List<UserInfo>();
            if (dtLoginHistory.Rows.Count > 0)
            {
                foreach (DataRow dr in dtLoginHistory.Rows)
                {
                    UserInfo userInfo = new UserInfo
                    {
                        //AuditTrailId= Convert.ToInt64(dr["Id"]),
                        AuditTrailId = Convert.ToInt64(dr["RowNumber"]),
                        UserName = dr["UserName"].GetEmptyOrString(),
                        LoginDateTime = Convert.ToDateTime(dr["LoginDateTime"]),
                        IPAddress = dr["IPAddress"].GetEmptyOrString()
                    };
                    loginHistory.Add(userInfo);
                }
            }
            return loginHistory;
        }

        public void InsertUnSuccessfulLoginDetails(string actionType, string Username, string Module = "")
        {
            bool loginStatus = false;
            UserInfo userInfo = new UserInfo { UserName = Username, ModuleName = Module };
            InsertLoginTimeInfo(actionType, loginStatus, userInfo);
            InsertUpdateLoginErrorInfo(userInfo);
        }

        public void InsertLoginTimeInfo(string actionType, bool loginStatus, UserInfo userInfo)
        {
            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            UserInfo UserIPaddressDetails = new UserInfo
            {
                BrowserType = browser.Type,
                AuditPageVisited = HttpContext.Current.Request.Url.AbsoluteUri,
                AuditPageReferer = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"],
                IPAddress = Utility.GetUser_IP(),
                LoginDateTime = userInfo.LoginDateTime,
                LogOutDateTime = userInfo.LogOutDateTime,
                UserName = userInfo.UserName,
                ActionPerformed = actionType,
                ActionDate = DateTime.Now,
                ModuleName = userInfo.Roles.Count != 0 && userInfo.CurrentRole != 0 ? Convert.ToString((Role)userInfo.CurrentRole) : userInfo.ModuleName,
                LoginStatus = (loginStatus) ? "SL" : "UL"
            };
            HttpContext.Current.Session["LoginAudit"] = UserIPaddressDetails;

            StoreIpAddress(UserIPaddressDetails);
        }

        public List<string> GetUserRolesByUserName(string userName)
        {
            List<ICommanParameter> cmd = new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.UserName,value=userName,Type=DbType.String}
            };
            DataSet dsRoles = DataAccess.Instance.ExecuteDataSet(StoredProcedure.CheckUserRoles, CommandType.StoredProcedure, cmd);
            List<string> userRoles = new List<string>();
            if (dsRoles.IsValid())
            {
                foreach (DataRow dr in dsRoles.Tables[0].Rows)
                {
                    userRoles.Add(dr["Role"].ToString());
                }
            }
            return userRoles;
        }

        public void InsertUpdateLoginErrorInfo(UserInfo userInfo)
        {
            List<ICommanParameter> parameterList = new List<ICommanParameter> {
             new CommanParameter{Name = DataBaseFields.UserName,Type=DbType.String, value=userInfo.UserName },
             new CommanParameter{Name = DataBaseFields.Error_Count,Type=DbType.Int32, value= 1 },
             new CommanParameter{Name = DataBaseFields.Lock_Time,Type=DbType.String, value= DateTime.Now},
             new CommanParameter{Name = DataBaseFields.Create_Time,Type=DbType.String, value=DateTime.Now },
             new CommanParameter{Name = DataBaseFields.Update_Time,Type=DbType.String, value=DateTime.Now }
           };
            DataAccess.Instance.ExecuteNonQuery(StoredProcedure.InsertUpdateLoginErrorInfo, CommandType.StoredProcedure, parameterList);
        }

        //public LoginErrorInfo GetLoginErrorInfo(LoginErrorInfo loginErrorInfo)
        //{
        //    List<ICommanParameter> parameterList = new List<ICommanParameter>
        //    {
        //        new CommanParameter{Name = DataBaseFields.UserName,Type=DbType.String, value=loginErrorInfo.UserName }
        //    };
        //    DataSet dtErrorInfo = DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetLoginErrorInfo, CommandType.StoredProcedure, parameterList);
        //    if (dtErrorInfo.Tables[0].Rows.Count > 0)
        //    {
        //        DataRow dr = dtErrorInfo.Tables[0].Rows[0];
        //        LoginErrorInfo loginError = new LoginErrorInfo
        //        {
        //            ErrorCount = Convert.ToInt32(dr["error_times"]),
        //            LockTime = dr["lock_time"] as DateTime?
        //        };
        //        return loginError;
        //    }
        //    return null;
        //}
    }
}
