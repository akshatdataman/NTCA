using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL;
using System.Data;
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.Admin
{
    public partial class ManageObligations : BasePage
    {
        int obligationFor;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindObligationFor();
                GetObligationList();
                UserBAL.Instance.InsertAuditTrailDetail("Visited Manage Obligation List Page", "Manage Obligation");
            }
        }

        protected void cgvManageFDObligation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvManageFDObligation.PageIndex = e.NewPageIndex;
            GetObligationList();
        }
        private void BindObligationFor()
        { 
            ddlObligationFor.DataSource = ObligationMasterBAL.Instance.GetObligationFor();
            ddlObligationFor.DataValueField = "RoleId";
            ddlObligationFor.DataTextField = "RoleName";
            ddlObligationFor.DataBind();
            ddlObligationFor.Items.Insert(0, "Select");
        }

        private void GetObligationList()
        {
            if (ddlObligationFor.SelectedIndex == 0)
                obligationFor = 6;
            else
                obligationFor = Convert.ToInt32(ddlObligationFor.SelectedValue);
            DataSet ds = ObligationMasterBAL.Instance.GetObligations(obligationFor);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cgvManageFDObligation.DataSource = ds;
                cgvManageFDObligation.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlObligationFor.SelectedIndex != 0)
                {
                    ObligationMasterBAL.Instance.AddObligation(Convert.ToInt32(ddlObligationFor.SelectedValue), txtObligation.Text);
                    GetObligationList();
                    string strSuccess = "Data saved successfully.";
                    vmSuccess.Message = strSuccess;
                    FlashMessage.ErrorMessage(vmSuccess.Message);
                    txtObligation.Text = string.Empty;
                    ddlObligationFor.SelectedIndex = 0;
                    UserBAL.Instance.InsertAuditTrailDetail("Created New Obligation", "Manage Obligation");
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

        protected void cgvManageFDObligation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            cgvManageFDObligation.EditIndex = -1;
            GetObligationList();
        }

        protected void cgvManageFDObligation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            cgvManageFDObligation.EditIndex = e.NewEditIndex;
            GetObligationList();
        }

        protected void cgvManageFDObligation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label Obligation = cgvManageFDObligation.Rows[e.RowIndex].FindControl("lblObligationId") as Label;
                int obligationId = Convert.ToInt32(Obligation.Text);
                TextBox txtObligation = cgvManageFDObligation.Rows[e.RowIndex].FindControl("txtObligation") as TextBox;
                string strObligation = txtObligation.Text;
                ObligationMasterBAL.Instance.UpdateObligation(obligationId, strObligation);

                cgvManageFDObligation.EditIndex = -1;
                GetObligationList();
                UserBAL.Instance.InsertAuditTrailDetail("Updated Obligation", "Manage Obligation");
                string strSuccess = "Data updated successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.ErrorMessage(vmSuccess.Message);
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

        protected void cgvManageFDObligation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label Obligation = cgvManageFDObligation.Rows[e.RowIndex].FindControl("lblObligationId") as Label;
                int obligationId = Convert.ToInt32(Obligation.Text);
                ObligationMasterBAL.Instance.DeleteObligation(obligationId);
                GetObligationList();
                string strSuccess = "Data deleted successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.ErrorMessage(vmSuccess.Message);
                UserBAL.Instance.InsertAuditTrailDetail("Deleted Obligation", "Manage Obligation");
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

        protected void ddlObligationFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetObligationList();
        }
    }
}