using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.DataEntity;
using System.Data;

namespace WiseThink.NTCA.BAL
{
    public class AlertBAL
    {
         #region Design Pattern
        private static AlertBAL instance;
        private static object syncRoot = new Object();
        private AlertBAL() { }

        public static AlertBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new AlertBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods
        public int SendAlerts(Alert alert)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spSendAlert, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
              new CommanParameter{Name=DataBaseFields.UserId,Type=System.Data.DbType.Int32,value=alert.UserId},
              new CommanParameter{Name=DataBaseFields.SentTo,Type=System.Data.DbType.String,value=alert.SentTo},
              new CommanParameter{Name=DataBaseFields.Subject,Type=System.Data.DbType.String,value=alert.Subject},             
              new CommanParameter{Name=DataBaseFields.Body,Type=System.Data.DbType.String,value=alert.Body},
              new CommanParameter{Name=DataBaseFields.APOId,Type=System.Data.DbType.String,value=alert.APOId},
             new CommanParameter{Name=DataBaseFields.LoggedInUser,Type=System.Data.DbType.String,value=alert.LoggedInUser},
            });
        }
        public DataSet GetAlerts(int loggedInUserId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAlerts, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.UserId,Type=System.Data.DbType.Int32,value=loggedInUserId},
             });
        }
        public DataSet GetEmailAddress(int userId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetEmailAddress, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.UserId,Type=System.Data.DbType.String,value=userId},
             });
        }
        public DataSet GetMultipleEmailAddress(string userIds)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetMultipleEmailAddress, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.UserIds,Type=System.Data.DbType.String,value=userIds},
             });
        }
        public DataSet GetUserNameList()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetUserNameList, System.Data.CommandType.StoredProcedure);
        }
         #endregion
    }
}
