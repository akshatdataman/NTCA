using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace WiseThink
{
    public enum ExportType
    {
        Csv = 1,
        Excel = 2,
        PDF = 3,
        Word = 4,

    }
    public class ExportManager
    {

        public ExportManager()
        {

        }

        public string getGridCellText(System.Web.UI.WebControls.TableCell tc)
        {
            string cellText = "";
            if (tc.HasControls())
            {
                foreach (System.Web.UI.Control c in tc.Controls)
                {
                    if (c.GetType() == typeof(System.Web.UI.WebControls.LinkButton))
                    {
                        System.Web.UI.WebControls.LinkButton lb = c as System.Web.UI.WebControls.LinkButton;
                        cellText = lb.Text.Trim();
                    }
                    else if (c.GetType() == typeof(System.Web.UI.WebControls.HyperLink))
                    {
                        System.Web.UI.WebControls.HyperLink h1 = c as System.Web.UI.WebControls.HyperLink;
                        cellText = h1.Text.Trim();
                    }
                    else if (c.GetType() == typeof(System.Web.UI.WebControls.Label))
                    {
                        System.Web.UI.WebControls.Label lbl = c as System.Web.UI.WebControls.Label;
                        cellText = lbl.Text.Trim();
                    }
                }
            }
            else
            {
                cellText = tc.Text;
            }
            return cellText;
        }
        public void ExportCSV(System.Web.UI.WebControls.GridView exportGV, System.Web.HttpResponse Response)
        {
            string strFileName = "Report" + System.DateTime.Now.Date.ToString("dd") + System.DateTime.Now.AddMonths(0).ToString("MM") + System.DateTime.Now.AddYears(0).ToString("yyyy") + System.DateTime.Now.Millisecond.ToString("0000");
            StringBuilder sb = new StringBuilder();
            System.Web.UI.WebControls.GridViewRow grHeader = exportGV.HeaderRow;
            int counter = 0;

            foreach (System.Web.UI.WebControls.TableCell tc in grHeader.Cells)
            {
                sb.Append("\"" + exportGV.Columns[counter].HeaderText.Trim() + "\",");
                counter++;
            }
            sb.AppendLine();

            foreach (System.Web.UI.WebControls.GridViewRow gr in exportGV.Rows)
            {
                foreach (System.Web.UI.WebControls.TableCell tc in gr.Cells)
                {
                    sb.Append("\"" + getGridCellText(tc) + "\",");
                }
                sb.AppendLine();
            }

            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Export.csv");
            Response.ContentType = "text/csv";
            Response.AddHeader("Pragma", "public");
            Response.Write(sb.ToString());
            Response.End();




        }

        public static void ExportToCsv_old(string gridViewText, string contentType, HttpResponse response)
        {
            const string m_Delimiter_Column = ",";
            string m_Delimiter_Row = Environment.NewLine;

            response.ContentType = contentType;

            System.Text.RegularExpressions.Regex m_RegEx = new System.Text.RegularExpressions.Regex(@"(>\s+<)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            gridViewText = m_RegEx.Replace(gridViewText, "><");

            gridViewText = gridViewText.Replace(m_Delimiter_Row, String.Empty);
            gridViewText = gridViewText.Replace("</td></tr>", m_Delimiter_Row);
            gridViewText = gridViewText.Replace("<tr><td>", String.Empty);
            gridViewText = gridViewText.Replace(m_Delimiter_Column, "\\" + m_Delimiter_Column);
            gridViewText = gridViewText.Replace("</td><td>", m_Delimiter_Column);

            m_RegEx = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            gridViewText = m_RegEx.Replace(gridViewText, String.Empty);

            gridViewText = HttpUtility.HtmlDecode(gridViewText);
            response.Write(gridViewText);
            response.End();
        }

        public void ExportExcel(System.Web.UI.WebControls.GridView exportGV, System.Web.HttpResponse Response, System.Web.UI.HtmlControls.HtmlForm htmlForm)
        {
            exportGV.AllowPaging = false;
            exportGV.Visible = true;
            string strFileName = "XcelReport-" + System.DateTime.Now.Date.ToString("dd") + System.DateTime.Now.AddMonths(0).ToString("MM") + System.DateTime.Now.AddYears(0).ToString("yyyy");
            string attachment = "attachment; filename=" + strFileName + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create a form to contain the grid
            exportGV.Parent.Controls.Add(htmlForm);
            htmlForm.Attributes["runat"] = "server";
            htmlForm.Controls.Add(exportGV);
            htmlForm.RenderControl(htw);

            Response.Write(sw.ToString());
            exportGV.Visible = false;
            Response.End();


        }

        public void ExportWord(System.Web.UI.WebControls.GridView exportGV, System.Web.HttpResponse Response, System.Web.UI.HtmlControls.HtmlForm htmlForm)
        {
            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment;filename=Export.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            exportGV.Parent.Controls.Add(htmlForm);
            htmlForm.Attributes["runat"] = "server";
            htmlForm.Controls.Add(exportGV);
            htmlForm.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }



        public void ExportPdf(System.Web.UI.WebControls.GridView exportGV, System.Web.HttpResponse Response, System.Web.UI.HtmlControls.HtmlForm htmlForm)
        {
            string strFileName = "PDFReport-" + System.DateTime.Now.Date.ToString("dd") + System.DateTime.Now.AddMonths(0).ToString("MM") + System.DateTime.Now.AddYears(0).ToString("yyyy");
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            HtmlForm frm = new HtmlForm();

            exportGV.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(exportGV);
            frm.RenderControl(hw);

            StringReader sr = new StringReader(Regex.Replace(sw.ToString(), "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase));
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();

        }



        public static void ExportToExcelWord(string gridViewText, string contentType, HttpResponse response)
        {
            response.ContentType = contentType;
            response.Write(gridViewText);
            response.End();
        }

        public string GetCurrentPageName()
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }

        private void ShowPdf(string strS, HttpResponse Response, string contentType, string filename)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = contentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.TransmitFile(strS);
            Response.End();
            //Response.WriteFile(strS);
            Response.Flush();
            Response.Clear();

        }

    }
}

