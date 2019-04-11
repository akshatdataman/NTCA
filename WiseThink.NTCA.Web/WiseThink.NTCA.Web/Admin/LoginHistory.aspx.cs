using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.BAL.Authorization;

namespace WiseThink.NTCA.Web.Admin
{
    public partial class LoginHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLoginHistory();
            }

        }
        private void BindLoginHistory()
        {
            List<UserInfo> auditTrailInfo = new AuthoProvider().GetLoginHistory();
            gvLoginHistory.DataSource = auditTrailInfo;
            gvLoginHistory.DataBind();
        }

        protected void gvLoginHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLoginHistory.PageIndex = e.NewPageIndex;
            BindLoginHistory();
        }
    }
}