using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WiseThink.NTCA.BAL.Helper
{
    public static class DownloadHelper
    {
        /// <summary>
        /// This is used to get the current Project Location.
        /// </summary>
        public static string ServerMapPath(string path)
        {
            return path;
        }
        /// <summary>
        /// This is used to get Current Response.
        /// </summary>
        public static HttpResponse GetHttpResponse()
        {
            return HttpContext.Current.Response;
        }
        /// <summary>
        /// This is used to download file from server.
        /// </summary>
        /// <param name="fileName"></param>
        public static void DownLoadFileFromServer(string filePath, string fileName)
        {
            //This is used to get Project Location.
            //string filePath = ServerMapPath(fileName);
            //This is used to get the current response.
            HttpResponse res = GetHttpResponse();
            res.Clear();
            res.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            res.ContentType = "application/octet-stream";
            res.WriteFile(filePath);
            res.Flush();
            res.End();
        }
        public static void Download(string sFileName, string sFilePath)
        {
            HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
            String Header = "Attachment; Filename=" + sFileName;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(sFilePath));
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();
        }

    }
}
