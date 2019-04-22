using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using WiseThink.NTCA.BAL;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class DownloadActivityList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetManageActivityData();
            }
        }
        private void GetManageActivityData()
        {
            DataTable dt = new DataTable();
            DataSet ds = ManageActivityBAL.Instance.GetManageActivityMasterData();
            cgvActivityItems.DataSource = ds.Tables[3];
            cgvActivityItems.DataBind();
        }

        protected void cgvActivityItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvActivityItems.PageIndex = e.NewPageIndex;
            GetManageActivityData();
        }

        protected void cgvActivityItems_PageIndexChanged(object sender, EventArgs e)
        {
            cgvActivityItems.EditIndex = -1;
            GetManageActivityData();
        }

        protected void imgbtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Activity.xls"));
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                cgvActivityItems.AllowPaging = false;
                GetManageActivityData();
                //Change the Header Row back to white color
                cgvActivityItems.HeaderRow.Style.Add("background-color", "#FFFFFF");
                //Applying stlye to gridview header cells
                for (int i = 0; i < cgvActivityItems.HeaderRow.Cells.Count; i++)
                {
                    cgvActivityItems.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
                }
                cgvActivityItems.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch
            {
                return;
            }
        }
    }
}