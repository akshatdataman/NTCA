using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiseThink.NTCA.Web.FieldDirector
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           // context.Response.ContentType = "text/plain";
          //  context.Response.Write("Hello World");
            try
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


                }
            }
            catch (Exception ex)
            {
                return;
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