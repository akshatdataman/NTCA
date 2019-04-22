using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.DataEntity;
using System.Collections;
using System.Configuration;
using System.Text.RegularExpressions;

namespace WiseThink.NTCA.Web.CWW_Secretary
{
    public partial class Home : BasePage
    {
        DataSet dsDashboard = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCWWDashboardData();
                GetAlerts();
                UserBAL.Instance.InsertAuditTrailDetail("Visited CWLW / SF Dashboard", "CWLW / SF Dashboard");
            }
        }
        private void GetCWWDashboardData()
        {

            try
            {
                string loggedInUser = AuthoProvider.User;
                List<Role> userRole = AuthoProvider.LoggedInRoles.ToList();
                if (userRole.Contains(Role.CWLW) || userRole.Contains(Role.SECRETARY))
                {
                    if (userRole.Contains(Role.CWLW))
                        CwwHomeHeader.InnerText = "CWLW Home";
                    else
                        CwwHomeHeader.InnerText = "Secretary Home";
                    int stateId = 0;
                    DataSet dsStateId = APOBAL.Instance.GetLoggedInUserStateId(loggedInUser);
                    if (dsStateId.Tables[0].Rows.Count == 1)
                    {
                        foreach (DataRow drStateId in dsStateId.Tables[0].Rows)
                        {

                            stateId = Convert.ToInt32(drStateId["StateId"].ToString());
                        }
                    }
                    if (stateId != 0)
                        dsDashboard = APOBAL.Instance.GetDashboardForCWW(stateId);
                    Session["dsDashboard"] = dsDashboard;

                    int countApo = dsDashboard.Tables[0].Rows.Count;
                    if (countApo > 0)
                    {
                        APOCountSpan.InnerText = "  (" + countApo.ToString() + ")";
                        gvAPoSubmitted.DataSource = dsDashboard.Tables[0];
                        gvAPoSubmitted.DataBind();
                    }
                    else
                    {
                        APOCountSpan.InnerText = "  (" + "0" + ")";
                        gvAPoSubmitted.DataSource = dsDashboard.Tables[0];
                        gvAPoSubmitted.DataBind();
                    }
                    int countQuarterly = dsDashboard.Tables[1].Rows.Count;
                    if (countQuarterly > 0)
                    {
                        QuarterlyCountSpan.InnerText = "  (" + countQuarterly.ToString() + ")";
                        gvAPoReportSubmitted.DataSource = dsDashboard.Tables[1];
                        gvAPoReportSubmitted.DataBind();
                    }
                    else
                    {
                        QuarterlyCountSpan.InnerText = "  (" + "0" + ")";
                        gvAPoReportSubmitted.DataSource = dsDashboard.Tables[1];
                        gvAPoReportSubmitted.DataBind();
                    }

                    int ApprovedCount = dsDashboard.Tables[2].Rows.Count;
                    if (ApprovedCount > 0)
                    {
                        ApprovedCountSpan.InnerText = "  (" + ApprovedCount.ToString() + ")";
                        gvApproved.DataSource = dsDashboard.Tables[2];
                        gvApproved.DataBind();
                    }
                    else
                    {
                        ApprovedCountSpan.InnerText = "  (" + "0" + ")";
                        gvApproved.DataSource = dsDashboard.Tables[2];
                        gvApproved.DataBind();
                    }
                    int additionalAPOCount = dsDashboard.Tables[3].Rows.Count;
                    if (additionalAPOCount > 0)
                    {
                        AdditionalAPOCountSpan.InnerText = "  (" + additionalAPOCount.ToString() + ")";
                        cgvAdditionalApo.DataSource = dsDashboard.Tables[3];
                        cgvAdditionalApo.DataBind();
                    }
                    else
                    {
                        AdditionalAPOCountSpan.InnerText = "  (" + "0" + ")";
                        cgvAdditionalApo.DataSource = dsDashboard.Tables[3];
                        cgvAdditionalApo.DataBind();
                    }
                    if (cgvAdditionalApo.Rows.Count > 0)
                        Additional.Style.Add("display", "block");
                    else
                        Additional.Style.Add("display", "none");
                }

            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
            finally
            {
                if (dsDashboard != null)
                {
                    dsDashboard.Dispose();
                }
            }
        }
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

        protected void gvAPoSubmitted_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAPoSubmitted.PageIndex = e.NewPageIndex;
            GetCWWDashboardData();
        }
        protected void gvAPoReportSubmitted_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAPoReportSubmitted.PageIndex = e.NewPageIndex;
            GetCWWDashboardData();

        }
        protected void gvApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvApproved.PageIndex = e.NewPageIndex;
            GetCWWDashboardData();
        }
        protected void cgvAdditionalApo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvAdditionalApo.PageIndex = e.NewPageIndex;
            GetCWWDashboardData();
        }
        protected void gvAPoSubmitted_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session["IsApprove"] = false;
            if (e.CommandName == "VIEWAPO")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string APOID = lnkView.CommandArgument;
                Response.Redirect("~/CWW-Secretary/ViewAPO.aspx?Id=" + APOID + "", false);
            }
            switch (e.CommandName)
            {
                case "Edit":
                    Session["IsEdit"] = true;
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/EditAPO.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "View":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/ViewFieldDirectorObligations.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewUC":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/ViewUtilizationCertificate.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewTRDetail":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/ViewTigerReserveDetails.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewCheckList":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/ViewCheckList.aspx?callfrom=SubmitAPO", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "UpdateStatus":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/UpdateAPOStatus.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "Approve":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/ApproveAPO.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "Submit":
                    //Session["IsEdit"] = true;
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/ObligationCWW.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "downloadProvisionalUc":
                    LinkButton lnkdownloadProvisionalUc = (LinkButton)e.CommandSource;
                    GridViewRow gvrow = lnkdownloadProvisionalUc.NamingContainer as GridViewRow;
                    string APOID = lnkdownloadProvisionalUc.CommandArgument;
                    string FileName = string.Empty;
                    DataSet dsUc = CheckListBAL.Instance.GetUCAndProvisionalUC(Convert.ToInt32(APOID));
                    if (dsUc != null)
                    {
                        DataRow dr = dsUc.Tables[0].Rows[0];
                        if (dr[0] != DBNull.Value)
                        {
                            FileName = Convert.ToString(dr["ProvisionalUcFileName"]);
                        }
                    }
                    if (!string.IsNullOrEmpty(FileName))
                    {
                        string filePath = "~/Upload/UC/Provisional/" + FileName;
                        Response.ContentType = "doc/pdf";
                        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
                        if (System.IO.File.Exists(Server.MapPath(filePath)))
                        {
                            Response.TransmitFile(Server.MapPath(filePath));
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                        }
                        Response.End();
                    }
                    else
                    {
                        string strError = ConfigurationManager.AppSettings["ProvisionalUcNotExist"];
                        vmError.Message = strError;
                        return;
                    }
                    break;
            }
            switch (e.CommandName)
            {
                case "downloadFinalUc":
                    LinkButton lnkdownloadFinalUc = (LinkButton)e.CommandSource;
                    GridViewRow gvrow = lnkdownloadFinalUc.NamingContainer as GridViewRow;
                    string APOID = lnkdownloadFinalUc.CommandArgument;
                    string FileName = string.Empty;
                    DataSet dsUc = CheckListBAL.Instance.GetUCAndProvisionalUC(Convert.ToInt32(APOID));
                    if (dsUc != null)
                    {
                        DataRow dr = dsUc.Tables[1].Rows[0];
                        if (dr[0] != DBNull.Value)
                        {
                            FileName = Convert.ToString(dr["FinnalUcFileName"]);
                        }

                    }
                    if (!string.IsNullOrEmpty(FileName))
                    {
                        string filePath = "~/Upload/UC/Final/" + FileName;
                        Response.ContentType = "doc/pdf";
                        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
                        if (System.IO.File.Exists(Server.MapPath(filePath)))
                        {
                            Response.TransmitFile(Server.MapPath(filePath));
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                        }
                        Response.End();
                    }
                    else
                    {
                        string strError = ConfigurationManager.AppSettings["FinalUcNotExist"];
                        vmError.Message = strError;
                        return;
                    }
                    break;
            }

        }
        protected void gvApproved_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VIEWAPO")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string APOID = lnkView.CommandArgument;
                Session["IsApprove"] = true;
                Response.Redirect("~/CWW-Secretary/ViewAPO.aspx?Id=" + APOID + "", false);
            }
            switch (e.CommandName)
            {
                case "downloadProvisionalUc":
                    LinkButton lnkdownloadProvisionalUc = (LinkButton)e.CommandSource;
                    GridViewRow gvrow = lnkdownloadProvisionalUc.NamingContainer as GridViewRow;
                    string APOID = lnkdownloadProvisionalUc.CommandArgument;
                    string FileName = string.Empty;
                    DataSet dsUc = CheckListBAL.Instance.GetUCAndProvisionalUC(Convert.ToInt32(APOID));
                    if (dsUc != null)
                    {
                        DataRow dr = dsUc.Tables[0].Rows[0];
                        if (dr[0] != DBNull.Value)
                        {
                            FileName = Convert.ToString(dr["ProvisionalUcFileName"]);
                        }
                    }
                    if (!string.IsNullOrEmpty(FileName))
                    {
                        string filePath = "~/Upload/UC/Provisional/" + FileName;
                        Response.ContentType = "doc/pdf";
                        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
                        if (System.IO.File.Exists(Server.MapPath(filePath)))
                        {
                            Response.TransmitFile(Server.MapPath(filePath));
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                        }
                        Response.End();
                    }
                    else
                    {
                        string strError = ConfigurationManager.AppSettings["ProvisionalUcNotExist"];
                        vmError.Message = strError;
                        return;
                    }
                    break;
            }
            switch (e.CommandName)
            {
                case "downloadFinalUc":
                    LinkButton lnkdownloadFinalUc = (LinkButton)e.CommandSource;
                    GridViewRow gvrow = lnkdownloadFinalUc.NamingContainer as GridViewRow;
                    string APOID = lnkdownloadFinalUc.CommandArgument;
                    string FileName = string.Empty;
                    DataSet dsUc = CheckListBAL.Instance.GetUCAndProvisionalUC(Convert.ToInt32(APOID));
                    if (dsUc != null)
                    {
                        DataRow dr = dsUc.Tables[1].Rows[0];
                        if (dr[0] != DBNull.Value)
                        {
                            FileName = Convert.ToString(dr["FinnalUcFileName"]);
                        }
                    }
                    if (!string.IsNullOrEmpty(FileName))
                    {
                        string filePath = "~/Upload/UC/Final/" + FileName;
                        Response.ContentType = "doc/pdf";
                        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
                        if (System.IO.File.Exists(Server.MapPath(filePath)))
                        {
                            Response.TransmitFile(Server.MapPath(filePath));
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                        }
                        Response.End();
                    }
                    else
                    {
                        string strError = ConfigurationManager.AppSettings["FinalUcNotExist"];
                        vmError.Message = strError;
                        return;
                    }
                    break;
            }
        }
        protected void cgvAdditionalApo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VIEWAPO")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string APOID = lnkView.CommandArgument;
                Response.Redirect("~/CWW-Secretary/ViewAPO.aspx?Id=" + APOID + "", false);
            }
            switch (e.CommandName)
            {
                case "Edit":
                    Session["IsEdit"] = true;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/EditAPO.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "View":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/ViewFieldDirectorObligations.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewUC":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/ViewUtilizationCertificate.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewTRDetail":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/ViewTigerReserveDetails.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "UpdateStatus":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/UpdateAPOStatus.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "Approve":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/ApproveAPO.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "Submit":
                    //Session["IsEdit"] = true;
                    Response.Redirect(string.Format("{0}: {1}", "~/CWW-Secretary/ObligationCWW.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "downloadProvisionalUc":
                    LinkButton lnkdownloadProvisionalUc = (LinkButton)e.CommandSource;
                    GridViewRow gvrow = lnkdownloadProvisionalUc.NamingContainer as GridViewRow;
                    string APOID = lnkdownloadProvisionalUc.CommandArgument;
                    string FileName = string.Empty;
                    DataSet dsUc = CheckListBAL.Instance.GetUCAndProvisionalUC(Convert.ToInt32(APOID));
                    if (dsUc != null)
                    {
                        DataRow dr = dsUc.Tables[0].Rows[0];
                        if (dr[0] != DBNull.Value)
                        {
                            FileName = Convert.ToString(dr["ProvisionalUcFileName"]);
                        }
                    }
                    if (!string.IsNullOrEmpty(FileName))
                    {
                        string filePath = "~/Upload/UC/Provisional/" + FileName;
                        Response.ContentType = "doc/pdf";
                        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
                        if (System.IO.File.Exists(Server.MapPath(filePath)))
                        {
                            Response.TransmitFile(Server.MapPath(filePath));
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                        }
                        Response.End();
                    }
                    else
                    {
                        string strError = ConfigurationManager.AppSettings["ProvisionalUcNotExist"];
                        vmError.Message = strError;
                        return;
                    }
                    break;
            }
            switch (e.CommandName)
            {
                case "downloadFinalUc":
                    LinkButton lnkdownloadFinalUc = (LinkButton)e.CommandSource;
                    GridViewRow gvrow = lnkdownloadFinalUc.NamingContainer as GridViewRow;
                    string APOID = lnkdownloadFinalUc.CommandArgument;
                    string FileName = string.Empty;
                    DataSet dsUc = CheckListBAL.Instance.GetUCAndProvisionalUC(Convert.ToInt32(APOID));
                    if (dsUc != null)
                    {
                        DataRow dr = dsUc.Tables[1].Rows[0];
                        if (dr[0] != DBNull.Value)
                        {
                            FileName = Convert.ToString(dr["FinnalUcFileName"]);
                        }
                    }
                    if (!string.IsNullOrEmpty(FileName))
                    {
                        string filePath = "~/Upload/UC/Final/" + FileName;
                        Response.ContentType = "doc/pdf";
                        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
                        if (System.IO.File.Exists(Server.MapPath(filePath)))
                        {
                            Response.TransmitFile(Server.MapPath(filePath));
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                        }
                        Response.End();
                    }
                    else
                    {
                        string strError = ConfigurationManager.AppSettings["FinalUcNotExist"];
                        vmError.Message = strError;
                        return;
                    }
                    break;
            }
            GetAlerts();
        }

        #region Code to filter data
        //    private void BasicSearch()
        //    {
        //    IEnumerable query1 =
        //from apo in dsDashboard.Tables[0].AsEnumerable()
        //where apo.Field("APOTitle") == "APO1"
        //select apo;
        //    }
        private void GetRowsByFilter()
        {
            DataSet ds = (DataSet)Session["dsDashboard"];
            DataTable table = ds.Tables[0];
            // Presuming the DataTable has a column named Date. 
            string expression;
            string txtApotitle = string.Empty;
            expression = "APOTitle = '" + txtAPOTitle + "'";
            DataRow[] foundRows;

            // Use the Select method to find all rows matching the filter.
            foundRows = table.Select(expression);

            // Print column 0 of each returned row.
            if (foundRows.Count() > 0)
            {
                for (int i = 0; i < foundRows.Length; i++)
                {
                    //Console.WriteLine(foundRows[i][0]);
                    APOCountSpan.InnerText = "  (" + foundRows.Count().ToString() + ")";
                    gvAPoSubmitted.DataSource = foundRows;
                    gvAPoSubmitted.DataBind();
                }
            }
            else
            {
                APOCountSpan.InnerText = "  (" + "0" + ")";
                gvAPoSubmitted.DataSource = null;
                gvAPoSubmitted.DataBind();
            }
        }
        #endregion

        //protected void btnbasicSearch_Click(object sender, EventArgs e)
        //{
        //    DataSet ds = (DataSet)Session["dsDashboard"];
        //    string apoTitle = txtAPOTitle.Text;
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        DataTable table1 = new DataTable();
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            if (Convert.ToString(ds.Tables[0].Rows[i]["APOTitle"]) == apoTitle)
        //            {
        //                table1 = (from DataRow dr in ds.Tables[0].Rows
        //                          where dr["APOTitle"].ToString() == apoTitle
        //                          select dr).CopyToDataTable();
        //                APOCountSpan.InnerText = "  (" + table1.Rows.Count + ")";
        //                gvAPoSubmitted.DataSource = table1;
        //                gvAPoSubmitted.DataBind();
        //            }
        //            else
        //            {
        //                APOCountSpan.InnerText = "  (" + table1.Rows.Count + ")";
        //                gvAPoSubmitted.DataSource = table1;
        //                gvAPoSubmitted.DataBind();
        //            }
        //        }
        //    }

        //    if (ds.Tables[1].Rows.Count > 0)
        //    {
        //        DataTable table2 = new DataTable();
        //        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
        //        {
        //            if (Convert.ToString(ds.Tables[0].Rows[i]["APOTitle"]) == apoTitle)
        //            {
        //                table2 = (from DataRow dr in ds.Tables[1].Rows
        //                          where dr["APOTitle"].ToString() == apoTitle
        //                          select dr).CopyToDataTable();
        //                QuarterlyCountSpan.InnerText = "  (" + table2.Rows.Count + ")";
        //                gvAPoReportSubmitted.DataSource = table2;
        //                gvAPoReportSubmitted.DataBind();
        //            }
        //            else
        //            {
        //                QuarterlyCountSpan.InnerText = "  (" + table2.Rows.Count + ")";
        //                gvAPoReportSubmitted.DataSource = table2;
        //                gvAPoReportSubmitted.DataBind();
        //            }
        //        }
        //    }

        //    if (ds.Tables[2].Rows.Count > 0)
        //    {
        //        DataTable table3 = new DataTable();
        //        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
        //        {
        //            if (Convert.ToString(ds.Tables[0].Rows[i]["APOTitle"]) == apoTitle)
        //            {
        //                table3 = (from DataRow dr in ds.Tables[2].Rows
        //                          where dr["APOTitle"].ToString() == apoTitle
        //                          select dr).CopyToDataTable();
        //                ApprovedCountSpan.InnerText = "  (" + table3.Rows.Count + ")";
        //                gvApproved.DataSource = table3;
        //                gvApproved.DataBind();
        //            }
        //            else
        //            {
        //                ApprovedCountSpan.InnerText = "  (" + table3.Rows.Count + ")";
        //                gvApproved.DataSource = table3;
        //                gvApproved.DataBind();
        //            }
        //        }
        //    }

        //}
        //protected void btnAdvanceSearch_Click(object sender, EventArgs e)
        //{
        //    DataSet ds = (DataSet)Session["dsDashboard"];
        //    string apoTitle = txtAPOFIleNo.Text;
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        DataTable table1 = new DataTable();
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            if (Convert.ToString(ds.Tables[0].Rows[i]["APOFileNo"]) == apoTitle)
        //            {
        //                table1 = (from DataRow dr in ds.Tables[0].Rows
        //                          where dr["APOFileNo"].ToString() == apoTitle
        //                          select dr).CopyToDataTable();
        //                APOCountSpan.InnerText = "  (" + table1.Rows.Count + ")";
        //                gvAPoSubmitted.DataSource = table1;
        //                gvAPoSubmitted.DataBind();
        //            }
        //            else
        //            {
        //                APOCountSpan.InnerText = "  (" + table1.Rows.Count + ")";
        //                gvAPoSubmitted.DataSource = table1;
        //                gvAPoSubmitted.DataBind();
        //            }
        //        }
        //    }

        //    if (ds.Tables[1].Rows.Count > 0)
        //    {
        //        DataTable table2 = new DataTable();
        //        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
        //        {
        //            if (Convert.ToString(ds.Tables[0].Rows[i]["APOFileNo"]) == apoTitle)
        //            {
        //                table2 = (from DataRow dr in ds.Tables[1].Rows
        //                          where dr["APOFileNo"].ToString() == apoTitle
        //                          select dr).CopyToDataTable();
        //                QuarterlyCountSpan.InnerText = "  (" + table2.Rows.Count + ")";
        //                gvAPoReportSubmitted.DataSource = table2;
        //                gvAPoReportSubmitted.DataBind();
        //            }
        //            else
        //            {
        //                QuarterlyCountSpan.InnerText = "  (" + table2.Rows.Count + ")";
        //                gvAPoReportSubmitted.DataSource = table2;
        //                gvAPoReportSubmitted.DataBind();
        //            }
        //        }
        //    }

        //    if (ds.Tables[2].Rows.Count > 0)
        //    {
        //        DataTable table3 = new DataTable();
        //        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
        //        {
        //            if (Convert.ToString(ds.Tables[0].Rows[i]["APOFileNo"]) == apoTitle)
        //            {
        //                table3 = (from DataRow dr in ds.Tables[2].Rows
        //                          where dr["APOFileNo"].ToString() == apoTitle
        //                          select dr).CopyToDataTable();
        //                ApprovedCountSpan.InnerText = "  (" + table3.Rows.Count + ")";
        //                gvApproved.DataSource = table3;
        //                gvApproved.DataBind();
        //            }
        //            else
        //            {
        //                ApprovedCountSpan.InnerText = "  (" + table3.Rows.Count + ")";
        //                gvApproved.DataSource = table3;
        //                gvApproved.DataBind();
        //            }
        //        }
        //    }

        //}
        #region Seach Functionality
        protected void btnbasicSearch_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Session["dsDashboard"];
            string apoTitle = Regex.Replace(txtAPOTitle.Text.Trim().ToLowerInvariant(), @"\s+", " ");
            if (!string.IsNullOrEmpty(apoTitle))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable submittedApoDt = new DataTable();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (Regex.Replace(ds.Tables[0].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                        {
                            var submittedApo = from myRow in ds.Tables[0].AsEnumerable()
                                               where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                                               select myRow;
                            foreach (var v in submittedApo)
                            {
                                submittedApoDt = submittedApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            APOCountSpan.InnerText = "  (" + submittedApoDt.Rows.Count + ")";
                            gvAPoSubmitted.DataSource = submittedApoDt;
                            gvAPoSubmitted.DataBind();
                        }
                    }
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable quarterlyReportsDt = new DataTable();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (Regex.Replace(ds.Tables[1].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                        {
                            var quarterlyReports = from myRow in ds.Tables[1].AsEnumerable()
                                                   where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                                                   select myRow;
                            foreach (var v in quarterlyReports)
                            {
                                quarterlyReportsDt = quarterlyReports.CopyToDataTable<DataRow>();
                                break;
                            }
                            QuarterlyCountSpan.InnerText = "  (" + quarterlyReportsDt.Rows.Count + ")";
                            gvAPoReportSubmitted.DataSource = quarterlyReportsDt;
                            gvAPoReportSubmitted.DataBind();
                        }
                    }
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataTable approvedApoDt = new DataTable();
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        if (Regex.Replace(ds.Tables[2].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                        {
                            var approvedApo = from myRow in ds.Tables[2].AsEnumerable()
                                              where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                                              select myRow;
                            foreach (var v in approvedApo)
                            {
                                approvedApoDt = approvedApo.CopyToDataTable<DataRow>();
                                break;
                            }

                            ApprovedCountSpan.InnerText = "  (" + approvedApoDt.Rows.Count + ")";
                            gvApproved.DataSource = approvedApoDt;
                            gvApproved.DataBind();
                        }
                    }
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable additionalApoDt = new DataTable();
                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        if (Regex.Replace(ds.Tables[3].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                        {
                            var additionalApo = from myRow in ds.Tables[3].AsEnumerable()
                                                where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                                                select myRow;
                            foreach (var v in additionalApo)
                            {
                                additionalApoDt = additionalApo.CopyToDataTable<DataRow>();
                                break;
                            }

                            //AdditionalApoCountSpan.InnerText = "  (" + additionalApoDt.Rows.Count + ")";
                            //gvApproved.DataSource = additionalApoDt;
                            //gvApproved.DataBind();
                        }
                    }
                }
            }
            else
            {
                GetCWWDashboardData();
            }
            GetAlerts();

        }
        protected void btnAdvanceSearch_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Session["dsDashboard"];
            string apoFileNo = Regex.Replace(txtAPOFIleNo.Text.Trim().ToLowerInvariant(), @"\s+", " ");
            string apoTitle = Regex.Replace(txtApoTitles.Text.Trim().ToLowerInvariant(), @"\s+", " ");
            string tigerReserve = Regex.Replace(txtTigerReserve.Text.Trim().ToLowerInvariant(), @"\s+", " ");
            string financialYear = Regex.Replace(txtYear.Text.Trim().ToLowerInvariant(), @"\s+", " ");
            if (!string.IsNullOrEmpty(apoFileNo) || !string.IsNullOrEmpty(apoTitle) || !string.IsNullOrEmpty(tigerReserve) || !string.IsNullOrEmpty(financialYear))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable submittedApoDt = new DataTable();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (Regex.Replace(ds.Tables[0].Rows[i]["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoFileNo)
                        {
                            var submittedApo = from myRow in ds.Tables[0].AsEnumerable()
                                               where (Regex.Replace(myRow["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoFileNo)
                                               select myRow;
                            foreach (var v in submittedApo)
                            {
                                submittedApoDt = submittedApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            APOCountSpan.InnerText = "  (" + submittedApoDt.Rows.Count + ")";
                            gvAPoSubmitted.DataSource = submittedApoDt;
                            gvAPoSubmitted.DataBind();
                        }
                        else if (Regex.Replace(ds.Tables[0].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                        {
                            var submittedApo = from myRow in ds.Tables[0].AsEnumerable()
                                               where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                                               select myRow;
                            foreach (var v in submittedApo)
                            {
                                submittedApoDt = submittedApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            APOCountSpan.InnerText = "  (" + submittedApoDt.Rows.Count + ")";
                            gvAPoSubmitted.DataSource = submittedApoDt;
                            gvAPoSubmitted.DataBind();
                        }
                        else if (Regex.Replace(ds.Tables[0].Rows[i]["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == tigerReserve)
                        {
                            var submittedApo = from myRow in ds.Tables[0].AsEnumerable()
                                               where (Regex.Replace(myRow["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == tigerReserve)
                                               select myRow;
                            foreach (var v in submittedApo)
                            {
                                submittedApoDt = submittedApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            APOCountSpan.InnerText = "  (" + submittedApoDt.Rows.Count + ")";
                            gvAPoSubmitted.DataSource = submittedApoDt;
                            gvAPoSubmitted.DataBind();
                        }
                        else if (Regex.Replace(ds.Tables[0].Rows[i]["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == financialYear)
                        {
                            var submittedApo = from myRow in ds.Tables[0].AsEnumerable()
                                               where (Regex.Replace(myRow["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == financialYear)
                                               select myRow;
                            foreach (var v in submittedApo)
                            {
                                submittedApoDt = submittedApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            APOCountSpan.InnerText = "  (" + submittedApoDt.Rows.Count + ")";
                            gvAPoSubmitted.DataSource = submittedApoDt;
                            gvAPoSubmitted.DataBind();
                        }
                        else
                        {
                            APOCountSpan.InnerText = "  (" + submittedApoDt.Rows.Count + ")";
                            gvAPoSubmitted.DataSource = submittedApoDt;
                            gvAPoSubmitted.DataBind();
                        }
                    }
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable quarterlyReportsDt = new DataTable();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (Regex.Replace(ds.Tables[1].Rows[i]["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoFileNo)
                        {
                            var quarterlyReports = from myRow in ds.Tables[1].AsEnumerable()
                                                   where (Regex.Replace(myRow["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoFileNo)
                                                   select myRow;
                            foreach (var v in quarterlyReports)
                            {
                                quarterlyReportsDt = quarterlyReports.CopyToDataTable<DataRow>();
                                break;
                            }
                            QuarterlyCountSpan.InnerText = "  (" + quarterlyReportsDt.Rows.Count + ")";
                            gvAPoReportSubmitted.DataSource = quarterlyReportsDt;
                            gvAPoReportSubmitted.DataBind();
                        }
                        else if (Regex.Replace(ds.Tables[1].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                        {
                            var quarterlyReports = from myRow in ds.Tables[1].AsEnumerable()
                                                   where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                                                   select myRow;
                            foreach (var v in quarterlyReports)
                            {
                                quarterlyReportsDt = quarterlyReports.CopyToDataTable<DataRow>();
                                break;
                            }
                            QuarterlyCountSpan.InnerText = "  (" + quarterlyReportsDt.Rows.Count + ")";
                            gvAPoReportSubmitted.DataSource = quarterlyReportsDt;
                            gvAPoReportSubmitted.DataBind();
                        }
                        else if (Regex.Replace(ds.Tables[1].Rows[i]["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == tigerReserve)
                        {
                            var quarterlyReports = from myRow in ds.Tables[1].AsEnumerable()
                                                   where (Regex.Replace(myRow["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == tigerReserve)
                                                   select myRow;
                            foreach (var v in quarterlyReports)
                            {
                                quarterlyReportsDt = quarterlyReports.CopyToDataTable<DataRow>();
                                break;
                            }
                            QuarterlyCountSpan.InnerText = "  (" + quarterlyReportsDt.Rows.Count + ")";
                            gvAPoReportSubmitted.DataSource = quarterlyReportsDt;
                            gvAPoReportSubmitted.DataBind();
                        }
                        else if (Regex.Replace(ds.Tables[1].Rows[i]["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == financialYear)
                        {
                            var quarterlyReports = from myRow in ds.Tables[1].AsEnumerable()
                                                   where (Regex.Replace(myRow["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == financialYear)
                                                   select myRow;
                            foreach (var v in quarterlyReports)
                            {
                                quarterlyReportsDt = quarterlyReports.CopyToDataTable<DataRow>();
                                break;
                            }
                            QuarterlyCountSpan.InnerText = "  (" + quarterlyReportsDt.Rows.Count + ")";
                            gvAPoReportSubmitted.DataSource = quarterlyReportsDt;
                            gvAPoReportSubmitted.DataBind();
                        }
                        else
                        {
                            QuarterlyCountSpan.InnerText = "  (" + quarterlyReportsDt.Rows.Count + ")";
                            gvAPoReportSubmitted.DataSource = quarterlyReportsDt;
                            gvAPoReportSubmitted.DataBind();
                        }
                    }
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataTable approvedApoDt = new DataTable();
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        if (Regex.Replace(ds.Tables[2].Rows[i]["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoFileNo)
                        {
                            var approvedApo = from myRow in ds.Tables[2].AsEnumerable()
                                              where (Regex.Replace(myRow["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoFileNo)
                                              select myRow;
                            foreach (var v in approvedApo)
                            {
                                approvedApoDt = approvedApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            ApprovedCountSpan.InnerText = "  (" + approvedApoDt.Rows.Count + ")";
                            gvApproved.DataSource = approvedApoDt;
                            gvApproved.DataBind();
                        }
                        else if (Regex.Replace(ds.Tables[2].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                        {
                            var approvedApo = from myRow in ds.Tables[2].AsEnumerable()
                                              where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                                              select myRow;
                            foreach (var v in approvedApo)
                            {
                                approvedApoDt = approvedApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            ApprovedCountSpan.InnerText = "  (" + approvedApoDt.Rows.Count + ")";
                            gvApproved.DataSource = approvedApoDt;
                            gvApproved.DataBind();
                        }
                        else if (Regex.Replace(ds.Tables[2].Rows[i]["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == tigerReserve)
                        {
                            var approvedApo = from myRow in ds.Tables[2].AsEnumerable()
                                              where (Regex.Replace(myRow["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == tigerReserve)
                                              select myRow;
                            foreach (var v in approvedApo)
                            {
                                approvedApoDt = approvedApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            ApprovedCountSpan.InnerText = "  (" + approvedApoDt.Rows.Count + ")";
                            gvApproved.DataSource = approvedApoDt;
                            gvApproved.DataBind();
                        }
                        else if (Regex.Replace(ds.Tables[2].Rows[i]["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == financialYear)
                        {
                            var approvedApo = from myRow in ds.Tables[2].AsEnumerable()
                                              where (Regex.Replace(myRow["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == financialYear)
                                              select myRow;
                            foreach (var v in approvedApo)
                            {
                                approvedApoDt = approvedApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            ApprovedCountSpan.InnerText = "  (" + approvedApoDt.Rows.Count + ")";
                            gvApproved.DataSource = approvedApoDt;
                            gvApproved.DataBind();
                        }
                        else
                        {
                            ApprovedCountSpan.InnerText = "  (" + approvedApoDt.Rows.Count + ")";
                            gvApproved.DataSource = approvedApoDt;
                            gvApproved.DataBind();
                        }
                    }
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable additionalApoDt = new DataTable();
                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        if (Regex.Replace(ds.Tables[3].Rows[i]["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoFileNo)
                        {
                            var additionalApo = from myRow in ds.Tables[3].AsEnumerable()
                                                where (Regex.Replace(myRow["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoFileNo)
                                                select myRow;
                            foreach (var v in additionalApo)
                            {
                                additionalApoDt = additionalApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            //ApprovedCountSpan.InnerText = "  (" + additionalApoDt.Rows.Count + ")";
                            //gvApproved.DataSource = additionalApoDt;
                            //gvApproved.DataBind();
                        }
                        else if (Regex.Replace(ds.Tables[3].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                        {
                            var additionalApo = from myRow in ds.Tables[3].AsEnumerable()
                                                where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == apoTitle)
                                                select myRow;
                            foreach (var v in additionalApo)
                            {
                                additionalApoDt = additionalApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            //ApprovedCountSpan.InnerText = "  (" + additionalApoDt.Rows.Count + ")";
                            //gvApproved.DataSource = additionalApoDt;
                            //gvApproved.DataBind();
                        }
                        else if (Regex.Replace(ds.Tables[3].Rows[i]["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == tigerReserve)
                        {
                            var additionalApo = from myRow in ds.Tables[3].AsEnumerable()
                                                where (Regex.Replace(myRow["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == tigerReserve)
                                                select myRow;
                            foreach (var v in additionalApo)
                            {
                                additionalApoDt = additionalApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            //ApprovedCountSpan.InnerText = "  (" + additionalApoDt.Rows.Count + ")";
                            //gvApproved.DataSource = additionalApoDt;
                            //gvApproved.DataBind();
                        }
                        else if (Regex.Replace(ds.Tables[3].Rows[i]["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == financialYear)
                        {
                            var additionalApo = from myRow in ds.Tables[3].AsEnumerable()
                                                where (Regex.Replace(myRow["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ") == financialYear)
                                                select myRow;
                            foreach (var v in additionalApo)
                            {
                                additionalApoDt = additionalApo.CopyToDataTable<DataRow>();
                                break;
                            }
                            //ApprovedCountSpan.InnerText = "  (" + additionalApoDt.Rows.Count + ")";
                            //gvApproved.DataSource = additionalApoDt;
                            //gvApproved.DataBind();
                        }
                        else
                        {
                            //ApprovedCountSpan.InnerText = "  (" + additionalApoDt.Rows.Count + ")";
                            //gvApproved.DataSource = additionalApoDt;
                            //gvApproved.DataBind();
                        }
                    }
                }
            }
            else if (string.IsNullOrEmpty(apoFileNo) && string.IsNullOrEmpty(apoTitle) && string.IsNullOrEmpty(tigerReserve) && string.IsNullOrEmpty(financialYear))
            {
                GetCWWDashboardData();
            }
            GetAlerts();

        }
        #endregion

        protected void gvAPoSubmitted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (gvAPoSubmitted.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvAPoSubmitted.Rows)
                {
                    LinkButton lnkPrevUc = row.FindControl("clbProvisionalUc") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkPrevUc);
                    LinkButton lnkFinalUc = row.FindControl("clbFinalUc") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFinalUc);
                }
            }
        }

        protected void cgvAdditionalApo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (cgvAdditionalApo.Rows.Count > 0)
            {
                foreach (GridViewRow row in cgvAdditionalApo.Rows)
                {
                    LinkButton lnkPrevUc = row.FindControl("clbProvisionalUc") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkPrevUc);
                    LinkButton lnkFinalUc = row.FindControl("clbFinalUc") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFinalUc);
                }
            }
        }
        protected void gvApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (gvApproved.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvApproved.Rows)
                {
                    LinkButton lnkPrevUc = row.FindControl("clbProvisionalUc") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkPrevUc);
                    LinkButton lnkFinalUc = row.FindControl("clbFinalUc") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFinalUc);
                }
            }
        }
    }
}