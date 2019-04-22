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
using System.Drawing;
using System.Text;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class DownloadExpendituresReport : BasePage
    {

        DateTime dtFromDate;
        DateTime dtToDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //AllExpenditureReports();
            }
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
                //Response.ClearContent();
                //Response.Buffer = true;
                //Response.Clear();
                //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Activity.xls"));
                //Response.ContentType = "application/ms-excel";
                //StringWriter sw = new StringWriter();
                //HtmlTextWriter htw = new HtmlTextWriter(sw);
                ////gvExpenditureReports.AllowPaging = false;
                ////BindGrid();
                ////Change the Header Row back to white color
                //gvExpenditureReports.HeaderRow.Style.Add("background-color", "#FFFFFF");
                ////Applying stlye to gridview header cells
                //for (int i = 0; i < gvExpenditureReports.HeaderRow.Cells.Count; i++)
                //{
                //    gvExpenditureReports.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
                //}
                //gvExpenditureReports.RenderControl(htw);
                //Response.Write(sw.ToString());
                //Response.End();

                ExportDivContentToMsExcel();
                
            }
            catch
            {
                return;
            }
        }
        private void ExportDivContentToMsExcel()
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;

                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                MainContent.RenderControl(htmlWrite);

                //build the content for the dynamic Word document
                //in HTML alongwith some Office specific style properties. 
                var strBody = new StringBuilder();

                strBody.Append("<html " +
                 "xmlns:o='urn:schemas-microsoft-com:office:office' " +
                 "xmlns:w='urn:schemas-microsoft-com:office:excel'" +
                  "xmlns='http://www.w3.org/TR/REC-html40'>" +
                  "<head><title>Time</title>");

                //The setting specifies document's view after it is downloaded as Print
                //instead of the default Web Layout
                strBody.Append("<!--[if gte mso 9]>" +
                 "<xml>" +
                 "<w:WordDocument>" +
                 "<w:View>Print</w:View>" +
                 "<w:Zoom>100</w:Zoom>" +
                 "<w:DoNotOptimizeForBrowser/>" +
                 "</w:WordDocument>" +
                 "</xml>" +
                 "<![endif]-->");

                strBody.Append("<style>" +
                 "<!-- /* Style Definitions */" +
                 "@page Section1" +
                 "   {size:9.5in 10.0in; " +
                 "   margin:1.0in 1.25in 1.0in 1.25in ; " +
                 "   mso-header-margin:.5in; " +
                 "   mso-footer-margin:.5in; mso-paper-source:0;}" +
                 " div.Section1" +
                 "   {page:Section1;}" +
                 "-->" +
                 "</style></head>");

                strBody.Append("<body lang=EN-US style='tab-interval:.5in'>" +
                 "<div class=Section1>");
                strBody.Append(stringWrite);
                strBody.Append("</div></body></html>");

                //Force this content to be downloaded 
                //as a excel file with the name of your choice
                string fileName = "Expenditure Report" + ".xls";
                Response.AppendHeader("Content-Type", "application/excel");
                Response.AppendHeader("Content-disposition", "attachment; filename='" + fileName + "'");

                Response.Write(strBody.ToString());
                Response.Flush();
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
            }

        }
        protected void rdoMonthly_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMonthly.Checked == true)
            {
                rdoPeriodic.Checked = false;
                dvMonth.Visible = true;
                dvPeriodic.Visible = false;
            }
            else if (rdoPeriodic.Checked == true)
            {
                rdoMonthly.Checked = false;
                dvMonth.Visible = false;
                dvPeriodic.Visible = true;
            }
            else
            {
                rdoMonthly.Checked = false;
                rdoPeriodic.Checked = false;
                dvMonth.Visible = false;
                dvPeriodic.Visible = false;
            }
            //BindGrid();
        }

        protected void chkPeriodic_CheckedChanged(object sender, EventArgs e)
        {
            //BindGrid();
        }

        private void MonthlyReportsList()
        {
            DataSet ds = new DataSet();
            string LoggedInUser = AuthoProvider.User;
            DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
            if (dsTigerReserveId != null)
            {
                DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                if (dr[0] != DBNull.Value)
                    ds = QuarterlyReportBAL.Instance.FetchExpenditureReports("Monthly", ddlMonth.SelectedValue, "", "", Convert.ToInt32(dr["TigerReserveId"]));
                    //oApo.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                //oApo.LoggedInUser = LoggedInUser;
            }
            
            if (ds != null)
            {
                gvExpenditureReports.DataSource = ds.Tables[2];
                gvExpenditureReports.DataBind();
            }
        }

        private void PeriodicReportsList()
        {
            DataSet ds = new DataSet();
            string LoggedInUser = AuthoProvider.User;
            DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
            if (dsTigerReserveId != null)
            {
                DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                if (dr[0] != DBNull.Value)
                    ds = QuarterlyReportBAL.Instance.FetchExpenditureReports("Periodic", "", dtFromDate.ToString(), dtToDate.ToString(), Convert.ToInt32(dr["TigerReserveId"]));
                //oApo.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                //oApo.LoggedInUser = LoggedInUser;
            }
           // DataSet ds = new DataSet();
           // ds = QuarterlyReportBAL.Instance.FetchExpenditureReports("Periodic", "", dtFromDate.ToString(), dtToDate.ToString());
            if (ds != null)
            {
                gvExpenditureReports.DataSource = ds.Tables[2];
                gvExpenditureReports.DataBind();
            }
        }

        private void AllExpenditureReports()
        {
            DataSet ds = new DataSet();
            string LoggedInUser = AuthoProvider.User;
            DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
            if (dsTigerReserveId != null)
            {
                DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                if (dr[0] != DBNull.Value)
                    ds = QuarterlyReportBAL.Instance.FetchExpenditureReports("both", "", "", "", Convert.ToInt32(dr["TigerReserveId"]));
                //oApo.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                //oApo.LoggedInUser = LoggedInUser;
            }

            //DataSet ds = new DataSet();
            //ds = QuarterlyReportBAL.Instance.FetchExpenditureReports("both","","","");
            if (ds != null)
            {
                gvExpenditureReports.DataSource = ds.Tables[2];
                gvExpenditureReports.DataBind();
            }
        }

        private void BindGrid()
        {
            if (rdoMonthly.Checked)// == true && rdoPeriodic.Checked == false)
            {
                MonthlyReportsList();
            }
            else if (rdoPeriodic.Checked)// == false && rdoPeriodic.Checked == true)
            {
                PeriodicReportsList();
            }
            //else if (rdoMonthly.Checked == true && rdoPeriodic.Checked == true)
            //{
            //    AllExpenditureReports();
            //}
        }
        public override void VerifyRenderingInServerForm(Control control)
        { }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlMonth.SelectedIndex > 0)
            {
                BindGrid();
            }
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtFromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
                dtToDate = Convert.ToDateTime(txtToDate.Text.Trim());

                if (dtFromDate < dtToDate)
                {
                    BindGrid();
                }
            }
            catch
            {

            }
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtFromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
                dtToDate = Convert.ToDateTime(txtToDate.Text.Trim());

                if (dtFromDate < dtToDate)
                {
                    BindGrid();
                }
            }
            catch
            {

            }
        }
    }
}