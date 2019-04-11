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
    public partial class ManageDesignation : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDesignationList();
                UserBAL.Instance.InsertAuditTrailDetail("Visited Manage Designation Page", "Manage Designation");
            }
        }

        protected void cgvDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvDesignation.PageIndex = e.NewPageIndex;
            GetDesignationList();
        }
        protected void cgvDesignation_PageIndexChanged(object sender, EventArgs e)
        {
            cgvDesignation.EditIndex = -1;
            GetDesignationList();
        }

        private void GetDesignationList()
        {
           DataSet ds = DesignationBAL.Instance.GetDesignation();
           if (ds.Tables[0].Rows.Count > 0)
           {
               cgvDesignation.DataSource = ds;
               cgvDesignation.DataBind();
           }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDesignation.Text))
                {
                    DesignationBAL.Instance.AddDesignation(txtDesignation.Text);
                    GetDesignationList();
                    string strSuccess = "Data saved successfully.";
                    vmSuccess.Message = strSuccess;
                    FlashMessage.ErrorMessage(vmSuccess.Message);
                    txtDesignation.Text = string.Empty;
                    UserBAL.Instance.InsertAuditTrailDetail("Created New Designation", "Manage Designation");
                }
                else
                {
                    string strError = "Please Enter the Designation.";
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    return;
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

        protected void cgvDesignation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            cgvDesignation.EditIndex = -1;
            GetDesignationList();
        }

        protected void cgvDesignation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            cgvDesignation.EditIndex = e.NewEditIndex;
            GetDesignationList();
        }

        protected void cgvDesignation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label Designation = cgvDesignation.Rows[e.RowIndex].FindControl("lblDesignationId") as Label;
                int designationId = Convert.ToInt32(Designation.Text);
                TextBox txtDesignation = cgvDesignation.Rows[e.RowIndex].FindControl("txtDesignation") as TextBox;
                string strDesignation = txtDesignation.Text;
                DesignationBAL.Instance.UpdateDesignation(designationId, strDesignation);
                UserBAL.Instance.InsertAuditTrailDetail("Updated Designation", "Manage Designation");
                cgvDesignation.EditIndex = -1;
                GetDesignationList();

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

        protected void cgvDesignation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label Designation = cgvDesignation.Rows[e.RowIndex].FindControl("lblDesignationId") as Label;
                int designationId = Convert.ToInt32(Designation.Text);
                DesignationBAL.Instance.DeleteDesignation(designationId);
                UserBAL.Instance.InsertAuditTrailDetail("Deleted Designation", "Manage Designation");
                GetDesignationList();
                string strSuccess = "Data deleted successfully.";
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
    }
}