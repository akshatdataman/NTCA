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
    public partial class ApproveAPO : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControls();
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
                if (ApoTitle.InnerText != "" && txtComments.Text != "")
                {
                    DataSet dsRODiaryNum = APOBAL.Instance.GetRegionalOfficerDiaryNumber(Convert.ToInt32(Session["APOFileId"]));
                    if (dsRODiaryNum.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsRODiaryNum.Tables[0].Rows[0];
                        if (dr[0] != DBNull.Value)
                        {
                            if (!string.IsNullOrEmpty(dr[0].ToString()))
                            {
                                reginalOfficerDiaryNo = dr["RegionalOfficerDiaryNumner"].ToString();
                                APOBAL.Instance.UpdateStatus(Convert.ToInt32(Session["APOFileId"]), 11, true, txtComments.Text, reginalOfficerDiaryNo, null);
                                string strSuccess = "APO status has been updated successfully.";
                                vmSuccess.Message = strSuccess;
                                FlashMessage.InfoMessage(vmSuccess.Message);
                                Session["apoFileId"] = null;
                            }
                            else
                            {
                                string strError = "Regional Officer has not taken any action against this APO, please alert him / her.";
                                vmError.Message = strError;
                                FlashMessage.ErrorMessage(vmError.Message);
                                return;
                            }
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

                else
                {
                    string strError = "Please provide remarks / comments before taking action.";
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

        protected void btnReject_Click(object sender, EventArgs e)
        {

            try
            {
                string reginalOfficerDiaryNo = string.Empty;
                if (ApoTitle.InnerText != "" && txtComments.Text != "")
                {
                    DataSet dsRODiaryNum = APOBAL.Instance.GetRegionalOfficerDiaryNumber(Convert.ToInt32(Session["APOFileId"]));
                    if (dsRODiaryNum.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsRODiaryNum.Tables[0].Rows[0];
                        if (!string.IsNullOrEmpty(dr[0].ToString()) || dr[0] != DBNull.Value)
                        {
                            reginalOfficerDiaryNo = dr["RegionalOfficerDiaryNumner"].ToString();
                            APOBAL.Instance.UpdateStatus(Convert.ToInt32(Session["APOFileId"]), 12, false, txtComments.Text, reginalOfficerDiaryNo, null);
                            string strSuccess = "APO status has been updated successfully.";
                            vmSuccess.Message = strSuccess;
                            FlashMessage.InfoMessage(vmSuccess.Message);
                            Session["apoFileId"] = null;
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
                else
                {
                    string strError = "Please provide remarks / comments before taking action.";
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

        protected void btnModify_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditApo.aspx", false);
        }

        
    }
}