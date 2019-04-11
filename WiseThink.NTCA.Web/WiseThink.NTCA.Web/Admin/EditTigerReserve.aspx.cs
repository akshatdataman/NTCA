using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.DataEntity.Entities;
using System.Configuration;
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.Admin
{
    public partial class WebForm5 : BasePage
    {
        int TigerReserveId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (!IsPostBack)
                {
                    BindControls();
                    txtDOR.Text = Convert.ToDateTime(DateTime.Now).ToShortDateString();
                    if (Session["TigerReserveId"] != null)
                    {
                        TigerReserveId = Convert.ToInt32(Session["TigerReserveId"]);
                        if (TigerReserveId != 0)
                        {
                            LoadData(TigerReserveId);
                            TigerReserveHeader.InnerText = "Edit Tiger Reserve";
                            btnSubmit.Text = "Update";
                            //Session["TigerReserveId"] = null;
                        }
                    }
                    else
                        Clear();
                    UserBAL.Instance.InsertAuditTrailDetail("Visited Add / Edit Tiger Reserve Page", "Manage Tiger Reserve");
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
        private void BindControls()
        {
            DataSet dsStateUser = TigerReserveBAL.Instance.GetStatesAndFeildDirector();

            ddlState.DataSource = dsStateUser.Tables[0];
            ddlState.DataValueField = "StateId";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();
            ddlState.Items.Insert(0, "Select");

            //ddlFeildDirector.DataSource = dsStateUser.Tables[1];
            //ddlFeildDirector.DataValueField = "UserId";
            //ddlFeildDirector.DataTextField = "FeildDirector";
            //ddlFeildDirector.DataBind();
            //ddlFeildDirector.Items.Insert(0, "Select");

        }
        private void LoadData(int _tigerReserveId)
        {
            DataSet ds = TigerReserveBAL.Instance.GetTigerReserve(_tigerReserveId);
            ddlState.Text = Convert.ToString(ds.Tables[0].Rows[0]["StateId"]);
            txtDistrict.Text = Convert.ToString(ds.Tables[0].Rows[0]["District"]);
            txtCity.Text = Convert.ToString(ds.Tables[0].Rows[0]["City"]);
            txtTigerReserve.Text = Convert.ToString(ds.Tables[0].Rows[0]["TigerReserveName"]);
            txtCoreArea.Text = Convert.ToString(ds.Tables[0].Rows[0]["CoreArea"]);
            txtBufferArea.Text = Convert.ToString(ds.Tables[0].Rows[0]["BufferArea"]);
            txtTotalArea.Text = Convert.ToString(ds.Tables[0].Rows[0]["TotalArea"]);
            if (ds.Tables[0].Rows[0]["DateOfRegistration"].ToString() != string.Empty)
                txtDOR.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateOfRegistration"]).Date.ToShortDateString();
            txtAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
            txtPinCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["PinCode"]);
            txtFieldDirector.Text = Convert.ToString(ds.Tables[0].Rows[0]["FieldDirector"]);
            txtPhoneNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["PhoneNumber"]);
            txtAlternatePhoneNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["AlternatePhoneNumber"]);
            txtMobileNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["MobileNumber"]);
            txtEmail.Text = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);

        }
        private void AddNewTigerReserve(TigerReserve tigerReserve)
        {
            try
            {
                Session.Remove("TigerReserveId");
                tigerReserve.State = ddlState.SelectedValue;
                tigerReserve.District = txtDistrict.Text;
                tigerReserve.City = txtCity.Text;
                tigerReserve.TigerReserveName = txtTigerReserve.Text;
                if (txtCoreArea.Text != "")
                    tigerReserve.CoreArea = txtCoreArea.Text;
                else
                    tigerReserve.CoreArea = "0.0";
                if (txtBufferArea.Text != "")
                    tigerReserve.BufferArea = txtBufferArea.Text;
                else
                    tigerReserve.BufferArea = "0.0";
                txtTotalArea.Text = Convert.ToString(Convert.ToDouble(tigerReserve.CoreArea) + Convert.ToDouble(tigerReserve.BufferArea));
                tigerReserve.TotalArea = txtTotalArea.Text;
                tigerReserve.DateOfRegistration = Convert.ToDateTime(txtDOR.Text.ToString());
                tigerReserve.Address = txtAddress.Text;
                tigerReserve.PinCode = txtPinCode.Text;
                tigerReserve.FeildDirector = txtFieldDirector.Text;
                tigerReserve.PhoneNumber = txtPhoneNumber.Text;
                tigerReserve.AlternatePhoneNumber = txtAlternatePhoneNumber.Text;
                tigerReserve.MobileNumber = txtMobileNumber.Text;
                tigerReserve.Email = txtEmail.Text;
                TigerReserveBAL.Instance.AddTigerReserve(tigerReserve);
                vmSuccess.Message = ConfigurationManager.AppSettings["SaveSuccessMes"].ToString();
                FlashMessage.InfoMessage(vmSuccess.Message);
                Clear();
                //Response.Redirect("TigerReserve.aspx", false);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        private void UpdateTigerReserve(TigerReserve tigerReserve, int _tigerReserveId)
        {
            try
            {
                Session["Updatebtn"] = 1;
                tigerReserve.State = ddlState.SelectedValue;
                tigerReserve.District = txtDistrict.Text;
                tigerReserve.City = txtCity.Text;
                tigerReserve.TigerReserveName = txtTigerReserve.Text;
                if (txtCoreArea.Text != "")
                    tigerReserve.CoreArea = txtCoreArea.Text;
                else
                    tigerReserve.CoreArea = "0.0";
                if (txtBufferArea.Text != "")
                    tigerReserve.BufferArea = txtBufferArea.Text;
                else
                    tigerReserve.BufferArea = "0.0";
                txtTotalArea.Text = Convert.ToString(Convert.ToDouble(tigerReserve.CoreArea) + Convert.ToDouble(tigerReserve.BufferArea));
                tigerReserve.TotalArea = txtTotalArea.Text;
                tigerReserve.DateOfRegistration = Convert.ToDateTime(txtDOR.Text.ToString());
                tigerReserve.Address = txtAddress.Text;
                tigerReserve.PinCode = txtPinCode.Text;
                tigerReserve.FeildDirector = txtFieldDirector.Text;
                tigerReserve.PhoneNumber = txtPhoneNumber.Text;
                tigerReserve.AlternatePhoneNumber = txtAlternatePhoneNumber.Text;
                tigerReserve.MobileNumber = txtMobileNumber.Text;
                tigerReserve.Email = txtEmail.Text;
                TigerReserveBAL.Instance.UpdateTigerReserve(tigerReserve, _tigerReserveId);
                vmSuccess.Message = ConfigurationManager.AppSettings["UpdateSuccessMes"].ToString(); ;
                FlashMessage.InfoMessage(vmSuccess.Message);
                //Response.Redirect("TigerReserve.aspx", false);
                
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateCaptcha())
            {
                return;
            }
            TigerReserve tr = new TigerReserve();
            TigerReserveId = Convert.ToInt32(Session["TigerReserveId"]);
            if (TigerReserveId != 0)
            {
                UpdateTigerReserve(tr, TigerReserveId);
                txtCaptchaCode.Text = "";
            }
            else
            {
                AddNewTigerReserve(tr);
                Clear();
            }

        }
        private void Clear()
        {
            ddlState.SelectedIndex = -1;
            txtDistrict.Text = "";
            txtCity.Text = "";
            txtTigerReserve.Text = "";
            txtDOR.Text = Convert.ToString(DateTime.Now.Date);
            txtAddress.Text = "";
            txtCoreArea.Text = "";
            txtBufferArea.Text = "";
            txtTotalArea.Text = "";
            txtPinCode.Text = "";
            txtFieldDirector.Text = "";
            txtPhoneNumber.Text = "";
            txtAlternatePhoneNumber.Text = "";
            txtMobileNumber.Text = "";
            txtEmail.Text = "";
            txtCaptchaCode.Text = "";
        }
        protected bool ValidateCaptcha()
        {
            if (!txtCaptchaCode.Text.Equals(Session["CaptchaImageText"].ToString()))
            {
                ////lblCaptchError.Text = "Captcha Code does not match! please try again.";
                //string ErrMessage = "Captcha Code does not match! please try again.";
                //vmError.Message = ErrMessage;
                //FlashMessage.ErrorMessage(vmError.Message);
                //txtCaptchaCode.Text = "";
                TigerReserveBAL.Instance.GenerateRandomCode();
                return false;
            }
            return true;
        }

        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //    Session.Remove("TigerReserveId");
        //    Session.Remove("Updatebtn");
        //    Response.Redirect("TigerReserve.aspx", false);
        //}
        
    }
}