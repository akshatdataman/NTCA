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
    public partial class Main : BaseMaster
    {
        private string _antiXsrfTokenValue;
        protected void Page_InIt(object sender, EventArgs e)
        {
            //if (Session.IsNewSession)
            //{
            //    Session["ForceSession"] = DateTime.Now;
            //}
            //Page.ViewStateUserKey = Session.SessionID;
            //if (Page.EnableViewState)
            //{
            //    if (!string.IsNullOrEmpty(Request.Params["__VIEWSTATE"]) && !string.IsNullOrEmpty(Request.Form["__VIewSTATE"]))
            //    {
            //        throw new Exception("ViewState Existed but not in form");
            //    }
            //}
            #region CSRF
            //First, check for the existence of the Anti-XSS cookie
            //  var requestCookie = Request.Cookies[AntiXsrfToken.AntiXsrfTokenKey];
            var requestCookie = (string)Session["xsrf"]; ;
             Guid requestCookieGuidValue;


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

            else
            {

                GenerateCSRFCookie();
            }

            Page.PreLoad += master_Page_PreLoad;
            #endregion
            Utility.SetNoCache();
            if (!AuthoProvider.IsLoggedIn)
            {
                RemovedLoggedUser();
                AuthoProvider.LogOut();
            }


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // If someone changes in his url than just sign out.
            if (Request.UrlReferrer != null && string.IsNullOrEmpty(Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1]))
            {
                RemovedLoggedUser();
                AuthoProvider.LogOut();
            }
            //Condition Added by indu during audit process, since above condition not working. 
            if (!Request.FilePath.Contains("Home"))
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    RemovedLoggedUser();
                    AuthoProvider.LogOut();
                }
            }
            else
            {
                if (Request.UrlReferrer == null)
                {
                    RemovedLoggedUser();
                    AuthoProvider.LogOut();
                }
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
        protected void lbtnHome_Click(object sender, EventArgs e)
        {
            var role = AuthoProvider.LoggedInRoles;
            if (role.Contains(Role.ADMIN))
            {
                Response.Redirect("Home.aspx", false);
            }

            else if (role.Contains(Role.NTCA))
            {
                Response.Redirect("Home.aspx", false);
            }
            else if (role.Contains(Role.REGIONALOFFICER))
            {
                Response.Redirect("Home.aspx", false);
            }
            else if (role.Contains(Role.CWLW))
            {
                Response.Redirect("Home.aspx", false);
            }
            else if (role.Contains(Role.SECRETARY))
            {
                Response.Redirect("Home.aspx", false);
            }
            else if (role.Contains(Role.FIELDDIRECTOR))
            {
                Response.Redirect("FieldDirectorHome.aspx", false);
            }
        }


        protected void lbtnLogOut_Click(object sender, EventArgs e)
        {
            UserBAL.Instance.InsertAuditTrailDetail("Logout", "Login Page");
            WiseThink.NTCA.BAL.Authorization.AuthoProvider.LogOut();
            Session.Abandon();
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
                    Response.Redirect("~/Login.aspx");
                }
            }
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
        private void GenerateCSRFCookie()
        {
            //Generate a new Anti-XSRF token
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            //Set the view state user key, which will be validated by the
            //framework during each request
            Page.ViewStateUserKey = _antiXsrfTokenValue;
            Session["xsrf"] = _antiXsrfTokenValue;
            //  //  Create the non - persistent CSRF cookie
            //    var responseCookie = new HttpCookie(AntiXsrfToken.AntiXsrfTokenKey)
            //    {
            //        //Set the HttpOnly property to prevent the cookie from
            //        //being accessed by client side script
            //        HttpOnly = true,

            //        //Add the Anti-XSRF token to the cookie value
            //        Value = _antiXsrfTokenValue
            //    };

            //    //If we are using SSL, the cookie should be set to secure to
            //    //prevent it from being sent over HTTP connections
            //    if (Request.IsSecureConnection)
            //        responseCookie.Secure = true;

            //    //Add the CSRF cookie to the response
            //    Response.Cookies.Set(responseCookie);
            //}
        }
    }
}