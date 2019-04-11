using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.DataEntity;
using System.Data;
using WiseThink.NTCA.BAL.Authorization;
using System.Web;

namespace WiseThink.NTCA.BAL
{
    public class UserBAL
    {
        #region Design Pattern
        
        private static UserBAL instance;
        private static object syncRoot = new Object();
        private UserBAL() { }

        public static UserBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new UserBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
        #region Methods

        /// <summary>
        /// Genarate randam number for captcha image
        /// </summary>
        /// <returns></returns>
        public string GenerateRandomCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
        }

        /// <summary>
        /// To create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>user object</returns>
        public int CreateUser(User user)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddUser, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.UserName,Type=System.Data.DbType.String,value=user.UserName},
             new CommanParameter{Name=DataBaseFields.Password,Type=System.Data.DbType.String,value=user.Password},
             new CommanParameter{Name=DataBaseFields.Title,Type=System.Data.DbType.String,value=user.Title},
             new CommanParameter{Name=DataBaseFields.FirstName,Type=System.Data.DbType.String,value=user.FirstName},
             new CommanParameter{Name=DataBaseFields.MiddleName,Type=System.Data.DbType.String,value=user.MiddleName},
             new CommanParameter{Name=DataBaseFields.LastName,Type=System.Data.DbType.String,value=user.LastName},
             new CommanParameter{Name=DataBaseFields.DesignationId,Type=System.Data.DbType.Int32,value=user.Designation},
             //new CommanParameter{Name="@DateofBirth",Type=System.Data.DbType.DateTime,value=user.DateOfBirth},
             new CommanParameter{Name=DataBaseFields.Gender,Type=System.Data.DbType.String,value=user.Gender},
             new CommanParameter{Name=DataBaseFields.Role,Type=System.Data.DbType.String,value=user.Role},
             new CommanParameter{Name=DataBaseFields.Address,Type=System.Data.DbType.String,value=user.Address},
             new CommanParameter{Name=DataBaseFields.PinCode,Type=System.Data.DbType.String,value=user.PinCode},
             new CommanParameter{Name=DataBaseFields.City,Type=System.Data.DbType.String,value=user.City},
             new CommanParameter{Name=DataBaseFields.District,Type=System.Data.DbType.String,value=user.District},
             new CommanParameter{Name=DataBaseFields.State,Type=System.Data.DbType.String,value=user.State},
             new CommanParameter{Name=DataBaseFields.Country,Type=System.Data.DbType.String,value=user.Country},
             new CommanParameter{Name=DataBaseFields.PhoneNumber,Type=System.Data.DbType.String,value=user.PhoneNumber},
             new CommanParameter{Name=DataBaseFields.MobileNumber,Type=System.Data.DbType.String,value=user.MobileNumber},
             new CommanParameter{Name=DataBaseFields.Email,Type=System.Data.DbType.String,value=user.Email},
             new CommanParameter{Name=DataBaseFields.FaxNumber,Type=System.Data.DbType.String,value=user.FaxNumber},
             new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=user.TigerReserveId},
             new CommanParameter{Name=DataBaseFields.IsActive,Type=System.Data.DbType.Boolean,value=user.IsActive},
            });

        }

        /// <summary>
        /// Update the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="_userID"></param>
        /// <returns></returns>
        public int UpdateUser(User user, int _userID)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spEditUser, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name="@UserId",Type=System.Data.DbType.String,value=_userID},
             new CommanParameter{Name=DataBaseFields.UserName,Type=System.Data.DbType.String,value=user.UserName},
             //new CommanParameter{Name=DataBaseFields.Password,Type=System.Data.DbType.String,value=user.Password},
             new CommanParameter{Name=DataBaseFields.Title,Type=System.Data.DbType.String,value=user.Title},
             new CommanParameter{Name=DataBaseFields.FirstName,Type=System.Data.DbType.String,value=user.FirstName},
             new CommanParameter{Name=DataBaseFields.MiddleName,Type=System.Data.DbType.String,value=user.MiddleName},
             new CommanParameter{Name=DataBaseFields.LastName,Type=System.Data.DbType.String,value=user.LastName},
             new CommanParameter{Name=DataBaseFields.DesignationId,Type=System.Data.DbType.Int32,value=user.Designation},
             //new CommanParameter{Name="@DateofBirth",Type=System.Data.DbType.DateTime,value=user.DateOfBirth},
             new CommanParameter{Name=DataBaseFields.Gender,Type=System.Data.DbType.String,value=user.Gender},
             new CommanParameter{Name=DataBaseFields.Role,Type=System.Data.DbType.String,value=user.Role},
             new CommanParameter{Name=DataBaseFields.Address,Type=System.Data.DbType.String,value=user.Address},
             new CommanParameter{Name=DataBaseFields.PinCode,Type=System.Data.DbType.String,value=user.PinCode},
             new CommanParameter{Name=DataBaseFields.City,Type=System.Data.DbType.String,value=user.City},
             new CommanParameter{Name=DataBaseFields.District,Type=System.Data.DbType.String,value=user.District},
             new CommanParameter{Name=DataBaseFields.State,Type=System.Data.DbType.String,value=user.State},
             new CommanParameter{Name=DataBaseFields.Country,Type=System.Data.DbType.String,value=user.Country},
             new CommanParameter{Name=DataBaseFields.PhoneNumber,Type=System.Data.DbType.String,value=user.PhoneNumber},
             new CommanParameter{Name=DataBaseFields.MobileNumber,Type=System.Data.DbType.String,value=user.MobileNumber},
             new CommanParameter{Name=DataBaseFields.Email,Type=System.Data.DbType.String,value=user.Email},
             new CommanParameter{Name=DataBaseFields.FaxNumber,Type=System.Data.DbType.String,value=user.FaxNumber},
             new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=user.TigerReserveId},
             new CommanParameter{Name=DataBaseFields.IsActive,Type=System.Data.DbType.Boolean,value=user.IsActive},
            });

        }
        public int EditProfile(User user, int _userID)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spEditProfile, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name="@UserId",Type=System.Data.DbType.String,value=_userID},
             new CommanParameter{Name=DataBaseFields.UserName,Type=System.Data.DbType.String,value=user.UserName},
             //new CommanParameter{Name=DataBaseFields.Password,Type=System.Data.DbType.String,value=user.Password},
             new CommanParameter{Name=DataBaseFields.Title,Type=System.Data.DbType.String,value=user.Title},
             new CommanParameter{Name=DataBaseFields.FirstName,Type=System.Data.DbType.String,value=user.FirstName},
             new CommanParameter{Name=DataBaseFields.MiddleName,Type=System.Data.DbType.String,value=user.MiddleName},
             new CommanParameter{Name=DataBaseFields.LastName,Type=System.Data.DbType.String,value=user.LastName},
             new CommanParameter{Name=DataBaseFields.DesignationId,Type=System.Data.DbType.Int32,value=user.Designation},
             //new CommanParameter{Name="@DateofBirth",Type=System.Data.DbType.DateTime,value=user.DateOfBirth},
             new CommanParameter{Name=DataBaseFields.Gender,Type=System.Data.DbType.String,value=user.Gender},
             new CommanParameter{Name=DataBaseFields.Role,Type=System.Data.DbType.String,value=user.Role},
             new CommanParameter{Name=DataBaseFields.Address,Type=System.Data.DbType.String,value= user.Address},
             new CommanParameter{Name=DataBaseFields.PinCode,Type=System.Data.DbType.String,value=user.PinCode},
             new CommanParameter{Name=DataBaseFields.City,Type=System.Data.DbType.String,value=user.City},
             new CommanParameter{Name=DataBaseFields.District,Type=System.Data.DbType.String,value=user.District},
             new CommanParameter{Name=DataBaseFields.State,Type=System.Data.DbType.String,value=user.State},
             new CommanParameter{Name=DataBaseFields.Country,Type=System.Data.DbType.String,value=user.Country},
             new CommanParameter{Name=DataBaseFields.PhoneNumber,Type=System.Data.DbType.String,value=user.PhoneNumber},
             new CommanParameter{Name=DataBaseFields.MobileNumber,Type=System.Data.DbType.String,value=user.MobileNumber},
             new CommanParameter{Name=DataBaseFields.Email,Type=System.Data.DbType.String,value=user.Email},
             new CommanParameter{Name=DataBaseFields.FaxNumber,Type=System.Data.DbType.String,value=user.FaxNumber},
            });

        }

        /// <summary>
        /// Get the list of users
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserList()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetUserList, System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Get the user details
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserDetails(int _userID)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetUserDetails, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {new CommanParameter{Name="@UserId",Type=System.Data.DbType.String,value=_userID}, });
        }
        /// <summary>
        /// Get the last registered user.
        /// </summary>
        /// <returns></returns>
        public DataSet GetLastUser()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetLastUser, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// Get user master data to bind all the dropdowns in user module.
        /// </summary>
        /// <returns></returns>
        public DataSet GetMasterDataForUserModule()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetUserMasterData, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// Get the Districts based on stateId
        /// </summary>
        /// <param name="_stateId"></param>
        /// <returns></returns>
        public DataSet GetDistrictBasedOnState(int _stateId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetDistrictBasedOnState, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {new CommanParameter{Name="@StateId",Type=System.Data.DbType.String,value=_stateId}, });
        }
        /// <summary>
        /// Get Old user password
        /// </summary>
        /// <param name="_userName"></param>
        /// <returns></returns>
        public DataSet GetOldPassword(string _userName)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetCurrentPassword, CommandType.StoredProcedure, new List<ICommanParameter> { new CommanParameter { Name = DataBaseFields.UserName, Type = System.Data.DbType.String, value = _userName }, });
        }
        public int GetLoggedInUserId(string _userName)
        {
            return Convert.ToInt32(DAL.DataAccess.Instance.ExecuteScalar(StoredProcedure.spGetLoggedInUserId, CommandType.StoredProcedure, new List<ICommanParameter> { new CommanParameter { Name = DataBaseFields.UserName, Type = System.Data.DbType.String, value = _userName }, }));
        }
        public DataSet GetTigerReserveState(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetTigerReserveState, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        public DataSet GetAllNTCAUsersEmail()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAllNTCAUsersEmail, System.Data.CommandType.StoredProcedure);
        }
        public DataSet GetAllNTCAUsersEmail(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAllNTCAANDCWLWEmail, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        public void InsertAuditTrailDetail(string actionType, string moduleName)
        {
            AuthoProvider authoProvider = new AuthoProvider();
            UserInfo userInfo = new UserInfo();
            if (HttpContext.Current.Session["LoginDateTime"] == null)
                userInfo.LoginDateTime = DateTime.Now;
            else
                userInfo.LoginDateTime = Convert.ToDateTime(HttpContext.Current.Session["LoginDateTime"]);
            if (actionType == "Logout")
                userInfo.LogOutDateTime = DateTime.Now;
            userInfo.UserName = AuthoProvider.User;
            userInfo.ModuleName = moduleName;
            authoProvider.InsertLoginTimeInfo(actionType, true, userInfo);
        }
        #endregion
    }
}
