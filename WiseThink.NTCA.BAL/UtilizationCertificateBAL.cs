using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DataEntity;

namespace WiseThink.NTCA.BAL
{
    public class UtilizationCertificateBAL
    {
         #region Design Pattern
        private static UtilizationCertificateBAL instance;
        private static object syncRoot = new Object();
        private UtilizationCertificateBAL() { }

        public static UtilizationCertificateBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new UtilizationCertificateBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods
        public int UploadFinalUC(int apoFileId, string finalUcName)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUploadFinalUC, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.Int32,value=apoFileId},
                new CommanParameter{Name="@FinnalUcFileName",Type=System.Data.DbType.String,value=finalUcName},
            });
        }
        public DataSet GetFinalUCFileName(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetFinnalUcFileName, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.String,value=apoFileId},
             });
        }
        public DataSet GetUtilizationCertificateDetails(int tigerReserveId, string previousFinancialYear)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetUCDetails, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name="@PreviousFinancialYear",Type=System.Data.DbType.String,value=previousFinancialYear},
             });
        }
        public DataSet GetAPOStateAndTigerReserveId(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetStateAndTigerReserveId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.String,value=apoFileId},
             });
        }

        //public DataSet GetUploadedFileName(string apoFileId)
        //{
        //    return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetExistFileUploadedName, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
        //     {
        //         new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.Int32,value=apoFileId},
        //     });
        //}

        public DataSet GetUploadedFileName(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetExistFileUploadedName, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.String,value=apoFileId},
             });
        }

        #region View Utilization Certificate
        //public DataSet GetViewFeedbackForFD(int tigerReserveId)
        //{
        //    return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spViewFDObligations, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
        //     {
        //        new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
        //     });
        //}
        //public DataSet GetViewFeedbackForCWW(int tigerReserveId, int stateId)
        //{
        //    return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spViewCWWObligations, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
        //     {
        //        new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
        //        new CommanParameter{Name=DataBaseFields.StateId,Type=System.Data.DbType.Int32,value=stateId},
        //     });
        //}
        #endregion
        #endregion
    }
}
