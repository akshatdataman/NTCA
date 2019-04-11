using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WiseThink.NTCA.UserControls
{
    public partial class BackButton : System.Web.UI.UserControl
    {
        /*
         * Added by indu on 01, Dec 2014
         */
        public string SetNavigateURL { set { HLBack.NavigateUrl = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            HLBack.NavigateUrl = Request.UrlReferrer.ToString();
        }
        /*
         * Commented by indu on 01, Dec 2014
         */
        //public string SetNavigateURL { set { HLBack.NavigateUrl = value; } }
        
    }
}