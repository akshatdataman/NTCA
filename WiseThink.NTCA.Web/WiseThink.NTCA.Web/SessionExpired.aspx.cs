using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WiseThink.NTCA.Web
{
    public partial class SessionExpired : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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