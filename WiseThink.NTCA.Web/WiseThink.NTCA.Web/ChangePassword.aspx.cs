using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.App_Code;
using System.Data;
using WiseThink.NTCA.BAL;
using System.Text.RegularExpressions;
using WiseThink.NTCA.Web;

namespace WiseThink.NTCA
{
    public partial class ChangePassword : BasePage
    {
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            #region CSRF
            //First, check for the existence of the Anti-XSS cookie
            //   var requestCookie = Request.Cookies[AntiXsrfToken.AntiXsrfTokenKey];

            Guid requestCookieGuidValue;

            var requestCookie = (string)Session["xsrf"];
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
            //  If the CSRF cookie is not found, then this is a new session.
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

            Page.ViewStateUserKey = _antiXsrfTokenValue;
            Session["xsrf"] = _antiXsrfTokenValue;

            //var responseCookie = new HttpCookie(AntiXsrfToken.AntiXsrfTokenKey)
            //{

            //    HttpOnly = true,
            //    Value = _antiXsrfTokenValue
            //};


            //if (Request.IsSecureConnection)
            //    responseCookie.Secure = true;


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
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUserName.Text = Convert.ToString(WiseThink.NTCA.BAL.Authorization.AuthoProvider.User);
            if (!IsPostBack)
            {
                UserBAL.Instance.InsertAuditTrailDetail("Visited Change Password Page", "Change Password");
                Session["LastUrl"] = Request.UrlReferrer.ToString();
            }
        }
        private void RemovedLoggedUser()
        {
            string userLoggedIn = Session["UserLoggedIn"] == null ? Application["currentUser"] == null ? string.Empty : (string)Application["currentUser"] : (string)Session["UserLoggedIn"];
            if (userLoggedIn.Length > 0)
            {
                System.Collections.Generic.List<string> d = Application["UsersLoggedIn"]
                    as System.Collections.Generic.List<string>;
                if (d != null)
                {
                    lock (d)
                    {
                        d.Remove(userLoggedIn);
                    }
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChangeOldPassword();
            RemovedLoggedUser();
            //AuthoProvider.LogOut();
            this.BackButton1.SetNavigateURL = Convert.ToString(Session["LastUrl"]);
            //if (ChangeOldPassword())
            //{
            //    RemovedLoggedUser();
            //    AuthoProvider.LogOut();
            //    //    UserBAL.Instance.InsertAuditTrailDetail("Password Changed", "Change Password");
            //    //    List<Role> userRole = AuthoProvider.LoggedInRoles.ToList();
            //    //    if (userRole.Contains(Role.ADMIN))
            //    //        Response.Redirect("~/Admin/Home.aspx", false);
            //    //    else if (userRole.Contains(Role.NTCA) || userRole.Contains(Role.REGIONALOFFICER))
            //    //        Response.Redirect("~/NTCA-RO/Home.aspx", false);
            //    //    else if (userRole.Contains(Role.CWLW) || userRole.Contains(Role.SECRETARY))
            //    //        Response.Redirect("~/CWW-Secretary/Home.aspx", false);
            //    //    else
            //    //        Response.Redirect("~/FieldDirector/FieldDirectorHome.aspx", false);
            //}
            //else
            //{

            //}
        }
        private bool IsCpmplexPassword()
        {
            Regex obj = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,10}");
            string complexValue = hdnComp.Value;
            var mixedPassword = AESEncrytDecry.DecryptStringAES(complexValue);
            string[] validValue = mixedPassword.Split('#');
            if (!obj.IsMatch(validValue[0]))
            {
                lblMessageDisplay.Text = "Password must contain: Minimum 8 and Maximum 10 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character";
                return false;
            }
            else
                return true;
        }
        /// <summary>
        /// Change old password
        /// Author: Indu
        /// </summary>
        private bool ChangeOldPassword()
        {
            try
            {
                if (!IsCpmplexPassword())
                    return false;
                //Get the old password from the database.
                DataSet dsOldPassword = UserBAL.Instance.GetOldPassword(lblUserName.Text);
                string strOldPassword = Convert.ToString(dsOldPassword.Tables[0].Rows[0].ItemArray[0]);
                //Commented by Indu because it is server side MD5 encriptioppn 
                //Checking the old password with entered old password.
                //string strOldPasswordHashed = MD5HASH.GetMD5HashCode(txtOldPassword.Text.Trim());
                //Added by Indu because it is alrardy encrypted at client side in MD5 encriptioppn format. 
                string strOldPasswordHashed = txtOldPassword.Text.Trim();
                if (strOldPassword.Equals(strOldPasswordHashed))
                {
                    if (txtNewPassword.Text.Trim().Equals(txtConfirmPassword.Text.Trim()))
                    {
                        UserInfo uInfo = AuthoProvider.GetLoggedInUser();
                        if (uInfo != null)
                        {
                            // Updating the password which user has entered, but in encrypted format.
                            //Commented by Indu because it is server side MD5 encriptioppn 
                            //AuthoProvider.UpdateTemporaryPassword(uInfo.UserName, MD5HASH.GetMD5HashCode(txtNewPassword.Text.Trim()), "C");
                            //Added by Indu because it is alrardy encrypted at client side in MD5 encriptioppn format. 
                            string clientMD5newPassword = txtNewPassword.Text.Trim();
                            AuthoProvider.UpdateTemporaryPassword(uInfo.UserName, clientMD5newPassword, "C");
                            string strSuccess = "Password Changed Successfully, Please use your changed Password in next time login...!";
                            uInfo.isFirstLogin = false;
                            HttpContext.Current.Session["user"] = uInfo;
                            vmSuccess.Message = strSuccess;
                            FlashMessage.ErrorMessage(vmError.Message);
                            return true;
                        }
                    }
                    else
                    {
                        txtNewPassword.Text = "";
                        txtConfirmPassword.Text = "";
                        string strError = "New Password and Confirm Password should be same...!";
                        vmError.Message = strError;
                        FlashMessage.ErrorMessage(vmError.Message);
                        return false;
                    }
                    return false;
                }
                else
                {
                    string strError = "Old password does not match. Please enter the correct old Password.";
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
                return false;
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                //return;
            }

        }
    }
}