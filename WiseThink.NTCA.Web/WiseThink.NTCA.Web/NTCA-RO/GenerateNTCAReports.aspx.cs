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
using System.Drawing;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class GenerateNTCAReports : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                BindLists();
                GetCurrentYear();
            }
        }

        private void BindLists()
        {
            DataSet ds = ManageActivityBAL.Instance.GetAreaActivityAndType();
            ddlActivity.DataSource = ds.Tables[2];
            ddlActivity.DataValueField = "ActivityId";
            ddlActivity.DataTextField = "ActivityName";
            ddlActivity.DataBind();
            ddlActivity.Items.Insert(0, "Select Activity");

            ddlActivityItem.DataSource = ds.Tables[3];
            ddlActivityItem.DataValueField = "ActivityItemId";
            ddlActivityItem.DataTextField = "ActivityItem";
            ddlActivityItem.DataBind();
            ddlActivityItem.Items.Insert(0, "Select Activity Item");

            ddlSubItem.DataSource = ds.Tables[4];
            ddlSubItem.DataValueField = "SubItemId";
            ddlSubItem.DataTextField = "SubItem";
            ddlSubItem.DataBind();
            ddlSubItem.Items.Insert(0, "Select Activity Sub Item");

        }

        protected void ddlReportsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GenerateReport.Visible = true;
                if(ddlReportsType.SelectedIndex==1)
                {
                    //FinancialYear.Visible = true;
                    Items.Visible = true;
                    SubItems.Visible = false;
                    Activity.Visible = false;
                    gvItemReports.DataSource = null;
                    gvItemReports.DataBind();
                    gvItemReports.Visible = false;
                }
                else if(ddlReportsType.SelectedIndex==2)
                {
                    //FinancialYear.Visible = true;
                    Items.Visible = false;
                    SubItems.Visible = false;
                    Activity.Visible = true;
                    gvItemReports.DataSource = null;
                    gvItemReports.DataBind();
                    gvItemReports.Visible = false;
                }
                else if (ddlReportsType.SelectedIndex==3)
                {
                    //FinancialYear.Visible = true;
                    Items.Visible = true;
                    SubItems.Visible = true;
                    Activity.Visible = true;
                    gvItemReports.DataSource = null;
                    gvItemReports.DataBind();
                    gvItemReports.Visible = false;
                }
                else if(ddlReportsType.SelectedIndex==4)
                {
                    //FinancialYear.Visible = true;
                    Items.Visible = false;
                    SubItems.Visible = false;
                    Activity.Visible = false;
                    gvItemReports.DataSource = null;
                    gvItemReports.DataBind();
                    gvItemReports.Visible = false;
                }
                else if (ddlReportsType.SelectedIndex == 5)
                {
                    //FinancialYear.Visible = true;
                    Items.Visible = false;
                    SubItems.Visible = false;
                    Activity.Visible = false;
                    gvItemReports.DataSource = null;
                    gvItemReports.DataBind();
                    gvItemReports.Visible = false;
                }
                else if (ddlReportsType.SelectedIndex == 6)
                {
                    //FinancialYear.Visible = true;
                    Items.Visible = false;
                    SubItems.Visible = false;
                    Activity.Visible = false;
                    gvStateWiseReport.DataSource = null;
                    gvStateWiseReport.DataBind();
                    divStateWise.Visible = false;
                }
                
            }
            catch
            {
                throw;
            }
        }

        protected void gvItemReports_PageIndexChanged(object sender, EventArgs e)
        {
            gvItemReports.EditIndex = -1;
            if (ddlReportsType.SelectedIndex == 1)
            {
                generateActivityReport();
            }
            else if (ddlReportsType.SelectedIndex == 2)
            {
                generateReportActivityItemWise();
            }
            else if (ddlReportsType.SelectedIndex == 3)
            {
                generateReportSubItemWise();
            }
            else if (ddlReportsType.SelectedIndex == 4)
            {
            }
            else if (ddlReportsType.SelectedIndex == 5)
            {
                generateReportTigerReserveWise();
            }
        }

        protected void gvItemReports_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemReports.PageIndex = e.NewPageIndex;
            if (ddlReportsType.SelectedIndex == 1)
            {
                generateActivityReport();
            }
            else if (ddlReportsType.SelectedIndex == 2)
            {
                generateReportActivityItemWise();
            }
            else if (ddlReportsType.SelectedIndex == 3)
            {
                generateReportSubItemWise();
            }
            else if (ddlReportsType.SelectedIndex == 4)
            {
            }
            else if (ddlReportsType.SelectedIndex == 5)
            {
                generateReportTigerReserveWise();
            }
        }

        protected void gvStateWiseReport_PageIndexChanged(object sender, EventArgs e)
        {
            gvStateWiseReport.EditIndex = -1;
            if (ddlReportsType.SelectedIndex == 6)
            {
                generateReportStateWise();
            }
        }

        protected void gvStateWiseReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStateWiseReport.PageIndex = e.NewPageIndex;
            if (ddlReportsType.SelectedIndex == 6)
            {
                generateReportStateWise();
            }
        }

        private void GetCurrentYear()
        {
            DataSet ds = new DataSet();
            CommonClass cs = new CommonClass();
            ds = cs.getCurrentYear();
            txtFromYear.Text = ds.Tables[0].Rows[0][0].ToString();
            txtToYear.Text = ds.Tables[0].Rows[0][0].ToString();
        }

        protected void btnGenerateReports_Click(object sender, EventArgs e)
        {
            if(ddlReportsType.SelectedIndex ==1)
            {
                if (ddlActivity.SelectedIndex == 0)
                {
                    ddlActivityRequired.IsValid = false;
                }
                else
                    generateActivityReport();
            }
            else if(ddlReportsType.SelectedIndex==2)
            {
                if (ddlActivityItem.SelectedIndex == 0)
                {
                    ddlActivityItemRequired.IsValid = false;
                }
                else
                generateReportActivityItemWise();
            }
            else if(ddlReportsType.SelectedIndex==3)
            {
                if (ddlActivityItem.SelectedIndex == 0)
                {
                    ddlSubItemRequired.IsValid = false;
                }
                else
                    generateReportSubItemWise();
            }
            else if (ddlReportsType.SelectedIndex == 4)
            {
            }
            else if (ddlReportsType.SelectedIndex == 5)
            {
                generateReportTigerReserveWise();
            }
            else if (ddlReportsType.SelectedIndex == 6)
            {
                generateReportStateWise();
            }
        }

        private void generateActivityReport()
        {
            DataSet ds = new DataSet();
            ds = QuarterlyReportBAL.Instance.GetReportsActivityWise(Convert.ToInt32(ddlActivity.SelectedValue), txtFromYear.Text.Trim());
            gvItemReports.DataSource = ds;
            gvItemReports.DataBind();
            gvItemReports.Visible = true;
        }

        private void generateReportActivityItemWise()
        {
            DataSet ds = new DataSet();
            ds = QuarterlyReportBAL.Instance.GetReportsActivityItemWise(Convert.ToInt32(ddlActivityItem.SelectedValue), txtFromYear.Text.Trim());
            gvItemReports.DataSource = ds;
            gvItemReports.DataBind();
            gvItemReports.Visible = true;
        }

        private void generateReportSubItemWise()
        {
            DataSet ds = new DataSet();
            ds = QuarterlyReportBAL.Instance.GetReportsSubItemWise(Convert.ToInt32(ddlSubItem.SelectedValue), txtFromYear.Text.Trim());
            gvItemReports.DataSource = ds;
            gvItemReports.DataBind();
            gvItemReports.Visible = true;
        }
        private void generateReportTigerReserveWise()
        {
            DataSet ds = new DataSet();
            ds = QuarterlyReportBAL.Instance.GetReportsTigerReserveWise(txtFromYear.Text.Trim());
            gvItemReports.DataSource = ds;
            gvItemReports.DataBind();
            gvItemReports.Visible = true;
        }
        private void generateReportStateWise()
        {
            DataSet ds = new DataSet();
            ds = QuarterlyReportBAL.Instance.GetReportsStateWise(txtFromYear.Text.Trim());
            gvStateWiseReport.DataSource = ds;
            gvStateWiseReport.DataBind();
            divStateWise.Visible = true;
            gvItemReports.Visible = false;
        }

        protected void ddlActivityItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = QuarterlyReportBAL.Instance.GetActivitySubItem(Convert.ToInt32(ddlActivityItem.SelectedValue));
                ddlSubItem.DataSource = ds.Tables[0];
                ddlSubItem.DataValueField = "SubItemId";
                ddlSubItem.DataTextField = "SubItem";
                ddlSubItem.DataBind();
                ddlSubItem.Items.Insert(0, "Select Sub Activity Item");
            }
            catch
            {
                throw;
            }
        }

    }
}