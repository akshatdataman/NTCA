using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL;
using System.Data;
using WiseThink.NTCA.BAL.Authorization;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class ViewCheckList : BasePage
    {
        int TigerReserverId, stateId;
        string currentFinancialYear = string.Empty;
        string previousFinancialYear = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsIds;
            if (Session["APOFileId"] != null)
            {
                dsIds = APOBAL.Instance.GetStateIdTigerReserveIdAndFinancialYear(Convert.ToInt32(Session["APOFileId"]));
                if (dsIds != null)
                {
                    DataRow dr = dsIds.Tables[0].Rows[0];
                    TigerReserverId = Convert.ToInt32(dr["TigerReserveId"]);
                    stateId = Convert.ToInt32(dr["StateId"]);
                    currentFinancialYear = dr["FinancialYear"].ToString();
                }
            }
            if (!IsPostBack)
            {
                GetCheckListFormat(TigerReserverId);
                UserBAL.Instance.InsertAuditTrailDetail("Visited View Checklist Page", "APO");
            }
        }
        private void GetCheckListFormat(int tigerReserveId)
        {
            DataSet dsCheckList = CheckListBAL.Instance.GetCheckListFormat(tigerReserveId, Request.QueryString["callfrom"].ToString());
            if (dsCheckList.Tables[0].Rows.Count > 0)
            {//Updation of information related to Tiger Reserve
                DataTable dt = dsCheckList.Tables[0];
                //string[] First = new string[] { "0", "Nature of items considered in APO" };
                string[] thirdLast = new string[] { "98", "Obligations Under Tri-MOU" };
                string[] secondLast = new string[] { "99", "Utilization Certificate" };
                string[] last = new string[] { "100", "Updation of information related to Tiger Reserve" };

                //cgvFDCheckList.Rows.
                //dt.Rows.Add(First);
                dt.Rows.Add(thirdLast);
                dt.Rows.Add(secondLast);
                dt.Rows.Add(last);
                cgvFDCheckList.DataSource = dt;
                cgvFDCheckList.DataBind();
                //LoadCheckListDraftData(cgvFDCheckList);
                //ButtonDiv.Style.Add("display", "block");
            }
            else
            {
                cgvFDCheckList.DataSource = null;
                cgvFDCheckList.DataBind();
            }
        }
        private void LoadCheckListDraftData(GridView gv)
        {
            try
            {

                DataSet dsDraft = CheckListBAL.Instance.GetCheckListDraft(TigerReserverId);
                if (dsDraft.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsDraft.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dsDraft.Tables[0].Rows[i];
                        foreach (GridViewRow row in gv.Rows)
                        {
                            Label lblActivityId = (Label)row.FindControl("lblActivityId");
                            RadioButtonList rblCheckedOrNot = row.FindControl("rblCheckedOrNot") as RadioButtonList;
                            int activityId = Convert.ToInt32(lblActivityId.Text);
                            string strCheckedOrNot = string.Empty;
                            //DataRow dr = dsDraft.Tables[0].Rows[0];
                            //if (dr["CheckedOrNotApplicable"].ToString() == "Checked" && dr["ActivityId"].ToString() == lblActivityId.Text)
                            //{
                            //    rblCheckedOrNot.SelectedValue = "1";
                            //}
                            //else if (dr["CheckedOrNotApplicable"].ToString() == "Not Applicable" && dr["ActivityId"].ToString() == lblActivityId.Text)
                            //{
                            //    rblCheckedOrNot.SelectedValue = "2";
                            //}
                        }
                    }
                    //Session["ChecklistSaved"] = true;
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        protected void cgvFDCheckList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView CustomGrid = (GridView)sender;
                CheckListBAL.Instance.GenerateGridSubHeader(CustomGrid, cgvFDCheckList);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                int count = cgvFDCheckList.Rows.Count - 1;
                if (cgvFDCheckList.Rows.Count - 1 > 0)
                {
                    GridView CustomGrid = (GridView)sender;
                    CheckListBAL.Instance.GenerateGridSubHeader2(CustomGrid, cgvFDCheckList, count);
                }
            }

        }
    }
}