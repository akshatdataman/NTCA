using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.DataEntity.Entities;

namespace WiseThink.NTCA.Web.UserControls
{
    public partial class ManageAllActivities : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                additionalActivityType.Style.Add("display", "none");
                additionalActivity.Style.Add("display", "none");
                AddActivityItem.Style.Add("display", "none");
                AddSubItem.Style.Add("display", "none");
                btnSubmit.Style.Add("display", "none");
                btnSubmitActivity.Style.Add("display", "none");
                btnSubmitActivityItem.Style.Add("display", "none");
                btnSubActivityItem.Style.Add("display", "none");
                BindCategory();
                BindActivity();
                BindActivityItem();
            }
        }
        protected void BindCategory()
        {
            DataSet dsCategory = ManageActivityBAL.Instance.GetAllCategory();
            if (dsCategory.Tables[0].Rows.Count > 0)
            {
                mChkCategory.DataSource = dsCategory.Tables[0];
                mChkCategory.DataTextField = dsCategory.Tables[0].Columns[DataBaseFields.CATEGORY_NAME].ToString();
                mChkCategory.DataValueField = dsCategory.Tables[0].Columns[DataBaseFields.CATEGORY_ID].ToString();
                mChkCategory.DataBind();
                ddlCategory.DataSource = dsCategory.Tables[0];
                ddlCategory.DataTextField = DataBaseFields.CATEGORY_NAME;
                ddlCategory.DataValueField = DataBaseFields.CATEGORY_ID;
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, "Select Activity Type");
            }
            else
            {
                mChkCategory.DataSource = null;
                mChkCategory.DataBind();
            }

        }
        protected void BindActivity()
        {
            DataSet dsActivity = ManageActivityBAL.Instance.GetAllActivity();
            if (dsActivity.Tables[0].Rows.Count > 0)
            {
                mChkActivities.DataSource = dsActivity.Tables[0];
                mChkActivities.DataTextField = dsActivity.Tables[0].Columns[DataBaseFields.ACTIVITY_NAME].ToString();
                mChkActivities.DataValueField = dsActivity.Tables[0].Columns[DataBaseFields.ACTIVITY_ID].ToString();
                mChkActivities.DataBind();
                ddlActivities.DataSource = dsActivity.Tables[0];
                ddlActivities.DataTextField = DataBaseFields.ACTIVITY_NAME;
                ddlActivities.DataValueField = DataBaseFields.ACTIVITY_ID;
                ddlActivities.DataBind();
                ddlActivities.Items.Insert(0, "Select Activity");
            }
            else
            {
                mChkActivities.DataSource = null;
                mChkActivities.DataBind();
            }

        }
        protected void BindActivityItem()
        {
            DataSet dsActivityItem = ManageActivityBAL.Instance.GetAllActivityItem();
            if (dsActivityItem.Tables[0].Rows.Count > 0)
            {
                mChkItems.DataSource = dsActivityItem.Tables[0];
                mChkItems.DataTextField = dsActivityItem.Tables[0].Columns[DataBaseFields.ACTIVITY_ITEM_NAME].ToString();
                mChkItems.DataValueField = dsActivityItem.Tables[0].Columns[DataBaseFields.ACTIVITY_ITEM_ID].ToString();
                mChkItems.DataBind();
                ddlActivityItems.DataSource = dsActivityItem.Tables[0];
                ddlActivityItems.DataTextField = DataBaseFields.ACTIVITY_ITEM_NAME;
                ddlActivityItems.DataValueField = DataBaseFields.ACTIVITY_ITEM_ID;
                ddlActivityItems.DataBind();
                ddlActivityItems.Items.Insert(0, "Select Activity Item");
            }
            else
            {
                mChkItems.DataSource = null;
                mChkItems.DataBind();
            }

        }
        protected void BindSubActivityItem()
        {
            DataSet dsSubActivityItem = ManageActivityBAL.Instance.GetAllSubActivityItem();
            if (dsSubActivityItem.Tables[0].Rows.Count > 0)
            {
                mChkSubItems.DataSource = dsSubActivityItem.Tables[0];
                mChkSubItems.DataTextField = dsSubActivityItem.Tables[0].Columns[DataBaseFields.SUB_ACTIVITY_ITEM_NAME].ToString();
                mChkSubItems.DataValueField = dsSubActivityItem.Tables[0].Columns[DataBaseFields.SUB_ACTIVITY_ITEM_ID].ToString();
                mChkSubItems.DataBind();
            }
            else
            {
                mChkSubItems.DataSource = null;
                mChkSubItems.DataBind();
            }
        }
        protected void mChkCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string categoryIds = mChkCategory.SelectedValueList;
            DataSet dsActivity = ManageActivityBAL.Instance.GetActivity(categoryIds);
            mChkActivities.DataSource = dsActivity.Tables[0];
            mChkActivities.DataTextField = dsActivity.Tables[0].Columns[DataBaseFields.ACTIVITY_NAME].ToString();
            mChkActivities.DataValueField = dsActivity.Tables[0].Columns[DataBaseFields.ACTIVITY_ID].ToString();
            mChkActivities.DataBind();
        }

        protected void mChkActivities_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string activityIds = mChkActivities.SelectedValueList;
            DataSet dsActivityItems = ManageActivityBAL.Instance.GetActivityItems(activityIds);
            mChkItems.DataSource = dsActivityItems.Tables[0];
            mChkItems.DataTextField = dsActivityItems.Tables[0].Columns[DataBaseFields.ACTIVITY_ITEM_NAME].ToString();
            mChkItems.DataValueField = dsActivityItems.Tables[0].Columns[DataBaseFields.ACTIVITY_ITEM_ID].ToString();
            mChkItems.DataBind();
        }

        protected void mChkItems_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string activityIds = mChkItems.SelectedValueList;
            DataSet dsSubActivityItems = ManageActivityBAL.Instance.GetSubActivityItems(activityIds);
            mChkSubItems.DataSource = dsSubActivityItems.Tables[0];
            mChkSubItems.DataTextField = dsSubActivityItems.Tables[0].Columns[DataBaseFields.SUB_ACTIVITY_ITEM_NAME].ToString();
            mChkSubItems.DataValueField = dsSubActivityItems.Tables[0].Columns[DataBaseFields.SUB_ACTIVITY_ITEM_ID].ToString();
            mChkSubItems.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            additionalActivityType.Style.Add("display", "block");
            btnSubmit.Style.Add("display", "block");
        }

        protected void btnAddActivity_Click(object sender, EventArgs e)
        {
            additionalActivity.Style.Add("display", "block");
            btnSubmitActivity.Style.Add("display", "block");
        }

        protected void btnAddActivityItem_Click(object sender, EventArgs e)
        {
            AddActivityItem.Style.Add("display", "block");
            btnSubmitActivityItem.Style.Add("display", "block");
        }
        protected void btnAddSubItem_Click(object sender, EventArgs e)
        {
            AddSubItem.Style.Add("display", "block");
            btnSubActivityItem.Style.Add("display", "block");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Activity oActivity = new Activity();
                oActivity.CategoryName = txtAddMore.Text;
                ManageActivityBAL.Instance.AddCategory(oActivity);
                BindCategory();
                txtAddMore.Text = "";
                btnSubmit.Style.Add("display", "none");
            }
            catch(Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void btnSubmitActivity_Click(object sender, EventArgs e)
        {
            try
            {
                Activity oActivity = new Activity();
                oActivity.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                oActivity.ActivityName = txtAddActivity.Text;
                ManageActivityBAL.Instance.AddActivityItem(oActivity);
                BindActivity();
                ddlCategory.SelectedIndex = -1;
                txtAddActivity.Text = "";
                btnSubmitActivity.Style.Add("display", "none");

            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void btnSubmitActivityItem_Click(object sender, EventArgs e)
        {
            try
            {
                Activity oActivity = new Activity();
                oActivity.ActivityId = Convert.ToInt32(ddlActivities.SelectedValue);
                oActivity.ActivityItemName = txtAddActivityItem.Text;
                ManageActivityBAL.Instance.AddActivityItem(oActivity);
                BindActivityItem();
                ddlActivities.SelectedIndex = -1;
                txtAddActivityItem.Text = "";
                btnSubmitActivityItem.Style.Add("display", "none");

            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        protected void btnSubActivityItem_Click(object sender, EventArgs e)
        {
            try
            {
                Activity oActivity = new Activity();
                oActivity.ActivityItemId = Convert.ToInt32(ddlActivityItems.SelectedValue);
                oActivity.SubActivityItemName = txtAddSubActivityItem.Text;
                ManageActivityBAL.Instance.AddSubActivityItem(oActivity);
                BindSubActivityItem();
                ddlActivityItems.SelectedIndex = -1;
                txtAddSubActivityItem.Text = "";
                btnSubActivityItem.Style.Add("display", "none");

            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
    }
}