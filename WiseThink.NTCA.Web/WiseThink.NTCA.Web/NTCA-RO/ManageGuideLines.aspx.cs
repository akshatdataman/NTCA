using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.App_Code;
using System.Data;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class ManageGuideLines : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetGuidelineList();
                UserBAL.Instance.InsertAuditTrailDetail("Visited Manage CSS-PT Guideline Page", "Manage CSS-PT Guidelines");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCSSPTParaNumber.Text) && !string.IsNullOrEmpty(txtCSSPTGuidelines.Text))
                {
                    ManageGuidelineBAL.Instance.AddGuideline(txtCSSPTParaNumber.Text, txtCSSPTGuidelines.Text);
                    GetGuidelineList();
                    string strSuccess = "Data saved successfully.";
                    vmSuccess.Message = strSuccess;
                    FlashMessage.ErrorMessage(vmSuccess.Message);
                    txtCSSPTParaNumber.Text = string.Empty;
                    txtCSSPTGuidelines.Text = string.Empty;
                    UserBAL.Instance.InsertAuditTrailDetail("Added CSS-PT Guideline", "Manage CSS-PT Guideline");
                }
                else
                {
                    string strError = "Please Enter the CSSPTParaNumber and CSSPT Guideline.";
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
        private void GetGuidelineList()
        {
            DataSet ds = ManageGuidelineBAL.Instance.GetGuidelineList();
            cgvManageGuidelines.DataSource = ds;
            cgvManageGuidelines.DataBind();
        }
        protected void cgvManageGuidelines_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvManageGuidelines.PageIndex = e.NewPageIndex;
            GetGuidelineList();
        }
        protected void cgvManageGuidelines_PageIndexChanged(object sender, EventArgs e)
        {
            cgvManageGuidelines.EditIndex = -1;
            GetGuidelineList();
        }
        protected void cgvManageGuidelines_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            cgvManageGuidelines.EditIndex = -1;
            GetGuidelineList();
        }

        protected void cgvManageGuidelines_RowEditing(object sender, GridViewEditEventArgs e)
        {
            cgvManageGuidelines.EditIndex = e.NewEditIndex;
            GetGuidelineList();
        }

        protected void cgvManageGuidelines_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label lblId = cgvManageGuidelines.Rows[e.RowIndex].FindControl("lblId") as Label;
                int Id = Convert.ToInt32(lblId.Text);
                TextBox txtCSSPTParaNumber = cgvManageGuidelines.Rows[e.RowIndex].FindControl("txtCSSPTParaNumber") as TextBox;
                TextBox txtCSSPTGuideline = cgvManageGuidelines.Rows[e.RowIndex].FindControl("txtCSSPTGuideline") as TextBox;
                string cssPTParaNumber = txtCSSPTParaNumber.Text;
                string cssPTGuideline = txtCSSPTGuideline.Text;
                ManageGuidelineBAL.Instance.UpdateGuideline(Id, cssPTParaNumber, cssPTGuideline);

                cgvManageGuidelines.EditIndex = -1;
                GetGuidelineList();

                string strSuccess = "Data updated successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.ErrorMessage(vmSuccess.Message);
                UserBAL.Instance.InsertAuditTrailDetail("Modified CSS-PT Guideline", "Manage CSS-PT Guidelines");
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
        protected void cgvManageGuidelines_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblId = cgvManageGuidelines.Rows[e.RowIndex].FindControl("lblId") as Label;
                int Id = Convert.ToInt32(lblId.Text);
                ManageGuidelineBAL.Instance.DeleteGuideline(Id);
                GetGuidelineList();
                string strSuccess = "Data deleted successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.ErrorMessage(vmSuccess.Message);
                UserBAL.Instance.InsertAuditTrailDetail("Deleted CSS-PT Guideline", "Manage CSS-PT Guidelines");
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