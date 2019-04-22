using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.DataEntity.Entities;
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.Admin
{
    public partial class ManageActivities : BasePage
    {  
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetManageActivityData();
                    GetActivitySubItems();
                    BindDropdowns();
                   
                    UserBAL.Instance.InsertAuditTrailDetail("Visited Manage Activites", "Manage Activites");
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                //return;
            }
        }
        private void RegisterPostBackControl()
        {
            //ScriptManager.GetCurrent(this).RegisterPostBackControl(content);
        }
        protected void cgvArea_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvArea.PageIndex = e.NewPageIndex;
            GetManageActivityData();
           
        }
        
        protected void cgvActivityType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvActivityType.PageIndex = e.NewPageIndex;
            GetManageActivityData();
        }
        protected void cgvActivity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvActivity.PageIndex = e.NewPageIndex;
            GetManageActivityData();
            
        }
        //protected void cgvActivity_PageIndexChanged(object sender, GridViewPageEventArgs e)
        //{
        //    cgvActivity.PageIndex = -1;
        //    GetManageActivityData();

        //}
        protected void cgvActivity_PageIndexChanged(object sender, EventArgs e)
        {
            cgvActivity.EditIndex = -1;
            GetManageActivityData();
            
        }
        protected void cgvActivityItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            cgvActivityItems.PageIndex = e.NewPageIndex;
            GetManageActivityData(); 
            GetSearchData();
            
        }
        protected void cgvActivityItems_PageIndexChanged(object sender, EventArgs e)
        {
            cgvActivityItems.EditIndex = -1;
            GetManageActivityData();
            GetSearchData();
            //AddMoreDiv.Visible = true;
        }
        
        private void GetManageActivityData()
        {
             DataTable dt = new DataTable();
             DataSet ds = ManageActivityBAL.Instance.GetManageActivityMasterData();
             ViewState["ds"] = ds;
            cgvArea.DataSource = ds.Tables[0];
            cgvArea.DataBind();
            
            cgvActivityType.DataSource = ds.Tables[1];
            cgvActivityType.DataBind();
            cgvActivity.DataSource = ds.Tables[2];
            cgvActivity.DataBind();
            if (ViewState["dv"] != null)
            {
                cgvActivityItems.DataSource = ViewState["dv"];
                cgvActivityItems.DataBind();
            }
            else
            {
                cgvActivityItems.DataSource = ds.Tables[3];
                cgvActivityItems.DataBind();
            }

            gvSubItems.DataSource = dt;
            gvSubItems.DataBind();
        }

        protected void cgvActivity_RowEditing(object sender, GridViewEditEventArgs e)
        {
            cgvActivity.EditIndex = e.NewEditIndex;
            GetManageActivityData();
        }

        protected void cgvActivity_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            cgvActivity.EditIndex = -1;
            GetManageActivityData();
        }

        protected void cgvActivity_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label id = cgvActivity.Rows[e.RowIndex].FindControl("lblId") as Label;
                int activityId = Convert.ToInt32(id.Text);
                TextBox activity = cgvActivity.Rows[e.RowIndex].FindControl("txtActivity") as TextBox;
                string strActivity = activity.Text;
                ManageActivityBAL.Instance.UpdateActivity(activityId, strActivity);
                UserBAL.Instance.InsertAuditTrailDetail("Updated Activity", "Manage Activites");
                cgvActivity.EditIndex = -1;
                GetManageActivityData();
                string strSuccess = "Activity has been updated successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                //return;
            }
        }

        protected void cgvActivityItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            cgvActivityItems.EditIndex = e.NewEditIndex;            
            GetSearchData();
           
            //AddMoreDiv.Visible = false;
        }

        protected void cgvActivityItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            cgvActivityItems.EditIndex = -1;
            GetSearchData();
            //AddMoreDiv.Visible = true;
        }

        protected void cgvActivityItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                DropDownList ddlArea = cgvActivityItems.Rows[e.RowIndex].FindControl("ddlArea") as DropDownList;
                int _areaId = Convert.ToInt32(ddlArea.SelectedValue);
                DropDownList ddlActivityType = cgvActivityItems.Rows[e.RowIndex].FindControl("ddlActivityType") as DropDownList;
                int _activityTypeId = Convert.ToInt32(ddlActivityType.SelectedValue);
                DropDownList ddlActivity = cgvActivityItems.Rows[e.RowIndex].FindControl("ddlActivity") as DropDownList;
                int _activityId = Convert.ToInt32(ddlActivity.SelectedValue);
                Label activityItemid = cgvActivityItems.Rows[e.RowIndex].FindControl("lblActivityItemId") as Label;
                TextBox lblCSSPTParaNo = cgvActivityItems.Rows[e.RowIndex].FindControl("txtCSSPTParaNo") as TextBox;
                string strCSSPTParaNo = lblCSSPTParaNo.Text;
                int activityItemId = Convert.ToInt32(activityItemid.Text);
                TextBox activityItem = cgvActivityItems.Rows[e.RowIndex].FindControl("txtActivityItem") as TextBox;
                string strActivityItem = activityItem.Text;
                CheckBox Gps = cgvActivityItems.Rows[e.RowIndex].FindControl("chkGPSActive") as CheckBox;
                string gpsRequired = Convert.ToString(Gps.Checked);
                ManageActivityBAL.Instance.UpdateActivityItem(_areaId, _activityTypeId, _activityId,strCSSPTParaNo, activityItemId, strActivityItem,gpsRequired);
                UserBAL.Instance.InsertAuditTrailDetail("Updated Activity Item", "Manage Activites");
                cgvActivityItems.EditIndex = -1;
                GetManageActivityData();
                GetSearchData();
                //AddMoreDiv.Visible = true;
                string strSuccess = "Activity item has been updated successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
               Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                //return;
            }
        }

        protected void btnAddActivity_Click(object sender, EventArgs e)
        {
            try
            {
                string activityName = txtActivity1.Text;
                ManageActivityBAL.Instance.AddActivity(activityName);
                string strSuccess = "Data saved successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
                txtActivity1.Text = string.Empty;
                GetManageActivityData();
                UserBAL.Instance.InsertAuditTrailDetail("Created Activity", "Manage Activites");
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                //return;
            }
        }
        private void ClearActivity()
        {
            txtActivity1.Text = "";
        }

        protected void btnActivityCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddActivityItem_Click(object sender, EventArgs e)
        {
            try
            {
                int val;
                Activity oActivity = new Activity();
                oActivity.AreaId = Convert.ToInt32(ddlAreaForActivityItem.SelectedValue);
                oActivity.ActivityTypeId = Convert.ToInt32(ddlActivityTypeForActivityItem.SelectedValue);
                oActivity.ActivityId = Convert.ToInt32(ddlActivityForActivityItem.SelectedValue);
                oActivity.ActivityItemName = txtActivityItem.Text;
                oActivity.ParaNoCSSPTGuidelines = txtCSSPTParaNo.Text;
                if (chkGPS.Checked == true)
                    val = 1;
                else
                    val = 0;
                oActivity.GpsStatus = Convert.ToString(val);
                ManageActivityBAL.Instance.AddActivityItem(oActivity);
                string strSuccess = "Data saved successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);                
                GetManageActivityData();
                GetSearchData();
                ClearActivityItem();
                UserBAL.Instance.InsertAuditTrailDetail("Created Activity Item", "Manage Activites");
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                //Response.RedirectPermanent("~/ErrorPage.aspx", false);
                //string strError = ex.Message;
                //vmError.Message = strError;
                //FlashMessage.ErrorMessage(vmError.Message);
                //return;
            }
        }
        private void ClearActivityItem()
        {
            ddlAreaForActivityItem.SelectedIndex = 0;
            ddlActivityTypeForActivityItem.SelectedIndex = 0;
            ddlActivityForActivityItem.SelectedIndex = 0;
            txtCSSPTParaNo.Text = "";
            txtActivityItem.Text = "";
            chkGPS.Checked = false;
        }

        protected void btnActivityItemCancel_Click(object sender, EventArgs e)
        {

        }
        
        private void BindDropdowns()
        {
            DataSet ds = ManageActivityBAL.Instance.GetAreaActivityAndType();

            ddlAreaForActivityItem.DataSource = ds.Tables[0];
            ddlAreaForActivityItem.DataValueField = "AreaId";
            ddlAreaForActivityItem.DataTextField = "Area";
            ddlAreaForActivityItem.DataBind();
            ddlAreaForActivityItem.Items.Insert(0, "Select Area");

            ddlItemAreaSearch.DataSource = ds.Tables[0];
            ddlItemAreaSearch.DataValueField = "AreaId";
            ddlItemAreaSearch.DataTextField = "Area";
            ddlItemAreaSearch.DataBind();
            ddlItemAreaSearch.Items.Insert(0, "Select Area");


            ddlItemAreaForActivitySubItem.DataSource = ds.Tables[0];
            ddlItemAreaForActivitySubItem.DataValueField = "AreaId";
            ddlItemAreaForActivitySubItem.DataTextField = "Area";
            ddlItemAreaForActivitySubItem.DataBind();
            ddlItemAreaForActivitySubItem.Items.Insert(0, "Select Area");

                     
            ddlActivityForActivityItem.DataSource = ds.Tables[1];
            ddlActivityForActivityItem.DataValueField = "ActivityTypeId";
            ddlActivityForActivityItem.DataTextField = "ActivityType";
            ddlActivityForActivityItem.DataBind();
            ddlActivityForActivityItem.Items.Insert(0, "Select Activity Type");

            ddlItemActivityTypeForActivitySubItem.DataSource = ds.Tables[1];
            ddlItemActivityTypeForActivitySubItem.DataValueField = "ActivityTypeId";
            ddlItemActivityTypeForActivitySubItem.DataTextField = "ActivityType";
            ddlItemActivityTypeForActivitySubItem.DataBind();
            ddlItemActivityTypeForActivitySubItem.Items.Insert(0, "Select Activity Type");

            ddlActivityTypeForActivityItem.DataSource = ds.Tables[1];
            ddlActivityTypeForActivityItem.DataValueField = "ActivityTypeId";
            ddlActivityTypeForActivityItem.DataTextField = "ActivityType";
            ddlActivityTypeForActivityItem.DataBind();
            ddlActivityTypeForActivityItem.Items.Insert(0, "Select Activity Type");

            ddlItemActivityTypeSearch.DataSource = ds.Tables[1];
            ddlItemActivityTypeSearch.DataValueField = "ActivityTypeId";
            ddlItemActivityTypeSearch.DataTextField = "ActivityType";
            ddlItemActivityTypeSearch.DataBind();
            ddlItemActivityTypeSearch.Items.Insert(0, "Select Activity Type");

            ddlActivityForActivityItem.DataSource = ds.Tables[2];
            ddlActivityForActivityItem.DataValueField = "ActivityId";
            ddlActivityForActivityItem.DataTextField = "ActivityName";
            ddlActivityForActivityItem.DataBind();
            ddlActivityForActivityItem.Items.Insert(0, "Select Activity");

            ddlActivityItemSearch.DataSource = ds.Tables[2];
            ddlActivityItemSearch.DataValueField = "ActivityId";
            ddlActivityItemSearch.DataTextField = "ActivityName";
            ddlActivityItemSearch.DataBind();
            ddlActivityItemSearch.Items.Insert(0, "Select Activity");

            ddlItemActivityForActivity.DataSource = ds.Tables[2];
            ddlItemActivityForActivity.DataValueField = "ActivityId";
            ddlItemActivityForActivity.DataTextField = "ActivityName";
            ddlItemActivityForActivity.DataBind();
            ddlItemActivityForActivity.Items.Insert(0, "Select Activity");

            ddlActivityForActivity.DataSource = ds.Tables[2];
            ddlActivityForActivity.DataValueField = "ActivityId";
            ddlActivityForActivity.DataTextField = "ActivityName";
            ddlActivityForActivity.DataBind();
            ddlActivityForActivity.Items.Insert(0, "Select Activity");

            ddlItemActivityForActivitySubItem.DataSource = ds.Tables[3];
            ddlItemActivityForActivitySubItem.DataValueField = "ActivityItemId";
            ddlItemActivityForActivitySubItem.DataTextField = "ActivityItem";
            ddlItemActivityForActivitySubItem.DataBind();
            ddlItemActivityForActivitySubItem.Items.Insert(0, "Select Activity Item");

            ddlActivityForActivitySubItem.DataSource = ds.Tables[3];
            ddlActivityForActivitySubItem.DataValueField = "ActivityItemId";
            ddlActivityForActivitySubItem.DataTextField = "ActivityItem";
            ddlActivityForActivitySubItem.DataBind();
            ddlActivityForActivitySubItem.Items.Insert(0, "Select Activity Item");
        }

        protected void cgvActivity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Button btn = e.Row.FindControl("btnEditActivity") as Button;
            //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btn);
            //GetManageActivityData();
        }

        //protected void ddlActivtyTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlAreaSearch.SelectedIndex != 0 && ddlActivtyTypeSearch.SelectedIndex == 0)
        //    {
        //        DataSet dsArea = (DataSet)ViewState["ds"];
        //        DataTable dtArea = dsArea.Tables[2];
        //        string filterCondition = "Area = '" + ddlAreaSearch.SelectedItem.Text + "'";
        //        DataView dv = new DataView(dtArea);
        //        dv.RowFilter = filterCondition;
        //        cgvActivity.DataSource = dv;
        //        cgvActivity.DataBind();
        //    }
        //    else if (ddlAreaSearch.SelectedIndex != 0 && ddlActivtyTypeSearch.SelectedIndex != 0)
        //    {
        //        DataSet dsArea = (DataSet)ViewState["ds"];
        //        DataTable dtArea = dsArea.Tables[2];
        //        string filterCondition = "Area = '" + ddlAreaSearch.SelectedItem.Text + "' AND ActivityType = '" + ddlActivtyTypeSearch.SelectedItem.Text + "'";
        //        DataView dv = new DataView(dtArea);
        //        dv.RowFilter = filterCondition;
        //        cgvActivity.DataSource = dv;
        //        cgvActivity.DataBind();
        //    }
        //    else if (ddlAreaSearch.SelectedIndex == 0 && ddlActivtyTypeSearch.SelectedIndex != 0)
        //    {
        //        DataSet dsArea = (DataSet)ViewState["ds"];
        //        DataTable dtArea = dsArea.Tables[2];
        //        string filterCondition = "ActivityType = '" + ddlActivtyTypeSearch.SelectedItem.Text + "'";
        //        DataView dv = new DataView(dtArea);
        //        dv.RowFilter = filterCondition;
        //        cgvActivity.DataSource = dv;
        //        cgvActivity.DataBind();
        //    }

        //}

        private void GetSearchData()
        {
            if (ddlItemAreaSearch.SelectedIndex != 0 && ddlItemActivityTypeSearch.SelectedIndex == 0 && ddlActivityItemSearch.SelectedIndex == 0)
            {
                DataSet dsArea = (DataSet)ViewState["ds"];
                DataTable dtArea = dsArea.Tables[3];
                string filterCondition = "Area = '" + ddlItemAreaSearch.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                cgvActivityItems.DataSource = dv;
                cgvActivityItems.DataBind();
            }
            else if (ddlItemAreaSearch.SelectedIndex != 0 && ddlItemActivityTypeSearch.SelectedIndex != 0 && ddlActivityItemSearch.SelectedIndex == 0)
            {
                DataSet dsArea = (DataSet)ViewState["ds"];
                DataTable dtArea = dsArea.Tables[3];
                string filterCondition = "Area = '" + ddlItemAreaSearch.SelectedItem.Text + "' AND ActivityType = '" + ddlItemActivityTypeSearch.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                cgvActivityItems.DataSource = dv;
                cgvActivityItems.DataBind();
            }
            else if (ddlItemAreaSearch.SelectedIndex != 0 && ddlItemActivityTypeSearch.SelectedIndex != 0 && ddlActivityItemSearch.SelectedIndex != 0)
            {
                DataSet dsArea = (DataSet)ViewState["ds"];
                DataTable dtArea = dsArea.Tables[3];
                string filterCondition = "Area = '" + ddlItemAreaSearch.SelectedItem.Text + "' AND ActivityType = '" + ddlItemActivityTypeSearch.SelectedItem.Text + "' AND ActivityName = '" + ddlActivityItemSearch.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                cgvActivityItems.DataSource = dv;
                cgvActivityItems.DataBind();
            }
            else if (ddlItemAreaSearch.SelectedIndex != 0 && ddlItemActivityTypeSearch.SelectedIndex == 0 && ddlActivityItemSearch.SelectedIndex != 0)
            {
                DataSet dsArea = (DataSet)ViewState["ds"];
                DataTable dtArea = dsArea.Tables[3];
                string filterCondition = "Area = '" + ddlItemAreaSearch.SelectedItem.Text + "' AND ActivityName = '" + ddlActivityItemSearch.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                cgvActivityItems.DataSource = dv;
                cgvActivityItems.DataBind();
            }
            else if (ddlItemAreaSearch.SelectedIndex == 0 && ddlItemActivityTypeSearch.SelectedIndex == 0 && ddlActivityItemSearch.SelectedIndex != 0)
            {
                DataSet dsArea = (DataSet)ViewState["ds"];
                DataTable dtArea = dsArea.Tables[3];
                string filterCondition = "ActivityName = '" + ddlActivityItemSearch.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                cgvActivityItems.DataSource = dv;
                cgvActivityItems.DataBind();
            }
            else if (ddlItemAreaSearch.SelectedIndex == 0 && ddlItemActivityTypeSearch.SelectedIndex != 0 && ddlActivityItemSearch.SelectedIndex == 0)
            {
                DataSet dsArea = (DataSet)ViewState["ds"];
                DataTable dtArea = dsArea.Tables[3];
                string filterCondition = "ActivityType = '" + ddlItemActivityTypeSearch.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                cgvActivityItems.DataSource = dv;
                cgvActivityItems.DataBind();
            }
            else if (ddlItemAreaSearch.SelectedIndex == 0 && ddlItemActivityTypeSearch.SelectedIndex != 0 && ddlActivityItemSearch.SelectedIndex != 0)
            {
                DataSet dsArea = (DataSet)ViewState["ds"];
                DataTable dtArea = dsArea.Tables[3];
                string filterCondition = "ActivityType = '" + ddlItemActivityTypeSearch.SelectedItem.Text + "' AND ActivityName = '" + ddlActivityItemSearch.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                cgvActivityItems.DataSource = dv;
                cgvActivityItems.DataBind();
            }
            else if (ddlItemAreaSearch.SelectedIndex == 0 && ddlItemActivityTypeSearch.SelectedIndex == 0 && ddlActivityItemSearch.SelectedIndex == 0)
            {
                DataSet dsArea = (DataSet)ViewState["ds"];
                DataTable dtArea = dsArea.Tables[3];
                DataView dv = new DataView(dtArea);
                cgvActivityItems.DataSource = dv;
                cgvActivityItems.DataBind();
            }
        }

        protected void ddlActivityItemSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSearchData();
        }


       
        protected void cgvActivityItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet ds = ManageActivityBAL.Instance.GetAreaActivityAndType();
            if (e.Row.RowType == DataControlRowType.DataRow && cgvActivityItems.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlArea = (DropDownList)e.Row.FindControl("ddlArea");
                Label lblAreaId = (Label)e.Row.FindControl("lblAreaId");


                ddlArea.DataSource = ds.Tables[0];
                ddlArea.DataValueField = "AreaId";
                ddlArea.DataTextField = "Area";
                ddlArea.DataBind();
                ddlArea.Items.Insert(0, new ListItem("Please select"));
                string areaId = lblAreaId.Text;
                ddlArea.Items.FindByValue(areaId).Selected = true;

                DropDownList ddlActivityType = (DropDownList)e.Row.FindControl("ddlActivityType");
                Label lblActivityTypeId = (Label)e.Row.FindControl("lblActivityTypeId");
                ddlActivityType.DataSource = ds.Tables[1];
                ddlActivityType.DataValueField = "ActivityTypeId";
                ddlActivityType.DataTextField = "ActivityType";
                ddlActivityType.DataBind();
                //ddlActivityType.Items.Insert(0, "Select Activity Type");
                ddlActivityType.Items.Insert(0, new ListItem("Please select"));
                string activityTypeId = lblActivityTypeId.Text;
                ddlActivityType.Items.FindByValue(activityTypeId).Selected = true;

                DropDownList ddlActivity = (DropDownList)e.Row.FindControl("ddlActivity");
                Label lblActivityId = (Label)e.Row.FindControl("lblActivityId");
                ddlActivity.DataSource = ds.Tables[2];
                ddlActivity.DataValueField = "ActivityId";
                ddlActivity.DataTextField = "ActivityName";
                ddlActivity.DataBind();
                //ddlActivity.Items.Insert(0, "Select Activity");
                ddlActivity.Items.Insert(0, new ListItem("Please select"));
                string activityId = lblActivityId.Text;
                ddlActivity.Items.FindByValue(activityId).Selected = true;
            }
            
        }

        protected void btnAddSubItems_Click(object sender, EventArgs e)
        {
            try
            {
                Activity SubActivity = new Activity();
                if(ddlActivityForActivitySubItem.SelectedValue!="Select Activity Item")
                {
                    SubActivity.ActivityItemId = Convert.ToInt32(ddlActivityForActivitySubItem.SelectedValue);
                }
                SubActivity.ParaNo = txtParaNo.Text.Trim();
                SubActivity.ActivitySubItem = txtSubActivityItem.Text.Trim();
                ManageActivityBAL.Instance.AddActivitySubItem(SubActivity);
                GetActivitySubItems();
                clear();
            }
            catch
            {
                throw;
            }
        }

        protected void clear()
        {

            ddlActivityForActivity.SelectedIndex = 0;
            ddlActivityForActivitySubItem.SelectedIndex = 0;
            txtParaNo.Text = "";
            txtSubActivityItem.Text = "";
        }

        protected void btnSubCancel_Click(object sender, EventArgs e)
        {

        }

       
        protected void ddlActivityForActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = ManageActivityBAL.Instance.GetActivityItemForAddNewSubItem(ddlActivityForActivity.SelectedValue);
                ddlActivityForActivitySubItem.DataSource = ds.Tables[0];
                ddlActivityForActivitySubItem.DataValueField = "ActivityItemId";
                ddlActivityForActivitySubItem.DataTextField = "ActivityItem";
                ddlActivityForActivitySubItem.DataBind();
                ddlActivityForActivitySubItem.Items.Insert(0, "Select Activity Item");
            }
            catch
            {
                throw;
            }
        }

        protected void gvSubItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSubItems.EditIndex = -1;
            GetActivitySubItemSearchData();
        }

        protected void gvSubItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSubItems.EditIndex = e.NewEditIndex;
            GetActivitySubItemSearchData();
        }

        protected void gvSubItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                DropDownList ddlSubItem = gvSubItems.Rows[e.RowIndex].FindControl("ddlSubItem") as DropDownList;
                int _activityTypeId = Convert.ToInt32(ddlSubItem.SelectedValue);
                
                TextBox txtSubCSSPTParaNo = gvSubItems.Rows[e.RowIndex].FindControl("txtSubCSSPTParaNo") as TextBox;
                string strCSSPTParaNo = txtSubCSSPTParaNo.Text;
                TextBox txtActivitySubItem = gvSubItems.Rows[e.RowIndex].FindControl("txtActivitySubItem") as TextBox;
                string activitySubItem = txtActivitySubItem.Text;

                Label activitySubItemid = gvSubItems.Rows[e.RowIndex].FindControl("lblSubItemId") as Label;
                int subItemId = Convert.ToInt32(activitySubItemid.Text);
               
                ManageActivityBAL.Instance.UpdateActivitySubItem(_activityTypeId, strCSSPTParaNo, activitySubItem, subItemId);
                UserBAL.Instance.InsertAuditTrailDetail("Updated Sub Activity Item", "Manage Activites");
                gvSubItems.EditIndex = -1;
                GetActivitySubItems();
                GetActivitySubItemSearchData();

            }
            catch
            {

            }
        }

        protected void gvSubItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet ds = ManageActivityBAL.Instance.GetAreaActivityAndType();
            if (e.Row.RowType == DataControlRowType.DataRow && gvSubItems.EditIndex == e.Row.RowIndex)
            {
                
                DropDownList ddlSubActivity = (DropDownList)e.Row.FindControl("ddlSubActivity");
                Label lblSubActivityId = (Label)e.Row.FindControl("lblSubActivityId");
                ddlSubActivity.DataSource = ds.Tables[2];
                ddlSubActivity.DataValueField = "ActivityId";
                ddlSubActivity.DataTextField = "ActivityName";
                ddlSubActivity.DataBind();
                //ddlActivity.Items.Insert(0, "Select Activity");
                ddlSubActivity.Items.Insert(0, new ListItem("Please select"));
                string activityId = lblSubActivityId.Text;
                ddlSubActivity.Items.FindByValue(activityId).Selected = true;

                DropDownList ddlSubItem = (DropDownList)e.Row.FindControl("ddlSubItem");
                Label lblSubActivityItemId = (Label)e.Row.FindControl("lblSubActivityItemId");
                //ddlSubItem.DataSource = ds.Tables[3];
                //ddlSubItem.DataValueField = "ActivityItemId";
                //ddlSubItem.DataTextField = "ActivityItem";
                //ddlSubItem.DataBind();                
                //ddlSubItem.Items.Insert(0, new ListItem("Please select"));
                ds = new DataSet();
                ds = ManageActivityBAL.Instance.GetActivityItemForAddNewSubItem(ddlSubActivity.SelectedValue);
                ddlSubItem.DataSource = ds.Tables[0];
                ddlSubItem.DataValueField = "ActivityItemId";
                ddlSubItem.DataTextField = "ActivityItem";
                ddlSubItem.DataBind();
                ddlSubItem.Items.Insert(0, "Select Activity Item");
                string subActivityId = lblSubActivityItemId.Text;
                ddlSubItem.Items.FindByValue(subActivityId).Selected = true;
            }
        }

        protected void gvSubItems_PageIndexChanged(object sender, EventArgs e)
        {
            cgvActivityItems.EditIndex = -1;
            GetActivitySubItems();
            GetActivitySubItemSearchData();
        }

        protected void gvSubItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSubItems.PageIndex = e.NewPageIndex;
            GetActivitySubItems();
            GetActivitySubItemSearchData();
        }

        private void GetActivitySubItems()
        {
            DataSet dsSub = ManageActivityBAL.Instance.GetActivitySubItemList();
            ViewState["dsSub"] = dsSub;
            gvSubItems.DataSource = dsSub.Tables[0];
            gvSubItems.DataBind();
        }

        private void GetActivitySubItemSearchData()
        {
           if (ddlItemAreaForActivitySubItem.SelectedIndex != 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex == 0 && ddlItemActivityForActivity.SelectedIndex == 0 && ddlItemActivityForActivitySubItem.SelectedIndex == 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                string filterCondition = "Area = '" + ddlItemAreaForActivitySubItem.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }
            else if (ddlItemAreaForActivitySubItem.SelectedIndex != 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex != 0 && ddlItemActivityForActivity.SelectedIndex == 0 && ddlItemActivityForActivitySubItem.SelectedIndex == 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                string filterCondition = "Area = '" + ddlItemAreaForActivitySubItem.SelectedItem.Text + "' AND ActivityType = '" + ddlItemActivityTypeForActivitySubItem.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }
            else if (ddlItemAreaForActivitySubItem.SelectedIndex != 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex != 0 && ddlItemActivityForActivity.SelectedIndex != 0 && ddlItemActivityForActivitySubItem.SelectedIndex == 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                string filterCondition = "Area = '" + ddlItemAreaForActivitySubItem.SelectedItem.Text + "' AND ActivityType = '" + ddlItemActivityTypeForActivitySubItem.SelectedItem.Text + "' AND ActivityName = '" + ddlItemActivityForActivity.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }
            else if (ddlItemAreaForActivitySubItem.SelectedIndex != 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex == 0 && ddlItemActivityForActivity.SelectedIndex != 0 && ddlItemActivityForActivitySubItem.SelectedIndex == 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                string filterCondition = "Area = '" + ddlItemAreaForActivitySubItem.SelectedItem.Text + "' AND ActivityName = '" + ddlItemActivityForActivity.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }
            else if (ddlItemAreaForActivitySubItem.SelectedIndex == 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex == 0 && ddlItemActivityForActivity.SelectedIndex != 0 && ddlItemActivityForActivitySubItem.SelectedIndex == 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                string filterCondition = "ActivityName = '" + ddlItemActivityForActivity.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }
            else if (ddlItemAreaForActivitySubItem.SelectedIndex == 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex != 0 && ddlItemActivityForActivity.SelectedIndex == 0 && ddlItemActivityForActivitySubItem.SelectedIndex == 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                string filterCondition = "ActivityType = '" + ddlItemActivityTypeForActivitySubItem.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }
            else if (ddlItemAreaForActivitySubItem.SelectedIndex == 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex != 0 && ddlItemActivityForActivity.SelectedIndex != 0 && ddlItemActivityForActivitySubItem.SelectedIndex == 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                string filterCondition = "ActivityType = '" + ddlItemActivityTypeForActivitySubItem.SelectedItem.Text + "' AND ActivityName = '" + ddlItemActivityForActivity.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }
            else if (ddlItemAreaForActivitySubItem.SelectedIndex == 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex == 0 && ddlItemActivityForActivity.SelectedIndex == 0 && ddlItemActivityForActivitySubItem.SelectedIndex == 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                DataView dv = new DataView(dtArea);
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }
            else if (ddlItemAreaForActivitySubItem.SelectedIndex != 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex == 0 && ddlItemActivityForActivity.SelectedIndex == 0 && ddlItemActivityForActivitySubItem.SelectedIndex != 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                string filterCondition = "Area = '" + ddlItemAreaForActivitySubItem.SelectedItem.Text + "' AND ActivityItem = '" + ddlItemActivityForActivitySubItem.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }
            else if (ddlItemAreaForActivitySubItem.SelectedIndex == 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex != 0 && ddlItemActivityForActivity.SelectedIndex == 0 && ddlItemActivityForActivitySubItem.SelectedIndex != 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                string filterCondition = "ActivityType = '" + ddlItemActivityTypeForActivitySubItem.SelectedItem.Text + "' AND ActivityItem = '" + ddlItemActivityForActivitySubItem.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }
            else if (ddlItemAreaForActivitySubItem.SelectedIndex == 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex == 0 && ddlItemActivityForActivity.SelectedIndex != 0 && ddlItemActivityForActivitySubItem.SelectedIndex != 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                string filterCondition = "ActivityName = '" + ddlItemActivityForActivity.SelectedItem.Text + "' AND ActivityItem = '" + ddlItemActivityForActivitySubItem.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }

            else if (ddlItemAreaForActivitySubItem.SelectedIndex == 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex == 0 && ddlItemActivityForActivity.SelectedIndex == 0 && ddlItemActivityForActivitySubItem.SelectedIndex != 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                string filterCondition = "ActivityItem = '" + ddlItemActivityForActivitySubItem.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }
            else if (ddlItemAreaForActivitySubItem.SelectedIndex != 0 && ddlItemActivityTypeForActivitySubItem.SelectedIndex != 0 && ddlItemActivityForActivity.SelectedIndex != 0 && ddlItemActivityForActivitySubItem.SelectedIndex != 0)
            {
                DataSet dsArea = (DataSet)ViewState["dsSub"];
                DataTable dtArea = dsArea.Tables[0];
                string filterCondition = "Area = '" + ddlItemAreaForActivitySubItem.SelectedItem.Text + "' AND ActivityType = '" + ddlItemActivityTypeForActivitySubItem.SelectedItem.Text + "' AND ActivityName = '" + ddlItemActivityForActivity.SelectedItem.Text + "' AND ActivityItem = '" + ddlItemActivityForActivitySubItem.SelectedItem.Text + "'";
                DataView dv = new DataView(dtArea);
                dv.RowFilter = filterCondition;
                gvSubItems.DataSource = dv;
                gvSubItems.DataBind();
            }
           
        }

        protected void chkGPSActive_CheckedChanged(object sender, EventArgs e)
        {

        }      
     
        protected void ddlSubActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            DropDownList ddlselectedSubActivity = (DropDownList)sender; 
            GridViewRow row = (GridViewRow)ddlselectedSubActivity.NamingContainer;
            DropDownList ddlSubItem = (DropDownList)row.FindControl("ddlSubItem");
            try
            {
                DataSet ds = new DataSet();
                ds = ManageActivityBAL.Instance.GetActivityItem(ddlselectedSubActivity.SelectedValue, ddlItemAreaForActivitySubItem.SelectedValue, ddlItemActivityTypeForActivitySubItem.SelectedValue);
                ddlSubItem.DataSource = ds;
                ddlSubItem.DataValueField = "ActivityItemId";
                ddlSubItem.DataTextField = "ActivityItem";
                ddlSubItem.DataBind();
                ddlSubItem.Items.Insert(0, "Select Activity Item");
            }
            catch
            {
                throw;
            }
        }

        protected void ddlItemActivityForActivitySubItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetActivitySubItemSearchData();
        }

        protected void ddlItemActivityTypeForActivitySubItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivityButtonsActive();
        }

        protected void ddlItemActivityForActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetActivitySubItemSearchData();
                DataSet ds = new DataSet();
                ds = ManageActivityBAL.Instance.GetActivityItem(ddlItemActivityForActivity.SelectedValue);
                ddlItemActivityForActivitySubItem.DataSource = ds.Tables[0];
                ddlItemActivityForActivitySubItem.DataValueField = "ActivityItemId";
                ddlItemActivityForActivitySubItem.DataTextField = "ActivityItem";
                ddlItemActivityForActivitySubItem.DataBind();
                ddlItemActivityForActivitySubItem.Items.Insert(0, "Select Activity Item");
            }
            catch
            {
                throw;
            }
        }

        protected void ddlItemAreaForActivitySubItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetActivitySubItemSearchData();
            ActivityButtonsActive();
        }

        private void ActivityButtonsActive()
        {
            if(ddlItemActivityTypeForActivitySubItem.SelectedIndex==0 || ddlItemAreaForActivitySubItem.SelectedIndex==0)
            {
                ddlItemActivityForActivity.Enabled = false;
                ddlItemActivityForActivitySubItem.Enabled = false;
            }
            else
            {
                ddlItemActivityForActivity.Enabled = true;
                ddlItemActivityForActivitySubItem.Enabled = true;
            }
        }
        

       
        
    }
}