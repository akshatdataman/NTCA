using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using System.Data.SqlClient;

namespace WiseThink.NTCA.Web.Admin
{
    public partial class WebForm12 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetStateList();
                UserBAL.Instance.InsertAuditTrailDetail("Visited State List Page", "Manage State");
            }
        }
        private void GetStateList()
        {
            cgvStateMaster.DataSource = StateBAL.Instance.GetStateList();
            cgvStateMaster.DataBind();
        }
        protected void imgbtnAction_click(object sender, EventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
            string stateId = cgvStateMaster.DataKeys[rowIndex].Values[0].ToString();
            int _stateId = Convert.ToInt32(stateId);
            Session["StateId"] = _stateId;
            Response.Redirect("~/Admin/EditStateMaster.aspx");
        }
        protected void cgvStateMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvStateMaster.PageIndex = e.NewPageIndex;
            GetStateList();
        }
    }
}