using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DAL;
using WiseThink.NTCA.DataEntity;

namespace WiseThink.NTCA.BAL.Reports
{
    //ReportsBAL class created By indu on 02 Dec 2014
    public class ReportsBAL
    {
        #region Design Pattern
        private static ReportsBAL instance;
        private static object syncRoot = new Object();
        private ReportsBAL() { }

        public static ReportsBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new ReportsBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Evaluation Due Report
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="moreCondition"></param>
        /// <returns></returns>
        public DataSet GetEvaluationDueReport(string conditions, string moreCondition = null)
        {
            string finalCondition = null;
            string addFilterCondition1 = " and T1.MaxDate < GETDATE()";
            string addFilterCondition2 = " or (T1.DateOfEvaluation is null and T1.AppId <= 1354)";
            if (string.IsNullOrEmpty(conditions))
            {
                finalCondition = "((T1.StatusId=" + (int)Status.PendingForEvaluation + ") or (T1.StatusId=" + (int)Status.AssignedToEvaluator + addFilterCondition1 + "))" + addFilterCondition2;
            }
            else if (conditions.Equals("Yes"))
            {
                finalCondition = "T1.StatusId=" + (int)Status.PendingForEvaluation + addFilterCondition2;
            }
            else
            {
                finalCondition = "T1.StatusId=" + (int)Status.AssignedToEvaluator + addFilterCondition1;
            }
           // List<CommanParameter> parameterList = new List<CommanParameter> {              
           //      new CommanParameter{Name = DataBaseFields.FILTER_VALUE ,Type=DbType.String, value=finalCondition} ,
           //       new CommanParameter{Name = DataBaseFields.EXTRAFILTER_VALUE ,Type=DbType.String, value=moreCondition} 
           //};
           // return DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetEvaluationDueReport, CommandType.StoredProcedure, parameterList);
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetEvaluationDueReport, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
                 new CommanParameter{Name = DataBaseFields.FILTER_VALUE ,Type=DbType.String, value=finalCondition} ,
                  new CommanParameter{Name = DataBaseFields.EXTRAFILTER_VALUE ,Type=DbType.String, value=moreCondition}, });
        }
        public string GetTempAssignedEvaluatorNameByZooID(int zooID, bool isMidTerm = false)
        {
            DataSet dsName = isMidTerm == true ? GetMidtermAssignedEvaluators(zooID) : GetAssignedByZooID(zooID);
            string strName = "";
            if (dsName.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drName in dsName.Tables[0].Rows)
                {
                    strName += drName[DataBaseFields.EVALUATOR_NAME].ToString() + ", ";
                }
                if (!string.IsNullOrEmpty(strName))
                {
                    strName = strName.Remove(strName.Length - 1, 1);
                }
            }
            return strName;
        }
        public DataSet GetMidtermAssignedEvaluators(int ZooId)
        {
           // List<CommanParameter> parameterList = new List<CommanParameter> {
           //  new CommanParameter{Name = DataBaseFields.ZOO_ID,Type=DbType.Int32, value=ZooId} 
           //};
           // string cmd = @"select Distinct EvaluatorName from ZooEvaluators ZE inner join EvaluatorInfo EI on EI.EvaluatorID=ZE.EvaluatorId where ZE.ZooId=@" + DataBaseFields.ZOO_ID;
           // return DataAccess.Instance.ExecuteDataSet(cmd, CommandType.Text, parameterList);

            string cmd = @"select Distinct EvaluatorName from ZooEvaluators ZE inner join EvaluatorInfo EI on EI.EvaluatorID=ZE.EvaluatorId where ZE.ZooId=@" + DataBaseFields.ZOO_ID;
            return DAL.DataAccess.Instance.ExecuteDataSet(cmd, System.Data.CommandType.Text, new List<ICommanParameter> { 
               new CommanParameter{Name = DataBaseFields.ZOO_ID,Type=DbType.Int32, value=ZooId}, });
        }
        public DataSet GetAssignedByZooID(int zooID)
        {
           // List<CommanParameter> parameterList = new List<CommanParameter> {
           //  new CommanParameter{Name = DataBaseFields.ZOO_ID,Type=DbType.Int32, value=zooID}, 
           //};
           // return DataAccess.Instance.ExecuteDataSet(StoredProcedure.EvaluatorGetAssignedByZooID, CommandType.StoredProcedure, parameterList);

            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.EvaluatorGetAssignedByZooID, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
               new CommanParameter{Name = DataBaseFields.ZOO_ID,Type=DbType.Int32, value=zooID}, });
        }
        public string GetAssignedEvaluatorNameByAppID(int appID)
        {
            DataSet dsName = GetAssignedByAppID(appID);
            string strName = "";
            if (dsName.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drName in dsName.Tables[0].Rows)
                {
                    if (!drName[DataBaseFields.EVALUATOR_NAME].IsDBNull())
                    {
                        strName += drName[DataBaseFields.EVALUATOR_NAME].ToString() + ", ";
                    }
                    //strName =strName+","+ drName[DataEntity.DataBaseFields.EVALUATOR_NAME].ToString();
                }
                if (!string.IsNullOrEmpty(strName))
                {
                    strName = strName.Remove(strName.Length - 1, 1);
                }
            }
            return strName;
        }
        public DataSet GetAssignedByAppID(int appID)
        {
           // List<CommanParameter> parameterList = new List<CommanParameter> {
           //  new CommanParameter{Name = DataBaseFields.APPLICATION_ID,Type=DbType.Int32, value=appID}, 
           //};
           // return DataAccess.Instance.ExecuteDataSet(StoredProcedure.EvaluatorGetAssignedByAppID, CommandType.StoredProcedure, parameterList);

            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.EvaluatorGetAssignedByAppID, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
                new CommanParameter{Name = DataBaseFields.APPLICATION_ID,Type=DbType.Int32, value=appID}, });
        }
        /// <summary>
        /// Evaluation Done for Zoos Report
        /// </summary>
        /// <param name="Filter"></param>
        /// <returns></returns>
        public DataSet GetEvaluationReportZooForZoo(string Filter = null)
        {
           // var parameterList = new List<CommanParameter> {
           //  new CommanParameter{Name = "Filter",Type=DbType.String, value=Filter}              
           //};
           // return DataAccess.Instance.ExecuteDataSet("Proc_Recognition_EvaluationDoneReport", CommandType.StoredProcedure, parameterList);

            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.Proc_Recognition_EvaluationDoneReport, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
               new CommanParameter{Name = "Filter",Type=DbType.String, value=Filter}, });
        }

        //Imported from cza EvaluationBAL
        /// <summary>
        /// Evaluators Details Report
        /// </summary>
        /// <returns></returns>
        public DataSet GetAll()
        {
            string commandText = @"Select distinct [EvaluatorID],[EvaluatorName],[Designation],[EvaluatorMobileNo],
                                    [EvaluatorUsername],EmailID,EvaluatorInfo.[IsActive] from [dbo].[EvaluatorInfo] 
                                    left join UserLogin on UserLogin.UserName=EvaluatorInfo.EvaluatorUsername
                                    order by [EvaluatorName]";
            return DataAccess.Instance.ExecuteDataSet(commandText);
            //return DataAccess.Instance.ExecuteDataSet(DataEntity.StoredProcedure.EvaluationPanelGetAll, CommandType.StoredProcedure);
        }
        /// <summary>
        /// Recognition Renewal Due Report
        /// </summary>
        /// <param name="Conditions"></param>
        /// <returns></returns>
        public DataSet GetAllDataForRenewalofRecog(string Conditions = null)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetDataForRenewal, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
                new CommanParameter{Name = DataBaseFields.FILTER_VALUE,Type=DbType.String, value=Conditions},                 
                 new CommanParameter{Name=DataBaseFields.STATUS_ID,Type=DbType.Int32,value=(int)Status.Recognized}, });
        }
        /// <summary>
        /// Recognition Done Report
        /// </summary>
        /// <param name="Filter"></param>
        /// <returns></returns>
        public DataSet GetRecognitionDoneReport(string Filter = null)
        {
        //    var parameterList = new List<CommanParameter> {
        //     new CommanParameter{Name = "Filter",Type=DbType.String, value=Filter},           
        //     new CommanParameter{Name = "Status",Type=DbType.Int32, value=(int)Status.Recognized}
        //   };
        //    return DataAccess.Instance.ExecuteDataSet("Proc_Recognition_DoneReport", CommandType.StoredProcedure, parameterList);
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.Proc_Recognition_DoneReport, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
                new CommanParameter{Name = "Filter",Type=DbType.String, value=Filter},           
                new CommanParameter{Name = "Status",Type=DbType.Int32, value=(int)Status.Recognized}, });
        }
        /// <summary>
        /// Recognition Reports
        /// </summary>
        /// <param name="applicationFilter"></param>
        /// <param name="zooFilter"></param>
        /// <param name="StatusId"></param>
        /// <returns></returns>
        public DataTable GetRecognitionReport(string applicationFilter, string zooFilter, int StatusId)
        {

           // List<CommanParameter> parameterList = new List<CommanParameter> {
           //    new CommanParameter{Name = DataEntity.DataBaseFields.FILTER_VALUE_Rec,Type=DbType.String, value=applicationFilter}, 
           //      new CommanParameter{Name = DataEntity.DataBaseFields.FILTER_VALUE ,Type=DbType.String, value=zooFilter}, 
           //      new CommanParameter{Name=DataEntity.DataBaseFields.STATUS_ID,Type=DbType.Int32,value=StatusId}
           //};
           // return DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetRecognitionReport, CommandType.StoredProcedure, parameterList).Tables[0];

            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.GetRecognitionReport, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
                new CommanParameter{Name = DataBaseFields.FILTER_VALUE_Rec,Type=DbType.String, value=applicationFilter}, 
                 new CommanParameter{Name = DataBaseFields.FILTER_VALUE ,Type=DbType.String, value=zooFilter}, 
                 new CommanParameter{Name=DataBaseFields.STATUS_ID,Type=DbType.Int32,value=StatusId}, }).Tables[0];

        }
        public DataSet GetAllStatus()
        {
            //List<CommanParameter> parameterList = new List<CommanParameter>
            //{
            //    new CommanParameter{Name = DataBaseFields.STATUS_ID,Type=DbType.Int32, value = (int)Status.Withheld.ToString().GetStatusValue() }
            //};
            //string cmd = "SELECT * FROM Status where StatusId<>@" + DataBaseFields.STATUS_ID;
            //return DataAccess.Instance.ExecuteDataSet(cmd, CommandType.Text, parameterList);

            string cmd = "SELECT * FROM Status where StatusId<>@" + DataBaseFields.STATUS_ID;
            return DAL.DataAccess.Instance.ExecuteDataSet(cmd, System.Data.CommandType.Text, new List<ICommanParameter> { 
               new CommanParameter{Name = DataBaseFields.STATUS_ID,Type=DbType.Int32, value = (int)Status.Withheld.ToString().GetStatusValue()}, });
        }
        /// <summary>
        /// Zoo Report
        /// </summary>
        /// <param name="filterParameters"></param>
        /// <returns></returns>
        public DataTable GetZooReport(string filterParameters)
        {
            //List<CommanParameter> parameterList = new List<CommanParameter> { new CommanParameter { Name = DataEntity.DataBaseFields.FILTER_VALUE, Type = DbType.String, value = filterParameters } };
            //return DataAccess.Instance.ExecuteDataSet(StoredProcedure.ZooGetReport, CommandType.StoredProcedure, parameterList).Tables[0];

            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.ZooGetReport, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { 
               new CommanParameter { Name = DataBaseFields.FILTER_VALUE, Type = DbType.String, value = filterParameters}, }).Tables[0];
        }
        #endregion
    }
}
