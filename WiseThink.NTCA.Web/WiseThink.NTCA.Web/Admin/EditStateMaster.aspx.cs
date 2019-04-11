using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.BAL;
using System.Data;
using WiseThink.NTCA.App_Code;
using System.Configuration;

namespace WiseThink.NTCA.Web.Admin
{
    public partial class EditStateMaster : BasePage
    {
        int StateId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["StateId"] != null)
                    {
                        StateId = Convert.ToInt32(Session["StateId"]);
                        if (StateId != 0)
                        {
                            DataSet ds = StateBAL.Instance.GetState(StateId);
                            txtState.Text = Convert.ToString(ds.Tables[0].Rows[0]["StateName"]);
                            chkIsNorthEastState.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsNorthEastState"]);
                            btnSave.Text = "Update";
                            StateHeader.InnerText = "Edit State";
                        }
                    }
                    UserBAL.Instance.InsertAuditTrailDetail("Visited Add / Edit State Master Page", "Manage State");
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
            try
            {
                State state = new State();
                if (Session["StateId"] != null)
                {
                    StateId = Convert.ToInt32(Session["StateId"]);
                }
                if (StateId != 0)
                {

                    state.StateName = txtState.Text;
                    if (chkIsNorthEastState.Checked)
                        state.IsNorthEastState = true;
                    else
                        state.IsNorthEastState = false;
                    StateBAL.Instance.UpdateState(state, StateId);
                    UserBAL.Instance.InsertAuditTrailDetail("Updated State Information", "Manage State");
                    Session["StateId"] = null;
                    vmSuccess.Message = ConfigurationManager.AppSettings["UpdateSuccessMes"].ToString(); ;
                    FlashMessage.InfoMessage(vmSuccess.Message);
                    //Response.Redirect("StateMaster.aspx", false);
                }
                else
                {
                    state.StateName = txtState.Text;
                    if (chkIsNorthEastState.Checked)
                        state.IsNorthEastState = true;
                    else
                        state.IsNorthEastState = false;
                    StateBAL.Instance.AddState(state);
                    UserBAL.Instance.InsertAuditTrailDetail("Created New State", "Manage State");
                    vmSuccess.Message = ConfigurationManager.AppSettings["SaveSuccessMes"].ToString();
                    FlashMessage.InfoMessage(vmSuccess.Message);
                    txtState.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void chkIsNorthEastState_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}