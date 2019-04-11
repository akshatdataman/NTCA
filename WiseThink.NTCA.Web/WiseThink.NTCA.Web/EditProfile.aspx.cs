using System;
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
using WiseThink.NTCA.BAL.Authorization;

namespace WiseThink.NTCA
{
    public partial class EditProfile : BasePage
    {
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            #region CSRF
            //First, check for the existence of the Anti-XSS cookie
            //  var requestCookie = Request.Cookies[AntiXsrfToken.AntiXsrfTokenKey];
            var requestCookie = (string)Session["xsrf"];
            Guid requestCookieGuidValue;


            //If the CSRF cookie is found, parse the token from the cookie.
            //Then, set the global page variable and view state user
            //key. The global variable will be used to validate that it matches in the view state form field in the Page.PreLoad
            //method.
            if (requestCookie != null && Guid.TryParse(requestCookie, out requestCookieGuidValue))
            {
                //Set the global token variable so the cookie value can be
                //validated against the value in the view state form field in
                //the Page.PreLoad method.
                _antiXsrfTokenValue = requestCookie;
                //Set the view state user key, which will be validated by the
                //framework during each request               
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            //If the CSRF cookie is not found, then this is a new session.
            else
            {
                GenerateCSRFCookie();
            }

            Page.PreLoad += master_Page_PreLoad;
            #endregion
        }
        private void GenerateCSRFCookie()
        {
            //Generate a new Anti-XSRF token
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            //Set the view state user key, which will be validated by the
            //framework during each request
            Page.ViewStateUserKey = _antiXsrfTokenValue;
            Session["xsrf"] = _antiXsrfTokenValue;
            //Create the non-persistent CSRF cookie
            //var responseCookie = new HttpCookie(AntiXsrfToken.AntiXsrfTokenKey)
            //{
            //    //Set the HttpOnly property to prevent the cookie from
            //    //being accessed by client side script
            //    HttpOnly = true,

            //    //Add the Anti-XSRF token to the cookie value
            //    Value = _antiXsrfTokenValue
            //};

            ////If we are using SSL, the cookie should be set to secure to
            ////prevent it from being sent over HTTP connections
            //if (Request.IsSecureConnection)
            //    responseCookie.Secure = true;

            ////Add the CSRF cookie to the response
            //Response.Cookies.Set(responseCookie);
        }
        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            //During the initial page load, add the Anti-XSRF token and user
            //name to the ViewState
            if (!IsPostBack)
            {
                //Set Anti-XSRF token

                ViewState[AntiXsrfToken.AntiXsrfTokenKey] = Page.ViewStateUserKey;
                Session[AntiXsrfToken.CSRFToken] = Page.ViewStateUserKey;
                //If a user name is assigned, set the user name
                ViewState[AntiXsrfToken.AntiXsrfUserNameKey] = AuthoProvider.User;
            }
            //During all subsequent post backs to the page, the token value from
            //the cookie should be validated against the token in the view state
            //form field. Additionally user name should be compared to the
            //authenticated users name
            else
            {
                //Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfToken.AntiXsrfTokenKey] != _antiXsrfTokenValue || (string)ViewState[AntiXsrfToken.AntiXsrfUserNameKey] != AuthoProvider.User)
                {
                    Response.Redirect("~/nocookie.htm");
                    // throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }

            }

        }
        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if (Session[AntiXsrfToken.CSRFToken] != null)
            {
                //var requestCookie = Request.Cookies[AntiXsrfToken.AntiXsrfTokenKey];
                //Guid requestCookieGuidValue;
                //if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
                //{
                //    if (requestCookie.Value == Session[AntiXsrfToken.CSRFToken].ToString())
                //    {
                //        //Stop Responce Here.
                //        Response.Redirect("~/nocookie.htm");
                //    }
                //}
            }
        }
        int UserId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                if (!IsPostBack)
                {
                    string userName = WiseThink.NTCA.BAL.Authorization.AuthoProvider.User;
                    UserId = UserBAL.Instance.GetLoggedInUserId(userName);
                    Session["UserId"] = UserId;
                    BindControls();
                    LoadData(UserId);
                    btnSubmit.Text = "Update";
                    txtUsername.Attributes.Add("readonly", "readonly");
                    
                    ddlDesignation.Enabled = false;
                    ddlDesignation.BackColor = System.Drawing.Color.Transparent;
                    ddlRole.Enabled = false;
                    ddlRole.BackColor = System.Drawing.Color.Transparent;
                    UserBAL.Instance.InsertAuditTrailDetail("Visited Edit Profile Page", "Edit Profile");
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
        /// <summary>
        /// To Bind the Dropdown Controls
        /// </summary>
        private void BindControls()
        {
            DataSet dsUser = UserBAL.Instance.GetMasterDataForUserModule();
            ddlRole.DataSource = dsUser.Tables[5];
            ddlRole.DataValueField = "RoleId";
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, "Select");

            ddlDesignation.DataSource = dsUser.Tables[4];
            ddlDesignation.DataValueField = "DesignationId";
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, "Select");

            ddlTitle.DataSource = dsUser.Tables[3];
            ddlTitle.DataValueField = "TitleId";
            ddlTitle.DataTextField = "Title";
            ddlTitle.DataBind();
            ddlTitle.Items.Insert(0, "Select");

