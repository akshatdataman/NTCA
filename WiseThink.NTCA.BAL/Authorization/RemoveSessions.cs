using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WiseThink.NTCA.DataEntity;

namespace WiseThink.NTCA.BAL.Authorization
{
    public class RemoveSessions
    {
        //public void ClearSession()
        //{

        //    System.Web.Security.FormsAuthentication.SignOut();
        //    HttpContext.Current.Session.Clear();
        //    HttpContext.Current.Session.Abandon();
        //    if (HttpContext.Current.Request.Cookies[AntiXsrfToken.ASPXAUTH] != null)
        //    {
        //        HttpContext.Current.Request.Cookies[AntiXsrfToken.ASPXAUTH].Expires = DateTime.Now.AddDays(-1);
        //    }

        //    SessionHijacking.RegenrateSessionId();
        //    AuthoCookie.ClearAuthoCookie();
        //    HttpContext.Current.Response.Cookies[AntiXsrfToken.AntiXsrfTokenKey].Expires = DateTime.Now.AddDays(-1);
        //}

        //public void KillSession(bool IsRedirect = false)
        //{
        //    Utility.RemoveUserFromList();
        //   // string actionType = "User logged out.";
        //   // Utility.InsertLogoutDetails(actionType);
        //    if (IsRedirect)
        //    {
        //        ClearSession();
        //        HttpContext.Current.Response.Redirect("~/Index.aspx");
        //    }
        //    else
        //    {
        //        ClearSession();
        //    }
        //}
    }
}
