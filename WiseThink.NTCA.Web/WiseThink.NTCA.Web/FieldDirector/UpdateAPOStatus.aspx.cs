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
    public partial class UpdateAPOStatus : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCurrentStatus();
                BindStatus();
                UserBAL.Instance.InsertAuditTrailDetail("Visited Update APO Status Page", "Update APO Status");
            }
        }
        private void BindStatus()
        {
            DataSet dsStatus = APOBAL.Instance.GetStatusList();
            ddlName.DataSource = dsStatus;
            ddlName.DataValueField = "StatusId";
            ddlName.DataTextField = "Status";
            ddlName.DataBind();
            ddlName.Items.Insert(0, "Select");
        }
        private void GetCurrentStatus()
        {
            int id = 1;
            DataSet dsCurrentStatus = APOBAL.Instance.GetCurrentAPOStatus(id);
            if (dsCurrentStatus != null)
            {
                DataRow dr = dsCurrentStatus.Tables[0].Rows[0];
                string strStatus = dr["Status"].ToString();
                CurrentStatus.InnerText = strStatus;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlName.SelectedValue != "0" && txtComments.Text != "")
                {
                    APOBAL.Instance.UpdateStatus(1, Convert.ToInt32(ddlName.SelectedValue),false, txtComments.Text,null,null);
                    Response.Redirect("FieldDirectorHome.aspx", false);
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FieldDirectorHome.aspx", false);
        }
    }
}