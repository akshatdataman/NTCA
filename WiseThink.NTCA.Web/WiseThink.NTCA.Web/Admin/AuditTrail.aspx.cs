using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.BAL.Authorization;

namespace WiseThink.NTCA.Admin
{
    public partial class AuditTrail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAuditTaril();
                lblMsg.Text = "(\"SL\" for Successfull Login, \"UL\" for Unsuccessful Login)";
            }
        }

        private void BindAuditTaril()
        {
            List<UserInfo> auditTrailInfo = new AuthoProvider().GetAuditTrailDetails();
            gvAuditReport.DataSource = auditTrailInfo;
            gvAuditReport.DataBind();
        }

        protected void gvAuditReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAuditReport.PageIndex = e.NewPageIndex;
            BindAuditTaril();
        }

    }
}