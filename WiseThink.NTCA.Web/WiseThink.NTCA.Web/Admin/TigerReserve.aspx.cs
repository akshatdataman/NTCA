using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using WiseThink.NTCA.BAL;

namespace WiseThink.NTCA.Web.Admin
{
    public partial class WebForm9 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTigerReserveList();
                if (Session["TigerReserveId"] != null && Convert.ToString(Session["Updatebtn"]) == "1")
                {
                    //string strSuccess = "User has been updated successfully.";
                    //vmSuccess.Message = strSuccess;
                    //FlashMessage.ErrorMessage(vmSuccess.Message);
                    Session.Remove("TigerReserveId");
                    Session.Remove("Updatebtn");
                }
                UserBAL.Instance.InsertAuditTrailDetail("Visited Tiger Reserve List Page", "Manage Tiger Reserve");
            }
        }
        private void GetTigerReserveList()
        {
            gvTigerReserve.DataSource = TigerReserveBAL.Instance.GetTigerReserveList();
            gvTigerReserve.DataBind();
        }
        protected void imgbtnAction_click(object sender, EventArgs e)
        {
            ImageButton ibtn = sender as ImageButton;
            int rowIndex = Convert.ToInt32(ibtn.Attributes["RowIndex"]);
            string tigerReserveId = gvTigerReserve.DataKeys[rowIndex].Values[0].ToString();
            int _tigerReserveId = Convert.ToInt32(tigerReserveId);
            Session["TigerReserveId"] = _tigerReserveId;
            Response.Redirect("~/Admin/EditTigerReserve.aspx");
        }
        protected void gvTigerReserve_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTigerReserve.PageIndex = e.NewPageIndex;
            GetTigerReserveList();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Session["TigerReserveId"] = null;
            Session["Updatebtn"] = null;
            Response.Redirect("~/Admin/EditTigerReserve.aspx", false);
        }
    }
}