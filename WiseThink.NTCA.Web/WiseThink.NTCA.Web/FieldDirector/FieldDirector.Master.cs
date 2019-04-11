using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class FieldDirectorMaster : BaseMaster
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DateTime.Now.Month == 4)
            {
                dvmarquee.Style.Add("display", "block");
                DataSet dsTotalDaysLeft = APOBAL.Instance.GetDurationToFillApo();
                if (dsTotalDaysLeft.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = dsTotalDaysLeft.Tables[0].Rows[0];
                    string TotalDays = dr["TotalDays"].ToString();
                    string FromDate = dr["FromDate"].ToString();
                    string ToDate = dr["ToDate"].ToString();
                    string message = lblApoTimeline.InnerText;
                    string finalMsg = message.Replace("20", TotalDays);
                    lblApoTimeline.InnerText = finalMsg;
                }
            }
            else
            {
                dvmarquee.Style.Add("display", "none");
            }
        }
    }
}