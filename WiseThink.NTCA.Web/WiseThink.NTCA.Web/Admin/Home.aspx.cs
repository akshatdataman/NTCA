using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.BAL;

namespace WiseThink.NTCA.Web.Admin
{
    public partial class WebForm2 : BasePage
    {
        DataSet dsDashboard = new DataSet();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //GetAdminDashboardData();
                GetAlerts();
                UserBAL.Instance.InsertAuditTrailDetail("Visted Super Admin Dashboard", "Super Admin Dashboard");
            }
        }
        //private void GetAdminDashboardData()
        //{

        //    try
        //    {
        //        string loggedInUser = AuthoProvider.User;
        //        List<Role> userRole = AuthoProvider.LoggedInRoles.ToList();
        //        if (userRole.Contains(Role.ADMIN))
        //        {
        //            dsDashboard = APOBAL.Instance.GetDashboardForAdmin();

        //            int countApo = dsDashboard.Tables[0].Rows.Count;
        //            if (countApo > 0)
        //            {
        //                APOSanctionDueCountSpan.InnerText = "  (" + countApo.ToString() + ")";
        //                gvAPoDueForSanction.DataSource = dsDashboard.Tables[0];
        //                gvAPoDueForSanction.DataBind();
        //            }
        //            else
        //            {
        //                APOSanctionDueCountSpan.InnerText = "  (" + "0" + ")";
        //                gvAPoDueForSanction.DataSource = dsDashboard.Tables[0];
        //                gvAPoDueForSanction.DataBind();
        //            }
        //            int countMonthly = dsDashboard.Tables[1].Rows.Count;
        //            if (countMonthly > 0)
        //            {
        //                MonthlyCountSpan.InnerText = "  (" + countMonthly.ToString() + ")";
        //                gvMonthlyReport.DataSource = dsDashboard.Tables[1];
        //                gvMonthlyReport.DataBind();
        //            }
        //            else
        //            {
        //                MonthlyCountSpan.InnerText = "  (" + "0" + ")";
        //                gvMonthlyReport.DataSource = dsDashboard.Tables[1];
        //                gvMonthlyReport.DataBind();
        //            }

        //            int SanctionCount = dsDashboard.Tables[2].Rows.Count;
        //            if (SanctionCount > 0)
        //            {
        //                SanctionCountSpan.InnerText = "  (" + SanctionCount.ToString() + ")";
        //                gvSanction.DataSource = dsDashboard.Tables[2];
        //                gvSanction.DataBind();
        //            }
        //            else
        //            {
        //                SanctionCountSpan.InnerText = "  (" + "0" + ")";
        //                gvSanction.DataSource = dsDashboard.Tables[2];
        //                gvSanction.DataBind();
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return;
        //    }
        //    finally
        //    {
        //        if (dsDashboard != null)
        //        {
        //            dsDashboard.Dispose();
        //        }
        //    }
        //}
        private void GetAlerts()
        {
            string loggedInUser = AuthoProvider.User;
            int _userId = UserBAL.Instance.GetLoggedInUserId(loggedInUser);
            DataSet dsAlerts = AlertBAL.Instance.GetAlerts(_userId);
            if (dsAlerts.Tables[0].Rows.Count > 0)
            {
                DisplayAlertDiv.Style.Add("display", "block");
                for (int i = 0; i < dsAlerts.Tables[0].Rows.Count; i++)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    createDiv.ID = "createDiv" + (i + 1);
                    createDiv.Attributes.Add("class", "alert alert-warning");
                    DisplayAlertDiv.Controls.Add(createDiv);

                    System.Web.UI.HtmlControls.HtmlGenericControl createCloseAcchor =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("a");
                    createCloseAcchor.Attributes.Add("class", "close LinkColor");
                    createCloseAcchor.Attributes.Add("data-dismiss", "alert");
                    createCloseAcchor.InnerHtml = "x";
                    createDiv.Controls.Add(createCloseAcchor);

                    System.Web.UI.HtmlControls.HtmlGenericControl createAcchor =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("a");
                    createAcchor.ID = "Notify" + (i + 1);
                    createAcchor.Attributes.Add("class", "LinkColor alert-warning");
                    createAcchor.InnerHtml = Convert.ToString(dsAlerts.Tables[0].Rows[i][DataBaseFields.Subject]);
                    createAcchor.Attributes.Add("href", "Alerts.aspx");
                    createDiv.Controls.Add(createAcchor);
                }
            }
            else
                DisplayAlertDiv.Style.Add("display", "none");
        }

        protected void imgbtnManageUser_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Users.aspx", false);
        }

        protected void imgbtnManageState_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("StateMaster.aspx", false);
        }
        protected void imgbtnManageTR_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("TigerReserve.aspx", false);
        }
        protected void imgbtnManageActivities_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ManageActivities.aspx", false);
        }
        //protected void gvAPoDueForSanction_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvAPoDueForSanction.PageIndex = e.NewPageIndex;
        //    GetAdminDashboardData();
        //}
        //protected void gvMonthlyReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvMonthlyReport.PageIndex = e.NewPageIndex;
        //    GetAdminDashboardData();

        //}
        //protected void gvSanction_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvSanction.PageIndex = e.NewPageIndex;
        //    GetAdminDashboardData();
        //}
    }
}