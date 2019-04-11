using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WiseThink.NTCA.Web.CWW_Secretary
{
    public partial class CWW_Secretary :BaseMaster
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}