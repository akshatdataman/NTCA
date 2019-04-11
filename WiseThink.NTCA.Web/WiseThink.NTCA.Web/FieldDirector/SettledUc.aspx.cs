using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.App_Code;
using WiseThink.NTCA.BAL.Authorization;
using System.IO;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class SettledUc : BasePage
    {
        public bool IsEdit = false;
        int tigerReserveId, stateId;
        string currentFinancialYear = string.Empty;
        string previousFinancialYear = string.Empty;
        int previousApoFileId;
        APO oApo = new APO();
        CommonClass cc = new CommonClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsIds;
            if (Session["APOFileId"] != null)
            {
                previousApoFileId = Convert.ToInt32(Session["APOFileId"]);
                dsIds = APOBAL.Instance.GetStateIdTigerReserveIdAndFinancialYear(previousApoFileId);
                if (dsIds != null)
                {
                    DataRow dr = dsIds.Tables[0].Rows[0];
                    oApo.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                    tigerReserveId = oApo.TigerReserveId;
                    stateId = Convert.ToInt32(dr["StateId"]);
                    currentFinancialYear = dr["FinancialYear"].ToString();
                    previousFinancialYear = cc.GetPreviousFinancialYear(currentFinancialYear);
                    previousFinancialYear = currentFinancialYear;
                    oApo.FinancialYear = previousFinancialYear;
                }
            }
            if (!IsPostBack)
            {
                GetUnspentActivityList();
            }
        }
        private void GetUnspentActivityList()
        {
            DataSet dsUnspent = new DataSet();
            dsUnspent = SettledUcBAL.Instance.GetGetUnspentActivitiesList(tigerReserveId, previousFinancialYear);
            cgvUnspent.DataSource = dsUnspent;
            cgvUnspent.DataBind();
            //if (dsUnspent.Tables[0].Rows.Count > 0)
            //{
            //    cgvUnspent.DataSource = dsUnspent.Tables[0];
            //    cgvUnspent.DataBind();
            //    //LoadGridviewDraftData(gvEcoDevCore);
            //}
        }
        protected void cgvUnspent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvUnspent.PageIndex = e.NewPageIndex;
            GetUnspentActivityList();
        }
        protected void rblRevalidateAdjustment_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rblName = sender as RadioButtonList;
            GridViewRow parentRow = rblName.NamingContainer as GridViewRow;
            foreach (GridViewRow row in cgvUnspent.Rows)
            {
                RadioButtonList rblRevalidateAdjustment = row.FindControl("rblRevalidateAdjustment") as RadioButtonList;
                RadioButtonList rblSpilloverAdjustment = row.FindControl("rblSpilloverAdjustment") as RadioButtonList;
                TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
                if (rblRevalidateAdjustment.SelectedValue == "1")
                {
                    txtJustification.Enabled = true;
                    txtJustification.Attributes.Add("req", "1");
                }
                else if (rblRevalidateAdjustment.SelectedValue == "2")
                {
                    txtJustification.Enabled = false;
                    txtJustification.Text = string.Empty;
                }
            }
        }
        //protected void rblSpilloverAdjustment_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    RadioButtonList rblName = sender as RadioButtonList;
        //    GridViewRow parentRow = rblName.NamingContainer as GridViewRow;
        //    string strName = rblName.Text;
        //    //Mutually exclusive RadioButtons
        //    foreach (GridViewRow row in cgvUnspent.Rows)
        //    {
        //        RadioButtonList rblRevalidateAdjustment = row.FindControl("rblRevalidateAdjustment") as RadioButtonList;
        //        RadioButtonList rblSpilloverAdjustment = row.FindControl("rblSpilloverAdjustment") as RadioButtonList;
        //        TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
        //        if (rblSpilloverAdjustment.SelectedValue == "1" || rblSpilloverAdjustment.SelectedValue == "2")
        //        {
        //            txtJustification.Enabled = false;
        //            txtJustification.Text = string.Empty;
        //            rblRevalidateAdjustment.Visible = false;
        //        }
        //    }
        //}
        private void InsertGridviewToDatabase(GridView gv)
        {
            try
            {
                foreach (GridViewRow row in gv.Rows)
                {
                    Label lblAreaId = (Label)row.FindControl("lblAreaId");
                    Label lblActivityTypeId = (Label)row.FindControl("lblActivityTypeId");
                    Label lblActivityId = (Label)row.FindControl("lblActivityId");
                    Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                    LinkButton lbCssPT = (LinkButton)row.FindControl("ParaNoCSSPTGuidelines");
                    TextBox txtParaNo = (TextBox)row.FindControl("txtParaNo");
                    TextBox txtNumberOfItem = (TextBox)row.FindControl("txtNumberOfItem");
                    TextBox txtSpcification = (TextBox)row.FindControl("txtSpcification");
                    TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                    Label lblReleasedAmount = (Label)row.FindControl("lblReleasedAmount");
                    //if (!string.IsNullOrEmpty(txtNumberOfItem.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
                    //{
                    //    double total = Convert.ToDouble(txtNumberOfItem.Text) * Convert.ToDouble(txtUnitPrice.Text);
                    //    txtTotal.Text = Convert.ToString(total);
                    //    txtTotal.Enabled = false;
                    //}
                    TextBox txtGPS = (TextBox)row.FindControl("txtGPS");
                    TextBox txtJustification = (TextBox)row.FindControl("txtJustification");
                    //FileUpload fuUploadDocument = (FileUpload)row.FindControl("fuUploadDocument");
                    //Label lblFileName = (Label)row.FindControl("lblFileName");
                    Label lblUnspentAmount = (Label)row.FindControl("lblUnspentAmount");
                    RadioButtonList rblRevalidateAdjustment = row.FindControl("rblRevalidateAdjustment") as RadioButtonList;
                    RadioButtonList rblSpilloverAdjustment = row.FindControl("rblSpilloverAdjustment") as RadioButtonList;
                    if (!string.IsNullOrEmpty(lblAreaId.Text))
                        oApo.AreaId = Convert.ToInt32(lblAreaId.Text);
                    if (!string.IsNullOrEmpty(lblActivityTypeId.Text))
                        oApo.ActivityTypeId = Convert.ToInt32(lblActivityTypeId.Text);
                    if (!string.IsNullOrEmpty(lblActivityId.Text))
                        oApo.ActivityId = Convert.ToInt32(lblActivityId.Text);
                    if (!string.IsNullOrEmpty(lblActivityItemId.Text))
                        oApo.ActivityItemId = Convert.ToInt32(lblActivityItemId.Text);
                    if (!string.IsNullOrEmpty(lblUnspentAmount.Text))
                        oApo.Unspent = Convert.ToDouble(lblUnspentAmount.Text);
                    else
                    {
                        if (!string.IsNullOrEmpty(lblReleasedAmount.Text))
                            oApo.Unspent = Convert.ToDouble(lblReleasedAmount.Text);
                    }
                    if (oApo.Unspent > 0)
                    {
                        oApo.IsSettledUnspent = true;
                        if (rblRevalidateAdjustment.SelectedValue == "1")
                        {
                            oApo.IsReValidate = true;
                            oApo.Justification = txtJustification.Text;
                        }
                        else
                        {
                            oApo.IsReValidate = false;
                        }
                        //if (rblSpilloverAdjustment.SelectedValue == "1")
                        //{
                        //    oApo.IsSpillOverAdjustment = true;
                        //    oApo.Justification = txtJustification.Text;
                        //}
                        //else
                        //{
                        //    oApo.IsSpillOverAdjustment = false;
                        //}
                        SettledUcBAL.Instance.SettledPFYUnspentAmount(oApo);
                        string strSuccess = "Unspent amount has been settled successfully.";
                        vmSuccess.Message = strSuccess;
                        FlashMessage.InfoMessage(vmSuccess.Message);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                //return;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                InsertGridviewToDatabase(cgvUnspent);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
    }
}