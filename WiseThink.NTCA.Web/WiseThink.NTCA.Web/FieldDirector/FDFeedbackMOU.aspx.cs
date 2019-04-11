using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.BAL.Authorization;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class FDFeedbackMOU : BasePage
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
                dsIds = FeedbackBAL.Instance.GetTigerReserveIdStateIdForFeedback(1);
            if (dsIds != null)
            {
                DataRow dr = dsIds.Tables[0].Rows[0];
                feedback.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                feedback.StateId = Convert.ToInt32(dr["StateId"]);
                feedback.LoggedInUser = LoggedInUser;
            }
            if (!IsPostBack)
            {
                GetFDFeedBackMOU();
                UserBAL.Instance.InsertAuditTrailDetail("Visited Field Director Feedback Page", "Feedback");
            }
        }

        private void GetFDFeedBackMOU()
        {
            DataSet dsFDFeedback = FeedbackBAL.Instance.GetFieldDirectorFeedback(feedback.TigerReserveId, feedback.StateId);
            gvFeebbackMOU.DataSource = dsFDFeedback;
            gvFeebbackMOU.DataBind();
        }
    }
}