using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.App_Code;
using System.Configuration;

namespace WiseThink.NTCA.Web.CWW_Secretary
{
    public partial class UpdateAPOStatus : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCurrentStatus();
                BindStatus();
                UserBAL.Instance.InsertAuditTrailDetail("Visited Update APO Status Page", "Update APO Status");
            }

        }
        private void BindStatus()
        {
            DataSet dsStatus = APOBAL.Instance.GetStatusList();
            ddlName.DataSource = dsStatus.Tables[0];
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
        private bool IsCWLWObligationsSubmitted(int apoFileId)
        {
            bool IsCWLWObligations = false;
            DataSet dsCheckObligation = CheckListBAL.Instance.CheckCWLWObligations(apoFileId);
            if (dsCheckObligation != null && dsCheckObligation.Tables[0].Rows.Count == 1)
            {
                DataRow dr = dsCheckObligation.Tables[0].Rows[0];
                if (!string.IsNullOrEmpty(dr["IsCWWObligationSubmitted"].ToString()))
                    IsCWLWObligations = Convert.ToBoolean(dr["IsCWWObligationSubmitted"]);
                if (IsCWLWObligations != true)
                {
                    string strInfo = ConfigurationManager.AppSettings["CheckCWLWObligations"];
                    vmError.Message = strInfo;
                    FlashMessage.InfoMessage(vmError.Message);
                }
                else
                    IsCWLWObligations = true;
            }
            return IsCWLWObligations;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string reginalOfficerDiaryNo = string.Empty;
                if (Session["apoFileId"] != null && ddlName.SelectedValue != "0" && txtComments.Text != "")
                {
                    if (ddlName.SelectedValue == "13")
                    {
                        if (!IsCWLWObligationsSubmitted(Convert.ToInt32(Session["apoFileId"])))
                            return;
                    }
                    APOBAL.Instance.UpdateDiaryNumber(Convert.ToInt32(Session["APOFileId"]));
                    DataSet dsRODiaryNum = APOBAL.Instance.GetRegionalOfficerDiaryNumber(Convert.ToInt32(Session["APOFileId"]));
                    if (dsRODiaryNum.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsRODiaryNum.Tables[0].Rows[0];
                        reginalOfficerDiaryNo = Convert.ToString(dr["RegionalOfficerDiaryNumner"]);
                        if (!string.IsNullOrEmpty(reginalOfficerDiaryNo))
                        {
                            APOBAL.Instance.UpdateStatus(Convert.ToInt32(Session["apoFileId"]), Convert.ToInt32(ddlName.SelectedValue), false, txtComments.Text, reginalOfficerDiaryNo, null);

                            UserBAL.Instance.InsertAuditTrailDetail("Updated APO Status", "Update APO Status");
                            if (ddlName.SelectedValue == "13")
                            {
                                string strSuccess = ConfigurationManager.AppSettings["ForwardToRegionalOfficer"];
                                vmSuccess.Message = strSuccess;
                                FlashMessage.InfoMessage(vmSuccess.Message);
                                Session["apoFileId"] = null;
                                Clear();
                            }
                            else if (ddlName.SelectedValue == "8")
                            {
                                string strSuccess = ConfigurationManager.AppSettings["ApoRejected"];
                                vmSuccess.Message = strSuccess;
                                FlashMessage.InfoMessage(vmSuccess.Message);
                                Session["apoFileId"] = null;
                                Clear();
                            }
                            else if (ddlName.SelectedValue == "3")
                            {
                                string strSuccess = ConfigurationManager.AppSettings["ModifyResubmit"];
                                vmSuccess.Message = strSuccess;
                                FlashMessage.InfoMessage(vmSuccess.Message);
                                Session["apoFileId"] = null;
                                Clear();
                            }
                        }
                        else
                        {
                            string strError = ConfigurationManager.AppSettings["ActionTakenByHigherAuthortity"];
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
            }
        }
        private void Clear()
        {
            ddlName.SelectedIndex = -1;
            txtComments.Text = "";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx", false);
        }
    }
}