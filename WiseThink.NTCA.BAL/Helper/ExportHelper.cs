using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.DataEntity;

namespace WiseThink.NTCA.BAL.Helper
{
    public class ExportHelper
    {
        private DataTable FormatDataTableDateTimeColumns(DataTable data)
        {
            DataTable newTable = new DataTable(data.TableName);
            List<int> dateTimeColumnsIndexs = new List<int>();
            foreach (DataColumn item in data.Columns)
            {
                newTable.Columns.Add(item.ColumnName);
                if (item.DataType.Equals(typeof(DateTime)))
                {
                    dateTimeColumnsIndexs.Add(data.Columns.IndexOf(item));
                }
            }
            foreach (DataRow dr in data.Rows)
            {
                DataRow tempRow = newTable.NewRow();
                foreach (DataColumn item in data.Columns)
                {
                    int cIndex = data.Columns.IndexOf(item);
                    if (dateTimeColumnsIndexs.Contains(cIndex))
                    {
                        tempRow[item.ColumnName] = dr[item.ColumnName].IsDBNull() ? "" : Convert.ToDateTime(dr[item.ColumnName]).GetFormatedDateTimeString();

                    }
                    else
                    {
                        tempRow[item.ColumnName] = dr[item.ColumnName];
                    }
                }
                newTable.Rows.Add(tempRow);
            }
            return newTable;
        }
        public void ExportToExcel(DataTable dtExcelData, string fileName)
        {
            try
            {
                GridView gvPDFTemp = new GridView();
                GridViewRow headerRow = gvPDFTemp.HeaderRow;
                gvPDFTemp.Style.Add("font-size", "8px");
                gvPDFTemp.HeaderStyle.BackColor = System.Drawing.Color.Yellow;
                gvPDFTemp.AllowPaging = false;
                gvPDFTemp.DataSource = FormatDataTableDateTimeColumns(dtExcelData);
                gvPDFTemp.DataBind();

                ExportToExcel(gvPDFTemp, fileName);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
            }
        }

        public void ExportToExcel(GridView gvPDFTemp, string fileName)
        {
            gvPDFTemp.Style.Add("font-size", "8px");
            gvPDFTemp.HeaderStyle.BackColor = System.Drawing.Color.Yellow;
            // gvPDFTemp.DataBind();
            HtmlGenericControl divTop = new HtmlGenericControl("div");

            StringWriter sw1 = new StringWriter();
            HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
            gvPDFTemp.RenderControl(hw1);
            StringReader sr1 = new StringReader(sw1.ToString());
            string html = GetHeaderText(sr1, fileName);
            divTop.InnerHtml = html;

            HttpContext context = HttpContext.Current;
            context.Response.ClearContent();
            // context.Response.ContentType = "application/ms-excel";
            context.Response.ContentType = "text/vnd.ms-excel";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            // gvPDFTemp.RenderControl(hw);
            divTop.RenderControl(hw);
            HttpContext.Current.Response.Write(sw);
            HttpContext.Current.Response.End();
        }

        private static string GetHeaderText(StringReader sr1, string fileName)
        {
            string html = @"<div style='text-align: center;width: 98%;'><center><div style='font-size: 20px; width:100%; font-weight:bold;'>Central Zoo Authority</div><div style='font-size: 10px; width:100%;'>(Statutory Body under the Ministry of Environment and Forests, Govt. of India)</div><br><div style='font-size: 15px; color:blue;text-decoration:underline;'>" + fileName + "</div></center></div><br><br><div>" + sr1.ReadToEnd() + "</div>";
            return html;
        }
        public void ExportToPDF(DataTable dtExportPDF, string fileName)
        {
            try
            {

                GridView gvPDFTemp = new GridView();
                gvPDFTemp.Style.Add("font-size", "8px");
                gvPDFTemp.HeaderStyle.BackColor = System.Drawing.Color.Yellow;
                gvPDFTemp.AllowPaging = false;
                gvPDFTemp.DataSource = FormatDataTableDateTimeColumns(dtExportPDF);
                gvPDFTemp.DataBind();
                ExportToPDF(gvPDFTemp, fileName);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
            }
        }

        public void ExportToPDF(GridView gvPDFTemp, string fileName)
        {
            HtmlGenericControl divTop = new HtmlGenericControl("div");

            StringWriter sw1 = new StringWriter();
            HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
            gvPDFTemp.RenderControl(hw1);
            StringReader sr1 = new StringReader(sw1.ToString());
            string html = GetHeaderText(sr1, fileName);// @"<div style='text-align: center;width: 98%;'><center><h1>Central Zoo Authority</h1><div style='font-size: 10px; width:100%;'>(Statutory Body under the Ministry of Environment and Forests, Govt. of India)</div></center></div>
            //                    <div>" + sr1.ReadToEnd() + "</div>";
            //string HeaderText = "<div style='text-align: left;width: 98%;'><center><h1>Central Zoo Authority</h1><div style='font-size: 1.5em; width:100%;'>(Statutory Body under the Ministry of Environment and Forests, Govt. of India)</div></center></div>";

            // Removing image for history report
            //html = html.Replace("../image/plus.gif", "");
            //html = html.Replace("../image/minus.gif", "");
            //html=html.Replace("src=\"\"","");
            divTop.InnerHtml = html;
            //divGrid.Controls.Add(gvPDFTemp);
            //divTop.Controls.Add(divGrid);
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            // gvPDFTemp.RenderControl(hw);
            divTop.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 0, 0, 15, 5);
            //pdfDoc.PageSize.Rotation
            //Document document = new Document(PageSize.A4, 0, 0, 150, 20);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
            pdfDoc.Open();

