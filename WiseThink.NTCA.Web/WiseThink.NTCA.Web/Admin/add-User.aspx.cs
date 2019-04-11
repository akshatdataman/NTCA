using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.BAL;
using System.Data;
using WiseThink.NTCA.App_Code;
using System.Configuration;
using System.Net.Mail;
using System.Web.Services;
using System.Data.SqlClient;
using Notifications;
using System.Text.RegularExpressions;

namespace WiseThink.NTCA.Admin
{
    public partial class add_User : BasePage
    {
        NotificationsSender oNotySender = new NotificationsSender();
        //protected void Page_PreInIt(object sender, EventArgs e)
        //{
        //    if (Session[AntiXsrfToken.CSRFToken] != null)
        //    {
        //        var requestCookie = Request.Cookies[AntiXsrfToken.AntiXsrfTokenKey];
        //        Guid requestCookieGuidValue;
        //        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        //        {
        //            if (requestCookie.Value == Session[AntiXsrfToken.CSRFToken].ToString())
        //            {
        //                //Stop Responce Here.
        //                Response.Redirect("#");
        //            }
        //        }
        //    }
        //}
        int UserId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //UserId = Convert.ToInt32(Request.QueryString["UserId"]);
                //TigerReserveDiv.Style.Add("display", "none");
                
                if (!IsPostBack)
                {
                    BindControls();
                    if (Session["UserId"] != null)
                    {
                        UserId = Convert.ToInt32(Session["UserId"]);
                        if (UserId != 0)
                        {
                            LoadData(UserId);
                            UserHeader.InnerText = "Edit User Details";
                            btnSubmit.Text = "Update";
                            txtUsername.Attributes.Add("readonly", "readonly");

                            if (ddlRole.SelectedValue == "6")
                            {
                                ddlTigerReserve.Enabled = true;
                                ddlTigerReserve.BackColor = System.Drawing.Color.Transparent;
                                
                            }
                            else
                            {
                                ddlTigerReserve.Enabled = false;
                                ddlTigerReserve.BackColor = System.Drawing.Color.Transparent;
                            }
                        }
                    }
                    UserBAL.Instance.InsertAuditTrailDetail("Visited Add / Edit User page", "Manage User");
                }
            }
            catch (Exception ex)
            {
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        /// <summary>
        /// To Bind the Dropdown Controls
        /// </summary>
        private void BindControls()
        {
            DataSet dsUser = UserBAL.Instance.GetMasterDataForUserModule();

            ddlCountry.DataSource = dsUser.Tables[0];
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataBind();


            ddlState.DataSource = dsUser.Tables[1];
            ddlState.DataValueField = "StateId";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();
            ddlState.Items.Insert(0, "Select");

            ddlTitle.DataSource = dsUser.Tables[3];
            ddlTitle.DataValueField = "TitleId";
            ddlTitle.DataTextField = "Title";
            ddlTitle.DataBind();
            ddlTitle.Items.Insert(0, "Select");

            ddlDesignation.DataSource = dsUser.Tables[4];
            ddlDesignation.DataValueField = "DesignationId";
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, "Select");

            ddlRole.DataSource = dsUser.Tables[5];
            ddlRole.DataValueField = "RoleId";
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, "Select");

