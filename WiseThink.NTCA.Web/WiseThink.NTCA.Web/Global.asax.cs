using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.Scheduler;
using System.Web.UI;

namespace WiseThink.NTCA.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            //Application["LoggedInUsers"] = new Dictionary<string, DateTime>();
            Application["UsersLoggedIn"] = new System.Collections.Generic.List<string>();
            log4net.Config.XmlConfigurator.Configure();
            JobScheduler.Start();
        }

        protected void Application_OnPostRequestHandlerExecute()
        {
            string Url = Request.RawUrl;
            int count = Url.Length - 10;
            if(count>=0)
            {
                string LoginUrl = Url.Substring(count);
                if (LoginUrl.Contains("."))
                {
                    string[] str = LoginUrl.Split('.');
                    string LoggedInUser = AuthoProvider.User;
                    if ((LoggedInUser == null || LoggedInUser == "") && LoginUrl != "Login.aspx" && LoginUrl != "Image.aspx" && str[1] == "aspx" && LoginUrl != "ForgotPassword.aspx" && LoginUrl != "sword.aspx")
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        protected void Application_PreSendRequestHeaders(Object source, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("x-frame-options");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("x-frame-options", "DENY");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            var application = sender as HttpApplication;
            if (application != null && application.Context != null)
            {
                application.Context.Response.Headers.Remove("Server");
            }

            // Remove the "Server" HTTP Header from response
            var app = sender as HttpApplication;
            if (app == null || app.Context == null)
            {
                return;
            }

            var headers = app.Context.Response.Headers;
            headers.Remove("Server");
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs.
            bool isNeedToRedirect = false;
            Exception ex = Server.GetLastError().GetBaseException();
            string errorMsg = "\r\n...............Error Details........................\r\n\n";
            HttpContext httpCont = HttpContext.Current;

            if (ex is HttpException && ex.InnerException is ViewStateException)
            {
                Response.Redirect(Request.Url.AbsoluteUri);
                return;
            }

            if (httpCont.Error.Message.ToLower() != "File does not exist.".ToLower())
            {
                try
                {
                    errorMsg += "\r\nError Message : " + (httpCont.Error.InnerException != null ? httpCont.Error.InnerException.Message : httpCont.Error.Message);
                    errorMsg += "\r\nError Timestamp : " + httpCont.Timestamp;
                    errorMsg += "\r\n Error File Path :" + httpCont.Request.PhysicalPath.ToString();
                    errorMsg += "\r\n Stack Trace :" + httpCont.Error.StackTrace;
                    LogHandler.LogFatal(errorMsg, ex, this.GetType());
                }
                catch (Exception ea)
                {
                    isNeedToRedirect = true;
                    errorMsg = "";
                    errorMsg += "\r\nError Message : " + (ea.InnerException != null ? ea.InnerException.Message : ea.Message);
                    errorMsg += "\r\nError Timestamp : " + httpCont.Timestamp;
                    // errorMsg += "\r\n Error File Path :" + httpCont.Request.PhysicalPath.ToString();
                    errorMsg += "\r\n Stack Trace :" + ea.StackTrace;
                    LogHandler.LogFatal(errorMsg, ex, this.GetType());
                }
                if (isNeedToRedirect || errorMsg.Contains("does not exist") || errorMsg.Contains("potentially dangerous Request") || errorMsg.Contains("ErrorPage.aspx") || errorMsg.Contains("Illegal characters in path"))
                {
                    Response.RedirectPermanent("~/ErrorPage.aspx", true);
                }
                else
                {
                    Response.RedirectPermanent("~/ErrorPage.aspx", true);
                }
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            //// Code that runs when a session ends. 
            //// Note: The Session_End event is raised only when the sessionstate mode
            //// is set to InProc in the Web.config file. If session mode is set to StateServer 
            //// or SQLServer, the event is not raised.

            //// 

            if (Session["UserInfo"] != null)
            {
                UserInfo logoutDetails = Session["UserInfo"] as UserInfo;
                Dictionary<string, DateTime> LoggedInUsers = Application["LoggedInUsers"] as Dictionary<string, DateTime>;
                if (LoggedInUsers != null)
                {
                    LoggedInUsers.Remove(logoutDetails.UserName);
                }
                Application["LoggedInUsers"] = LoggedInUsers;
            }

            // NOTE: you might want to call this from the .Logout() method - aswell -, to speed things up
            string userLoggedIn = Session["UserLoggedIn"] == null ? string.Empty : (string)Session["UserLoggedIn"];
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

        protected void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            if (HttpContext.Current != null)
            {
                string userLoggedIn = Session["UserLoggedIn"] == null ? string.Empty : (string)Session["UserLoggedIn"];
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
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {

            //string _sessionBrowserInfo = string.Empty;
            if (HttpContext.Current.Session != null)
            {
                SessionHijacking.ValidateSession();
                UserInfo uinfo = Session["UserInfo"] as UserInfo;
                if (uinfo != null)
                {
                    Dictionary<string, DateTime> LoggedInUsers = Application["LoggedInUsers"] as Dictionary<string, DateTime>;
                    if (LoggedInUsers != null)
                    {
                        if (LoggedInUsers.Keys.Contains(uinfo.UserName))
                        {
                            LoggedInUsers.Remove(uinfo.UserName);
                            LoggedInUsers.Add(uinfo.UserName, DateTime.Now);
                        }
                        else
                        {
                            //LoggedInUsers.Add(loginEntitie.UserName, DateTime.Now);
                        }
                        Application["LoggedInUsers"] = LoggedInUsers;
                    }
                }
            }
        }
    }
}