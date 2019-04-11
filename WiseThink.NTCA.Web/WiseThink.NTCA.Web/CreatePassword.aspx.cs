using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.BAL;
namespace WiseThink.NTCA
{
    public partial class CreatePassword : BasePage
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
                UserBAL.Instance.InsertAuditTrailDetail("Visited Create Password Page", "Create Password");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewPassword.Text.Trim().Equals(txtConfirmPassword.Text.Trim()))
                {
                    UserInfo uInfo = AuthoProvider.GetLoggedInUser();
                    if (uInfo != null)
                    {
                        // Updating the password which user has entered, but in encrypted format. (Zahir)

                        //AuthoProvider.UpdateTemporaryPassword(uInfo.UserName, MD5HASH.GetMD5HashCode(txtNewPassword.Text.Trim()), "C");
                        AuthoProvider.UpdateTemporaryPassword(uInfo.UserName, txtNewPassword.Text.Trim(), "C");
                        lblMessageDisplay.Text = "Password Created Successfully, Please login using your new Password...!";
                        lblMessageDisplay.ForeColor = System.Drawing.Color.Green;
                        uInfo.isFirstLogin = false;
                        Application["ValidApplicationUser"] = Request.Url.AbsolutePath;
                        HttpContext.Current.Session["user"] = uInfo;
                        UserBAL.Instance.InsertAuditTrailDetail("Password Created Successfully", "Create Password");
                        RemovedLoggedUser();
                        AuthoProvider.LogOut();
                        Response.Redirect("Login.aspx", false);
                    }
                }
                else
                {
                    txtNewPassword.Text = "";
                    txtConfirmPassword.Text = "";
                    lblMessageDisplay.Text = "New Password and Confirm Password should be same...!";
                    lblMessageDisplay.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
                
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            UserInfo uinfo = new UserInfo();
            Application["LoggedInUsers"] = null;
            //Dictionary<string, DateTime> LoggedInUsers = Application["LoggedInUsers"] as Dictionary<string, DateTime>;
            //foreach (var item in LoggedInUsers.Where(kvp => kvp.Value == DateTime.Now.AddMilliseconds(-1000000)).ToList())
            //{
            //    LoggedInUsers.Remove(item.Key);
            //}
            AuthoProvider.LogOut();
            Response.Redirect("Login.aspx", false);
        }

        
    }
}