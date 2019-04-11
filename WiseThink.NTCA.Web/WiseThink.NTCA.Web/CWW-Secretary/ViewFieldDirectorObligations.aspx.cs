using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace WiseThink.NTCA.Web.CWW_Secretary
{
    public partial class ViewFieldDirectorObligations : BasePage
    {
        int tigerReserveId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["APOFileId"] != null)
            {
                int apoFileId = Convert.ToInt32(Session["APOFileId"]);
                DataSet ds = ObligationBAL.Instance.GetAPOStateAndTigerReserveId(apoFileId);
                if (ds != null)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    tigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                }
            }
            if (!IsPostBack)
            {
                ViewFieldDirectorObligation();
            }
        }
        private void ViewFieldDirectorObligation()
        {
            DataSet dsFdObligation = ObligationBAL.Instance.GetObligationViewForFD(tigerReserveId);
            cgvObligationFD.DataSource = dsFdObligation;
            cgvObligationFD.DataBind();
        }

        protected void cgvObligationFD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvObligationFD.PageIndex = e.NewPageIndex;
            ViewFieldDirectorObligation();
        }

        protected void imgbtnPdf_click(object sender, ImageClickEventArgs e)
        {
            ExportGridToPDF();
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
            cgvObligationFD.RenderControl(htmlWrite1);
            
            StringReader sr = new StringReader(stringWrite1.ToString());
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
    }
}