            ddlTigerReserve.DataSource = dsUser.Tables[6];
            ddlTigerReserve.DataValueField = "TigerReserveId";
            ddlTigerReserve.DataTextField = "TigerReserveName";
            ddlTigerReserve.DataBind();
            ddlTigerReserve.Items.Insert(0, "Select");
        }
        private void LoadData(int _userId)
        {
            try
            {
                DataSet ds = UserBAL.Instance.GetUserDetails(_userId);
                txtUsername.Text = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]);
                ddlTitle.Text = Convert.ToString(ds.Tables[0].Rows[0]["Title"]);
                txtFirstName.Text = Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]);
                txtMiddleName.Text = Convert.ToString(ds.Tables[0].Rows[0]["MiddleName"]);
                txtLastName.Text = Convert.ToString(ds.Tables[0].Rows[0]["LastName"]);
                ddlDesignation.Text = Convert.ToString(ds.Tables[0].Rows[0]["DesignationId"]);
                //if (ds.Tables[0].Rows[0]["DateOfBirth"].ToString() != string.Empty)
                //txtDateOfBirth.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateOfBirth"]).Date.ToShortDateString();
                if ((string)ds.Tables[0].Rows[0]["Gender"] == "1")
                    rbtnMale.Checked = true;
                else
                    rbtnFemale.Checked = true;
                //ddlGender.Text = Convert.ToString(ds.Tables[0].Rows[0]["Gender"]);
                ddlRole.Text = Convert.ToString(ds.Tables[0].Rows[0]["Role"]);
                ddlRole.Enabled = false;
                ddlRole.BackColor = System.Drawing.Color.Transparent;
                ddlTigerReserve.Text=Convert.ToString(ds.Tables[0].Rows[0]["TigerReserveId"]);
                txtArea.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                txtPincode.Text = Convert.ToString(ds.Tables[0].Rows[0]["PinCode"]);
                txtCity.Text = Convert.ToString(ds.Tables[0].Rows[0]["City"]);
                ddlCountry.Text = Convert.ToString(ds.Tables[0].Rows[0]["Country"]);
                ddlState.Text = Convert.ToString(ds.Tables[0].Rows[0]["State"]);
                txtDistrict.Text = Convert.ToString(ds.Tables[0].Rows[0]["District"]);
                txtPhoneNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["PhoneNumber"]);
                txtMobileNO.Text = Convert.ToString(ds.Tables[0].Rows[0]["MobileNumber"]);
                txtFaxNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["FaxNumber"]);
                txtEmail.Text = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
                if (!ds.Tables[0].Rows[0]["IsActive"].IsDBNull())
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    bool IsActive = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);
                    chkIsActive.Checked = IsActive;
                }
                else
                    chkIsActive.Checked = false;
                if (ddlRole.SelectedValue == "6")
                {
                    ddlTigerReserve.Text = Convert.ToString(ds.Tables[0].Rows[0]["TigerReserveId"]);
                    ddlTigerReserve.Enabled = true;
                    ddlTigerReserve.BackColor = System.Drawing.Color.Transparent;
                }
                else
                {
                    ddlTigerReserve.Text = "Select";
                    ddlTigerReserve.Enabled = false;
                    ddlTigerReserve.CssClass = ddlTigerReserve.CssClass.Replace("req", "");
                }
            }
            catch (Exception ex)
            {
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        protected bool ValidateCaptcha()
        {
            //if (!txtUniquCharacter.Text.Equals(Session["CaptchaImageText"].ToString()))
            //{
            //    //lblCaptchError.Text = "Captcha Code does not match! please try again.";
            //    string ErrMessage = "Captcha Code does not match! please try again.";
            //    vmError.Message = ErrMessage;
            //    FlashMessage.ErrorMessage(vmError.Message);
            //    txtUniquCharacter.Text = "";
            //    UserBAL.Instance.GenerateRandomCode();
            //    return false;
            //}
            return true;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateEmail(txtEmail.Text))
                return;
            if (!ValidateCaptcha())
            {
                return;
            }
            User user = new User();
            UserId = Convert.ToInt32(Session["UserId"]);
            if (UserId != 0)
            {
               
                UpdateUser(user, UserId);
                string strSuccess = "User has been updated successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.ErrorMessage(vmSuccess.Message);
                //oNotySender.SendUserRegisterNotifications(user.UserName);
                UserBAL.Instance.InsertAuditTrailDetail("Updated User details", "Manage User");
            }
            else
            {
                CreateUser(user);
                string strSuccess = "User has been registered successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.ErrorMessage(vmSuccess.Message);
                oNotySender.SendUserRegisterNotifications(txtUsername.Text);
                Clear();
                UserBAL.Instance.InsertAuditTrailDetail("Created New User", "Manage User");
            }

        }
        private bool ValidateEmail(string Eemail)
        {
            Regex regex = new Regex("(?<user>[^@]+)@(?<host>.+)");
            Match match = regex.Match(Eemail.ToString());
            if (match.Success)
                return true;
            else
                return false;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Users.aspx");
        }
        private void CreateUser(User user)
        {
            try
            {
                Session.Remove("UserId");
                user.UserName = txtUsername.Text;
                user.Title = ddlTitle.SelectedValue;
                user.FirstName = txtFirstName.Text;
                user.MiddleName = txtMiddleName.Text;
                user.LastName = txtLastName.Text;
                if (ddlDesignation.SelectedIndex != 0)
                    user.Designation = Convert.ToInt32(ddlDesignation.SelectedValue);
                user.Password = UserBAL.Instance.GenerateRandomCode();
                ViewState["DefaultPassword"] = user.Password;
                user.Password = MD5HASH.GetMD5HashCode(ViewState["DefaultPassword"].ToString().Trim());
                //user.DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text.ToString());
                if (rbtnMale.Checked == true)
                    user.Gender = "1";
                else
                    user.Gender = "2";
                //user.Gender = ddlGender.SelectedValue;
                user.Role = ddlRole.SelectedValue;
                user.Address = txtArea.Text;
                user.PinCode = txtPincode.Text;
                user.City = txtCity.Text;
                user.PhoneNumber = txtPhoneNo.Text;
                user.District = txtDistrict.Text;
                user.MobileNumber = txtMobileNO.Text;
                user.PhoneNumber = txtPhoneNo.Text;
                user.State = ddlState.SelectedValue;
                user.Country = ddlCountry.SelectedValue;
                user.Email = txtEmail.Text;
                user.FaxNumber = txtFaxNo.Text;
                if (chkIsActive.Checked)
                    user.IsActive = true;
                else
                    user.IsActive = false;
                if (ddlRole.SelectedValue == "6")
                    user.TigerReserveId = Convert.ToInt32(ddlTigerReserve.SelectedValue);
                UserBAL.Instance.CreateUser(user);
                //Response.Redirect("~/Admin/Users.aspx");
                SendMail(user.UserName, ViewState["DefaultPassword"].ToString());
                Clear();
            }
            catch (Exception ex)
            {
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        private void UpdateUser(User user, int _userId)
        {
            try
            {
                Session["Updatebtn"] = 1;
                user.UserName = txtUsername.Text;
                user.Title = ddlTitle.SelectedValue;
                user.FirstName = txtFirstName.Text;
                user.MiddleName = txtMiddleName.Text;
                user.LastName = txtLastName.Text;
                if (ddlDesignation.SelectedIndex != 0)
                    user.Designation = Convert.ToInt32(ddlDesignation.SelectedValue);
                user.Password = UserBAL.Instance.GenerateRandomCode();
                //user.DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text.ToString());
                if (rbtnMale.Checked == true)
                    user.Gender = "1";
                else
                    user.Gender = "2";
                //user.Gender = ddlGender.SelectedValue;
                user.Role = ddlRole.SelectedValue;
                if(ddlTigerReserve.SelectedIndex!=0)
                user.TigerReserveId = Convert.ToInt32(ddlTigerReserve.SelectedValue);
                user.Address = txtArea.Text;
                user.PinCode = txtPincode.Text;
                user.City = txtCity.Text;
                user.PhoneNumber = txtPhoneNo.Text;
                user.District = txtDistrict.Text;
                user.MobileNumber = txtMobileNO.Text;
                user.PhoneNumber = txtPhoneNo.Text;
                user.State = ddlState.SelectedValue;
                user.Country = ddlCountry.SelectedValue;
                user.Email = txtEmail.Text;
                user.FaxNumber = txtFaxNo.Text;
                if (chkIsActive.Checked)
                    user.IsActive = true;
                else
                    user.IsActive = false;
                if (ddlRole.SelectedValue == "6")
                    user.TigerReserveId = Convert.ToInt32(ddlTigerReserve.SelectedValue);
                UserBAL.Instance.UpdateUser(user, _userId);
                Response.Redirect("~/Admin/Users.aspx",false);
            }
            catch (Exception ex)
            {
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        /// <summary>
        /// Check whether user exist or not
        /// </summary>
        /// <param name="username"></param>
        /// <returns>returnValue</returns>
        [WebMethod]
        public static string CheckUserName(string username)
        {
            string returnValue = string.Empty;
            string connection = ConfigurationManager.ConnectionStrings["NtcaConnectionString"].ToString();
            SqlConnection con = new SqlConnection(connection);
            SqlCommand sqlCmd = new SqlCommand("select count(UserName) from dbo.UserProfile where UserName='" + username + "'", con);
            con.Open();
            int result = int.Parse((sqlCmd.ExecuteScalar().ToString()));
            if (result == 1)
            {
                returnValue = "<font color='#cc0000'><b>'" + username + "'</b> is already in use.</font>";
            }
            else if (result == 0)
            {
                returnValue = "Available";
            }
            return returnValue;
        }       
        private void Clear()
        {
            txtUsername.Text = "";
            ddlTitle.SelectedIndex = -1;
            txtFirstName.Text =  "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            //txtDateOfBirth.Text = Convert.ToString(DateTime.Now.Date);
            //ddlGender.SelectedIndex = -1;
            rbtnMale.Checked = false;
            rbtnFemale.Checked = false;
            ddlDesignation.SelectedIndex = -1;
            ddlRole.SelectedIndex = -1;
            ddlTigerReserve.SelectedIndex = -1;
            ddlCountry.SelectedIndex = -1;
            ddlState.SelectedIndex = -1;
            txtDistrict.Text = "";
            txtCity.Text = "";
            txtArea.Text = "";
            //ddlQuestion.SelectedIndex = -1;
            txtPincode.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNO.Text = "";
            txtFaxNo.Text = "";
            txtEmail.Text = "";
            //txtAnswer.Text = "";
            //txtUniquCharacter.Text = "";
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int _stateId = Convert.ToInt32(ddlState.SelectedItem.Value);
            //ddlDistrict.DataSource = UserBAL.Instance.GetDistrictBasedOnState(_stateId);
            //ddlDistrict.DataValueField = "DistrictId";
            //ddlDistrict.DataTextField = "DistrictName";
            //ddlDistrict.DataBind();
        }

        protected void lbtnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            //UserBAL.Instance.GenerateRandomCode();
            //txtUniquCharacter.Text = "";
        }
        /// <summary>
        /// Method For sending Default Password through e-mail
        /// </summary>
        protected void SendMail(string userName, string defaultPassword)
        {
            try
            {
                string strMailBody = BuildFormat();
                if (!string.IsNullOrEmpty(strMailBody))
                {
                    string strMailForm = ConfigurationManager.AppSettings["MailFrom"].ToString();
                    string strEmail = txtEmail.Text.Trim().ToString();
                    string strMailSubject = ConfigurationManager.AppSettings["MailSubjectForDefaultPwd"].ToString();
                    strMailBody = strMailBody.Replace("{0}", userName);
                    strMailBody = strMailBody.Replace("{1}", userName);
                    strMailBody = strMailBody.Replace("{2}", defaultPassword);
                    MailMessage oMailMessage = new MailMessage(strMailForm, strEmail, strMailSubject, strMailBody);
                    if (defaultPassword != null)
                    {
                        if (MailManager.SendMail(oMailMessage))
                        {
                            string strSuccess = "The password has been sent to your email ID. Please check your Inbox.";
                            vmSuccess.Message = strSuccess;
                            FlashMessage.ErrorMessage(vmError.Message);
                        }
                        else
                        {
                            string strFailed = MailManager.ErrorMessage;
                            vmError.Message = strFailed;
                            FlashMessage.ErrorMessage(vmError.Message);
                        }
                    }
                    else
                    {
                        string strInvalid = "Password can not be sent to this email-id.";
                        vmError.Message = strInvalid;
                        FlashMessage.ErrorMessage(vmError.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        
        /// <summary>
        /// Method for building mailbody.
        /// </summary>
        /// <returns>strMailBody</returns>
        public string BuildFormat()
        {
            string strMailBody = "";
            using (TextReader tr = new StreamReader(Server.MapPath("~/EmailTemplates/DefaultPassword.htm")))
            {
                strMailBody = tr.ReadToEnd();
            }
            return strMailBody;
        }

        protected void chkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsActive.Checked)
                chkIsActive.Text = "Active";
            else
                chkIsActive.Text = "Inactive";
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRole.SelectedValue == "6")
            {
                ddlTigerReserve.Attributes.Add("req", "1");
                ddlTigerReserve.Enabled = true;
                ddlTigerReserve.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                //ddlTigerReserve.Attributes.Add("readonly", "true");
                ddlTigerReserve.Enabled = false;
                ddlTigerReserve.BackColor = System.Drawing.Color.Transparent;
                ddlTigerReserve.Attributes.Remove("req");
                ddlTigerReserve.SelectedIndex = -1;
                ddlState.SelectedIndex = -1;
                ddlTigerReserve.CssClass = ddlTigerReserve.CssClass.Replace("req", "");
                //BindControls();
            }

        }

        protected void ddlTigerReserve_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTigerReserve.SelectedIndex != 0)
            {
                int tigerReserveId = Convert.ToInt32(ddlTigerReserve.SelectedValue);
                DataSet dsTrState = UserBAL.Instance.GetTigerReserveState(tigerReserveId);
                ddlState.DataSource = dsTrState;
                ddlState.DataValueField = "StateId";
                ddlState.DataTextField = "StateName";
                ddlState.DataBind();
                ddlState.Items.Insert(0, "Select");
            }
        }
    }
}