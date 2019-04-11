using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WiseThink.NTCA.Web
{
    /// <summary>
    /// Summary description for uploader
    /// </summary>
    public class uploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        fileExtension = Path.GetExtension(fileName);

                        string pathToSave_100 = Path.Combine(HttpContext.Current.Server.MapPath("~/Upload/ApoDocuments/"), fileName);

                        LogHandler.LogInfo(pathToSave_100, this.GetType());
                        pathToSave_100 = "http://103.227.69.123/NTCAAPP/Upload/ApoDocuments/" + fileName;

                        LogHandler.LogInfo(pathToSave_100, this.GetType());

                        file.SaveAs(pathToSave_100);
                    }
                }


               
            }
            catch (Exception ac)
            {
                LogHandler.LogInfo(ac.ToString(), this.GetType());
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