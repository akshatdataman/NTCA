using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.DataEntity;


namespace WiseThink.NTCA
{
    public partial class Site1 : BaseMaster
    {

        protected void Page_PreInIt(object sender, EventArgs e)
        {
            if(Session["UserLoggedIn"] == null)
            {
                Response.Redirect("~/SessionExpired.aspx");
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.SetNoCache();
        }




    }
}