using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

namespace WiseThink
{
    public static class ExtensionMethods
    {
        public static bool IsValid(this DataSet ds)
        {
            return (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0);
        }
        public static string GetEmptyOrString(this object o)
        {
            return o.IsDBNull() || o.IsNull() ? "" : o.ToString();
        }
        public static bool GetBoolean(this object o)
        {
            return (o.IsDBNull() || o.IsNull()) ? false : Convert.ToBoolean(o);
        }
        public static DateTime GetDayEndTime(this DateTime o)
        {
            return o.AddDays(1).AddSeconds(-1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static object Round(this object o)
        {
            if (!o.IsDBNull())
            {
                o = Math.Round(Convert.ToDouble(o), 3);
                return o;
            }
            return 0;
        }
        public static DataTable RoundData(this DataTable dt, params int[] indexes)
        {

            foreach (DataRow item in dt.Rows)
            {
                foreach (int i in indexes)
                {
                    item[dt.Columns[i]] = item[dt.Columns[i]].Round();
                }
            }
            return dt;
        }

        public static bool IsDBNull(this Object o)
        {
            return o == DBNull.Value;
        }
        public static bool IsNull(this Object o)
        {
            return o == null;
        }
        public static string GetApplicationFormatString(this DateTime date)
        {
            return date.ToString(ConfigManager.DateTimeFormat);
        }
        public static string GetApplicationFormatString(this DateTime date, string format)
        {
            return date.ToString(format);
        }
        public static T ConvertToEnum<T>(this string status, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), status, ignoreCase);
        }

        public static string RemoveScriptAndHtml(this string strValue)
        {
            var newHtml = RemoveScript(strValue);
            Regex regxScriptRemoval = new Regex(@"<[^>]+>[\s\S]*?</[^>]+>");
            newHtml = regxScriptRemoval.Replace(newHtml, "");
            regxScriptRemoval = new Regex(@"<[^>]*/>");
            newHtml = regxScriptRemoval.Replace(newHtml, "");
            return newHtml;
        }
        public static string RemoveScript(this string strValue)
        {
            Regex regxScriptRemoval = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            var newHtml = regxScriptRemoval.Replace(strValue, "");
            return newHtml;
        }
        public static string FormatEmailIdWithInString(this string strValue)
        {
            string pattren = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            if (Regex.IsMatch(strValue, pattren))
            {
                MatchCollection col = Regex.Matches(strValue, pattren);
                foreach (Match m in col)
                {
                    string val = m.Value.Replace("@", "[at]");
                    val = val.Replace(".", "[dot]");
                    strValue = strValue.Replace(m.Value, val);
                }
            }
            return strValue;
        }
        public static string FormatStringToValidMailId(this string strValue)
        {
            strValue = strValue.Replace("[at]", "@");
            strValue = strValue.Replace("[dot]", ".");
            return strValue;
        }
        public static string CurrentFinancialYear(this DateTime date)
        {
            if ((DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3))
            {
                string CurrentFinanacialYear = Convert.ToString(DateTime.Now.Year - 1) + "-" + Convert.ToString(DateTime.Now.Year);
                return CurrentFinanacialYear;
            }
            else
            {
                string CurrentFinanacialYear = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Year + 1);
                return CurrentFinanacialYear;
            }
        }
        public static string PreviousFinancialYear(this DateTime date)
        {
            if ((DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3))
            {
                string PreviousFinancialYear = Convert.ToString(DateTime.Now.Year - 2) + "-" + Convert.ToString(DateTime.Now.Year - 1);
                return PreviousFinancialYear;
            }
            else
            {
                string PreviousFinancialYear = Convert.ToString(DateTime.Now.Year - 1) + "-" + Convert.ToString(DateTime.Now.Year);
                return PreviousFinancialYear;
            }
        }
        public static string GetFormatedDateTimeString(this DateTime date)
        {
            return date.ToString(System.Configuration.ConfigurationManager.AppSettings["datetimeFormat"]);
        }
        public static string GetFormatedDateTimeString(this DateTime date, string format)
        {
            return date.ToString(format);
        }
        /// <summary>
        /// GetClearString
        /// Author: Indu
        /// Date: 02 Dec 2014
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string GetClearString(this string strValue)
        {
            Regex regxScriptRemoval = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            var newHtml = regxScriptRemoval.Replace(strValue, "");
            regxScriptRemoval = new Regex(@"<[^>]+>[\s\S]*?</[^>]+>");
            newHtml = regxScriptRemoval.Replace(newHtml, "");
            regxScriptRemoval = new Regex(@"<[^>]*/>");
            newHtml = regxScriptRemoval.Replace(newHtml, "");
            return newHtml;
        }
        /// <summary>
        /// GetStatusValue
        /// Author: Indu
        /// Date: 02 Dec 2014
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static WiseThink.NTCA.DataEntity.Status GetStatusValue(this string status)
        {
            return (WiseThink.NTCA.DataEntity.Status)Enum.Parse(typeof(WiseThink.NTCA.DataEntity.Status), status);
        }
    }
}
