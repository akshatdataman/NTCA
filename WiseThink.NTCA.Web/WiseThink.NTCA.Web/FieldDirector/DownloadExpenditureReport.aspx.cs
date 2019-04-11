using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.BAL.Authorization;
using System.IO;
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class DownloadExpenditureReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvExpenditureReports_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExpenditureReports.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gvExpenditureReports_PageIndexChanged(object sender, EventArgs e)
        {
            gvExpenditureReports.EditIndex = -1;
            BindGrid();
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
                gvExpenditureReports.AllowPaging = false;
                BindGrid();
                //Change the Header Row back to white color
                gvExpenditureReports.HeaderRow.Style.Add("background-color", "#FFFFFF");
                //Applying stlye to gridview header cells
                for (int i = 0; i < gvExpenditureReports.HeaderRow.Cells.Count; i++)
                {
                    gvExpenditureReports.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
                }
                gvExpenditureReports.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        protected void chkMonthly_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void chkPeriodic_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void MonthlyReportsList()
        {
            DataSet ds = new DataSet();
            ds = QuarterlyReportBAL.Instance.FetchExpenditureReports("Monthly");
            if(ds!= null)
            {
                gvExpenditureReports.DataSource = ds.Tables[2];
                gvExpenditureReports.DataBind();
            }
        }

        private void PeriodicReportsList()
        {
            DataSet ds = new DataSet();
            ds = QuarterlyReportBAL.Instance.FetchExpenditureReports("Periodic");
            if (ds != null)
            {
                gvExpenditureReports.DataSource = ds.Tables[2];
                gvExpenditureReports.DataBind();
            }
        }

        private void AllExpenditureReports()
        {
            DataSet ds = new DataSet();
            ds = QuarterlyReportBAL.Instance.FetchExpenditureReports("both");
            if (ds != null)
            {
                gvExpenditureReports.DataSource = ds.Tables[2];
                gvExpenditureReports.DataBind();
            }
        }

        private void BindGrid()
        {
            if (chkMonthly.Checked == true && chkPeriodic.Checked == false)
            {
                MonthlyReportsList();
            }
            else if (chkMonthly.Checked == false && chkPeriodic.Checked == true)
            {
                PeriodicReportsList();
            }
            else if (chkMonthly.Checked == true && chkPeriodic.Checked == true)
            {
                AllExpenditureReports();
            }
        }
    }
}