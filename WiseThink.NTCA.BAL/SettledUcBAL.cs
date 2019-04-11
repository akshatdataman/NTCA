using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;

namespace WiseThink.NTCA.BAL
{
    public class SettledUcBAL
    {
        #region Design Pattern
        private static SettledUcBAL instance;
        private static object syncRoot = new Object();
        private SettledUcBAL() { }

        public static SettledUcBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new SettledUcBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods

        public DataSet GetUtilizationCertificateDetails(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetUCDetails, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        public DataSet GetAPOStateAndTigerReserveId(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetStateAndTigerReserveId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.String,value=apoFileId},
             });
        }
        public DataSet GetGetUnspentActivitiesList(int tigerReserveId, string previousFinancialYear)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetUnspentActivitiesList, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
                 {
                    new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                    new CommanParameter{Name="@PreviousFinancialYear",Type=System.Data.DbType.String,value=previousFinancialYear},
                 });
        }
        public int SettledPFYUnspentAmount(APO apo)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spSettledUnspent, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
             new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=apo.TigerReserveId},
             new CommanParameter{Name=DataBaseFields.IsSettledUnspent,Type=System.Data.DbType.Boolean,value=apo.IsSettledUnspent},
             new CommanParameter{Name=DataBaseFields.AreaId,Type=System.Data.DbType.Int32,value=apo.AreaId},
             new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.Int32,value=apo.ActivityTypeId},
             new CommanParameter{Name=DataBaseFields.ActivityId,Type=System.Data.DbType.Int32,value=apo.ActivityId},
             new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.Int32,value=apo.ActivityItemId},
             new CommanParameter{Name=DataBaseFields.FinancialYear,Type=System.Data.DbType.String,value=apo.FinancialYear},
             new CommanParameter{Name=DataBaseFields.ParaNoCSSPTGuidelines,Type=System.Data.DbType.String,value=apo.ParaNoCSSPTGuidelines},
             new CommanParameter{Name=DataBaseFields.ParaNoTCP,Type=System.Data.DbType.String,value=apo.ParaNoTCP},
             new CommanParameter{Name=DataBaseFields.NumberOfItems,Type=System.Data.DbType.Decimal,value=apo.NumberOfItems},
             new CommanParameter{Name=DataBaseFields.Specification,Type=System.Data.DbType.String,value=apo.Specification},
             new CommanParameter{Name=DataBaseFields.UnitPrice,Type=System.Data.DbType.Decimal,value=apo.UnitPrice},
             new CommanParameter{Name=DataBaseFields.Total,Type=System.Data.DbType.Decimal,value=apo.Total},
             new CommanParameter{Name=DataBaseFields.GPS,Type=System.Data.DbType.Decimal,value=apo.GPS},
             new CommanParameter{Name=DataBaseFields.Justification,Type=System.Data.DbType.String,value=apo.Justification},
             new CommanParameter{Name=DataBaseFields.Document,Type=System.Data.DbType.String,value=apo.Document},
             new CommanParameter{Name=DataBaseFields.Unspent,Type=System.Data.DbType.Decimal,value=apo.Unspent},
             new CommanParameter{Name=DataBaseFields.IsReValidationOrAdjustment,Type=System.Data.DbType.Boolean,value=apo.IsReValidate},
             new CommanParameter{Name=DataBaseFields.IsAdjustmentOrSpillOverAdjustment,Type=System.Data.DbType.Boolean,value=apo.IsSpillOverAdjustment},
            });

        }
        #region View Settled Unspent Amount 
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
