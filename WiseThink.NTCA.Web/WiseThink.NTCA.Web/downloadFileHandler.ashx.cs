using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiseThink.NTCA.Web
{
    /// <summary>
    /// Summary description for downloadFileHandler
    /// </summary>
    public class downloadFileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string fileName = string.Empty;
            if (context.Request.QueryString["fileName"] != null)
            {
                fileName = context.Request.QueryString["fileName"].ToString();
            }
            string file = context.Server.MapPath("/Upload/ApoDocuments/" + fileName);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
            try
            {
                if (fileInfo.Exists)
                {
                    context.Response.Clear();
                    string value = "attachment;filename=" + fileInfo.Name;
                    context.Response.AppendHeader("Content-Disposition", value);
                    context.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.TransmitFile(fileInfo.FullName);
                   
                }
                else
                {
                    throw new Exception("file not found");
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
              //  context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}