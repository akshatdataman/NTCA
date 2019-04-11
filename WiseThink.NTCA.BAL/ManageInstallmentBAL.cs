using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiseThink.NTCA.DataEntity;
using System.Data;

namespace WiseThink.NTCA.BAL
{
    public class ManageInstallmentBAL
    {
        #region Design Pattern

        private static ManageInstallmentBAL instance;
        private static object syncRoot = new Object();
        private ManageInstallmentBAL() { }

        public static ManageInstallmentBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new ManageInstallmentBAL();
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
            //return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetInstallments, System.Data.CommandType.StoredProcedure);
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetInstallments, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                 });
        }
        #endregion 
    }
}
