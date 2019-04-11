using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.Data;

namespace WiseThink.NTCA.DataEntity
{
    public class Utility
    {
        ////Commented by Indu
        //public static string GetUser_IP()
        //{ 
        //    string VisitorsIPAddr;
        //    if (string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
        //    {
        //        VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    else
        //    {
        //        VisitorsIPAddr =
        //        HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        //    }
        //    return VisitorsIPAddr;

        //}
        
        /// <summary>
        /// Added on 08, Feb 2016 by Indu
        /// </summary>
        /// <returns></returns>
        public static string GetUser_IP()
        {
            var ipAddress = string.Empty;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null &&
                     System.Web.HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"].Length != 0)
            {
                ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
            }
            else if (System.Web.HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                ipAddress = System.Web.HttpContext.Current.Request.UserHostName;
            }
            return ipAddress;
        } 
        public static void SetNoCache()
        {
            HttpContext.Current. Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            HttpContext.Current.Response.Expires = -1500;
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);            
        }
        /// <summary>
        /// PrintGridData
        /// Added on 01, Dec 2014 by Indu
        /// </summary>
        /// <param name="dtGridData"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string PrintGridData(DataTable dtGridData, List<PrintKeyValuePair> values)
        {
            StringBuilder sbText = new StringBuilder();
            sbText.Append("<Table class='GridViewStyle' cellspacing='0' rules='all' border='1' style='width:100%;border-collapse:collapse;'>");
            sbText.Append("<tr>");
            sbText.Append("<th scope='col'>S No.</th>");
            foreach (PrintKeyValuePair col in values)
            {

                if (dtGridData.Columns.Contains(col.TableColumn))
                {
                    sbText.Append("<th scope='col'>" + col.DisplayColumn + "</th>");
                }
            }
            sbText.Append("</tr>");
            int counter = 1;
            if (dtGridData.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGridData.Rows)
                {
                    sbText.Append("<tr>");
                    sbText.Append("<td>" + counter + "</td>");
                    foreach (PrintKeyValuePair col in values)
                    {
                        if (dtGridData.Columns.Contains(col.TableColumn))
                        {
                            sbText.Append("<td>" + dr[col.TableColumn].ToString() + "</td>");
                        }
                    }
                    sbText.Append("</tr>");
                    counter++;
                }
            }
            sbText.Append("</Table>");
            return sbText.ToString();
        }
      
    }
}

