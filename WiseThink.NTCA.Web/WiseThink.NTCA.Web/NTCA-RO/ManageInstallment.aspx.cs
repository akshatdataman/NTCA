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
    public partial class ManageInstallment : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtSetSecondInstallment.Attributes.Add("readonly", "true");
            if (!IsPostBack)
            {
                BindTigerReserve();
                UserBAL.Instance.InsertAuditTrailDetail("Visited Manage Installment Page", "Manage Installment");
            }
        }
        private void BindTigerReserve()
        {
            DataSet dsTr = TigerReserveBAL.Instance.GetTigerReserveName();
            ddlTigerReserve.DataSource = dsTr;
            ddlTigerReserve.DataValueField = "TigerReserveId";
            ddlTigerReserve.DataTextField = "TigerReserveName";
            ddlTigerReserve.DataBind();
            ddlTigerReserve.Items.Insert(0, "Select");
        }
        private void GetInstallments(int tigerReserveId)
        {
            DataSet dsInstallment = new DataSet();
            dsInstallment = ManageInstallmentBAL.Instance.GetInstallments(tigerReserveId);
            if (dsInstallment.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsInstallment.Tables[0].Rows[0];
                txtSetFirstInstallment.Text = dr[DataBaseFields.FirstInstallment].ToString();
                txtSetSecondInstallment.Text = dr[DataBaseFields.SecondInstallment].ToString();
            }
        }
        protected void ddlTigerReserve_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTigerReserve.SelectedValue != "0")
            {
                GetInstallments(Convert.ToInt32(ddlTigerReserve.SelectedValue));
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSetFirstInstallment.Text) && !string.IsNullOrEmpty(txtSetSecondInstallment.Text))
                {
                    double firstInstallment = Convert.ToDouble(txtSetFirstInstallment.Text);
                    double secondInstallment = Convert.ToDouble(txtSetSecondInstallment.Text);
                    if (firstInstallment <= 100.00 && secondInstallment <= 100.00)
                    {
                        ManageInstallmentBAL.Instance.UpdateInstallments(Convert.ToInt32(ddlTigerReserve.SelectedValue), firstInstallment, secondInstallment);
                        string strSuccess = "Installments has been updated successfully.";
                        vmSuccess.Message = strSuccess;
                        FlashMessage.InfoMessage(vmSuccess.Message);
                        UserBAL.Instance.InsertAuditTrailDetail("Updated Installment Ratio", "Manage Installment");
                    }
                    else
                    {
                        string strError = "First installment should be greater than or equal to 000.00 and less than or equal to 100.00 !";
                        vmError.Message = strError;
                        FlashMessage.ErrorMessage(vmError.Message);
                        return;
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

        protected void txtSetFirstInstallment_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSetFirstInstallment.Text))
            {
                double temp;
                if (double.TryParse(txtSetFirstInstallment.Text, out temp))
                {
                    if (temp >= 000.00 && temp <= 100.00)
                    {
                        double firstInstallment = temp;
                        txtSetSecondInstallment.Text = Convert.ToString(100.00 - Convert.ToDouble(firstInstallment));
                    }
                    else
                    {
                        string strError = "First installment should be greater than or equal to '000.00' OR less than or equal to '100.00' !";
                        vmError.Message = strError;
                        FlashMessage.ErrorMessage(vmError.Message);
                        return;
                    }
                }
                else
                {
                    string strError = "Entered text is not a decimal number, please enter decimal number and try again !";
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    return;
                }
            }
            else
            {
                string strError = "Please enter the first installment !";
                txtSetSecondInstallment.Text = string.Empty;
                vmError.Message = strError;
                FlashMessage.ErrorMessage(vmError.Message);
                return;
            }
        }
    }
}