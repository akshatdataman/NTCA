using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL;
using System.Data;
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.CWW_Secretary
{
    public partial class ApproveAPO : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControls();
                UserBAL.Instance.InsertAuditTrailDetail("Visited Appreove - Reject Page", "Approve - Reject");
            }
        }
        private void BindControls()
        {
            if (Session["APOFileId"] != null)
            {
                int apoFileId = Convert.ToInt32(Session["APOFileId"]);
                DataSet ds = APOBAL.Instance.GetAPOForApproval(apoFileId);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    ApoTitle.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["APOTitle"]);
                    TigerReserveName.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["TigerReserveName"]);
                    ApoFileNumber.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["ApoFileNumber"]);
                    FDName.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["FieldDirector"]);
                }
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                string reginalOfficerDiaryNo = string.Empty;
                if (Session["APOFileId"] != null && ApoTitle.InnerText != "" && txtComments.Text != "")
                {
                    DataSet dsRODiaryNum = APOBAL.Instance.GetRegionalOfficerDiaryNumber(Convert.ToInt32(Session["APOFileId"]));
                     if (dsRODiaryNum.Tables[0].Rows.Count > 0)
                     {
                         DataRow dr = dsRODiaryNum.Tables[0].Rows[0];
                         reginalOfficerDiaryNo = Convert.ToString(dr["RegionalOfficerDiaryNumner"]);
                         if (string.IsNullOrEmpty(reginalOfficerDiaryNo) || dr[0] == DBNull.Value)
                         {
                             int apoFileId = Convert.ToInt32(Session["APOFileId"]);
                             APOBAL.Instance.UpdateStatus(apoFileId, 13, false, txtComments.Text, null,null);
                             string strSuccess = "APO status has been approved successfully.";
                             UserBAL.Instance.InsertAuditTrailDetail("Apo Forwarded to Reginal Officer", "Approve - Reject");
                             vmSuccess.Message = strSuccess;
                             FlashMessage.InfoMessage(vmSuccess.Message);
                             Session["apoFileId"] = null;
                         }
                         else
                         {
                             string strError = "You can not approve this APO, since it has been approved by higher authority.";
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

        protected void btnModify_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditApo.aspx", false);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx", false);
        }
    }
}