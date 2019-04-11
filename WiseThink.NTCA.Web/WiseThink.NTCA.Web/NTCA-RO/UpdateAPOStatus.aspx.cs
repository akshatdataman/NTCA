using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL;
using System.Data;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class UpdateAPOStatus : BasePage
    {
        List<Role> userRole = new List<Role>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string loggedInUser = AuthoProvider.User;
                userRole = AuthoProvider.LoggedInRoles.ToList();
                if (userRole.Contains(Role.REGIONALOFFICER))
                {
                    RoDiaryDiv.Style.Add("display", "block");
                    txtRODiaryNum.Attributes.Add("req","1");
                    BypassReasonDiv.Style.Add("display","none");
                    txtBypass.Attributes.Clear();
                }
                else
                {
                    RoDiaryDiv.Style.Add("display", "none");
                    BypassReasonDiv.Style.Add("display", "block");
                    txtRODiaryNum.Attributes.Clear();
                }
                GetCurrentStatus();
                BindStatus();
                UserBAL.Instance.InsertAuditTrailDetail("Visited Update APO Status Page", "Update APO Status");
            }

        }
        private void BindStatus()
        {
            DataSet dsStatus = APOBAL.Instance.GetStatusList();
            if (userRole.Contains(Role.REGIONALOFFICER))
                ddlName.DataSource = dsStatus.Tables[2];
            else
                ddlName.DataSource = dsStatus.Tables[1];
            ddlName.DataValueField = "StatusId";
            ddlName.DataTextField = "Status";
            ddlName.DataBind();
            ddlName.Items.Insert(0, "Select");
        }
        private void GetCurrentStatus()
        {
            int apoFileId = 0;
            if (Session["APOFileId"] != null)
                apoFileId = Convert.ToInt32(Session["APOFileId"]);
            DataSet dsCurrentStatus = APOBAL.Instance.GetCurrentAPOStatus(apoFileId);
            if (dsCurrentStatus != null && dsCurrentStatus.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsCurrentStatus.Tables[0].Rows[0];
                string strStatus = dr["Status"].ToString();
                CurrentStatus.InnerText = strStatus;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsByPass = true;
                string reginalOfficerDiaryNo = string.Empty;
                if (Session["apoFileId"] != null && ddlName.SelectedValue != "0" && txtComments.Text != "")
                {
                    userRole = AuthoProvider.LoggedInRoles.ToList();
                    if (userRole.Contains(Role.NTCA) && IsByPass == false)
                    {
                        DataSet dsRODiaryNum = APOBAL.Instance.GetRegionalOfficerDiaryNumber(Convert.ToInt32(Session["APOFileId"]));
                        if (dsRODiaryNum.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = dsRODiaryNum.Tables[0].Rows[0];
                            reginalOfficerDiaryNo = Convert.ToString(dr["RegionalOfficerDiaryNumner"]);
                            if (!string.IsNullOrEmpty(reginalOfficerDiaryNo) || dr[0] != DBNull.Value)
                            {
                                APOBAL.Instance.UpdateStatus(Convert.ToInt32(Session["apoFileId"]), Convert.ToInt32(ddlName.SelectedValue), true, txtComments.Text, reginalOfficerDiaryNo,txtBypass.Text);
                                string strSuccess = "APO status has been updated successfully.";
                                vmSuccess.Message = strSuccess;
                                FlashMessage.InfoMessage(vmSuccess.Message);
                                Session["apoFileId"] = null;
                                Clear();
                            }
                            else
                            {
                                string strError = "Regional Officer has not taken any action against this APO, please alert him / her.";
                                vmError.Message = strError;
                                FlashMessage.ErrorMessage(vmError.Message);
                                return;
                            }
                        }
                    }
                    
                    if (userRole.Contains(Role.NTCA) && IsByPass==true)
                    {
                        APOBAL.Instance.UpdateStatus(Convert.ToInt32(Session["apoFileId"]), Convert.ToInt32(ddlName.SelectedValue), true, txtComments.Text, reginalOfficerDiaryNo, txtBypass.Text);
                        string strSuccess = "APO has been byPaas successfully.";
                        vmSuccess.Message = strSuccess;
                        FlashMessage.InfoMessage(vmSuccess.Message);
                        Session["apoFileId"] = null;
                        Clear();
                    }
                    else if (userRole.Contains(Role.REGIONALOFFICER))
                    {
                        if (!string.IsNullOrEmpty(txtRODiaryNum.Text))
                        {
                            APOBAL.Instance.UpdateStatus(Convert.ToInt32(Session["apoFileId"]), Convert.ToInt32(ddlName.SelectedValue), false, txtComments.Text, txtRODiaryNum.Text,null);
                            string strSuccess = "APO status has been updated successfully.";
                            UserBAL.Instance.InsertAuditTrailDetail("Updated APO Status", "Update APO Status");
                            vmSuccess.Message = strSuccess;
                            FlashMessage.InfoMessage(vmSuccess.Message);
                            Session["apoFileId"] = null;
                            Clear();
                        }
                        else
                        {
                            string strError = "Please enter your dairy number.";
                            vmError.Message = strError;
                            FlashMessage.ErrorMessage(vmError.Message);
                            return;
                        }
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
        private void Clear()
        {
            ddlName.SelectedIndex = -1;
            txtRODiaryNum.Text = "";
            txtComments.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx", false);
        }
    }
}