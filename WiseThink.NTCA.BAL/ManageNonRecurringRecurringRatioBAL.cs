using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiseThink.NTCA.DataEntity;
using System.Data;

namespace WiseThink.NTCA.BAL
{
    public class ManageNonRecurringRecurringRatioBAL
    {
         #region Design Pattern

        private static ManageNonRecurringRecurringRatioBAL instance;
        private static object syncRoot = new Object();
        private ManageNonRecurringRecurringRatioBAL() { }

        public static ManageNonRecurringRecurringRatioBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new ManageNonRecurringRecurringRatioBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
        #region Method
        public int UpdateInstallments(int tigerReserveId, double firstInstallment, double secondInstallment)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spEditInstallments, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.FirstInstallment,Type=System.Data.DbType.Double,value=firstInstallment},
                new CommanParameter{Name=DataBaseFields.SecondInstallment,Type=System.Data.DbType.Double,value=secondInstallment},
            });
        }
        public DataSet GetInstallments(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetInstallments, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                 });
        }

        public DataSet GetNonRecurringAndRecurringRatio(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetNonRecurringAndRecurringRatio, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        public int UpdateRecuringAndNonReecurringRatio(int _tigerReserveID, int centralNonRecurringRatio, int centralRecurringRatio, int stateNonRecurringRatio, int stateRecurringRatio)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateNonRecurringAndRecurringRatio, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=_tigerReserveID},
                new CommanParameter{Name=DataBaseFields.CentralRecurringRatio,Type=System.Data.DbType.Int32,value=centralRecurringRatio},
                new CommanParameter{Name=DataBaseFields.CentralNonRecurringRatio,Type=System.Data.DbType.Int32,value=centralNonRecurringRatio},
                new CommanParameter{Name=DataBaseFields.StateNonRecurringRatio,Type=System.Data.DbType.Int32,value=stateNonRecurringRatio},
                new CommanParameter{Name=DataBaseFields.StateRecurringRatio,Type=System.Data.DbType.Int32,value=stateRecurringRatio},
            });

        }
        #endregion 
    }
}
