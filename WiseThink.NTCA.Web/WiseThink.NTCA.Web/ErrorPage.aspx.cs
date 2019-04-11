using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL.Authorization;

namespace WiseThink.NTCA
{
    public partial class ErrorPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthoProvider.LogOut();
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
                }
            }
        }
    }
}