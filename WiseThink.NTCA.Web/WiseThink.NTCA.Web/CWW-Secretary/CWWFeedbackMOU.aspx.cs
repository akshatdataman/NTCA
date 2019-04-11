using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.BAL.Authorization;
using System.Data;

namespace WiseThink.NTCA.Web.CWW_Secretary
{
    public partial class CWWFeedbackMOU : BasePage
    {
        Feedback feedback = new Feedback();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsIds;
            string LoggedInUser = AuthoProvider.User;
            if (Session["APOFileId"] != null)
            {
                dsIds = FeedbackBAL.Instance.GetTigerReserveIdStateIdForFeedback(Convert.ToInt32(Session["APOFileId"]));
                Session["APOFileId"] = null;
            }
            else
                dsIds = null;
            if (dsIds != null)
            {
                DataRow dr = dsIds.Tables[0].Rows[0];
                feedback.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                feedback.StateId = Convert.ToInt32(dr["StateId"]);
                feedback.LoggedInUser = LoggedInUser;
            }
            if (!IsPostBack)
            {
                GetCWWFeedBackMOU();
                UserBAL.Instance.InsertAuditTrailDetail("Visited CWLW Obligation Under Tri-MOU", "CWLW Obligation");
            }
        }
        private void GetCWWFeedBackMOU()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(feedback.TigerReserveId)) && !string.IsNullOrEmpty(Convert.ToString(feedback.StateId)))
            {
                DataSet dsFDFeedback = FeedbackBAL.Instance.GetCWWFeedback(feedback.TigerReserveId, feedback.StateId);
                gvFeebbackMOU.DataSource = dsFDFeedback;
                gvFeebbackMOU.DataBind();
            }
        }
    }
}