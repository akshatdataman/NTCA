using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiseThink.NTCA.Web.App_Code
{
    public class FileUploadHandler : IHttpHandler  
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string fname = context.Server.MapPath("~/Upload/ApoDocuments/" + file.FileName);
                    file.SaveAs(fname);
                }
                context.Response.ContentType = "text/plain";
                //context.Response.Write("File Uploaded Successfully!");
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