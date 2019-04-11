using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WiseThink.NTCA.DataEntity;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WiseThink.NTCA.BAL
{
    public class CheckListBAL
    {
        #region Design Pattern
        private static CheckListBAL instance;
        private static object syncRoot = new Object();
        private CheckListBAL() { }

        public static CheckListBAL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new CheckListBAL();
                    }
                }
                return instance;
            }
        }
        #endregion
        #region Methods
        public void GenerateGridSubHeader(object sender, GridView Grid)
        {
            if (Grid.Rows.Count == 1)
            {
                GridViewRow row = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
                TableHeaderCell HeaderCell;
                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "A.";
                HeaderCell.HorizontalAlign = HorizontalAlign.Left;
                HeaderCell.ColumnSpan = 1;
                HeaderCell.CssClass = "HeaderStyle";
                HtmlGenericControl span1 = new HtmlGenericControl("span1");
                span1.InnerHtml = HeaderCell.Text;

                HeaderCell.Controls.Add(span1);
                row.Cells.AddAt(0, HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "Nature of items considered in APO";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 2;
                HeaderCell.CssClass = "HeaderStyle";
                HtmlGenericControl span2 = new HtmlGenericControl("span2");
                span2.InnerHtml = HeaderCell.Text;

                HeaderCell.Controls.Add(span2);
                row.Cells.AddAt(1, HeaderCell);

                Grid.Controls[0].Controls.AddAt(1, row);
            }

        }

        public void GenerateGridSubHeader2(object sender, GridView Grid, int count)
        {
                GridViewRow row = new GridViewRow(count, 1, DataControlRowType.Header, DataControlRowState.Insert);
                TableHeaderCell HeaderCell;
                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "B.";
                HeaderCell.HorizontalAlign = HorizontalAlign.Left;
                HeaderCell.ColumnSpan = 1;
                HeaderCell.CssClass = "HeaderStyle";
                HtmlGenericControl span1 = new HtmlGenericControl("span1");
                span1.InnerHtml = HeaderCell.Text;

                HeaderCell.Controls.Add(span1);
                row.Cells.AddAt(0, HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "Other mandatory details for Apo";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 2;
                HeaderCell.CssClass = "HeaderStyle";
                HtmlGenericControl span2 = new HtmlGenericControl("span2");
                span2.InnerHtml = HeaderCell.Text;

                HeaderCell.Controls.Add(span2);
                row.Cells.AddAt(1, HeaderCell);

                Grid.Controls[0].Controls.AddAt(count, row);
            

        }
        public void GenerateGridSubHeader3(object sender, GridView Grid, int count)
        {
            GridViewRow row = new GridViewRow(count, 1, DataControlRowType.Header, DataControlRowState.Insert);
            TableHeaderCell HeaderCell;
            HeaderCell = new TableHeaderCell();
            HeaderCell.Text = "C.";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "HeaderStyle";
            HtmlGenericControl span1 = new HtmlGenericControl("span1");
            span1.InnerHtml = HeaderCell.Text;

            HeaderCell.Controls.Add(span1);
            row.Cells.AddAt(0, HeaderCell);

            HeaderCell = new TableHeaderCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HtmlGenericControl span2 = new HtmlGenericControl("span2");
            span2.InnerHtml = HeaderCell.Text;

            HeaderCell.Controls.Add(span2);
            row.Cells.AddAt(1, HeaderCell);

            Grid.Controls[0].Controls.AddAt(count, row);


        }
        public void GenerateGridSubHeader4(object sender, GridView Grid, int count)
        {


            GridViewRow row = new GridViewRow(count, 1, DataControlRowType.Header, DataControlRowState.Insert);
            TableHeaderCell HeaderCell;
            HeaderCell = new TableHeaderCell();
            HeaderCell.Text = "D.";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "HeaderStyle";
            HtmlGenericControl span1 = new HtmlGenericControl("span1");
            span1.InnerHtml = HeaderCell.Text;

            HeaderCell.Controls.Add(span1);
            row.Cells.AddAt(0, HeaderCell);

            HeaderCell = new TableHeaderCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HtmlGenericControl span2 = new HtmlGenericControl("span2");
            span2.InnerHtml = HeaderCell.Text;

            HeaderCell.Controls.Add(span2);
            row.Cells.AddAt(1, HeaderCell);

            Grid.Controls[0].Controls.AddAt(count, row);


        }

        public DataSet GetLoggedInUserTigerReserveId(string loggedInUser)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetLoggedInUserTigerReserveId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.LoggedInUser,Type=System.Data.DbType.String,value=loggedInUser},
             });
        }
        public int UploadProvisionalUC(int apoFileId, string provisionalUcName)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUploadProvisionalUC, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.Int32,value=apoFileId},
                new CommanParameter{Name="@ProvisionalUcFileName",Type=System.Data.DbType.String,value=provisionalUcName},
            });
        }

        public DataSet GetAPOStateAndTigerReserveId(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetStateAndTigerReserveId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.String,value=apoFileId},
             });
        }
        public DataSet IsObligationUnSpentAmountSettled(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spChecckObligationsAndAnspentAmountSettled, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.String,value=apoFileId},
             });
        }
        public DataSet CheckCWLWObligations(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spChecckCWWObligations, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.String,value=apoFileId},
             });
        }
        public DataSet GetCheckListFormat(int tigerReserveId,string CallFrom)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetFDCheckListFormat, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                new CommanParameter{Name="@CallFrom",Type=System.Data.DbType.String,value=CallFrom}
             });
        }
        public int SubmitCheckList(int activityId, int tigerReserveId, string CheckListValue)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spSubmitCheckList, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.ACTIVITY_ID,Type=System.Data.DbType.Int32,value=activityId},
                 new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
                 new CommanParameter{Name=DataBaseFields.CheckedOrNotApplicable,Type=System.Data.DbType.String,value=CheckListValue},
            });
        }

        public DataSet GetCheckListDraft(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetCheckListDraftData, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        public int UpdateCheckList(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spUpdateFDObligationstatus, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                 new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
            });
        }
        public DataSet GetStateId(string userName)
        {

            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetUserStateId, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.LoggedInUser,Type=System.Data.DbType.String,value=userName},
             });
        }
        public int ForwardAPOToCWW(int apoFileId, int statusId)
        {
            return DAL.DataAccess.Instance.ExecuteNonQuery(StoredProcedure.spForwardAPOToCWW, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
            {
                new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.Int32,value=apoFileId},
                new CommanParameter{Name="@StatusId",Type=System.Data.DbType.Int32,value=statusId},
            });
        }
        public DataSet GetApoCounts(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetTigerReserveApoCount, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        public DataSet GetTigerReserveName(int _tigerReserveID)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetParticularTigerReserveName, System.Data.CommandType.StoredProcedure, new List<ICommanParameter> { new CommanParameter { Name = DataBaseFields.TigerReserveId, Type = System.Data.DbType.String, value = _tigerReserveID }, });
        }
        #region Duration of APO Submission
        public DataSet IsAllowedAfterDueDate(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spIsAllowedAfterDueDate, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name="@APOFileId",Type=System.Data.DbType.String,value=apoFileId},
             });
        }
        public DataSet GetUCAndProvisionalUC(int apoFileId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spGetUCAndProvisionalUC, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.APOFileId,Type=System.Data.DbType.Int32,value=apoFileId},
             });
        }
        public bool IsAllowApoSubmission()
        {
            DateTime current = DateTime.Now;
            int day = current.Day;
            int month = current.Month;
            int year = current.Year;
            if (day <= 20 && month == 04)
                return true;
            else
                return false;
        }
        #endregion
        #region CheckList View
        public DataSet GetCheckListViewForFD(int tigerReserveId)
        {
            return DAL.DataAccess.Instance.ExecuteDataSet(StoredProcedure.spViewFDObligations, System.Data.CommandType.StoredProcedure, new List<ICommanParameter>
             {
                new CommanParameter{Name=DataBaseFields.TigerReserveId,Type=System.Data.DbType.Int32,value=tigerReserveId},
             });
        }
        #endregion
        #endregion
    }
}
