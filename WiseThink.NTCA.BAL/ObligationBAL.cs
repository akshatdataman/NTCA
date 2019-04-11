using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;

namespace WiseThink.NTCA.BAL
{
    public class ObligationBAL
    {
        #region Design Pattern
        private static ObligationBAL instance;
        private static object syncRoot = new Object();
        private ObligationBAL() { }

        public static ObligationBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new ObligationBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods

        public DataSet GetLoggedInUserTigerReserveId(string loggedInUser)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetLoggedInUserTigerReserveId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.LoggedInUser,Type=System.Data.DbType.String,value=loggedInUser},
             });
        }
        public DataSet GetAPOStateAndTigerReserveId(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetStateAndTigerReserveId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.String,value=apoFileId},
             });
        }
        public DataSet GetObligationFDFormat()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetFDObligationFormat, System.Data.CommandType.StoredProcedure);
        }
        public DataSet GetObligationCWWFormat()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetCWWObligationFormat, System.Data.CommandType.StoredProcedure);
        }
        public int SubmitObligationsForFD(Obligations obligations)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spSubmitObligationsForFD, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.ObligationId,Type=System.Data.DbType.Int32,value=obligations.ObligationId},
                 new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=obligations.TigerReserveId},
                 new CommanParameter{Name=DataBaseFields.CompledOrNotOrNotApplicable,Type=System.Data.DbType.String,value=obligations.CompiledOrNot},
                 new CommanParameter{Name=DataBaseFields.LevelOfCompliance,Type=System.Data.DbType.Int32,value=obligations.ComplianceLevel},
                 new CommanParameter{Name=DataBaseFields.ReasonIfNotCompiled,Type=System.Data.DbType.String,value=obligations.Reason},
            });
        }
        public int SubmitObligationsForCWW(Obligations obligations)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spSubmitObligationsForCWW, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.ObligationId,Type=System.Data.DbType.Int32,value=obligations.ObligationId},
                 new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=obligations.TigerReserveId},
                 new CommanParameter{Name=DataBaseFields.StateId,Type=System.Data.DbType.Int32,value=obligations.StateId},
                 new CommanParameter{Name=DataBaseFields.CompledOrNotOrNotApplicable,Type=System.Data.DbType.String,value=obligations.CompiledOrNot},
                 new CommanParameter{Name=DataBaseFields.LevelOfCompliance,Type=System.Data.DbType.Int32,value=obligations.ComplianceLevel},
                 new CommanParameter{Name=DataBaseFields.ReasonIfNotCompiled,Type=System.Data.DbType.String,value=obligations.Reason},
            });
        }
        public DataSet GetFieldDirectorObligations(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetFDObligations, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        public DataSet GetCWWObligations(int tigerReserveId, int stateId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetCWWObligations, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                 new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.StateId,Type=System.Data.DbType.Int32,value=stateId},
             });
        }
        public int UpdateFieldDirectorObligations(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateFDObligationstatus, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
            });
        }
        public int UpdateCWWObligations(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateCWWObligationstatus, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
            });
        }
        public DataSet UpdateAPO(string inputJSON)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spUpdateAPO, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.inputJSON,Type=System.Data.DbType.String,value=inputJSON},
             });
        }
        public DataSet GetStateId(string userName)
        {

            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetUserStateId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.LoggedInUser,Type=System.Data.DbType.String,value=userName},
             });
        }
       
        #region Obligation View
        public DataSet GetObligationViewForFD(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spViewFDObligations, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        public DataSet GetObligationViewForCWW(int tigerReserveId, int stateId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spViewCWWObligations, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.StateId,Type=System.Data.DbType.Int32,value=stateId},
             });
        }
        #endregion
        #endregion
    }
}
