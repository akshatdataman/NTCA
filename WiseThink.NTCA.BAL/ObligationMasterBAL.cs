using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DataEntity;

namespace WiseThink.NTCA.BAL
{
    public class ObligationMasterBAL
    {
         #region Design Pattern
        private static ObligationMasterBAL instance;
        private static object syncRoot = new Object();
        private ObligationMasterBAL() { }

        public static ObligationMasterBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new ObligationMasterBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods
        public DataSet GetObligationFor()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetObligationFor, System.Data.CommandType.StoredProcedure);
        }
        public int AddObligation(int roleId, string obligation)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spAddObligation, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name="@RoleId",Type=System.Data.DbType.String,value=roleId},
                new CommanParameter{Name=DataBaseFields.Descriptions,Type=System.Data.DbType.String,value=obligation},
            });
        }
        public int UpdateObligation(int obligationId, string obligation)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spEditObligation, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.ObligationId,Type=System.Data.DbType.String,value=obligationId},
                new CommanParameter{Name=DataBaseFields.Descriptions,Type=System.Data.DbType.String,value=obligation},
            });
        }
        public int DeleteObligation(int obligationId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spDeleteObligation, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.ObligationId,Type=System.Data.DbType.String,value=obligationId},
            });
        }
        public DataSet GetObligations(int roleId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetObligationMaster, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name="@RoleId",Type=System.Data.DbType.String,value=roleId},
            });
        }
         #endregion
    }
}
