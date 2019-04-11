using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.BAL.Authorization;
using System.Net.Mail;
using WiseThink.NTCA.Web;
using WiseThink.NTCA.DataEntity;

namespace WiseThink.NTCA
{
    public partial class ForgotPassword : BasePage
    {
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            #region CSRF
            //First, check for the existence of the Anti-XSS cookie
            var requestCookie = Request.Cookies[AntiXsrfToken.AntiXsrfTokenKey];
            Guid requestCookieGuidValue;


            //If the CSRF cookie is found, parse the token from the cookie.
            //Then, set the global page variable and view state user
            //key. The global variable will be used to validate that it matches in the view state form field in the Page.PreLoad
            //method.
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                //Set the global token variable so the cookie value can be
                //validated against the value in the view state form field in
                //the Page.PreLoad method.
                _antiXsrfTokenValue = requestCookie.Value;
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
            //Create the non-persistent CSRF cookie
            var responseCookie = new HttpCookie(AntiXsrfToken.AntiXsrfTokenKey)
            {
                //Set the HttpOnly property to prevent the cookie from
                //being accessed by client side script
                HttpOnly = true,

                //Add the Anti-XSRF token to the cookie value
                Value = _antiXsrfTokenValue
            };

            //If we are using SSL, the cookie should be set to secure to
            //prevent it from being sent over HTTP connections
            if (Request.IsSecureConnection)
                responseCookie.Secure = true;

            //Add the CSRF cookie to the response
            Response.Cookies.Set(responseCookie);
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
            if (!IsPostBack)
            {
                //UserBAL.Instance.InsertAuditTrailDetail("Visited Forgot Password Page", "Forgot Password");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageDisplay.Text = "";
                string captchaString = Session["CaptchaImageText"].ToString();
                if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtEmail.Text))
                {
                    lblMessageDisplay.Text = "Please enter either Username or Email at a time.";
                    //Please enter Valid Username or Email Address...!";
                    return;
                }
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    //lblMessageDisplay.Text = "Please enter Valid Username or Email Address...!";
                    txtEmail.Attributes.Add("email", "1");
                    // return;
                }
                DataSet ds = new DataSet();
                if (txtUserName.Text.Trim() == "" && txtEmail.Text.Trim() == "")
                {
                    lblMessageDisplay.Text = "Please enter Valid Username or Email Address...!";
                }
                else if (!captchaString.Equals(txtCaptcha.Text))
                {
                    txtCaptcha.Text = "";
                    lblMessageDisplay.Text = "Code entered does not match, please try again !";
                    return;
                }
                else if (txtUserName.Text.Trim() != "")
                {
                    // If user has entered username while making a request of new password. (Zahir)
                    ds = AuthoProvider.IsUserExists(txtUserName.Text);
                }
                else
                {
                    // If user has entered Email while making a request of new password. (Zahir)
                    ds = AuthoProvider.IsUserExists(txtEmail.Text.Trim());
                }

                if (ds.IsValid())
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    CommonClass cRandom = new CommonClass();

                    string pwd = cRandom.RandomPassword(8); // Generating the new random password. (Zahir)

                    bool ifSuccess = cRandom.SendMail(pwd, Convert.ToString(dr["Email"])); // Sending new password to user on its registered email address. (Zahir)

                    if (ifSuccess)
                    {
                        // after email is sent successfull the new generated password is encrypted and stored in the database. (Zahir)
                        pwd = MD5HASH.GetMD5HashCode(pwd);
                        AuthoProvider.UpdateTemporaryPassword(Convert.ToString(dr["UserName"]), pwd, "F");
                        lblMessageDisplay.Text = "Your new Temporary Password is being sent to your Email, Please Check your Email...!";
                        Session["CaptchaImageText"] = null;
                        UserBAL.Instance.InsertAuditTrailDetail("Temporary Password has sent to registered Email", "Forgot Password");
                    }
                    else
                    {
                        lblMessageDisplay.Text = "Error Occured while sending Email...!";
                    }
                }
                else
                {
                    lblMessageDisplay.Text = "Please enter Valid Username or Email Address...!";
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
    }
}