            htmlparser.Parse(sr);
            pdfDoc.Close();
            HttpContext.Current.Response.Write(pdfDoc);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public void ExportToPDF(string dtExportPDF, string fileName)
        {
            try
            {
                StringBuilder strPDFLatterText = new StringBuilder();
                strPDFLatterText.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body>");
                strPDFLatterText.Append(dtExportPDF);

                strPDFLatterText.Append("</body></html>");


                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringReader sr = new StringReader(strPDFLatterText.ToString());
                Document pdfDoc = new Document(PageSize.A4.Rotate(), 0, 0, 15, 5);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
            }
        }


        public string ExportMailPDF(DataTable dtZooDetails, string subjectOfLatter, string mapPath)
        {
            try
            {
                DataRow drZooData;//= new DataRow();
                drZooData = dtZooDetails.Rows[0];
                StringBuilder strPDFLatterText = new StringBuilder();
                strPDFLatterText.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body>");

                strPDFLatterText.Append("<div style='padding: 5px; border: 1px solid #999999; width: 800px'>");
                strPDFLatterText.Append("<table cellpadding='0' cellspacing='0' width='100%'><tr>");
                strPDFLatterText.Append("<td style='line-height: 30px; font-size: 18px; text-align: center; font-weight: bold;'>");
                strPDFLatterText.Append("Central Zoo Authority</td></tr><tr>");
                strPDFLatterText.Append("<td style='text-align: center; line-height: 30px; font-size: 12px; font-weight: bold;'>");
                strPDFLatterText.Append("(Statutory Body under the Ministry of Environment and Forests, Govt. of India)");
                strPDFLatterText.Append("</td></tr>");
                strPDFLatterText.Append("<tr ><td >---------------------------------------------------------------------------------------------------------------------------------</td></tr>");
                strPDFLatterText.Append("<tr ><td ><br /><br /></td></tr>");
                strPDFLatterText.Append("<tr><td align='right'>" + System.DateTime.Today.ToLongDateString() + "</td></tr><tr><td>");
                strPDFLatterText.Append(drZooData[DataBaseFields.ZOO_NAME] + "<br />");
                strPDFLatterText.Append("Application No : " + drZooData[DataBaseFields.APPLICATION_ID] + "<br />");
                strPDFLatterText.Append("Address: " + drZooData[DataBaseFields.ZOO_ADDRESS] + ", " + drZooData[DataBaseFields.REGION_NAME] + ", " + drZooData[DataBaseFields.CITY_NAME] + ", " + drZooData[DataBaseFields.STATE_NAME] + "<br />");
                strPDFLatterText.Append("Contact No : " + drZooData[DataBaseFields.ZOO_CONTAUTH_TEL] + "<br />");
                strPDFLatterText.Append("Email ID: " + drZooData[DataBaseFields.ZOO_CONTAUTH_MAIL] + "<br />");
                strPDFLatterText.Append("</td></tr><tr><td><br /><b>");
                strPDFLatterText.Append("Subject : </b> " + subjectOfLatter + " </td></tr>");
                strPDFLatterText.Append("<tr><td>&nbsp;<br /><br /></td></tr><tr><td align='justify'>");
                strPDFLatterText.Append("A good way to keep a customer up to date on the progress of a project is to send a letter to the client. This is very important during projects since the consumer is the recipient of the work. The letter should be concise and should contain all the information about the project. A time line or a Gantt chart should be included in order to show the progress much more clearly. This is a good indicator of the direction and accomplishments done for the purpose of the consumer. The letter is used in the field of engineering, construction and other fields where long and short-term projects are done.");

                strPDFLatterText.Append("</td></tr><tr><td>&nbsp;<br /><br /></td></tr>");
                strPDFLatterText.Append("<tr><td style='padding-left: 300px'><div style='float: right; width: 250px'><b>Sincerely<br /></b><br />");
                strPDFLatterText.Append("Central Zoo Authority<br />");
                strPDFLatterText.Append("Gurgaon 1023434");
                strPDFLatterText.Append("</div></td></tr></table></div>");
                strPDFLatterText.Append("</body></html>");
                string pdfFileNamePath = mapPath + dtZooDetails.Rows[0][DataBaseFields.ZOO_NAME].ToString().Replace(" ", "_") + subjectOfLatter.Replace(" ", "_") + ".pdf";
                String htmlText = strPDFLatterText.ToString();// System.IO.File.ReadAllText(mapPath + "\\1.htm");
                htmlText.Replace("{", ""); htmlText.Replace("}", "");
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(pdfFileNamePath, FileMode.Create));
                document.Open();
                iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                hw.Parse(new StringReader(htmlText));
                document.Close();
                StringBuilder sb = new StringBuilder();
                sb.Append(htmlText);

                return pdfFileNamePath.ToString();
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                return "";
            }

        }


    }
}
