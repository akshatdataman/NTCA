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

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class FDFeedback : BasePage
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
            if (!Page.IsPostBack)
            {
                GetFeedbackFDFormat();
            }
        }
        private void GetFeedbackFDFormat()
        {
            DataSet dsObligationFD = FeedbackBAL.Instance.GetFeedbackFDFormat();
            gvFeebbackFD.DataSource = dsObligationFD;
            gvFeebbackFD.DataBind();
            LoadFeedbackData(gvFeebbackFD);
        }
        private void LoadFeedbackData(GridView gv)
        {
            try
            {
                DataSet dsDraft = FeedbackBAL.Instance.GetFieldDirectorFeedback(feedback.TigerReserveId, feedback.StateId);
                if (dsDraft.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsDraft.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dsDraft.Tables[0].Rows[i];
                        foreach (GridViewRow row in gv.Rows)
                        {
                            Label lblObligationId = (Label)row.FindControl("lblObligationId");
                            Label lblCompiledOrNot = (Label)row.FindControl("lblCompileOrNot");
                            Label lblReason = (Label)row.FindControl("lblReason");
                            TextBox txtScore = (TextBox)row.FindControl("txtScore");
                            TextBox txtCompliance = (TextBox)row.FindControl("txtCompliance");
                            TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                            //DataRow dr = dsDraft.Tables[0].Rows[0];
                            if (dr["ObligationId"].ToString() == lblObligationId.Text)
                            {
                                txtScore.Text = dr["Score"].ToString();
                                txtCompliance.Text = dr["ComplianceprocessUnderway"].ToString();
                                txtRemarks.Text = dr["Remarks"].ToString();
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        private void SubmitFeedbackFD(GridView gv)
        {
            try
            {
                foreach (GridViewRow row in gv.Rows)
                {

                    Label lblObligationId = (Label)row.FindControl("lblObligationId");
                    Label lblCompiledOrNot = (Label)row.FindControl("lblCompileOrNot");
                    Label lblReason = (Label)row.FindControl("lblReason");
                    TextBox txtScore = (TextBox)row.FindControl("txtScore");
                    TextBox txtCompliance = (TextBox)row.FindControl("txtCompliance");
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    if (!string.IsNullOrEmpty(txtScore.Text))
                    {
                        feedback.ObligationId = Convert.ToInt32(lblObligationId.Text);
                        feedback.CompiledOrNot = lblCompiledOrNot.Text;
                        feedback.ReasonIfNotCompiled = lblReason.Text;
                        feedback.Score = Convert.ToInt32(txtScore.Text);
                        feedback.ComplianceProcess = txtCompliance.Text;
                        feedback.Remarks = txtRemarks.Text;

                        FeedbackBAL.Instance.SubmitFeedbackForFD(feedback);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SubmitFeedbackFD(gvFeebbackFD);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}