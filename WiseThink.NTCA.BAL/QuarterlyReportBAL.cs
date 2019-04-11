using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;

namespace WiseThink.NTCA.BAL
{
    public class QuarterlyReportBAL
    {
        #region Design Pattern
        private static QuarterlyReportBAL instance;
        private static object syncRoot = new Object();
        private QuarterlyReportBAL() { }

        public static QuarterlyReportBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new QuarterlyReportBAL();
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
        public DataSet GetLoggedInUserStateId(string loggedInUser)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetLoggedInUserStateId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.LoggedInUser,Type=System.Data.DbType.String,value=loggedInUser},
             });
        }
        public DataSet GetQuarterListt()
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetQuarter, System.Data.CommandType.StoredProcedure);
        }
        public DataSet GetQuarterlyReportFormatForCWwAndNTCAQuartely(int tigerReserveId, int monthid)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetQuarterlyReportFormatDataForCWWANDOfficerBYPeriod, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name="PeriodId",Type=DbType.Int32,value=monthid}
             });
        }


        public DataSet GetQuarterlyReportFormatForCWwAndNTCA(int tigerReserveId, int monthid)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetQuarterlyReportFormatDataForCWWANDOfficer, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.MonthId,Type=DbType.Int32,value=monthid}
             });
        }

        public DataSet GetQuarterlyReportFormat(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetQuarterlyReportFormatData, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        public DataSet GetQuarterlyReportDraftDataByMonth(int tigerReserveId, int quarterId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetQuarterlyReportDraftDataByMonths, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.QuarterId,Type=System.Data.DbType.Int32,value=quarterId},
             });
        }
        public DataSet GetQuarterlyReportDraftData(int tigerReserveId, int quarterId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetQuarterlyReportDraftData, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name=DataBaseFields.QuarterId,Type=System.Data.DbType.Int32,value=quarterId},
             });
        }
        public int SubmitQuarterlyReport(APO apo)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spSubmitQuarterlyReport, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name=DataBaseFields.Month,Type=System.Data.DbType.Int32,value=apo.Month},
                new CommanParameter{Name=DataBaseFields.FromDate,Type=System.Data.DbType.Date,value=apo.FromDate},
                new CommanParameter{Name=DataBaseFields.ToDate,Type=System.Data.DbType.Date,value=apo.ToDate},
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=apo.TigerReserveId},
                new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.Int32,value=apo.ActivityTypeId},
                new CommanParameter{Name=DataBaseFields.ActivityId,Type=System.Data.DbType.Int32,value=apo.ActivityId},
                new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.Int32,value=apo.ActivityItemId},
                new CommanParameter{Name=DataBaseFields.NumberOfItems,Type=System.Data.DbType.Decimal,value=apo.NumberOfItems},
                new CommanParameter{Name=DataBaseFields.PhysicalProgress,Type=System.Data.DbType.String,value=apo.PhysicalProgress},
                new CommanParameter{Name=DataBaseFields.CentralFinancialProgress,Type=System.Data.DbType.String,value=apo.CentralFinancialProgress},     
                 new CommanParameter{Name=DataBaseFields.StateFinancialProgress,Type=System.Data.DbType.String,value=apo.StateFinancialProgress},               
                new CommanParameter{Name=DataBaseFields.FinancialTotal,Type=System.Data.DbType.String,value=apo.FinancialTotal},
                new CommanParameter{Name=DataBaseFields.IsFilled,Type=System.Data.DbType.Boolean,value=apo.IsFilled},
            });

        }

        public DataSet FetchExpenditureReports(string flag, string month, string fromdate, string todate, int tigerReserveId)
        {
            DataSet ds = DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spFetchExpenditureReports, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {                
                new CommanParameter{Name="@Flag",Type=System.Data.DbType.String,value=flag.Trim()},
                new CommanParameter{Name="@MonthId",Type=System.Data.DbType.String,value=month},
                new CommanParameter{Name="@FromDate",Type=System.Data.DbType.String,value=fromdate},
                new CommanParameter{Name="@ToDate",Type=System.Data.DbType.String,value=todate},
                 new CommanParameter{Name="@TigerReserverID",Type=System.Data.DbType.String,value=tigerReserveId},
            });

            return ds;
        }

        public DataSet GetReportsActivityWise(int activity, string financialYear)
        {
            DataSet ds = DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetDataforActivityWise, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {                
                new CommanParameter{Name="@ActivityId",Type=System.Data.DbType.Int32,value=activity},
                new CommanParameter{Name="@FinancialYear",Type=System.Data.DbType.String,value=financialYear},
            });

            return ds;
        }

        public DataSet GetReportsActivityItemWise(int activityItem, string financialYear)
        {
            DataSet ds = DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetDataforActivityItemWise, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {                
                new CommanParameter{Name="@ActivityItemId",Type=System.Data.DbType.Int32,value=activityItem},
                new CommanParameter{Name="@FinancialYear",Type=System.Data.DbType.String,value=financialYear},
            });

            return ds;
        }

        public DataSet GetReportsSubItemWise(int subItem, string financialYear)
        {
            DataSet ds = DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetDataforSubItemWise, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {                
                new CommanParameter{Name="@SubItemId",Type=System.Data.DbType.Int32,value=subItem},
                new CommanParameter{Name="@FinancialYear",Type=System.Data.DbType.String,value=financialYear},
            });

            return ds;
        }
        public DataSet GetReportsTigerReserveWise(string financialYear)
        {
            DataSet ds = DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetDataforTigerReserveWise, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name="@FinancialYear",Type=System.Data.DbType.String,value=financialYear},
            });

            return ds;
        }
        public DataSet GetReportsStateWise(string financialYear)
        {
            DataSet ds = DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetDataforStateWise, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name="@FinancialYear",Type=System.Data.DbType.String,value=financialYear},
            });

            return ds;
        }

        public DataSet GetActivitySubItem(int activityItemId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetSubItemListItemWise, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> 
            { new CommanParameter { Name = "@ActivityItemId", Type = System.Data.DbType.Int32, value = activityItemId },
            });
        }

        public DataSet GetApoForModification(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetAPOForEdit, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
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
        public int ModifyQuarterlyReport(APO apo)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spModifyQuarterlyReport, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name="@ID",Type=System.Data.DbType.Int32,value=apo.APOId},
                //new CommanParameter{Name=DataBaseFields.Month,Type=System.Data.DbType.Int32,value=apo.Month},
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=apo.TigerReserveId},
                new CommanParameter{Name=DataBaseFields.ActivityTypeId,Type=System.Data.DbType.Int32,value=apo.ActivityTypeId},
                new CommanParameter{Name=DataBaseFields.ActivityId,Type=System.Data.DbType.Int32,value=apo.ActivityId},
                new CommanParameter{Name=DataBaseFields.ActivityItemId,Type=System.Data.DbType.Int32,value=apo.ActivityItemId},
                new CommanParameter{Name=DataBaseFields.NumberOfItems,Type=System.Data.DbType.Decimal,value=apo.NumberOfItems},
                new CommanParameter{Name=DataBaseFields.PhysicalProgress,Type=System.Data.DbType.String,value=apo.PhysicalProgress},
                new CommanParameter{Name=DataBaseFields.CentralFinancialProgress,Type=System.Data.DbType.String,value=apo.CentralFinancialProgress},     
                 new CommanParameter{Name=DataBaseFields.StateFinancialProgress,Type=System.Data.DbType.String,value=apo.StateFinancialProgress},               
                new CommanParameter{Name=DataBaseFields.FinancialTotal,Type=System.Data.DbType.String,value=apo.FinancialTotal},
                new CommanParameter{Name=DataBaseFields.IsFilled,Type=System.Data.DbType.Boolean,value=apo.IsFilled},
                new CommanParameter{Name=DataBaseFields.ModifiedBy,Type=System.Data.DbType.String,value=apo.LoggedInUser},
            });
        }

        #region Quarterly Report View
        public DataSet GetLoggedUserRole(string userName)
        {

            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.CheckUserRoles, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name="@UserName",Type=System.Data.DbType.String,value=userName},
             });
        }
        public DataSet GetApoForFDView(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spViewAPOForFD, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        #endregion
        #endregion

    }
}
