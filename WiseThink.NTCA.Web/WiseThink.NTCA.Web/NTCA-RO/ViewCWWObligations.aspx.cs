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
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.DataEntity.Entities;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class ViewCWWObligations : BasePage
    {
        Obligations obligation = new Obligations();
        protected void Page_Load(object sender, EventArgs e)
        {
            string LoggedInUser = AuthoProvider.User;
            DataSet dsStateId = ObligationBAL.Instance.GetStateId(LoggedInUser);
            if (Session["APOFileId"] != null)
            {
                int apoFileId = Convert.ToInt32(Session["APOFileId"]);
                DataSet ds = ObligationBAL.Instance.GetAPOStateAndTigerReserveId(apoFileId);
                if (ds != null)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    obligation.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                    obligation.StateId = Convert.ToInt32(dr["StateId"]);
                    obligation.LoggedInUser = LoggedInUser;
                }
                Session["APOFileId"] = null;
            }
            //if (dsStateId != null)
            //{
            //    DataRow dr = dsStateId.Tables[0].Rows[0];
            //    obligation.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
            //    obligation.StateId = Convert.ToInt32(dr["StateId"]);
            //    obligation.LoggedInUser = LoggedInUser;
            //}
            if (!IsPostBack)
            {
                ViewCWWObligation();
                UserBAL.Instance.InsertAuditTrailDetail("Visited View CWLW Obligation", "Obligation Under Tri-MOU");
            }
        }
        private void ViewCWWObligation()
        {
            DataSet dsFdObligation = ObligationBAL.Instance.GetObligationViewForCWW(obligation.TigerReserveId, obligation.StateId);
            cgvObligationCWW.DataSource = dsFdObligation;
            cgvObligationCWW.DataBind();
        }

        protected void cgvObligationCWW_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvObligationCWW.PageIndex = e.NewPageIndex;
            ViewCWWObligation();
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
            cgvObligationCWW.RenderControl(htmlWrite1);
            //For Second DataTable 

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