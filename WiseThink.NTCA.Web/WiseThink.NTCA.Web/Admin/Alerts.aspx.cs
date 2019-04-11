using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL.Authorization;
using System.Data;
using WiseThink.NTCA.BAL;

namespace WiseThink.NTCA.Web.Admin
{
    public partial class Alerts : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllAlerts();
                UserBAL.Instance.InsertAuditTrailDetail("Visited Alert List Page", "Alerts");
            }
        }
        protected void cgvAlerts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvAlerts.PageIndex = e.NewPageIndex;
            GetAllAlerts();
        }
        private void GetAllAlerts()
        {
            string loggedInUser = AuthoProvider.User;
            int _userId = UserBAL.Instance.GetLoggedInUserId(loggedInUser);
            DataSet dsAlert = AlertBAL.Instance.GetAlerts(_userId);
            cgvAlerts.DataSource = dsAlert.Tables[1];
            cgvAlerts.DataBind();
        }
        
        
        protected void btnSendAlert_Click(object sender, EventArgs e)
        {
            Response.Redirect("SendAlert.aspx", false);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx", false);
        }
    }
}