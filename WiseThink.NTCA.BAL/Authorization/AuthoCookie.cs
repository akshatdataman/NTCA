using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WiseThink.NTCA.BAL.Authorization
{
    public class AuthoCookie
    {
        public static void CreateAuthoCookie()
        {
            HttpCookie c = new HttpCookie("AuthoCookie");
            string cValue = Guid.NewGuid().ToString().Replace("-", "");
            HttpContext.Current.Session["AuthoCookie"] = cValue;
            c.Value = cValue;
            HttpContext.Current.Response.Cookies.Add(c);
        }

        public static void ClearAuthoCookie()
        {
            if (HttpContext.Current.Request.Cookies["AuthoCookie"] != null)
            {
                // string cValue = Guid.NewGuid().ToString().Replace("-", "");
                HttpContext.Current.Session["AuthoCookie"] = null;
                HttpContext.Current.Response.Cookies["AuthoCookie"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        public static void RegenerateAuthoCookie()
        {
            if (HttpContext.Current.Request.Cookies["AuthoCookie"] != null)
            {
                string cValue = Guid.NewGuid().ToString().Replace("-", "");
                HttpContext.Current.Session["AuthoCookie"] = cValue;
                HttpContext.Current.Response.Cookies["AuthoCookie"].Value = cValue;
            }
            else
            {
                CreateAuthoCookie();
            }
        }
        public static bool IsValidAuthoCookie()
        {
            if (HttpContext.Current.Request.Cookies["AuthoCookie"] != null && HttpContext.Current.Session["AuthoCookie"] != null)
            {
                return HttpContext.Current.Session["AuthoCookie"].ToString() == HttpContext.Current.Request.Cookies["AuthoCookie"].Value;
            }
            else
            {
                return false;
            }
        }
    }
}
