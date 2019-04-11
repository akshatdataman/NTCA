using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;

namespace WiseThink.NTCA.BAL
{
    public class CentralStateShareBAL
    {
         #region Design Pattern
        private static CentralStateShareBAL instance;
        private static object syncRoot = new Object();
        private CentralStateShareBAL() { }

        public static CentralStateShareBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new CentralStateShareBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods

        public DataSet GetCentralStateShare(int tigerReserveId, string previousFinancialYear)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetCentralStateShare, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.PreviousFinancialYear,Type=System.Data.DbType.String,value=previousFinancialYear},
             });
        }
        public DataSet GetCentralStateFundRelease()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spCentralStateFundReleaseDetails, System.Data.CommandType.StoredProcedure);
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
        public int UpdateCentralFirstReleasedAmount(CentralStateShare centralStateShare)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateCentralFirstReleased, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.Int32,value=centralStateShare.APOId},
                 new CommanParameter{Name=DataBaseFields.Quantity,Type=System.Data.DbType.Decimal,value=centralStateShare.Quantity},
                 new CommanParameter{Name=DataBaseFields.SanctionedAmount,Type=System.Data.DbType.Decimal,value=centralStateShare.CFYSanctionAmount},
                 new CommanParameter{Name=DataBaseFields.UnspentAdjustedAmount,Type=System.Data.DbType.Decimal,value=centralStateShare.UnspentAdjustedAmount},
                 new CommanParameter{Name=DataBaseFields.CentralShare,Type=System.Data.DbType.Decimal,value=centralStateShare.CentralShare},
                 new CommanParameter{Name=DataBaseFields.StateShare,Type=System.Data.DbType.Decimal,value=centralStateShare.StateShare},
                 new CommanParameter{Name=DataBaseFields.FirstCentralRelease,Type=System.Data.DbType.Decimal,value=centralStateShare.FirstCentralRelease},
                 new CommanParameter{Name=DataBaseFields.IFDDiaryNumber,Type=System.Data.DbType.String,value=centralStateShare.IFDDiaryNumber},
                 new CommanParameter{Name=DataBaseFields.IFDDate,Type=System.Data.DbType.DateTime,value=centralStateShare.IFDDate},
            });

        }
        public int UpdateCentralSecondReleasedAmount(CentralStateShare centralStateShare)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateCentralSecondReleased, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.Int32,value=centralStateShare.APOId},
                 new CommanParameter{Name=DataBaseFields.SecondCentralRelease,Type=System.Data.DbType.Decimal,value=centralStateShare.SecondCentralRelease},
            });

        }
        public int UpdateStateFirstReleasedAmount(CentralStateShare centralStateShare)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateStateFirstReleased, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.Int32,value=centralStateShare.APOId},
                 new CommanParameter{Name=DataBaseFields.FirstStateRelease,Type=System.Data.DbType.Decimal,value=centralStateShare.FirstStateRelease},
            });

        }
        public int UpdateStateSecondReleasedAmount(CentralStateShare centralStateShare)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateStateSecondReleased, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.Int32,value=centralStateShare.APOId},
                 new CommanParameter{Name=DataBaseFields.SecondStateRelease,Type=System.Data.DbType.Decimal,value=centralStateShare.SecondStateRelease},
            });

        }
        
        #endregion
    }
}
