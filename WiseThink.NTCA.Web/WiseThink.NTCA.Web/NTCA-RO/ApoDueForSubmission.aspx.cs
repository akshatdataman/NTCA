using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class ApoDueForSubmission : BasePage
    {
        DataSet dsApoCounts;
        int _apoCount = 0;
        int tigerReserveId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetApoDueForSubmission();
            }

        }
        protected void cgvDueApo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvDueApo.PageIndex = e.NewPageIndex;
            GetApoDueForSubmission();
        }
        protected void cgvDueAdditionalApo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvDueAdditionalApo.PageIndex = e.NewPageIndex;
            GetApoDueForSubmission();
        }
        protected void cgvDueApo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "VIEWAPO")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string APOID = lnkView.CommandArgument;
                Response.Redirect("~/NTCA-RO/ViewAPO.aspx?Id=" + APOID + "", false);
            }
            switch (e.CommandName)
            {
                case "Edit":
                    Session["IsEdit"] = true;
                    Response.Redirect(string.Format("~/NTCA-RO/EditApoCopy.aspx?Edit=True", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "UpdateStatus":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    Label lblIsFDObligation = (Label)row.FindControl("lblIsFDObligation");
                    Label lblUnspentAmountSettled = (Label)row.FindControl("lblUnspentAmountSettled");
                    string APOFileId = lnkView.CommandArgument;
                    //Session["APOFileId"] = APOFileId;
                    //if (lblIsFDObligation.Text == "False")
                    //{
                    //    string strError = "Apo can't forwarded to CWLW, Field Director has not submitted Obligation Under Tri-MOU";
                    //    vmError.Message = strError;
                    //    FlashMessage.ErrorMessage(vmError.Message);
                    //    return;
                    //}
                    DataSet ds = ObligationBAL.Instance.GetAPOStateAndTigerReserveId(Convert.ToInt32(APOFileId));
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        tigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                    }
                        dsApoCounts = CheckListBAL.Instance.GetApoCounts(tigerReserveId);
                    if (dsApoCounts.Tables[0].Rows.Count == 1)
                    {
                        DataRow drApoCount = dsApoCounts.Tables[0].Rows[0];
                        _apoCount = Convert.ToInt32(drApoCount["ApoCounts"]);
                    }
                    if (_apoCount > 1)
                    {
                        if (lblUnspentAmountSettled.Text == "False")
                        {
                            string strError = "Apo can't forwarded to CWLW, Field Director has not settled previous year unspent amount.";
                            vmError.Message = strError;
                            FlashMessage.ErrorMessage(vmError.Message);
                            return;
                        }
                    }
                    //int i = APOBAL.Instance.APOMoveToCWLW(Convert.ToInt32(APOFileId));
                    int i = APOBAL.Instance.AllowAfterDueDate(Convert.ToInt32(APOFileId));
                    string strSuccess = "Allowed to submit Apo after the due date with special permision.";
                    vmSuccess.Message = strSuccess;
                    FlashMessage.ErrorMessage(vmSuccess.Message);
                    GetApoDueForSubmission();
                    //if (i ==  1)
                    //{
                    //    string strSuccess = "APO Forwarded to CWLW successfully.";
                    //    vmSuccess.Message = strSuccess;
                    //    FlashMessage.ErrorMessage(vmSuccess.Message);
                    //    GetApoDueForSubmission();
                    //}
                    //else
                    //{
                    //    Response.RedirectPermanent("~/ErrorPage.aspx",true);
                    //}
                    break;
            }
        }
        protected void cgvDueAdditionalApo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VIEWAPO")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string APOID = lnkView.CommandArgument;
                Response.Redirect("~/NTCA-RO/ViewAPO.aspx?Id=" + APOID + "", false);
            }
            
            switch (e.CommandName)
            {
                case "UpdateStatus":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    Label lblIsFDObligation = (Label)row.FindControl("lblIsFDObligation");
                    Label lblUnspentAmountSettled = (Label)row.FindControl("lblUnspentAmountSettled");
                    string APOFileId = lnkView.CommandArgument;
                    //Session["APOFileId"] = APOFileId;
                    if (lblIsFDObligation.Text == "False")
                    {
                        string strError = "Additonal Apo can't forwarded to CWLW, Field Director has not submitted Obligation Under Tri-MOU";
                        vmError.Message = strError;
                        FlashMessage.ErrorMessage(vmError.Message);
                        return;
                    }
                    if (lblUnspentAmountSettled.Text == "False")
                    {
                        string strError = "Additional Apo can't forwarded to CWLW, Field Director has not settled previous year unspent amount.";
                        vmError.Message = strError;
                        FlashMessage.ErrorMessage(vmError.Message);
                        return;
                    }
                    int i = APOBAL.Instance.AdditionalAPOMoveToCWLW(Convert.ToInt32(APOFileId));
                    string strSuccess = "Additional APO Forwarded to CWLW successfully.";
                    vmSuccess.Message = strSuccess;
                    FlashMessage.ErrorMessage(vmSuccess.Message);
                    GetApoDueForSubmission();
                    //if (i ==  -1)
                    //{
                    //    string strSuccess = "Additional APO Forwarded to CWLW successfully.";
                    //    vmSuccess.Message = strSuccess;
                    //    FlashMessage.ErrorMessage(vmSuccess.Message);
                    //    GetApoDueForSubmission();
                    //}
                    //else
                    //{
                    //    Response.RedirectPermanent("~/ErrorPage.aspx",true);
                    //}
                    break;
            }
        }
        private void GetApoDueForSubmission()
        {
            DataSet dsApoDue = APOBAL.Instance.GetApoDueForSubmission();
            cgvDueApo.DataSource = dsApoDue.Tables[0];
            cgvDueApo.DataBind();
            cgvDueAdditionalApo.DataSource = dsApoDue.Tables[1];
            cgvDueAdditionalApo.DataBind();
        }

        protected void cgvDueApo_PageIndexChanged(object sender, EventArgs e)
        {
            cgvDueApo.EditIndex = -1;
            GetApoDueForSubmission();
        }

        protected void cgvDueAdditionalApo_PageIndexChanged(object sender, EventArgs e)
        {
            cgvDueAdditionalApo.EditIndex = -1;
            GetApoDueForSubmission();
        }
    }
}