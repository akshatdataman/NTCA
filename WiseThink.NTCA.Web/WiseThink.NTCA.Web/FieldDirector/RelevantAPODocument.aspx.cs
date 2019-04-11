using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.BAL.Helper;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class RelevantAPODocument : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetRelevantAPODocumentList();
            }
        }

        protected void cgvActivityItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvActivityItems.PageIndex = e.NewPageIndex;
            GetRelevantAPODocumentList();
        }

        protected void cgvActivityItems_PageIndexChanged(object sender, EventArgs e)
        {
            cgvActivityItems.EditIndex = -1;
            GetRelevantAPODocumentList();
        }

        private void GetRelevantAPODocumentList()
        {
            DataTable dt = new DataTable();
            CommonClass cs = new CommonClass();
            DataSet ds = cs.GetRelevantAPODocument();
            cgvActivityItems.DataSource = ds;
            cgvActivityItems.DataBind();
        }

        protected void cgvActivityItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "download")
                {
                    //LinkButton lnkdownload = (LinkButton)e.CommandSource;
                    //GridViewRow gvrow = lnkdownload.NamingContainer as GridViewRow;
                    ////string fileName = gvNRCore.DataKeys[gvrow.RowIndex].Value.ToString();
                    //string filePath = "~/Upload/ApoDocuments/" + lnkdownload.Text;
                    //Response.ContentType = "application/octet-stream";
                    //Response.AddHeader("Content-Disposition", "attachment;filename=\"" + lnkdownload.Text + "\"");
                    //Response.TransmitFile(Server.MapPath(filePath));
                    //Response.End();



                    //LinkButton lnkdownload = (LinkButton)e.CommandSource;
                    //GridViewRow gvrow = lnkdownload.NamingContainer as GridViewRow;
                    ////string fileName = cgvActivityItems.DataKeys[gvrow.RowIndex].Value.ToString();

                    //if (lnkdownload.Text.ToUpper().Contains(".XLS") || lnkdownload.Text.ToUpper().Contains(".XLSX"))
                    //{
                    //    string filePath = "~/Upload/ApoDocuments/" + lnkdownload.Text;
                    //    Response.AddHeader("Content-Type", "application/Excel");
                    //    Response.ContentType = "application/vnd.ms-excel";
                    //    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + lnkdownload.Text + "\"");
                    //    Response.TransmitFile(Server.MapPath(filePath));
                    //    Response.End();
                    //}



                    LinkButton lbtnDocumentFile = (LinkButton)e.CommandSource;
                    GridViewRow gvrow = lbtnDocumentFile.NamingContainer as GridViewRow;
                    //string filePath = "~/Upload/ApoDocuments/";
                    if (lbtnDocumentFile.Text != string.Empty)
                    {
                        WebClient req = new WebClient();
                        HttpResponse response = HttpContext.Current.Response;
                        //filePath = filePath + lbtnDocumentFile.Text;
                        string filePath = "~/Upload/ApoDocuments/" + lbtnDocumentFile.Text;
                        response.Clear();
                        response.ClearContent();
                        response.ClearHeaders();
                        response.Buffer = true;
                        response.AddHeader("Content-Disposition", "attachment;filename='" + lbtnDocumentFile.Text + "'");
                        byte[] data = req.DownloadData(Server.MapPath(filePath));
                        response.BinaryWrite(data);
                        //UpdatePanel2.Update();
                        Response.Flush();
                        response.End();
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                string strInvalid = ex.Message;
                return;
            }
        }

    }
}