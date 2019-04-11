using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;

namespace WiseThink.NTCA.BAL
{
    public class FeedbackBAL
    {
        #region Design Pattern
        private static FeedbackBAL instance;
        private static object syncRoot = new Object();
        private FeedbackBAL() { }

        public static FeedbackBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new FeedbackBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
         #region Methods

        public DataSet GetTigerReserveIdStateIdForFeedback(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetTigerReserveIdStateId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.Int32,value=apoFileId},
             });
        }
        public DataSet GetAPOStateAndTigerReserveId(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetStateAndTigerReserveId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.String,value=apoFileId},
             });
        }

        public DataSet GetFeedbackFDFormat()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetFDFeedbackFormat, System.Data.CommandType.StoredProcedure);
        }
        public DataSet GetFeedbackCWWFormat()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetCWWFeedbackFormat, System.Data.CommandType.StoredProcedure);
        }
        public int SubmitFeedbackForFD(Feedback feedback)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spSubmitFeedbackForFD, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.ObligationId,Type=System.Data.DbType.Int32,value=feedback.ObligationId},
                 new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=feedback.TigerReserveId},
                 new CommanParameter{Name=DataBaseFields.StateId,Type=System.Data.DbType.Int32,value=feedback.StateId},
                 new CommanParameter{Name=DataBaseFields.CompledOrNotOrNotApplicable,Type=System.Data.DbType.String,value=feedback.CompiledOrNot},
                 new CommanParameter{Name=DataBaseFields.ReasonIfNotCompiled,Type=System.Data.DbType.String,value=feedback.ReasonIfNotCompiled},
                 new CommanParameter{Name=DataBaseFields.Score,Type=System.Data.DbType.Int32,value=feedback.Score},
                 new CommanParameter{Name=DataBaseFields.ComplianceprocessUnderway,Type=System.Data.DbType.String,value=feedback.ComplianceProcess},
                 new CommanParameter{Name=DataBaseFields.Remarks,Type=System.Data.DbType.String,value=feedback.Remarks},
            });
        }
        public int SubmitFeedbackForCWW(Feedback feedback)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spSubmitFeedbackForCWW, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.ObligationId,Type=System.Data.DbType.Int32,value=feedback.ObligationId},
                 new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=feedback.TigerReserveId},
                 new CommanParameter{Name=DataBaseFields.StateId,Type=System.Data.DbType.Int32,value=feedback.StateId},
                 new CommanParameter{Name=DataBaseFields.CompledOrNotOrNotApplicable,Type=System.Data.DbType.String,value=feedback.CompiledOrNot},
                 new CommanParameter{Name=DataBaseFields.ReasonIfNotCompiled,Type=System.Data.DbType.String,value=feedback.ReasonIfNotCompiled},
                 new CommanParameter{Name=DataBaseFields.Score,Type=System.Data.DbType.Int32,value=feedback.Score},
                 new CommanParameter{Name=DataBaseFields.ComplianceprocessUnderway,Type=System.Data.DbType.String,value=feedback.ComplianceProcess},
                 new CommanParameter{Name=DataBaseFields.Remarks,Type=System.Data.DbType.String,value=feedback.Remarks},
            });
        }
        public DataSet GetFieldDirectorFeedback(int tigerReserveId, int stateId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetFDFeedback, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.StateId,Type=System.Data.DbType.Int32,value=stateId},
             });
        }
        public DataSet GetCWWFeedback(int tigerReserveId, int stateId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetCWWFeedback, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                 new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.StateId,Type=System.Data.DbType.Int32,value=stateId},
             });
        }
        public int UpdateFieldDirectorFeedback(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateFDObligationstatus, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
            });
        }
        public int UpdateCWWFeedback(int tigerReserveId)
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
       
        #region View Feedback
        public DataSet GetViewFeedbackForFD(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spViewFDObligations, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        public DataSet GetViewFeedbackForCWW(int tigerReserveId, int stateId)
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
