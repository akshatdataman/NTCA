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


namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class EcoDevelopment : System.Web.UI.Page
    {
        int tigerReserveId;
        protected void Page_Load(object sender, EventArgs e)
        {
            string LoggedInUser = AuthoProvider.User;
            DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
            if (dsTigerReserveId != null)
            {
                DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                if (dr[0] != DBNull.Value)
                    tigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
            }
            if (!IsPostBack)
            {
                GetApoForView();
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
            if (loggedInUserRole == "FIELDDIRECTOR")
                dsViewApo = APOBAL.Instance.GetApoForView(tigerReserveId,"");
            else if (loggedInUserRole == "CWLW" || loggedInUserRole == "SECRETARY")
                dsViewApo = APOBAL.Instance.GetApoForView(tigerReserveId,"");
            else if (loggedInUserRole == "NTCA" || loggedInUserRole == "REGIONALOFFICER")
                dsViewApo = APOBAL.Instance.GetApoForView(tigerReserveId,"");

            gvNonRecurring.DataSource = dsViewApo.Tables[0];
            gvNonRecurring.DataBind();
            gvRecuring.DataSource = dsViewApo.Tables[1];
            gvRecuring.DataBind();
        }

        protected void imgbtnPdf_click(object sender, ImageClickEventArgs e)
        {
            ExportGridToPDF();
            //ImageButton ibtn = sender as ImageButton;
            //GridViewRow gvrow = ibtn.NamingContainer as GridViewRow;
            //string fileName = gvNonRecurring.DataKeys[gvrow.RowIndex].Value.ToString();
            //string filePath = "~/Downloads/" + fileName;
            //Response.ContentType = "doc/pdf";
            //Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
            //Response.TransmitFile();
            //Response.End();

        }
        private void ExportGridToPDF()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.pdf", "PDFExport"));
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //For First DataTable
            System.IO.StringWriter stringWrite1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite1 = new HtmlTextWriter(stringWrite1);
            //DataGrid myDataGrid1 = new DataGrid();
            //myDataGrid1.DataSource = dt1;
            //myDataGrid1.DataBind();
            gvNonRecurring.RenderControl(htmlWrite1);
            //For Second DataTable 
            System.IO.StringWriter stringWrite2 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite2 = new HtmlTextWriter(stringWrite2);
            //DataGrid myDataGrid2 = new DataGrid();
            //myDataGrid2.DataSource = dt2;
            //myDataGrid2.DataBind();
            gvRecuring.RenderControl(htmlWrite2);
            //You can add more DataTable
            StringReader sr = new StringReader(stringWrite1.ToString() + stringWrite2.ToString());
            Document pdfDoc = new Document(new Rectangle(288f, 144f), 10f, 10f, 10f, 0f);
            pdfDoc.SetPageSize(PageSize.A4.Rotate());
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            HttpContext.Current.Response.Write(pdfDoc);
            HttpContext.Current.Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        { }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("FieldDirectorHome.aspx", false);
        }

        //protected void lnkPrint_Click(object sender, EventArgs e)
        //{
        //    HttpContext.Current.Response.Clear();
        //    //HttpContext.Current.Response.ContentType = "application/pdf";
        //    //HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.pdf", "PDFExport"));
        //    //HttpContext.Current.Response.Charset = "utf-8";
        //    //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //    //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    //For First DataTable
        //    System.IO.StringWriter stringWrite1 = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter htmlWrite1 = new HtmlTextWriter(stringWrite1);
        //    //DataGrid myDataGrid1 = new DataGrid();
        //    //myDataGrid1.DataSource = dt1;
        //    //myDataGrid1.DataBind();
        //    gvNonRecurring.RenderControl(htmlWrite1);
        //    //For Second DataTable 
        //    System.IO.StringWriter stringWrite2 = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter htmlWrite2 = new HtmlTextWriter(stringWrite2);
        //    //DataGrid myDataGrid2 = new DataGrid();
        //    //myDataGrid2.DataSource = dt2;
        //    //myDataGrid2.DataBind();
        //    gvRecuring.RenderControl(htmlWrite2);
        //    //You can add more DataTable
        //    StringReader sr = new StringReader(stringWrite1.ToString() + stringWrite2.ToString());
        //    Document pdfDoc = new Document(new Rectangle(288f, 144f), 10f, 10f, 10f, 0f);
        //    pdfDoc.SetPageSize(PageSize.A4.Rotate());
        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
        //    //pdfDoc.Open();
        //    //htmlparser.Parse(sr);
        //    //pdfDoc.Close();
        //    HttpContext.Current.Response.Write(pdfDoc);
        //    HttpContext.Current.Response.End();

        //    Process printjob = new Process();
        //    printjob.StartInfo.FileName = pdfDoc.ToString();
        //    printjob.StartInfo.Verb = "Print";
        //    printjob.StartInfo.CreateNoWindow = true;
        //    printjob.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //    PrinterSettings setting = new PrinterSettings();
        //    setting.DefaultPageSettings.Landscape = true;
        //    printjob.Start();
        //}
    
    }
}