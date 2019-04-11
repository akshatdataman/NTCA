using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.App_Code;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.DataEntity;

namespace WiseThink.NTCA.Web.CWW_Secretary
{
    public partial class MonitoringCentral_StateShare : BasePage
    {
        CentralStateShare centralStateShare = new CentralStateShare();
        CommonClass cc = new CommonClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            //FistInstallmentDiv.Style.Add("display", "none");
            //SecondInstallmentDiv.Style.Add("display", "none");
            txtFirstRelease.Attributes.Add("readonly", "true");
            //Commented on 3 march, since client need econd installment editable
            //txtSecondRelease.Attributes.Add("readonly", "true");
            IFDdiv.Style.Add("display", "none");
            if (!IsPostBack)
            {
                GetCurrentYearApprovedApoTitle();
                if (ddlApoTitle.SelectedIndex != 0)
                {
                    GetCentralStateShare();
                }
                else
                {
                    FistInstallmentDiv.Style.Add("display", "none");
                    SecondInstallmentDiv.Style.Add("display", "none");
                    cgvCentralStateShare.DataSource = null;
                    cgvCentralStateShare.DataBind();
                }
                UserBAL.Instance.InsertAuditTrailDetail("Visited Monitoring Central-State Share Page", "Monitoring Central-State Share");
            }
            //if (!string.IsNullOrEmpty(txtIfdDiaryNumber.Text))
            //{
            //    IFDdiv.Style.Add("display", "none");
            //    FistInstallmentDiv.Style.Add("display", "block");
            //    SecondInstallmentDiv.Style.Add("display", "block");
            //}
            //else
            //{
            //    IFDdiv.Style.Add("display", "none");
            //    FistInstallmentDiv.Style.Add("display", "none");
            //    SecondInstallmentDiv.Style.Add("display", "none");
            //}
        }
        private void GetCentralStateShare()
        {
            int tigerReserveId;
            if (ddlApoTitle.SelectedIndex == 0)
                tigerReserveId = -1;
            else
                tigerReserveId = Convert.ToInt32(ddlApoTitle.SelectedValue);
            DataSet dsTigerReserveId = CentralStateShareBAL.Instance.GetAPOStateAndTigerReserveId(tigerReserveId);
            if (dsTigerReserveId != null)
            {
                if (dsTigerReserveId.Tables[0].Rows.Count == 1)
                {
                    DataRow drTrId = dsTigerReserveId.Tables[0].Rows[0];
                    if (drTrId[0] != DBNull.Value)
                    {
                        centralStateShare.TigerReserveId = Convert.ToInt32(drTrId["TigerReserveId"]);
                        centralStateShare.CurrentFinancialYear = Convert.ToString(drTrId["FinancialYear"]);
                        centralStateShare.PreviousFinancialYear = cc.GetPreviousFinancialYear(centralStateShare.CurrentFinancialYear);
                    }
                }

                DataSet ds = CentralStateShareBAL.Instance.GetCentralStateShare(centralStateShare.TigerReserveId, centralStateShare.PreviousFinancialYear);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["IFDDiaryNumber"])))
                    {
                        txtIfdDiaryNumber.Text = Convert.ToString(dr["IFDDiaryNumber"]);
                        DateTime dt = new DateTime();
                        string date = Convert.ToString(dr["IFDate"]);
                        dt = Convert.ToDateTime(date);
                        txtIfdDate.Text = dt.ToShortDateString();
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["IFDDiaryNumber"])))
                    {
                        if (Convert.ToDouble(dr["FirstStateRelease"]) == 0.00000)
                        {
                            FistInstallmentDiv.Style.Add("display", "block");
                            SecondInstallmentDiv.Style.Add("display", "none");
                        }
                        else if (Convert.ToDouble(dr["SecondStateRelease"]) == 0.00000)
                        {
                            FistInstallmentDiv.Style.Add("display", "none");
                            SecondInstallmentDiv.Style.Add("display", "block");
                        }
                        else
                        {
                            FistInstallmentDiv.Style.Add("display", "none");
                            SecondInstallmentDiv.Style.Add("display", "none");
                        }
                    }
                    else
                    {
                        FistInstallmentDiv.Style.Add("display", "none");
                        SecondInstallmentDiv.Style.Add("display", "none");
                    }
                    txtFirstRelease.Text = Convert.ToString(dr["StateFirstInstallmentToBeRelease"]);
                    txtSecondRelease.Text = Convert.ToString(dr["StateSecondInstallmentToBeRelease"]);
                    centralStateShare.Quantity = Convert.ToDouble(dr["Quantity"]);
                    centralStateShare.CFYSanctionAmount = Convert.ToDouble(dr["SanctionAmount"]);
                    centralStateShare.CentralShare = Convert.ToDouble(dr["CentralShare"]);
                    centralStateShare.StateShare = Convert.ToDouble(dr["StateShare"]);
                    centralStateShare.FirstStateRelease = Convert.ToDouble(dr["StateFirstInstallmentToBeRelease"]);
                    centralStateShare.SecondStateRelease = Convert.ToDouble(dr["StateSecondInstallmentToBeRelease"]);
                    centralStateShare.UnspentAdjustedAmount = Convert.ToDouble(dr["UnspentAmount"]);
                    cgvCentralStateShare.DataSource = ds;
                    cgvCentralStateShare.DataBind();
                    Session["centralStateShare"] = centralStateShare;
                }
            }

        }
        private void GetCurrentYearApprovedApoTitle()
        {

            UserInfo uinfo = new UserInfo();
            uinfo = HttpContext.Current.Session["UserInfo"] as UserInfo;
            DataSet dsCurrentApo = APOBAL.Instance.GetApprovedAPOTitleForCWW(uinfo.UserName);
            ddlApoTitle.DataSource = dsCurrentApo;
            ddlApoTitle.DataValueField = "APOFileId";
            ddlApoTitle.DataTextField = "APOTitle";
            ddlApoTitle.DataBind();
            ddlApoTitle.Items.Insert(0, "Select");
        }
        protected void cgvCentralStateShare_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvCentralStateShare.PageIndex = e.NewPageIndex;
            GetCentralStateShare();
        }
        protected void cgvCentralStateShare_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            cgvCentralStateShare.EditIndex = -1;
            GetCentralStateShare();
        }

        protected void cgvCentralStateShare_RowEditing(object sender, GridViewEditEventArgs e)
        {
            cgvCentralStateShare.EditIndex = e.NewEditIndex;
            GetCentralStateShare();
        }

        protected void cgvCentralStateShare_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label lblAPOFileId = cgvCentralStateShare.Rows[e.RowIndex].FindControl("lblAPOFileId") as Label;
                int apoFileId = Convert.ToInt32(lblAPOFileId.Text);
                TextBox txtFirstReleasedAmount = cgvCentralStateShare.Rows[e.RowIndex].FindControl("txtFirstReleasedAmount") as TextBox;
                TextBox txtSecondReleasedAmount = cgvCentralStateShare.Rows[e.RowIndex].FindControl("txtSecondReleasedAmount") as TextBox;
                string firstReleasedAmount = txtFirstReleasedAmount.Text;
                string secondReleasedAmount = txtSecondReleasedAmount.Text;
                DesignationBAL.Instance.UpdateDesignation(apoFileId, firstReleasedAmount);

                cgvCentralStateShare.EditIndex = -1;
                GetCentralStateShare();

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

        protected void btnFirstRelease_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["centralStateShare"] != null)
                {
                    centralStateShare = (CentralStateShare)Session["centralStateShare"];
                    centralStateShare.IFDDiaryNumber = txtIfdDiaryNumber.Text;
                    if (!string.IsNullOrEmpty(txtIfdDate.Text))
                        centralStateShare.IFDDate = Convert.ToDateTime(txtIfdDate.Text);
                    else
                        centralStateShare.IFDDate = DateTime.Now;
                    CentralStateShareBAL.Instance.UpdateStateFirstReleasedAmount(centralStateShare);
                    string strSuccess = "First installment of state share has been released successfully.";
                    vmSuccess.Message = strSuccess;
                    FlashMessage.ErrorMessage(vmSuccess.Message);
                    Session["centralStateShare"] = null;
                    GetCentralStateShare();
                    FistInstallmentDiv.Style.Add("display", "none");
                    SecondInstallmentDiv.Style.Add("display", "block");
                    UserBAL.Instance.InsertAuditTrailDetail("Released First Installment of State Share", "Monitoring Central-State Share");
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
        private void ReleaseFund(int tigerReserveId, string previousFinancialYear)
        {

        }
        protected void btnSecondRelease_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["centralStateShare"] != null)
                {
                    centralStateShare = (CentralStateShare)Session["centralStateShare"];
                    CentralStateShareBAL.Instance.UpdateStateSecondReleasedAmount(centralStateShare);
                    string strSuccess = "Second installment of state share has been released successfully.";
                    vmSuccess.Message = strSuccess;
                    FlashMessage.ErrorMessage(vmSuccess.Message);
                    Session["centralStateShare"] = null;
                    GetCentralStateShare();
                    IFDdiv.Style.Add("display", "none");
                    FistInstallmentDiv.Style.Add("display", "none");
                    SecondInstallmentDiv.Style.Add("display", "none");
                    UserBAL.Instance.InsertAuditTrailDetail("Released Second Installment of State Share", "Monitoring Central-State Share");
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

        protected void ddlApoTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlApoTitle.SelectedIndex != 0)
            {
                centralStateShare.APOId = Convert.ToInt32(ddlApoTitle.SelectedValue);
                //IFDdiv.Style.Add("display", "block");
                //FistInstallmentDiv.Style.Add("display", "block");
                GetCentralStateShare();
            }
            else
            {
                cgvCentralStateShare.DataSource = null;
                cgvCentralStateShare.DataBind();
            }
        }
    }
}