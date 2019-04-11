using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.DataEntity;
using System.Configuration;
using System.Net;
using WiseThink.NTCA.BAL.Helper;
using System.IO;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class WebForm5 : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }
        public static int TigerReserveId;
        protected void Page_Load(object sender, EventArgs e)
        {
            string LoggedInUser = AuthoProvider.User;
            DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);

            if (dsTigerReserveId != null)
            {
                if (dsTigerReserveId.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                    if (dr[0] != DBNull.Value)
                        TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                }
            }
            if (!IsPostBack)
            {
                GetDashboardData();
                GetAlerts();
                Session["IsEdit"] = null;
                UserBAL.Instance.InsertAuditTrailDetail("Visited Field Director Dashboard Page", "Field Director Dashboard");
            }
        }
        private void GetDashboardData()
        {
            DataSet dsDashboard = new DataSet();
            try
            {
                UserInfo uinfo = new UserInfo();
                if (AuthoProvider.LoggedInRoles != null)
                {
                    List<Role> userRole = AuthoProvider.LoggedInRoles.ToList();
                    if (userRole.Contains(Role.FIELDDIRECTOR))
                    {
                        dsDashboard = APOBAL.Instance.GetDashboardForFD(TigerReserveId);

                        int countApo = dsDashboard.Tables[0].Rows.Count;
                        if (countApo > 0)
                        {
                            APOCountSpan.InnerText = "  (" + countApo.ToString() + ")";
                            cgvAPoSubmitted.DataSource = dsDashboard.Tables[0];
                            cgvAPoSubmitted.DataBind();
                        }
                        else
                        {
                            APOCountSpan.InnerText = "  (" + "0" + ")";
                            cgvAPoSubmitted.DataSource = dsDashboard.Tables[0];
                            cgvAPoSubmitted.DataBind();
                        }
                        int countQuarterly = dsDashboard.Tables[1].Rows.Count;
                        if (countQuarterly > 0)
                        {
                            QuarterlyCountSpan.InnerText = "  (" + countQuarterly.ToString() + ")";
                            gvQuarterlyReport.DataSource = dsDashboard.Tables[1];
                            gvQuarterlyReport.DataBind();
                        }
                        else
                        {
                            QuarterlyCountSpan.InnerText = "  (" + "0" + ")";
                            gvQuarterlyReport.DataSource = dsDashboard.Tables[1];
                            gvQuarterlyReport.DataBind();
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
                    createAcchor.Attributes.Add("href", "FieldDirectorAlerts.aspx");
                    createDiv.Controls.Add(createAcchor);
                }
            }
            else
                DisplayAlertDiv.Style.Add("display", "none");
        }

        protected void cgvAPoSubmitted_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvAPoSubmitted.PageIndex = e.NewPageIndex;
            GetDashboardData();
        }

        protected void cgvAPoSubmitted_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session["IsApprove"] = false;
            if (e.CommandName == "VIEW")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string APOID = lnkView.CommandArgument;
                Response.Redirect("~/FieldDirector/ViewAPO.aspx?Id=" + APOID + "", false);
            }
            switch (e.CommandName)
            {
                case "Edit":
                    Session["IsEdit"] = true;
                    LinkButton lnkEdit = (LinkButton)e.CommandSource;
                    string APOFileId = lnkEdit.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("~/FieldDirector/SubmitAPONew.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "EditAdditional":
                    Session["IsEdit"] = true;
                    Response.Redirect(string.Format("~/FieldDirector/AdditionalAPO.aspx?Edit=True", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "GetUc":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("~/FieldDirector/FDUtilizationCertificate.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "Submit":
                    //Session["IsEdit"] = true;
                    Response.Redirect(string.Format("~/FieldDirector/ObligationFD.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewTRDetail":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("~/FieldDirector/ViewTigerReserveDetails.aspx", false));
                    break;
            }
            //switch (e.CommandName)
            //{
            //    case "downloadProvisionalUc":
            //        LinkButton lnkdownloadProvisionalUc = (LinkButton)e.CommandSource;
            //        GridViewRow gvrow = lnkdownloadProvisionalUc.NamingContainer as GridViewRow;
            //        string APOID = lnkdownloadProvisionalUc.CommandArgument;
            //        string FileName = string.Empty;
            //        DataSet dsUc = CheckListBAL.Instance.GetUCAndProvisionalUC(Convert.ToInt32(APOID));
            //        if (dsUc != null)
            //        {
            //            DataRow dr = dsUc.Tables[0].Rows[0];
            //            if (dr[0] != DBNull.Value)
            //            {
            //                FileName = Convert.ToString(dr["ProvisionalUcFileName"]);
            //            }
            //        }
            //        if (!string.IsNullOrEmpty(FileName))
            //        {
            //            string filePath = "~/Upload/UC/Provisional/";
            //            //Response.ContentType = "doc/pdf";
            //            //Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
            //            //Response.TransmitFile(Server.MapPath(filePath));
            //            //Response.End();
            //            WebClient req = new WebClient();
            //            HttpResponse response = HttpContext.Current.Response;
            //            filePath = filePath + FileName;
            //            response.Clear();
            //            response.ClearContent();
            //            response.ClearHeaders();
            //            response.Buffer = true;
            //            response.AddHeader("Content-Disposition", "attachment;filename='" + FileName + "'");
            //            byte[] data = req.DownloadData(Server.MapPath(filePath));
            //            response.BinaryWrite(data);
            //            Response.Flush();
            //            response.End();
            //        }
            //        else
            //        {
            //            string strError = ConfigurationManager.AppSettings["ProvisionalUcNotExist"];
            //            vmError.Message = strError;
            //            return;
            //        }
            //        break;
            //}
            //switch (e.CommandName)
            //{
            //    case "downloadFinalUc":
            //        LinkButton lnkdownloadFinalUc = (LinkButton)e.CommandSource;
            //        GridViewRow gvrow = lnkdownloadFinalUc.NamingContainer as GridViewRow;
            //        string APOID = lnkdownloadFinalUc.CommandArgument;
            //        string FileName = string.Empty;
            //        DataSet dsUc = CheckListBAL.Instance.GetUCAndProvisionalUC(Convert.ToInt32(APOID));
            //        if (dsUc != null)
            //        {
            //            DataRow dr = dsUc.Tables[1].Rows[0];
            //            if (dr[0] != DBNull.Value)
            //            {
            //                FileName = Convert.ToString(dr["FinnalUcFileName"]);
            //            }
            //        }
            //        if (!string.IsNullOrEmpty(FileName))
            //        {
            //            string filePath = "~/Upload/UC/Final/";
            //            //Response.ContentType = "doc/pdf";
            //            //Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
            //            //Response.TransmitFile(Server.MapPath(filePath));
            //            //Response.End();
            //            WebClient req = new WebClient();
            //            HttpResponse response = HttpContext.Current.Response;
            //            filePath = filePath + FileName;
            //            response.Clear();
            //            response.ClearContent();
            //            response.ClearHeaders();
            //            response.Buffer = true;
            //            response.AddHeader("Content-Disposition", "attachment;filename='" + FileName + "'");
            //            byte[] data = req.DownloadData(Server.MapPath(filePath));
            //            response.BinaryWrite(data);
            //            Response.Flush();
            //            response.End();
            //        }
            //        else
            //        {
            //            string strError = ConfigurationManager.AppSettings["FinalUcNotExist"];
            //            vmError.Message = strError;
            //            return;
            //        }
            //        break;
            //}
            GetAlerts();
        }
        public void TheDownload(string path)
        {
            System.IO.FileInfo toDownload = new System.IO.FileInfo(path);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                       "attachment; filename=" + toDownload.Name);
            HttpContext.Current.Response.AddHeader("Content-Length",
                       toDownload.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.WriteFile(path);
            HttpContext.Current.Response.End();
        }
        protected void gvApproved_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ApprovedApoView")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string APOID = lnkView.CommandArgument;
                Session["IsApprove"] = true;
                Response.Redirect("~/FieldDirector/ViewAPO.aspx?Id=" + APOID + "", false);
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

                        //TheDownload(filePath);
                        //Response.ContentType = "application/pdf";
                        //Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.pdf");
                        //Response.TransmitFile(Server.MapPath(filePath));
                        //Response.End();

                        //DownloadHelper.Download(FileName, filePath);

                        //if (FileName.EndsWith(".txt"))
                        //{
                        //    Response.ContentType = "application/txt";
                        //}
                        //else if (FileName.EndsWith(".pdf"))
                        //{
                        //    Response.ContentType = "application/pdf";
                        //}
                        //else if (FileName.EndsWith(".docx"))
                        //{
                        //    Response.ContentType = "application/docx";
                        //}
                        //else
                        //{
                        //    Response.ContentType = "image/jpg";
                        //}
                        //Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
                        //Response.TransmitFile(Server.MapPath(filePath));
                        //Response.End();

                        ////string filePath = "~/Upload/UC/Provisional/";
                        //string filePath = Server.MapPath("~/Upload/UC/Provisional/" + FileName);
                        //DownloadHelper.ServerMapPath(filePath);
                        //DownloadHelper.DownLoadFileFromServer(filePath, FileName);



                        //////string filePath = (sender as LinkButton).CommandArgument;
                        //////Response.ContentType = ContentType;
                        //////Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                        //////Response.WriteFile(filePath);
                        //////Response.End();

                        WebClient req = new WebClient();
                        HttpResponse response = HttpContext.Current.Response;
                        //filePath = filePath + FileName;
                        response.Clear();
                        response.ClearContent();
                        response.ClearHeaders();
                        response.Buffer = true;
                        response.AddHeader("Content-Disposition", "attachment;filename='" + FileName + "'");
                        if (System.IO.File.Exists(Server.MapPath(filePath)))
                        {
                            byte[] data = req.DownloadData(Server.MapPath(filePath));
                            response.BinaryWrite(data);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "alert('File does not exists');", true);
                        }
                        uppnlApprovedAPO.Update();
                        Response.Flush();
                        response.End();

                        //string filename = lnkfilepath.Text;
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



        protected void gvQuarterlyReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "QuarterlyReportView")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string APOID = lnkView.CommandArgument;
                Response.Redirect("~/FieldDirector/SubmitQuarterlyReport.aspx", false);
            }
            switch (e.CommandName)
            {
                case "Edit":
                    Session["IsEdit"] = true;
                    Response.Redirect(string.Format("~/FieldDirector/SubmitQuarterlyReport.aspx?Edit=True", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "Submit":
                    //Session["IsEdit"] = true;
                    Response.Redirect(string.Format("~/FieldDirector/ObligationFD.aspx", false));
                    break;
            }
            GetAlerts();
        }

        protected void cgvAdditionalApo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvAdditionalApo.PageIndex = e.NewPageIndex;
            GetDashboardData();
        }

        protected void cgvAdditionalApo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VIEW")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string APOID = lnkView.CommandArgument;
                Response.Redirect("~/FieldDirector/ViewAPO.aspx?Id=" + APOID + "", false);
            }
            switch (e.CommandName)
            {
                case "Edit":
                    Session["IsEdit"] = true;
                    Response.Redirect(string.Format("~/FieldDirector/SubmitAPONew.aspx?Edit=True", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "EditAdditional":
                    Session["IsEdit"] = true;
                    Response.Redirect(string.Format("~/FieldDirector/AdditionalAPO.aspx?Edit=True", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "GetUc":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("~/FieldDirector/FDUtilizationCertificate.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "Submit":
                    //Session["IsEdit"] = true;
                    Response.Redirect(string.Format("~/FieldDirector/ObligationFD.aspx", false));
                    break;
            }
            switch (e.CommandName)
            {
                case "ViewTRDetail":
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string APOFileId = lnkView.CommandArgument;
                    Session["APOFileId"] = APOFileId;
                    Response.Redirect(string.Format("~/FieldDirector/ViewTigerReserveDetails.aspx", false));
                    break;
            }
            GetAlerts();
        }

        protected void gvApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvApproved.PageIndex = e.NewPageIndex;
            GetDashboardData();
        }
        protected void gvQuarterlyReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvQuarterlyReport.PageIndex = e.NewPageIndex;
            GetDashboardData();
        }

        protected void cgvAPoSubmitted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (cgvAPoSubmitted.Rows.Count > 0)
            {
                //foreach (GridViewRow row in cgvAPoSubmitted.Rows)
                //{
                //    LinkButton lnkPrevUc = row.FindControl("clbProvisionalUc") as LinkButton;
                //    ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkPrevUc);
                //    LinkButton lnkFinalUc = row.FindControl("clbFinalUc") as LinkButton;
                //    ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFinalUc);
                //}
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