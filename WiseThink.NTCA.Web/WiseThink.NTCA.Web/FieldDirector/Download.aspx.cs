using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using WiseThink.NTCA.BAL;
using System.Diagnostics;
using System.Drawing.Printing;
using WiseThink.NTCA.App_Code;
using System.IO;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class WebForm1 : BasePage
    {
        public string DocumentName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDocumentList();
                UserBAL.Instance.InsertAuditTrailDetail("Visited Download Page", "Download");
            }
        }
        private void GetDocumentList()
        {
            cgvDocuments.DataSource = DocumentBAL.Instance.GetUploadedDocuments();
            cgvDocuments.DataBind();
        }
        protected void cgvDocuments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvDocuments.PageIndex = e.NewPageIndex;
            GetDocumentList();
        }
        protected void imgbtnPdf_click(object sender, EventArgs e)
        {
            try
            {
                ImageButton ibtn = sender as ImageButton;
                GridViewRow gvrow = ibtn.NamingContainer as GridViewRow;
                string fileName = cgvDocuments.DataKeys[gvrow.RowIndex].Value.ToString();
                string filePath = "~/Downloads/" + fileName;
                Response.ContentType = "doc/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                if (System.IO.File.Exists(Server.MapPath(filePath)))
                {
                    Response.TransmitFile(Server.MapPath(filePath));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                }
                Response.End();
            }
            catch (FileNotFoundException ex)
            {
                string strInvalid = ex.Message;
                vmError.Message = strInvalid;
                FlashMessage.ErrorMessage(vmError.Message);
                return;
            }

        }
        protected void cgvDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "download")
            {
                LinkButton lnkdownload = (LinkButton)e.CommandSource;
                GridViewRow gvrow = lnkdownload.NamingContainer as GridViewRow;
                string fileName = cgvDocuments.DataKeys[gvrow.RowIndex].Value.ToString();
                string filePath = "~/Downloads/" + fileName;
                Response.ContentType = "doc/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                if (System.IO.File.Exists(Server.MapPath(filePath)))
                {
                    Response.TransmitFile(Server.MapPath(filePath));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                }
                Response.End();

            }
            if (e.CommandName == "print")
            {
                try
                {
                    LinkButton lnkPrint = (LinkButton)e.CommandSource;
                    GridViewRow gvrow = lnkPrint.NamingContainer as GridViewRow;
                    string fileName = cgvDocuments.DataKeys[gvrow.RowIndex].Value.ToString();
                    string filePath = "~/Downloads/" + fileName;
                    if (System.IO.File.Exists(Server.MapPath(filePath)))
                    {
                        string pdfPath = Server.MapPath(filePath);
                        Process printjob = new Process();
                        printjob.StartInfo.FileName = Server.MapPath(filePath);
                        printjob.StartInfo.Verb = "Print";
                        printjob.StartInfo.CreateNoWindow = true;
                        printjob.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        PrinterSettings setting = new PrinterSettings();
                        setting.DefaultPageSettings.Landscape = true;
                        printjob.Start();
                        string strSuccess = "Pdf has printed successfully.";
                        vmSuccess.Message = strSuccess;
                        FlashMessage.InfoMessage(vmSuccess.Message);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                    }
                }
                catch (Exception ex)
                {
                    LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                    Response.RedirectPermanent("~/ErrorPage.aspx", false);
                    //string strError = ex.Message;
                    //vmError.Message = strError;
                    //FlashMessage.ErrorMessage(vmError.Message);
                    //return;
                }
            }
        }
    }
}