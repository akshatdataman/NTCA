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
    public partial class ManageNonRecurringRecurringRatio : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtStateNonRecurringRatio.Attributes.Add("readonly", "true");
            txtStateRecurringRatio.Attributes.Add("readonly", "true");
            if (!IsPostBack)
            {
                BindTigerReserve();
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

        protected void ddlTigerReserve_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTigerReserve.SelectedValue != "0")
            {
                DataSet dsNRandRecurringRatio = ManageNonRecurringRecurringRatioBAL.Instance.GetNonRecurringAndRecurringRatio(Convert.ToInt32(ddlTigerReserve.SelectedValue));
                if (dsNRandRecurringRatio.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsNRandRecurringRatio.Tables[0].Rows[0];
                    txtCentralNonRecurringRatio.Text = Convert.ToString(dr[DataBaseFields.CentralNonRecurringRatio]);
                    txtStateNonRecurringRatio.Text = Convert.ToString(dr[DataBaseFields.StateNonRecurringRatio]);
                    txtCentralRecurringRatio.Text = Convert.ToString(dr[DataBaseFields.CentralRecurringRatio]);
                    txtStateRecurringRatio.Text = Convert.ToString(dr[DataBaseFields.StateRecurringRatio]);
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int tigerReserveId = Convert.ToInt32(ddlTigerReserve.SelectedValue);
                int centralNonRecurringRatio = Convert.ToInt32(txtCentralNonRecurringRatio.Text);
                int centralRecurringRatio = Convert.ToInt32(txtCentralRecurringRatio.Text);
                int stateNonRecurringRatio = Convert.ToInt32(txtStateNonRecurringRatio.Text);
                int stateRecurringRatio = Convert.ToInt32(txtStateRecurringRatio.Text);
                if (centralNonRecurringRatio <= 100 && centralRecurringRatio <= 100)
                {
                    ManageNonRecurringRecurringRatioBAL.Instance.UpdateRecuringAndNonReecurringRatio(tigerReserveId, centralNonRecurringRatio, centralRecurringRatio, stateNonRecurringRatio, stateRecurringRatio);
                    string strSuccess = "Non-Recurring and Recurring ratio updated successfully.";
                    vmSuccess.Message = strSuccess;
                    FlashMessage.InfoMessage(vmSuccess.Message);
                }
                else
                {
                    string strError = "Non-Recurring ratio should be less than or equal to 100 !";
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

        protected void txtCentralNonRecurringRatio_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCentralNonRecurringRatio.Text))
            {
                int tempNonRecurring;
                if (int.TryParse(txtCentralNonRecurringRatio.Text, out tempNonRecurring))
                {
                    if (tempNonRecurring >= 0 && tempNonRecurring <= 100)
                    {
                        int centralNonRecurringRatio = tempNonRecurring;
                        txtStateNonRecurringRatio.Text = Convert.ToString(100 - centralNonRecurringRatio);
                    }
                    else
                    {
                        string strError = "Central non-recurring ratio should be greater than or equal to '0' OR less than or equal to '100' !";
                        vmError.Message = strError;
                        FlashMessage.ErrorMessage(vmError.Message);
                        return;
                    }
                }
                else
                {
                    string strError = "Entered text is not a number, please enter numeric value and try again !";
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    return;
                }
            }
            
            else
            {
                string strError = "Please enter the central non-recurring ratio !";
                vmError.Message = strError;
                txtStateRecurringRatio.Text = string.Empty;
                txtStateNonRecurringRatio.Text = string.Empty;
                FlashMessage.ErrorMessage(vmError.Message);
                return;
            }
        }

        protected void txtCentralRecurringRatio_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCentralRecurringRatio.Text))
            {
                int tempRecurring;
                if (int.TryParse(txtCentralRecurringRatio.Text, out tempRecurring))
                {
                    if (tempRecurring >= 0 && tempRecurring <= 100)
                    {
                        int centralRecurringRatio = tempRecurring;
                        txtStateRecurringRatio.Text = Convert.ToString(100 - centralRecurringRatio);
                    }
                    else
                    {
                        string strError = "Central recurring ratio should be greater than or equal to '0' OR less than or equal to '100' !";
                        vmError.Message = strError;
                        FlashMessage.ErrorMessage(vmError.Message);
                        return;
                    }
                }
                else
                {
                    string strError = "Entered text is not a number, please enter numeric value and try again !";
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    return;
                }
            }
            else
            {
                string strError = "Please enter the central recurring ratio !";
                vmError.Message = strError;
                txtStateRecurringRatio.Text = string.Empty;
                txtStateNonRecurringRatio.Text = string.Empty;
                FlashMessage.ErrorMessage(vmError.Message);
                return;
            }
        }
    }
}