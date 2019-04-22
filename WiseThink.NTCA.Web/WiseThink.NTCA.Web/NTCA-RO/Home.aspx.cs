using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.BAL;
using System.Configuration;
using System.Text.RegularExpressions;

namespace WiseThink.NTCA.Web.NTCA_RO
{
    public partial class Home : BasePage
    {
        DataSet dsDashboard = new DataSet();
        List<Role> userRole = new List<Role>();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(HttpContext.Current.Session != null)
            //{
            if (!IsPostBack)
            {
                GetNTCADashboardData();
                GetAlerts();
                UserBAL.Instance.InsertAuditTrailDetail("Visited NTCA Dashboard Page", "NTCA Dashboard");
            }
            //}
            //else
            //{
            //    Response.Redirect("~/Login.aspx");
            //}
        }
        private void RegisterPostBackControl()
        {
            foreach (GridViewRow row in gvAPoReportSubmitted.Rows)
            {
                LinkButton lnkPrevUc = row.FindControl("clbProvisionalUc") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkPrevUc);
            }
        }
        private void GetNTCADashboardData()
        {
            try
            {
                userRole = AuthoProvider.LoggedInRoles.ToList();
                if (userRole.Contains(Role.NTCA) || userRole.Contains(Role.REGIONALOFFICER))
                {
                    if (userRole.Contains(Role.NTCA))
                        NTCAHomeHeader.InnerText = "NTCA Head Office Home";
                    else
                        NTCAHomeHeader.InnerText = "NTCA Regional Office Home";
                    dsDashboard = APOBAL.Instance.GetDashboardForNTCA();
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
            GetNTCADashboardData();
        }
        protected void gvAPoReportSubmitted_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAPoReportSubmitted.PageIndex = e.NewPageIndex;
            GetNTCADashboardData();

        }
        protected void gvApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvApproved.PageIndex = e.NewPageIndex;
            GetNTCADashboardData();
        }
        protected void cgvAdditionalApo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvAdditionalApo.PageIndex = e.NewPageIndex;
            GetNTCADashboardData();
        }
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
                        if (Regex.Replace(ds.Tables[0].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
                        {
                            var submittedApo = from myRow in ds.Tables[0].AsEnumerable()
                                               where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
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
                        if (Regex.Replace(ds.Tables[0].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
                        {
                            var quarterlyReports = from myRow in ds.Tables[1].AsEnumerable()
                                                   where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
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
                        if (Regex.Replace(ds.Tables[0].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
                        {
                            var approvedApo = from myRow in ds.Tables[2].AsEnumerable()
                                              where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
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
                        if (Regex.Replace(ds.Tables[0].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
                        {
                            var additionalApo = from myRow in ds.Tables[3].AsEnumerable()
                                                where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
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
                GetNTCADashboardData();
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

            if (apoFileNo.Length == 0)
                apoFileNo = "ZAHIRMULTANI";
            if (apoTitle.Length == 0)
                apoTitle = "SHIVANISHARMA";
            if (tigerReserve.Length == 0)
                tigerReserve = "ZAHIRMULTANI";
            if (financialYear.Length == 0)
                financialYear = "SHIVANISHARMA";
            if (!string.IsNullOrEmpty(apoFileNo) || !string.IsNullOrEmpty(apoTitle) || !string.IsNullOrEmpty(tigerReserve) || !string.IsNullOrEmpty(financialYear))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable submittedApoDt = new DataTable();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (Regex.Replace(ds.Tables[0].Rows[i]["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
                        {
                            var submittedApo = from myRow in ds.Tables[0].AsEnumerable()
                                               where (Regex.Replace(myRow["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
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
                        else if (Regex.Replace(ds.Tables[0].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
                        {
                            var submittedApo = from myRow in ds.Tables[0].AsEnumerable()
                                               where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
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
                        else if (Regex.Replace(ds.Tables[0].Rows[i]["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(tigerReserve))
                        {
                            var submittedApo = from myRow in ds.Tables[0].AsEnumerable()
                                               where (Regex.Replace(myRow["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(tigerReserve))
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
                        else if (Regex.Replace(ds.Tables[0].Rows[i]["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(financialYear))
                        {
                            var submittedApo = from myRow in ds.Tables[0].AsEnumerable()
                                               where (Regex.Replace(myRow["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(financialYear))
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
                        if (Regex.Replace(ds.Tables[1].Rows[i]["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoFileNo))
                        {
                            var quarterlyReports = from myRow in ds.Tables[1].AsEnumerable()
                                                   where (Regex.Replace(myRow["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoFileNo))
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
                        else if (Regex.Replace(ds.Tables[1].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
                        {
                            var quarterlyReports = from myRow in ds.Tables[1].AsEnumerable()
                                                   where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
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
                        else if (Regex.Replace(ds.Tables[1].Rows[i]["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(tigerReserve))
                        {
                            var quarterlyReports = from myRow in ds.Tables[1].AsEnumerable()
                                                   where (Regex.Replace(myRow["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(tigerReserve))
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
                        else if (Regex.Replace(ds.Tables[1].Rows[i]["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(financialYear))
                        {
                            var quarterlyReports = from myRow in ds.Tables[1].AsEnumerable()
                                                   where (Regex.Replace(myRow["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(financialYear))
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
                        if (Regex.Replace(ds.Tables[2].Rows[i]["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoFileNo))
                        {
                            var approvedApo = from myRow in ds.Tables[2].AsEnumerable()
                                              where (Regex.Replace(myRow["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoFileNo))
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
                        else if (Regex.Replace(ds.Tables[2].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
                        {
                            var approvedApo = from myRow in ds.Tables[2].AsEnumerable()
                                              where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
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
                        else if (Regex.Replace(ds.Tables[2].Rows[i]["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(tigerReserve))
                        {
                            var approvedApo = from myRow in ds.Tables[2].AsEnumerable()
                                              where (Regex.Replace(myRow["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(tigerReserve))
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
                        else if (Regex.Replace(ds.Tables[2].Rows[i]["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(financialYear))
                        {
                            var approvedApo = from myRow in ds.Tables[2].AsEnumerable()
                                              where (Regex.Replace(myRow["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(financialYear))
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
                        if (Regex.Replace(ds.Tables[3].Rows[i]["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoFileNo))
                        {
                            var additionalApo = from myRow in ds.Tables[3].AsEnumerable()
                                                where (Regex.Replace(myRow["APOFileNo"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoFileNo))
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
                        else if (Regex.Replace(ds.Tables[3].Rows[i]["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
                        {
                            var additionalApo = from myRow in ds.Tables[3].AsEnumerable()
                                                where (Regex.Replace(myRow["APOTitle"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(apoTitle))
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
                        else if (Regex.Replace(ds.Tables[3].Rows[i]["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(tigerReserve))
                        {
                            var additionalApo = from myRow in ds.Tables[3].AsEnumerable()
                                                where (Regex.Replace(myRow["TigerReserveName"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(tigerReserve))
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
                        else if (Regex.Replace(ds.Tables[3].Rows[i]["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(financialYear))
                        {
                            var additionalApo = from myRow in ds.Tables[3].AsEnumerable()
                                                where (Regex.Replace(myRow["FinancialYear"].ToString().Trim().ToLowerInvariant(), @"\s+", " ").Contains(financialYear))
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
                GetNTCADashboardData();
            }
            GetAlerts();
        }
        #endregion

        protected void gvAPoSubmitted_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VIEWAPO")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string APOID = lnkView.CommandArgument;
                Response.Redirect("~/NTCA-RO/ViewAPO.aspx?Id=" + APOID + "", false);
            }
            switch (e.CommandName)
            {
                case "Edit":

                    LinkButton clbEdit = (LinkButton)e.CommandSource;
                    string APOFileId = clbEdit.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Session["IsEdit"] = true;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/EditApoCopy.aspx?Edit=True", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewFD":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/ViewFieldDirectorObligations.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {

                case "Approve":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/ApproveAPO.aspx", false));

                    break;

            }
            switch (e.CommandName)
            {
                case "GetUc":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/ViewUtilizationCertificate.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "FeedBackFD":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/FDFeedback.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewCWW":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/ViewCWWObligations.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewCheckList":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Session["CallFrom"] = "ViewAPO";
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/ViewCheckList.aspx?callFrom=SubmitAPO", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "UpdateStatus":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/UpdateAPOStatus.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewTRDetail":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/ViewTigerReserveDetails.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "FeedBackCWW":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/CWWFeedback.aspx", false));
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

        protected void cgvAdditionalApo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VIEWAPO")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string APOID = lnkView.CommandArgument;
                Response.Redirect("~/NTCA-RO/ViewAPO.aspx?Id=" + APOID + "", false);
            }
            switch (e.CommandName)
            {
                case "Edit":
                    Session["IsEdit"] = true;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/EditApo.aspx?Edit=True", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewFD":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/ViewFieldDirectorObligations.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {

                case "Approve":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/ApproveAPO.aspx", false));

                    break;

            }
            switch (e.CommandName)
            {
                case "GetUc":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/ViewUtilizationCertificate.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "FeedBackFD":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/FDFeedback.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewCWW":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/ViewCWWObligations.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "UpdateStatus":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/UpdateAPOStatus.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewTRDetail":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/ViewTigerReserveDetails.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "FeedBackCWW":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("{0}: {1}", "~/NTCA-RO/CWWFeedback.aspx", false));
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
                        ViewState.Clear();
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

        protected void gvAPoSubmitted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Session["IsApprove"] = false;
            if (gvAPoSubmitted.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvAPoSubmitted.Rows)
                {
                    if (userRole.Contains(Role.REGIONALOFFICER))
                    {
                        LinkButton clbApprove = (LinkButton)row.FindControl("clbApprove");
                        clbApprove.Style.Add("display", "none");

                        LinkButton clbSanctioned = (LinkButton)row.FindControl("clbSanctioned");
                        clbSanctioned.Style.Add("display", "none");

                        LinkButton lbViewUc = (LinkButton)row.FindControl("lbViewUc");
                        lbViewUc.Style.Add("display", "none");

                        LinkButton clbEdit = (LinkButton)row.FindControl("clbEdit");
                        clbEdit.Style.Add("display", "none");

                        LinkButton lbViewFDObligation = (LinkButton)row.FindControl("lbViewFDObligation");
                        lbViewFDObligation.Style.Add("display", "none");

                        LinkButton lbFeedbackFD = (LinkButton)row.FindControl("lbFeedbackFD");
                        lbFeedbackFD.Style.Add("display", "none");

                        LinkButton lbViewCWWObligation = (LinkButton)row.FindControl("lbViewCWWObligation");
                        lbViewCWWObligation.Style.Add("display", "none");

                        LinkButton lbFeedbackCWW = (LinkButton)row.FindControl("lbFeedbackCWW");
                        lbFeedbackCWW.Style.Add("display", "none");

                    }
                    else
                    {
                        LinkButton clbApprove = (LinkButton)row.FindControl("clbApprove");
                        clbApprove.Style.Add("display", "block");

                        LinkButton clbSanctioned = (LinkButton)row.FindControl("clbSanctioned");
                        clbSanctioned.Style.Add("display", "none");

                        LinkButton lbViewUc = (LinkButton)row.FindControl("lbViewUc");
                        lbViewUc.Style.Add("display", "block");

                        LinkButton clbEdit = (LinkButton)row.FindControl("clbEdit");
                        clbEdit.Style.Add("display", "block");

                        LinkButton lbViewFDObligation = (LinkButton)row.FindControl("lbViewFDObligation");
                        lbViewFDObligation.Style.Add("display", "block");

                        LinkButton lbFeedbackFD = (LinkButton)row.FindControl("lbFeedbackFD");
                        lbFeedbackFD.Style.Add("display", "block");

                        LinkButton lbViewCWWObligation = (LinkButton)row.FindControl("lbViewCWWObligation");
                        lbViewCWWObligation.Style.Add("display", "block");

                        LinkButton lbFeedbackCWW = (LinkButton)row.FindControl("lbFeedbackCWW");
                        lbFeedbackCWW.Style.Add("display", "block");
                    }
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
        protected void gvApproved_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VIEWAPO")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string APOID = lnkView.CommandArgument;
                Session["IsApprove"] = true;
                Response.Redirect("~/NTCA-RO/ViewAPO.aspx?Id=" + APOID + "", false);
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
    }
}