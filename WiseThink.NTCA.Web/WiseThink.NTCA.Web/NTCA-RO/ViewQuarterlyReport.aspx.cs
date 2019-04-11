using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.DataEntity.Entities;
using System.Data;
using WiseThink.NTCA.BAL;
using System.Configuration;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Drawing.Printing;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class ViewQuarterlyReport : BasePage
    {
        public bool IsEdit = false;
        string previousFinancialYear = string.Empty;
        APO oApo = new APO();
        int apoFileId = 0;
        DataSet dsIds;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (ddlApoTitle.SelectedValue != "0" && ddlApoTitle.SelectedValue != "" && ddlApoTitle.SelectedValue != "Select")
            //{
            //    apoFileId = Convert.ToInt32(ddlApoTitle.SelectedValue);
            //    ApoTitle.InnerText = ddlApoTitle.SelectedItem.Text;
            //    if (apoFileId != 0)
            //        dsIds = APOBAL.Instance.GetStateIdTigerReserveIdAndFinancialYear(apoFileId);
            //    if (dsIds != null)
            //    {
            //        DataRow dr = dsIds.Tables[0].Rows[0];
            //        oApo.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
            //        oApo.StateId = Convert.ToInt32(dr["StateId"]);
            //        oApo.FinancialYear = dr["FinancialYear"].ToString();
            //    }
            //}
            if (!IsPostBack)
            {
                lblAPOTitle.Visible = false;
                ddlApoTitle.Visible = false;
                GetReportType();
                //// GetCurrentYearApprovedApoTitle();
                // BindQuarters();
                // GetQuarterlyReportFormat();
                // UserBAL.Instance.InsertAuditTrailDetail("Visited View Quarterly Report Page", "Quarterly Report");
            }
        }

        private void GetReportType()
        {
            ddlReportType.Items.Clear();
            ddlReportType.Items.Insert(0, "Select");
            ddlReportType.Items.Insert(1, new ListItem("Monthly", "1"));
            ddlReportType.Items.Insert(2, new ListItem("Periodically", "2"));
        }

        protected void gvNonRecurring_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manually once the header created
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView ProductGrid = (GridView)sender;
                APOBAL.Instance.GenerateQuarterlyReportHeader(ProductGrid, gvNonRecurring);
            }
        }

        protected void gvRecurring_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manually once the header created
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView ProductGrid = (GridView)sender;
                APOBAL.Instance.GenerateQuarterlyReportHeader(ProductGrid, gvRecurring);
            }
        }

        protected void ddlApoTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlApoTitle.SelectedIndex != 0)
            {
                if (ddlReportType.SelectedValue != "0")
                {
                    ApoTitle.Visible = true;
                }
                apoFileId = Convert.ToInt32(ddlApoTitle.SelectedValue);
                ApoTitle.InnerText = ddlApoTitle.SelectedItem.Text;
                if (apoFileId != 0)
                    dsIds = APOBAL.Instance.GetStateIdTigerReserveIdAndFinancialYear(apoFileId);
                if (dsIds != null)
                {
                    DataRow dr = dsIds.Tables[0].Rows[0];
                    oApo.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                    oApo.StateId = Convert.ToInt32(dr["StateId"]);
                    oApo.FinancialYear = dr["FinancialYear"].ToString();
                }
                if (ddlReportType.SelectedValue == "3" || ddlReportType.SelectedValue == "4" || ddlReportType.SelectedValue == "5" || ddlReportType.SelectedValue == "6" ||
                 ddlReportType.SelectedValue == "7" || ddlReportType.SelectedValue == "8" || ddlReportType.SelectedValue == "9" || ddlReportType.SelectedValue == "10" ||
                 ddlReportType.SelectedValue == "11" || ddlReportType.SelectedValue == "12" || ddlReportType.SelectedValue == "13" || ddlReportType.SelectedValue == "14")
                {

                    GetQuarterlyReportFormatMontly();
                }
                else if (ddlReportType.SelectedValue == "15" || ddlReportType.SelectedValue == "16" || ddlReportType.SelectedValue == "17" || ddlReportType.SelectedValue == "18")
                {
                    //GetCurrentYearApprovedApoTitle();
                    //BindQuarters();
                    GetQuarterlyReportFormatQuaterly();
                }
            }
        }
        private void GetCurrentYearApprovedApoTitle()
        {
            DataSet dsCurrentApo = APOBAL.Instance.GetCurrentFinancialYearApprovedApo();
            ddlApoTitle.DataSource = dsCurrentApo;
            ddlApoTitle.DataValueField = "APOFileId";
            ddlApoTitle.DataTextField = "APOTitle";
            ddlApoTitle.DataBind();
            ddlApoTitle.Items.Insert(0, "Select APO Title");
        }
        private void BindQuarters()
        {
            DataSet dsQuarter = QuarterlyReportBAL.Instance.GetQuarterListt();
            ddlNRQuarter.DataSource = dsQuarter.Tables[0];
            ddlNRQuarter.DataValueField = "QuarterId";
            ddlNRQuarter.DataTextField = "QuarterName";
            ddlNRQuarter.DataBind();
            //ddlNRQuarter.Items.Insert(0, "Select");

            //ddlRQuarter.DataSource = dsQuarter.Tables[0];
            //ddlRQuarter.DataValueField = "QuarterId";
            //ddlRQuarter.DataTextField = "QuarterName";
            //ddlRQuarter.DataBind();
            //ddlRQuarter.Items.Insert(0, "Select");
        }
        private void GetQuarterlyReportFormat()
        {
            DataSet dsReport = new DataSet();
            //if (IsEdit == true)
            //    dsReport = APOBAL.Instance.GetApoForModification(Convert.ToInt32(oApo.TigerReserveId));
            //else
            //dsReport = null;
            //if (dsReport.Tables[0].Rows.Count > 0)
            //{
            //    gvNonRecurring.DataSource = dsReport.Tables[0];
            //    gvNonRecurring.DataBind();
            //    LoadQuarterlyReportDraftData(gvNonRecurring);
            //    //btnNonRecurringSave.Style.Add("display", "block");
            //}
            //else
            //{
            //    gvNonRecurring.DataSource = dsReport.Tables[0];
            //    gvNonRecurring.DataBind();
            //    //btnNonRecurringSave.Style.Add("display", "none");
            //}
            //if (dsReport.Tables[1].Rows.Count > 0)
            //{
            //    gvRecurring.DataSource = dsReport.Tables[1];
            //    gvRecurring.DataBind();
            //    LoadQuarterlyReportDraftData(gvRecurring);
            //    //btnRecurringSave.Style.Add("display", "block");
            //}
            //else
            //{
            //    gvRecurring.DataSource = dsReport.Tables[1];
            //    gvRecurring.DataBind();
            //    //btnRecurringSave.Style.Add("display", "none");
            //}
        }
        protected void imgbtnWord_click(object sender, ImageClickEventArgs e)
        {
            if (gvNonRecurring.Rows.Count > 0 || gvRecurring.Rows.Count > 0)
                ExportDivContentToMsWord();
        }
        protected void imgbtnExcel_click(object sender, ImageClickEventArgs e)
        {
            if (gvNonRecurring.Rows.Count > 0 || gvRecurring.Rows.Count > 0)
                ExportDivContentToMsExcel();
        }
        protected void imgbtnPdf_click(object sender, ImageClickEventArgs e)
        {
            if (gvNonRecurring.Rows.Count > 0 || gvRecurring.Rows.Count > 0)
                ExportDivContentToPDF();
        }
        protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
        {

        }
        private void ExportDivContentToPDF()
        {
            System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
            try
            {
                // create an API client instance
                string userName = ConfigurationManager.AppSettings["pdfcrowdUsername"].ToString();
                string APIKey = ConfigurationManager.AppSettings["pdfcrowdAPIKey"].ToString();
                pdfcrowd.Client client = new pdfcrowd.Client(userName, APIKey);

                // convert a web page and write the generated PDF to a memory stream
                MemoryStream Stream = new MemoryStream();
                //client.convertURI("http://www.google.com", Stream);

                // set HTTP response headers
                Response.Clear();
                Response.AddHeader("Content-Type", "application/pdf");
                Response.AddHeader("Cache-Control", "max-age=0");
                Response.AddHeader("Accept-Ranges", "none");
                Response.AddHeader("Content-Disposition", "attachment; filename=QuarterlyReportPdfExport.pdf");
                System.IO.StringWriter stringWrite1 = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite1 = new HtmlTextWriter(stringWrite1);
                MainContent.RenderControl(htmlWrite1);
                client.convertHtml(stringWrite1.ToString(), Stream);
                // send the generated PDF
                Stream.WriteTo(Response.OutputStream);
                Stream.Close();
                Response.Flush();
            }
            catch (pdfcrowd.Error why)
            {
                LogHandler.LogFatal((why.InnerException != null ? why.InnerException.Message : why.Message), why, this.GetType());
            }
        }
        private void ExportDivContentToMsWord()
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
                 "xmlns:w='urn:schemas-microsoft-com:office:word'" +
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
                //as a Word document with the name of your choice
                Response.AppendHeader("Content-Type", "application/msword");
                Response.AppendHeader("Content-disposition", "attachment; filename=QuarterlyReportWordExport.doc");
                Response.Write(strBody.ToString());
                Response.Flush();
            }

            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
            }
        }
        private void ExportDivContentToMsExcel()
        {
            try
            {
                StringBuilder StrHtmlGenerate = new StringBuilder();
                StringBuilder StrExport = new StringBuilder();

                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                MainContent.RenderControl(htmlWrite);
                StrExport.Append(stringWrite);
                StrExport.Append("</div></body></html>");
                string strFile = "QuarterlyReportExcelExport.xls";
                string strcontentType = "application/excel";
                Response.ClearContent();
                Response.ClearHeaders();
                Response.BufferOutput = true;
                Response.ContentType = strcontentType;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
                Response.Write(StrExport.ToString());
                Response.Flush();
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
            }

        }
        private void PrintDiv()
        {
            Process printjob = new Process();
            printjob.StartInfo.FileName = MainContent.ToString();
            printjob.StartInfo.Verb = "Print";
            printjob.StartInfo.CreateNoWindow = true;
            printjob.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            PrinterSettings setting = new PrinterSettings();
            setting.DefaultPageSettings.Landscape = true;
            printjob.Start();
        }
        public override void VerifyRenderingInServerForm(Control control)
        { }
        private void LoadQuarterlyReportDraftData(GridView gv)
        {
            try
            {
                int trId = Convert.ToInt32(oApo.TigerReserveId);
                int quarterId;
                DataSet dsDraft = null;
                if (Request.CurrentExecutionFilePath.Contains("#RecuringDiv"))
                {
                    quarterId = Convert.ToInt32(ddlNRQuarter.SelectedValue);
                    dsDraft = QuarterlyReportBAL.Instance.GetQuarterlyReportDraftData(trId, quarterId);
                }
                else
                {
                    quarterId = Convert.ToInt32(ddlNRQuarter.SelectedValue);
                    dsDraft = QuarterlyReportBAL.Instance.GetQuarterlyReportDraftData(trId, quarterId);
                }

                for (int i = 0; i < dsDraft.Tables.Count; i++)
                {
                    if (dsDraft.Tables[i].Rows.Count > 0)
                    {
                        for (int j = 0; j < dsDraft.Tables[i].Rows.Count; j++)
                        {
                            DataRow dr = dsDraft.Tables[i].Rows[j];
                            foreach (GridViewRow row in gv.Rows)
                            {
                                Label lblActivityTypeId = (Label)row.FindControl("lblActivityTypeId");
                                Label lblActivityId = (Label)row.FindControl("lblActivityId");
                                Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                                Label txtQuantity = (Label)row.FindControl("txtQuantity");
                                Label txtLocation = (Label)row.FindControl("txtLocation");
                                Label txtPhysicalTarget = (Label)row.FindControl("txtPhysicalTarget");
                                Label txtFinancial = (Label)row.FindControl("txtFinancial");
                                Label lblState = (Label)row.FindControl("lblState");
                                Label lblTotal = (Label)row.FindControl("lblTotal");

                                if (lblActivityId.Text == dr["ActivityId"].ToString() && lblActivityItemId.Text == dr["ActivityItemId"].ToString())
                                {
                                    if (IsEdit == true)
                                    {
                                        HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
                                        hdnId.Value = dr["ID"].ToString();
                                    }
                                    txtQuantity.Text = dr["NumberOfItems"].ToString();
                                    txtLocation.Text = dr["TigerReserveName"].ToString();
                                    txtPhysicalTarget.Text = dr["PhysicalAssessment"].ToString();
                                    txtFinancial.Text = dr["CentralFinancialProgress"].ToString();
                                    lblState.Text = dr["StateFinancialProgress"].ToString();
                                    lblTotal.Text = dr["FinancialAssessment"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                //if (!Response.IsRequestBeingRedirected)
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                //return;
            }
        }

        protected void ddlNRQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetQuarterlyReportFormat();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx", false);
        }

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAPOTitle.Visible = true;
            ddlApoTitle.Visible = true;
           // ddlReportType.Visible = true;
            GetCurrentYearApprovedApoTitle();
            BindQuarters();
            if (ddlReportType.SelectedValue == "1")
            {
                lblReortTypeTitle.Text="Select Month.";
                RemoveandInsertMonthInDropDown();
            }
            else if (ddlReportType.SelectedValue == "2")
            {
                lblReortTypeTitle.Text = "Select Quater.";
                RemoveAndInsertQuaterInDropDown();
            }
            else
            {
                gvNonRecurring.DataSource = null;
                gvRecurring.DataSource = null;
                gvRecurring.DataBind();
                gvNonRecurring.DataBind();
                ApoTitle.Visible = false;
            }
            apoFileId = 0;
        }

        private void GetQuarterlyReportFormatMontly()
        {
            int selectedMonthId = Convert.ToInt32(ddlReportType.SelectedValue);
            int monthid = selectedMonthId - 2;
            DataSet dsReport = new DataSet();
            //if (IsEdit == true)
            //    dsReport = APOBAL.Instance.GetApoForModification(Convert.ToInt32(oApo.TigerReserveId));
            //else
            dsReport = QuarterlyReportBAL.Instance.GetQuarterlyReportFormatForCWwAndNTCA(oApo.TigerReserveId,monthid);
            if (dsReport.Tables[0].Rows.Count > 0)
            {
                gvNonRecurring.DataSource = dsReport.Tables[0];
                gvNonRecurring.DataBind();
                LoadQuarterlyReportDraftData(gvNonRecurring);
                //btnNonRecurringSave.Style.Add("display", "block");
            }
            else
            {
                gvNonRecurring.DataSource = dsReport.Tables[0];
                gvNonRecurring.DataBind();
                //btnNonRecurringSave.Style.Add("display", "none");
            }
            if (dsReport.Tables[1].Rows.Count > 0)
            {
                gvRecurring.DataSource = dsReport.Tables[1];
                gvRecurring.DataBind();
                LoadQuarterlyReportDraftData(gvRecurring);
                //btnRecurringSave.Style.Add("display", "block");
            }
            else
            {
                gvRecurring.DataSource = dsReport.Tables[1];
                gvRecurring.DataBind();
                //btnRecurringSave.Style.Add("display", "none");
            }

            //throw new NotImplementedException();
        }

        private void GetQuarterlyReportFormatQuaterly()
        {
            int selectedQuarterId = Convert.ToInt32(ddlReportType.SelectedValue);
            int quarterId = selectedQuarterId - 15;
            DataSet dsReport = new DataSet();
            //if (IsEdit == true)
            //    dsReport = APOBAL.Instance.GetApoForModification(Convert.ToInt32(oApo.TigerReserveId));
            //else
            dsReport = QuarterlyReportBAL.Instance.GetQuarterlyReportFormatForCWwAndNTCAQuartely(oApo.TigerReserveId,quarterId);
            if (dsReport.Tables[0].Rows.Count > 0)
            {
                gvNonRecurring.DataSource = dsReport.Tables[0];
                gvNonRecurring.DataBind();
                LoadQuarterlyReportDraftData(gvNonRecurring);
                //btnNonRecurringSave.Style.Add("display", "block");
            }
            else
            {
                gvNonRecurring.DataSource = dsReport.Tables[0];
                gvNonRecurring.DataBind();
                //btnNonRecurringSave.Style.Add("display", "none");
            }
            if (dsReport.Tables[1].Rows.Count > 0)
            {
                gvRecurring.DataSource = dsReport.Tables[1];
                gvRecurring.DataBind();
                LoadQuarterlyReportDraftData(gvRecurring);
                //btnRecurringSave.Style.Add("display", "block");
            }
            else
            {
                gvRecurring.DataSource = dsReport.Tables[1];
                gvRecurring.DataBind();
                //btnRecurringSave.Style.Add("display", "none");
            }

           // throw new NotImplementedException();
        }

        private void RemoveAndInsertQuaterInDropDown()
        {
            ddlReportType.Items.Clear();
            ddlReportType.Items.Insert(0, new ListItem("Select","0"));
            ddlReportType.Items.Insert(1, new ListItem("FirstQuarter", "15"));
            ddlReportType.Items.Insert(2, new ListItem("SecondQuarter", "16"));
            ddlReportType.Items.Insert(3, new ListItem("ThirdQuarter", "17"));
            ddlReportType.Items.Insert(4, new ListItem("FourthQuarter", "18"));
        }

        private void RemoveandInsertMonthInDropDown()
        {

            ddlReportType.Items.Clear();
            ddlReportType.Items.Insert(0, new ListItem("Select", "0"));

            ddlReportType.Items.Insert(1, new ListItem("January", "3"));
            ddlReportType.Items.Insert(2, new ListItem("Febuary", "4"));
            ddlReportType.Items.Insert(3, new ListItem("March", "5"));
            ddlReportType.Items.Insert(4, new ListItem("April", "6"));
            ddlReportType.Items.Insert(5, new ListItem("May", "7"));
            ddlReportType.Items.Insert(6, new ListItem("June", "8"));
            ddlReportType.Items.Insert(7, new ListItem("July", "9"));
            ddlReportType.Items.Insert(8, new ListItem("August", "10"));
            ddlReportType.Items.Insert(9, new ListItem("September", "11"));
            ddlReportType.Items.Insert(10, new ListItem("October", "12"));
            ddlReportType.Items.Insert(11, new ListItem("November", "13"));
            ddlReportType.Items.Insert(12, new ListItem("December", "14"));
        }
    }
}