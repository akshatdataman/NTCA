using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.DataEntity;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.BAL.Authorization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Text;
using System.Configuration;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class ViewPfyAPO : BasePage
    {
        int tigerReserveId, stateId;
        string currentFinancialYear = string.Empty;
        string previousFinancialYear = string.Empty;
        int previousApoFileId;
        //CommonClass cc = new CommonClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["PFY"] != null)
            {
                string LoggedInUser = AuthoProvider.User;
                DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
                if (dsTigerReserveId != null)
                {
                    DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                    if (dr[0] != DBNull.Value)
                        tigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                    else
                        tigerReserveId = 0;
                    if (tigerReserveId != 0)
                    {
                        DataSet dsCFY = APOBAL.Instance.GetCurrentYearAPOFileId(tigerReserveId);
                        if (dsCFY.Tables[0].Rows.Count == 1)
                        {
                            DataRow drCFY = dsCFY.Tables[0].Rows[0];
                            currentFinancialYear = Convert.ToString(drCFY["FinancialYear"]);
                        }
                        if (!string.IsNullOrEmpty(currentFinancialYear))
                        {
                            CommonClass cc = new CommonClass();
                            previousFinancialYear = cc.GetPreviousFinancialYear(currentFinancialYear);
                            DataSet dsPFY = APOBAL.Instance.GetPreviousYearAPOFileId(tigerReserveId, previousFinancialYear);
                            if (dsPFY.Tables[0].Rows.Count == 1)
                            {
                                DataRow drPFY = dsPFY.Tables[0].Rows[0];
                                previousApoFileId = Convert.ToInt32(drPFY["APOFileId"]);
                                previousFinancialYear = Convert.ToString(drPFY["FinancialYear"]);
                            }
                        }

                    }

                }
            }
            if (!IsPostBack)
            {
                GetApoForView();
                UserBAL.Instance.InsertAuditTrailDetail("Visited View Previous Year APO Page", "APO");
            }
        }
        private void GetApoForView()
        {
            DataSet dsViewApo = new DataSet();
            string LoggedInUser = AuthoProvider.User;
            string loggedInUserRole = string.Empty;
            DataSet dsUserRole = APOBAL.Instance.GetLoggedUserRole(LoggedInUser);
            if (dsUserRole != null)
            {
                DataRow dr = dsUserRole.Tables[0].Rows[0];
                loggedInUserRole = dr["RoleName"].ToString();
            }
            //CommonClass cc = new CommonClass();
            //previousFinancialYear = cc.GetPreviousFinancialYear(currentFinancialYear);
            if (loggedInUserRole == "FIELDDIRECTOR")
                dsViewApo = APOBAL.Instance.GetPreviousYearApoForView(tigerReserveId, previousFinancialYear);
            else if (loggedInUserRole == "CWLW" || loggedInUserRole == "SECRETARY")
                dsViewApo = APOBAL.Instance.GetPreviousYearApoForView(tigerReserveId, previousFinancialYear);
            else if (loggedInUserRole == "NTCA" || loggedInUserRole == "REGIONALOFFICER")
                dsViewApo = APOBAL.Instance.GetPreviousYearApoForView(tigerReserveId, previousFinancialYear);
            if (dsViewApo.Tables[0].Rows.Count > 0)
            {
                DataRow dr1 = dsViewApo.Tables[0].Rows[0];
                ApoTitle.InnerText = Convert.ToString(dr1["APOTitle"]);
            }
            gvNonRecurring.DataSource = dsViewApo.Tables[0];
            gvNonRecurring.DataBind();
            gvRecuring.DataSource = dsViewApo.Tables[1];
            gvRecuring.DataBind();
            //gvEcoDevelopment.DataSource = dsViewApo.Tables[2];
            //gvEcoDevelopment.DataBind();
        }
        protected void imgbtnWord_click(object sender, ImageClickEventArgs e)
        {
            ExportDivContentToMsWord();
        }
        protected void imgbtnExcel_click(object sender, ImageClickEventArgs e)
        {
            ExportDivContentToMsExcel();
        }
        protected void imgbtnPdf_click(object sender, ImageClickEventArgs e)
        {
            ExportDivContentToPDF();
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
                Response.AddHeader("Content-Disposition", "attachment; filename=ApoPdfExport.pdf");
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
                Response.AppendHeader("Content-disposition", "attachment; filename=ApoWordExport.doc");
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
                string strFile = "ApoExcelExport.xls";
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("FieldDirectorHome.aspx", false);
        }
    }
}