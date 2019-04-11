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
using System.IO;
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class GenerateReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetStateList();
                GetTigerReserveNames();
                BindLists();
            }
        }

        private void GetStateList()
        {
            DataSet ds = StateBAL.Instance.GetStateList();
            chkState.DataSource = ds;
            chkState.DataTextField = "StateName";
            chkState.DataValueField = "StateId";
            chkState.DataBind();
        }

        private void GetTigerReserveNames()
        {
            DataSet ds = TigerReserveBAL.Instance.GetTigerReserveName();
            chkTigerReserve.DataSource = ds;
            chkTigerReserve.DataTextField = "TigerReserveName";
            chkTigerReserve.DataValueField = "TigerReserveId";
            chkTigerReserve.DataBind();
        }

        private void BindLists()
        {
            DataSet ds = ManageActivityBAL.Instance.GetAreaActivityAndType();

            chkArea.DataSource = ds.Tables[0];
            chkArea.DataValueField = "AreaId";
            chkArea.DataTextField = "Area";
            chkArea.DataBind();

            chkActivityType.DataSource = ds.Tables[1];
            chkActivityType.DataValueField = "ActivityTypeId";
            chkActivityType.DataTextField = "ActivityType";
            chkActivityType.DataBind();

            chkActivity.DataSource = ds.Tables[2];
            chkActivity.DataValueField = "ActivityId";
            chkActivity.DataTextField = "ActivityName";
            chkActivity.DataBind();

            chkActivityItems.DataSource = ds.Tables[3];
            chkActivityItems.DataValueField = "ActivityItemId";
            chkActivityItems.DataTextField = "ActivityItem";
            chkActivityItems.DataBind();

            chkSubActivity.DataSource = ds.Tables[4];
            chkSubActivity.DataValueField = "SubItemId";
            chkSubActivity.DataTextField = "SubItem";
            chkSubActivity.DataBind();
        }

        protected void chkState_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTigerReserveBasedOnStates();
        }

        protected void chkTigerReserve_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void chkActivityType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void chkArea_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GetTigerReserveBasedOnStates()
        {
            foreach (ListItem li in chkState.Items)
            {
                if (li.Selected)
                {
                    DataSet ds = TigerReserveBAL.Instance.GetTigerReserveBasedOnStates(li.Value);
                    chkTigerReserve.Items.Add(ds.ToString());
                    chkTigerReserve.DataTextField = "TigerReserveName";
                    chkTigerReserve.DataValueField = "TigerReserveId";
                    chkTigerReserve.DataBind();
                }
            }
        }
    }
}