            ddlCountry.DataSource = dsUser.Tables[0];
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataBind();


            ddlState.DataSource = dsUser.Tables[1];
            ddlState.DataValueField = "StateId";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();
            ddlState.Items.Insert(0, "Select");


        }
        private void LoadData(int _userId)
        {
            try
            {
                DataSet ds = UserBAL.Instance.GetUserDetails(_userId);
                txtUsername.Text = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]);
                ddlTitle.Text = Convert.ToString(ds.Tables[0].Rows[0]["Title"]);
                txtFirstName.Text = Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]);
                txtMiddleName.Text =Convert.ToString(ds.Tables[0].Rows[0]["MiddleName"]);
                txtLastName.Text = Convert.ToString(ds.Tables[0].Rows[0]["LastName"]);
                ddlDesignation.Text = Convert.ToString(ds.Tables[0].Rows[0]["DesignationId"]);
                if ((string)ds.Tables[0].Rows[0]["Gender"] == "1")
                    rbtnMale.Checked = true;
                else
                    rbtnFemale.Checked = true;
                ddlRole.Text = Convert.ToString(ds.Tables[0].Rows[0]["Role"]);
                ddlRole.Enabled = false;
                ddlRole.BackColor = System.Drawing.Color.Transparent;
                txtArea.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                txtPincode.Text = Convert.ToString(ds.Tables[0].Rows[0]["PinCode"]);
                txtCity.Text = Convert.ToString(ds.Tables[0].Rows[0]["City"]);
                ddlCountry.Text = Convert.ToString(ds.Tables[0].Rows[0]["Country"]);
                ddlState.Text = Convert.ToString(ds.Tables[0].Rows[0]["State"]);
                txtDistrict.Text = Convert.ToString(ds.Tables[0].Rows[0]["District"]);
                txtPhoneNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["PhoneNumber"]);
                txtMobileNO.Text = Convert.ToString(ds.Tables[0].Rows[0]["MobileNumber"]);
                txtFaxNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["FaxNumber"]);
                txtEmail.Text =Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
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
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {    
            User user = new User();
            if (Convert.ToInt32(Session["UserId"]) != 0)
            {
                UpdateUserProfile(user, Convert.ToInt32(Session["UserId"]));
                string strSuccess = "User has been updated successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.ErrorMessage(vmSuccess.Message);
                UserBAL.Instance.InsertAuditTrailDetail("Updated Edit Profile ", "Edit Profile");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string url = Request.UrlReferrer.AbsolutePath;
            Response.Redirect(url);
        }
        private void UpdateUserProfile(User user, int _userId)
        {
            try
            {
                user.UserName = txtUsername.Text;
                user.Title = ddlTitle.SelectedValue;
                user.FirstName = txtFirstName.Text;
                user.MiddleName = txtMiddleName.Text;
                user.LastName = txtLastName.Text;
                if (ddlDesignation.SelectedIndex != 0)
                    user.Designation = Convert.ToInt32(ddlDesignation.SelectedValue);
                user.Password = UserBAL.Instance.GenerateRandomCode();
                if (rbtnMale.Checked == true)
                    user.Gender = "1";
                else
                    user.Gender = "2";
                user.Role = ddlRole.SelectedValue;
                user.Address = txtArea.Text;
                user.PinCode = txtPincode.Text;
                user.City = txtCity.Text;
                user.PhoneNumber = txtPhoneNo.Text;
                user.District = txtDistrict.Text;
                user.MobileNumber =txtMobileNO.Text;
                user.PhoneNumber = txtPhoneNo.Text;
                user.State = ddlState.SelectedValue;
                user.Country = ddlCountry.SelectedValue;
                user.Email = txtEmail.Text;
                user.FaxNumber = txtFaxNo.Text;
                UserBAL.Instance.EditProfile(user, _userId);

                //List<Role> userRole = AuthoProvider.LoggedInRoles.ToList();
                //if (userRole.Contains(Role.ADMIN))
                //    Response.Redirect("~/Admin/Home.aspx", false);
                //else if (userRole.Contains(Role.NTCA) || userRole.Contains(Role.REGIONALOFFICER))
                //    Response.Redirect("~/NTCA-RO/Home.aspx", false);
                //else if (userRole.Contains(Role.CWW) || userRole.Contains(Role.SECRETARY))
                //    Response.Redirect("~/CWW-Secretary/Home.aspx", false);
                //else
                //    Response.Redirect("~/FieldDirector/FieldDirectorHome.aspx", false);
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

        protected void btnback_Click(object sender, EventArgs e)
        {
            List<Role> userRole = AuthoProvider.LoggedInRoles.ToList();
            if (userRole.Contains(Role.ADMIN))
                Response.Redirect("~/Admin/Home.aspx", false);
            else if (userRole.Contains(Role.NTCA) || userRole.Contains(Role.REGIONALOFFICER))
                Response.Redirect("~/NTCA-RO/Home.aspx", false);
            else if (userRole.Contains(Role.CWLW) || userRole.Contains(Role.SECRETARY))
                Response.Redirect("~/CWW-Secretary/Home.aspx", false);
            else
                Response.Redirect("~/FieldDirector/FieldDirectorHome.aspx", false);
        }
    }
}