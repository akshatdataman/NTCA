using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WiseThink.NTCA.BAL;
using WiseThink.NTCA.BAL.Authorization;
using WiseThink.NTCA.App_Code;
using WiseThink.NTCA.DataEntity.Entities;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using WiseThink.NTCA.DataEntity;
using WiseThink.NTCA.BAL.Helper;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class CheckList : BasePage
    {
        int TigerReserverId;
        string currentFinancialYear = string.Empty;
        string previousFinancialYear = string.Empty;
        int previousApoFileId;
        DataSet dsApoCounts;
        int _apoCount = 0;
        UtilizationCertificate uCertificate = new UtilizationCertificate();
        protected void Page_Load(object sender, EventArgs e)
        {
            //ButtonDiv.Style.Add("display", "none");
            Session["NonRecurring"] = null;
            Session["Recurring"] = null;
            string LoggedInUser = AuthoProvider.User;
            DataSet dsTigerReserveId = CheckListBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
            if (dsTigerReserveId != null)
            {
                Session["TigerReserverId"] = null;
                DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                if (dr[0] != DBNull.Value)
                {
                    TigerReserverId = Convert.ToInt32(dr["TigerReserveId"]);
                    Session["TigerReserverId"] = TigerReserverId;
                }
                DataSet dsGetAPOFileId = APOBAL.Instance.GetAPOFileId(TigerReserverId, Request.QueryString["callfrom"]);
                if (dsGetAPOFileId != null)
                {
                    Session["dsGetAPOFileId"] = null;
                    if (dsGetAPOFileId.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr1 = dsGetAPOFileId.Tables[0].Rows[0];
                        if (dr1[0] != DBNull.Value)
                        {
                            Session["APOFileId"] = Convert.ToInt32(dr1["APOFileId"]);
                        }
                    }
                }
                if (Session["APOFileId"] != null)
                {
                    DataSet dsUC = CheckListBAL.Instance.GetUCAndProvisionalUC(Convert.ToInt32(Session["APOFileId"]));
                    if (dsUC.Tables[0].Rows.Count == 1)
                    {
                        DataRow drUc = dsUC.Tables[0].Rows[0];
                        if (drUc[0] != DBNull.Value)
                        {
                            lbtnFileName.Text = drUc["ProvisionalUcFileName"].ToString();
                        }
                        else
                            lbtnFileName.Text = string.Empty;
                    }
                }
            }
            if (!IsPostBack)
            {
                try
                {
                    GetCheckListFormat(TigerReserverId);
                    UserBAL.Instance.InsertAuditTrailDetail("Visited Checklist Page", "APO");
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
        }
        private bool IsUnspentAmountSettled(int tigerReserveId)
        {
            bool IsUnspent = false;

            if (tigerReserveId != 0)
            {
                dsApoCounts = CheckListBAL.Instance.GetApoCounts(tigerReserveId);
                if (dsApoCounts.Tables[0].Rows.Count == 1)
                {
                    DataRow drApoCount = dsApoCounts.Tables[0].Rows[0];
                    _apoCount = Convert.ToInt32(drApoCount["ApoCounts"]);
                }
                if (_apoCount == 1)
                    IsUnspent = true;
                else if (_apoCount > 1)
                {
                    DataSet dsCFY = APOBAL.Instance.GetCurrentYearAPOFileId(tigerReserveId);
                    if (dsCFY.Tables[0].Rows.Count == 1)
                    {
                        DataRow drCFY = dsCFY.Tables[0].Rows[0];
                        currentFinancialYear = Convert.ToString(drCFY["FinancialYear"]);
                    }
                    if (!string.IsNullOrEmpty(currentFinancialYear))
                    {
                        CommonClass cc = new CommonClass();
                        previousFinancialYear = cc.GetPreviousFinancialYear(currentFinancialYear);
                        DataSet dsPFY = APOBAL.Instance.GetPreviousYearAPOFileId(tigerReserveId, previousFinancialYear);
                        if (dsPFY.Tables[0].Rows.Count == 1)
                        {
                            DataRow drPFY = dsPFY.Tables[0].Rows[0];
                            previousApoFileId = Convert.ToInt32(drPFY["APOFileId"]);
                            previousFinancialYear = Convert.ToString(drPFY["FinancialYear"]);
                        }
                        DataSet dsUnspent = UtilizationCertificateBAL.Instance.GetUtilizationCertificateDetails(tigerReserveId, previousFinancialYear);
                        if (dsUnspent.Tables[0].Rows.Count == 1)
                        {
                            DataRow dr = dsUnspent.Tables[0].Rows[0];
                            uCertificate.SanctionAmount = Convert.ToDouble(dr["SanctionAmount"]);
                            if (!string.IsNullOrEmpty(dr["SpentAmount"].ToString()))
                                uCertificate.UCAmount = Convert.ToDouble(dr["SpentAmount"]);
                            else
                                uCertificate.UCAmount = 0.0;
                            if (!string.IsNullOrEmpty(dr["UnspentAmount"].ToString()))
                                uCertificate.UnSpendAmount = Convert.ToDouble(dr["UnspentAmount"]);
                            else
                                uCertificate.UnSpendAmount = 0.0;
                        }
                        if (uCertificate.SanctionAmount == uCertificate.UCAmount + uCertificate.UnSpendAmount)
                            IsUnspent = true;
                        else
                            IsUnspent = false;
                    }

                }
            }
            return IsUnspent;
        }
        private void GetCheckListFormat(int tigerReserveId)
        {
            DataSet dsCheckList = CheckListBAL.Instance.GetCheckListFormat(tigerReserveId, Request.QueryString["callfrom"].ToString());
            if (dsCheckList.Tables[0].Rows.Count > 0)
            {//Updation of information related to Tiger Reserve
                DataTable dt = dsCheckList.Tables[0];
                //string[] First = new string[] { "0", "Nature of items considered in APO" };
                string[] thirdLast = new string[] { "98", "Obligations Under Tri-MOU" };
                string[] secondLast = new string[] { "99", "Utilization Certificate" };
                string[] last = new string[] { "100", "Updation of information related to Tiger Reserve" };

                //cgvFDCheckList.Rows.
                //dt.Rows.Add(First);
                dt.Rows.Add(thirdLast);
                dt.Rows.Add(secondLast);
                dt.Rows.Add(last);
                cgvFDCheckList.DataSource = dt;
                cgvFDCheckList.DataBind();
                LoadCheckListDraftData(cgvFDCheckList);
                //ButtonDiv.Style.Add("display", "block");
            }
            else
            {
                cgvFDCheckList.DataSource = null;
                cgvFDCheckList.DataBind();
                ButtonDiv.Style.Add("display", "none");
            }
        }

        protected void cgvFDCheckList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvFDCheckList.PageIndex = e.NewPageIndex;
            GetCheckListFormat(Convert.ToInt32(Session["TigerReserverId"]));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in cgvFDCheckList.Rows)
                {
                    RequiredFieldValidator rfv = (RequiredFieldValidator)row.FindControl("RequiredFieldValidator1");
                    rfv.Enabled = true;
                }
                if (!IsFinalUcOrProvisionalUcExist(Convert.ToInt32(Session["APOFileId"])))
                {
                    string strError = ConfigurationManager.AppSettings["UploadProvisionalUc"];
                    vmError.Message = strError;
                    return;
                }
                if (!CheckApoStatusBeforeAnyAction())
                    return;
                if (cgvFDCheckList.Rows.Count > 0)
                {
                    SubmitCheckList(cgvFDCheckList);
                    string strSuccess = "Checklist saved successfully.";
                    vmSuccess.Message = strSuccess;
                    FlashMessage.InfoMessage(vmSuccess.Message);
                    UserBAL.Instance.InsertAuditTrailDetail("Saved Checklist", "APO");
                    LoadCheckListDraftData(cgvFDCheckList);
                }
                else
                {
                    string strError = ConfigurationManager.AppSettings["EmptyChecklist"];
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    return;
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
        private bool IsFinalUcOrProvisionalUcExist(int apoFileId)
        {
            bool IsUcExist = false;
            string ProvisionalUc = string.Empty;
            string FinalUc = string.Empty;
            DataSet dsUc = CheckListBAL.Instance.GetUCAndProvisionalUC(Convert.ToInt32(apoFileId));
            if (dsUc != null)
            {
                if(dsUc.Tables[0].Rows.Count>0)
                {
                    DataRow dr = dsUc.Tables[0].Rows[0];
                    if (dr[0] != DBNull.Value)
                    {
                        ProvisionalUc = Convert.ToString(dr["ProvisionalUcFileName"]);
                    }
                }
            }
            else if (dsUc != null)
            {
                DataRow dr = dsUc.Tables[1].Rows[0];
                if (dr[0] != DBNull.Value)
                {
                    FinalUc = Convert.ToString(dr["FinnalUcFileName"]);
                }

            }
            if ((!string.IsNullOrEmpty(ProvisionalUc)) || (!string.IsNullOrEmpty(FinalUc)))
                IsUcExist = true;
            else
                IsUcExist = false;

            return IsUcExist;
        }
        private void SubmitCheckList(GridView gv)
        {
            try
            {
                foreach (GridViewRow row in gv.Rows)
                {
                    Label lblActivityId = (Label)row.FindControl("lblActivityId");
                    RadioButtonList rblCompiledOrNot = row.FindControl("rblCheckedOrNot") as RadioButtonList;
                    int activityId = Convert.ToInt32(lblActivityId.Text);
                    string strCheckedOrNot = string.Empty;
                    if (rblCompiledOrNot.SelectedValue == "1")
                    {
                        strCheckedOrNot = rblCompiledOrNot.SelectedItem.Text;
                    }
                    else if (rblCompiledOrNot.SelectedValue == "2")
                    {
                        strCheckedOrNot = rblCompiledOrNot.SelectedItem.Text;
                    }
                    if (rblCompiledOrNot.SelectedValue != "")
                        CheckListBAL.Instance.SubmitCheckList(activityId, TigerReserverId, strCheckedOrNot);
                }
                UserBAL.Instance.InsertAuditTrailDetail("Forwarded APO to CWLW", "APO");
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
        private void SendAlerts(string alertMessage)
        {
            try
            {
                string tigerReserveName = string.Empty;
                DataSet dsGetTigerReserveName = CheckListBAL.Instance.GetTigerReserveName(TigerReserverId);
                if (dsGetTigerReserveName != null)
                {
                    DataRow dr2 = dsGetTigerReserveName.Tables[0].Rows[0];
                    if (dr2[0] != DBNull.Value)
                    {
                        tigerReserveName = Convert.ToString(dr2["TigerReserveName"]);
                    }
                }
                int n = 0;
                string EmailAddress = string.Empty;
                string userName = string.Empty;
                Alert alert = new Alert();
                DataSet dsEmail = UserBAL.Instance.GetAllNTCAUsersEmail();
                List<string> Emailid = new List<string>();
                for (int i = 0; i < dsEmail.Tables[2].Rows.Count; i++)
                {
                    if (dsEmail != null)
                    {
                        DataRow dr = dsEmail.Tables[2].Rows[0];
                        if (dr[0] != DBNull.Value)
                        {
                            alert.UserId = Convert.ToInt32(dsEmail.Tables[2].Rows[i]["UserId"]);
                            alert.SentTo = Convert.ToString(dsEmail.Tables[2].Rows[i]["UserName"]);
                            EmailAddress = Convert.ToString(dsEmail.Tables[2].Rows[i]["Email"]);
                        }
                    }
                    Emailid.Add(EmailAddress);
                    alert.Subject = tigerReserveName + ", " + ConfigurationManager.AppSettings["TryingSubmitApoAfterDueDate"];
                    alert.Body = tigerReserveName + ", " + alertMessage;
                    //Remove bellow hard coded APO title once we have APO in the database
                    alert.APOTitle = "APO 1";
                    alert.LoggedInUser = AuthoProvider.User;
                    n = AlertBAL.Instance.SendAlerts(alert);

                    if (n != 0)
                    {
                        string subject = alert.Subject;
                        string body = alert.Body;
                        SendEmailService sendEmail = new SendEmailService();
                        sendEmail.SendEmails(Emailid, subject, body);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        private void SendAlertsOnSuccess(string alertMessage)
        {
            try
            {
                int n = 0;
                string EmailAddress = string.Empty;
                string userName = string.Empty;
                Alert alert = new Alert();
                DataSet dsEmail = UserBAL.Instance.GetAllNTCAUsersEmail(TigerReserverId);
                for (int index = 0; index <= dsEmail.Tables.Count; index++)
                {
                    List<string> Emailid = new List<string>();
                    for (int i = 0; i < dsEmail.Tables[index].Rows.Count; i++)
                    {
                        if (dsEmail != null)
                        {
                            DataRow dr = dsEmail.Tables[1].Rows[0];
                            if (dr[0] != DBNull.Value)
                            {
                                alert.UserId = Convert.ToInt32(dsEmail.Tables[1].Rows[i]["UserId"]);
                                alert.SentTo = Convert.ToString(dsEmail.Tables[1].Rows[i]["UserName"]);
                                EmailAddress = Convert.ToString(dsEmail.Tables[1].Rows[i]["Email"]);
                            }
                        }
                        Emailid.Add(EmailAddress);
                        alert.Subject = ConfigurationManager.AppSettings["AlertSubject"];
                        alert.Body = alertMessage;

                        alert.LoggedInUser = ConfigurationManager.AppSettings["AlertSentBy"];
                        n = AlertBAL.Instance.SendAlerts(alert);

                        if (n != 0)
                        {
                            string subject = alert.Subject;
                            string body = alert.Body;
                            SendEmailService sendEmail = new SendEmailService();
                            sendEmail.SendEmails(Emailid, subject, body);
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in cgvFDCheckList.Rows)
                {
                    RequiredFieldValidator rfv = (RequiredFieldValidator)row.FindControl("RequiredFieldValidator1");
                    rfv.Enabled = false;
                }
                if (!IsFinalUcOrProvisionalUcExist(Convert.ToInt32(Session["APOFileId"])))
                {
                    string strError = ConfigurationManager.AppSettings["UploadProvisionalUc"];
                    vmError.Message = strError;
                    return;
                }

                if (!CheckApoStatusBeforeAnyAction())
                    return;

                if (Convert.ToBoolean(Session["ChecklistSaved"]) != true)
                {
                    string strError = ConfigurationManager.AppSettings["ChecklistNotSaved"];
                    vmError.Message = strError;
                    return;
                }
                //bool IsHasPermission = false;
                //if (Session["APOFileId"] != null)
                //{
                //    DataSet dsIsAllow = CheckListBAL.Instance.IsAllowedAfterDueDate(Convert.ToInt32(Session["APOFileId"]));
                //    if (dsIsAllow != null)
                //    {
                //        Session["dsGetAPOFileId"] = null;
                //        DataRow dr = dsIsAllow.Tables[0].Rows[0];
                //        if (dr[0] != DBNull.Value)
                //        {
                //            IsHasPermission = Convert.ToBoolean(dr["IsAllowAfterDueDate"]);
                //        }
                //    }
                //}
                //bool IsAllow = CheckListBAL.Instance.IsAllowApoSubmission();
                //if (!IsHasPermission == true)
                //{

                //    if (!IsAllow == true)
                //    {
                //        string strError = ConfigurationManager.AppSettings["ApoDueDateOver"];
                //        string allertMes = ConfigurationManager.AppSettings["ApoDueDateOverAlert"];
                //        SendAlerts(allertMes);
                //        vmError.Message = strError;
                //        return;
                //    }
                //}

                //int id = Convert.ToInt32(Session["TigerReserverId"]);
                //if (!IsUnspentAmountSettled(id))
                //{
                //    string strError = "Please first settled your previous year unspent amount.";
                //    vmError.Message = strError;
                //    FlashMessage.ErrorMessage(vmError.Message);
                //    return;
                //}
                if (Session["TigerReserverId"] != null)
                    TigerReserverId = Convert.ToInt32(Session["TigerReserverId"]);
                int apoFileId = 0;
                DataSet dsApoId = APOBAL.Instance.GetCurrentYearAPOFileId(TigerReserverId);
                if (dsApoId.Tables[0].Rows.Count == 1)
                {
                    apoFileId = Convert.ToInt32(dsApoId.Tables[0].Rows[0]["APOFileId"]);
                    DataSet dsCheckObligation = CheckListBAL.Instance.IsObligationUnSpentAmountSettled(apoFileId);
                    if (dsCheckObligation != null)
                    {
                        bool IsFDObligations = false;
                        bool IsUnspentAmount = false;
                        DataRow dr = dsCheckObligation.Tables[0].Rows[0];
                        if (dr[0] != DBNull.Value)
                        {
                            if (!string.IsNullOrEmpty(dr["IsFDObligationSubmitted"].ToString()))
                                IsFDObligations = Convert.ToBoolean(dr["IsFDObligationSubmitted"]);
                        }
                        if (!string.IsNullOrEmpty(dr["IsUnspentAmountSettled"].ToString()))
                            IsUnspentAmount = Convert.ToBoolean(dr["IsUnspentAmountSettled"]);
                        if (IsFDObligations != true)
                        {
                            string strInfo = ConfigurationManager.AppSettings["CheckFDObligations"];
                            vmError.Message = strInfo;
                            FlashMessage.InfoMessage(vmError.Message);
                            return;
                        }
                        //if (IsUnspentAmount != true)
                        //{
                        //    if (_apoCount == 1)
                        //    {
                        //        CheckListBAL.Instance.ForwardAPOToCWW(apoFileId, 10);
                        //        string strSuccess = "APO  forwarded to cheief wildlife warden / secretary forest successfully.";
                        //        vmSuccess.Message = strSuccess;
                        //        FlashMessage.InfoMessage(vmSuccess.Message);
                        //        IsUnspentAmount = true;
                        //        return;
                        //    }
                        //    else
                        //    {
                        //        string strInfo = "Please settled unspent amount, before submiting APO to CWLW / SF.";
                        //        vmError.Message = strInfo;
                        //        FlashMessage.InfoMessage(vmError.Message);
                        //    }
                        //}
                        else
                        {
                            CheckListBAL.Instance.ForwardAPOToCWW(apoFileId, 10);
                            string strSuccess = ConfigurationManager.AppSettings["ForwardToCWLW"];
                            vmSuccess.Message = strSuccess;
                            FlashMessage.InfoMessage(vmSuccess.Message);
                            return;
                        }
                    }
                }
                bool IsHasPermission = false;
                if (Session["APOFileId"] != null)
                {
                    DataSet dsIsAllow = CheckListBAL.Instance.IsAllowedAfterDueDate(Convert.ToInt32(Session["APOFileId"]));
                    if (dsIsAllow != null)
                    {
                        Session["dsGetAPOFileId"] = null;
                        DataRow dr = dsIsAllow.Tables[0].Rows[0];
                        if (dr[0] != DBNull.Value)
                        {
                            IsHasPermission = Convert.ToBoolean(dr["IsAllowAfterDueDate"]);
                        }
                    }
                }
                bool IsAllow = CheckListBAL.Instance.IsAllowApoSubmission();
                if (!IsHasPermission == true)
                {

                    if (!IsAllow == true)
                    {
                        string strError = ConfigurationManager.AppSettings["ApoDueDateOver"];
                        string allertMes = ConfigurationManager.AppSettings["ApoDueDateOverAlert"];
                        SendAlerts(allertMes);
                        vmError.Message = strError;
                        return;
                    }
                    else
                    {
                        string message = ConfigurationManager.AppSettings["ApoSubmittedByFD"];
                        SendAlertsOnSuccess(message);
                    }
                }
                else
                {
                    string strError = "There is not any data to submit.";
                    vmError.Message = strError;
                    FlashMessage.ErrorMessage(vmError.Message);
                    return;
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
        protected void rblCheckedOrNot_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rblName = sender as RadioButtonList;
            GridViewRow parentRow = rblName.NamingContainer as GridViewRow;
            string strName = rblName.Text;
            //Mutually exclusive RadioButtons
            foreach (GridViewRow row in cgvFDCheckList.Rows)
            {
                Label lblActivityId = (Label)row.FindControl("lblActivityId");
                RadioButtonList rblCheckedOrNot = row.FindControl("rblCheckedOrNot") as RadioButtonList;
                if (rblCheckedOrNot.SelectedValue == "1")
                {

                }
                else if (rblCheckedOrNot.SelectedValue == "2")
                {

                }
            }
        }
        private void LoadCheckListDraftData(GridView gv)
        {
            try
            {

                DataSet dsDraft = CheckListBAL.Instance.GetCheckListDraft(TigerReserverId);
                if (dsDraft.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsDraft.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dsDraft.Tables[0].Rows[i];
                        foreach (GridViewRow row in gv.Rows)
                        {
                            Label lblActivityId = (Label)row.FindControl("lblActivityId");
                            RadioButtonList rblCheckedOrNot = row.FindControl("rblCheckedOrNot") as RadioButtonList;
                            int activityId = Convert.ToInt32(lblActivityId.Text);
                            string strCheckedOrNot = string.Empty;
                            //DataRow dr = dsDraft.Tables[0].Rows[0];
                            if (dr["CheckedOrNotApplicable"].ToString() == "Checked" && dr["ActivityId"].ToString() == lblActivityId.Text)
                            {
                                rblCheckedOrNot.SelectedValue = "1";
                            }
                            else if (dr["CheckedOrNotApplicable"].ToString() == "Not Applicable" && dr["ActivityId"].ToString() == lblActivityId.Text)
                            {
                                rblCheckedOrNot.SelectedValue = "2";
                            }
                        }
                    }
                    Session["ChecklistSaved"] = true;
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        //protected void cgvFDCheckList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.Header)
        //    {
        //    //    Table tbl = e.Row.Parent as Table;
        //    //    if (tbl != null)
        //    //    {
        //            GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);
        //            TableCell HeaderCell;
        //            HeaderCell = new TableCell();
        //            HeaderCell.Text = "A.";
        //            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //            HeaderCell.ColumnSpan = 1;
        //            HeaderCell.CssClass = "HeaderStyle";
        //            HtmlGenericControl span = new HtmlGenericControl("span");
        //            span.InnerHtml = HeaderCell.Text;
        //            HeaderCell.Controls.Add(span);
        //            row.Cells.Add(HeaderCell);

        //            TableCell HeaderCell2;
        //            HeaderCell2 = new TableCell();
        //            HeaderCell2.Text = "Nature of items considered in APO";
        //            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
        //            HeaderCell2.ColumnSpan = 2;
        //            HeaderCell2.CssClass = "HeaderStyle";
        //            HtmlGenericControl span2 = new HtmlGenericControl("span2");
        //            span2.InnerHtml = HeaderCell2.Text;

        //            HeaderCell.Controls.Add(span2);
        //            row.Cells.Add(HeaderCell2);

        //            cgvFDCheckList.Controls[0].Controls.AddAt(1, row);
        //            //cgvFDCheckList.DataBind();
        //        //}
        //    }
        //}

        protected void cgvFDCheckList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView CustomGrid = (GridView)sender;
                CheckListBAL.Instance.GenerateGridSubHeader(CustomGrid, cgvFDCheckList);
                //int count = cgvFDCheckList.Rows.Count;
                //if (count == cgvFDCheckList.Rows.Count - 3)
                //{
                //    CheckListBAL.Instance.GenerateGridSubHeader2(CustomGrid, cgvFDCheckList);
                //}
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                int count = cgvFDCheckList.Rows.Count - 1;
                if (cgvFDCheckList.Rows.Count - 1 > 0)
                {
                    GridView CustomGrid = (GridView)sender;
                    CheckListBAL.Instance.GenerateGridSubHeader2(CustomGrid, cgvFDCheckList, count);
                }

                //if (cgvFDCheckList.Rows.Count + 2 > 0)
                //{
                //    GridView CustomGrid = (GridView)sender;
                //    CheckListBAL.Instance.GenerateGridSubHeader3(CustomGrid, cgvFDCheckList, count);
                //}
                //if (cgvFDCheckList.Rows.Count + 4 > 0)
                //{
                //    GridView CustomGrid = (GridView)sender;
                //    CheckListBAL.Instance.GenerateGridSubHeader4(CustomGrid, cgvFDCheckList, count);
                //}

            }

        }

        protected void btnUploadProvisionalUc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in cgvFDCheckList.Rows)
                {
                    RequiredFieldValidator rfv = (RequiredFieldValidator)row.FindControl("RequiredFieldValidator1");
                    rfv.Enabled = false;
                }
                if (fuUploadProvisionalUC.HasFile)
                {
                    string fileName = Path.GetFileName(fuUploadProvisionalUC.PostedFile.FileName);
                    int extensionCount = 0;

                    string[] splitFile = fileName.Split('.');
                    foreach (string ext in splitFile)
                    {
                        if (ext.ToLower() == "jpg" || ext.ToLower() == "png" || ext.ToLower() == "gif" || ext.ToLower() == "sql" || ext.ToLower() == "psd" ||
                            ext.ToLower() == "pdf" || ext.ToLower() == "txt" || ext.ToLower() == "rtf" || ext.ToLower() == "doc" || ext.ToLower() == "docx" ||
                            ext.ToLower() == "xls" || ext.ToLower() == "xlsx" || ext.ToLower() == "html" || ext.ToLower() == "htm" || ext.ToLower() == ".jpeg" ||
                            ext.ToLower() == "exe")
                        {
                            extensionCount++;
                        }
                    }
                    if (extensionCount <= 1)
                    {
                        fuUploadProvisionalUC.PostedFile.SaveAs(Server.MapPath("~/Upload/UC/Provisional/") + fileName);
                        uCertificate.ProvisionalUC = fuUploadProvisionalUC.FileName;

                        Stream fs = fuUploadProvisionalUC.PostedFile.InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        if (bytes[0] == 37 && bytes[1] == 80)
                        {
                            fs.Close();
                            //br.Close();
                            //fs.Dispose();
                            //br.Dispose();
                            fuUploadProvisionalUC.PostedFile.InputStream.Close();
                            bytes = null;

                            if (Session["APOFileId"] != null && !string.IsNullOrEmpty(uCertificate.ProvisionalUC))
                            {
                                int apoFileId = Convert.ToInt32(Session["APOFileId"]);
                                CheckListBAL.Instance.UploadProvisionalUC(apoFileId, uCertificate.ProvisionalUC);
                                string strSuccess = ConfigurationManager.AppSettings["ProvisionalUc"];
                                vmSuccess.Message = strSuccess;
                                FlashMessage.InfoMessage(vmSuccess.Message);
                                lbtnFileName.Text = uCertificate.ProvisionalUC;
                            }
                        }
                        else
                        {
                            string strError = "Only PDF Files are allowed";
                            uCertificate.ProvisionalUC = string.Empty;
                            vmError.Message = strError;
                            FlashMessage.ErrorMessage(vmError.Message);
                            File.Delete(Server.MapPath("~/Upload/UC/Provisional/") + fileName);
                            return;
                        }
                    }
                    else
                    {
                        string strSuccess = "Invalid File";
                        vmSuccess.Message = strSuccess;
                        FlashMessage.InfoMessage(vmSuccess.Message);
                        lbtnFileName.Text = uCertificate.ProvisionalUC;
                    }
                }
                else if (!string.IsNullOrEmpty(lblFileName.Text))
                    uCertificate.ProvisionalUC = lblFileName.Text;
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void lbtnFileName_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = Server.MapPath("~/Upload/UC/Provisional/"); //"~/Upload/UC/Provisional/";
                if (lbtnFileName.Text != string.Empty)
                {
                    //DownloadHelper.Download(lbtnFileName.Text, filePath);

                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    filePath = filePath + lbtnFileName.Text;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.AddHeader("Content-Disposition", "attachment;filename='" + lbtnFileName.Text + "'");
                    byte[] data = req.DownloadData(filePath);
                    response.BinaryWrite(data);
                    response.Flush();
                    response.End();
                }
            }
            catch
            {
                return;
            }
        }
        private bool CheckApoStatusBeforeAnyAction()
        {
            bool IsAuthorized = false;
            string LoggedInUser = AuthoProvider.User;
            string loggedInUserRole = string.Empty;
            int statusId = 0;
            DataSet dsUserRole = APOBAL.Instance.GetLoggedUserRole(LoggedInUser);
            if (dsUserRole != null)
            {
                DataRow dr = dsUserRole.Tables[0].Rows[0];
                loggedInUserRole = dr["RoleName"].ToString();
            }
            if (Session["APOFileId"] != null)
            {
                int apoFileId = Convert.ToInt32(Session["APOFileId"]);
                DataSet dsCurrentStatus = APOBAL.Instance.GetCurrentAPOStatus(apoFileId);
                if (dsCurrentStatus != null && dsCurrentStatus.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsCurrentStatus.Tables[0].Rows[0];
                    statusId = Convert.ToInt32(dr["StatusId"]);
                }
            }
            if ((loggedInUserRole == "FIELDDIRECTOR" && statusId == 1) || (loggedInUserRole == "FIELDDIRECTOR" && statusId == 3))
            {
                IsAuthorized = true;
            }
            else if (loggedInUserRole == "CWLW" && statusId == 10)
            {
                IsAuthorized = true;
            }
            else if (loggedInUserRole == "REGIONALOFFICER" && (statusId == 13 || statusId == 16))
            {
                IsAuthorized = true;
            }
            else if (loggedInUserRole.Equals("NTCA"))
            {
                IsAuthorized = true;
            }
            else
            {
                string strError = ConfigurationManager.AppSettings["CheckApoStatus"];
                vmError.Message = strError;
                FlashMessage.ErrorMessage(vmError.Message);
                IsAuthorized = false;
            }
            return IsAuthorized;
        }

        protected void lbtnNonRecurring_Click(object sender, EventArgs e)
        {
            Session["NonRecurring"] = 1;
            Response.Redirect("SubmitQuarterlyReport.aspx");
        }

        protected void lbtnRecurring_Click(object sender, EventArgs e)
        {
            Session["Recurring"] = 1;
            Response.Redirect("SubmitQuarterlyReport.aspx");
        }



    }
}