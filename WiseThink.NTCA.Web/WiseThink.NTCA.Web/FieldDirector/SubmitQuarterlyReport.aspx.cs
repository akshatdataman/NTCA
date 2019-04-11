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

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class SubmitQuarterlyReport : BasePage
    {
        public bool IsEdit = false;
        APO oApo = new APO();
        protected void Page_Load(object sender, EventArgs e)
        {
            string LoggedInUser = AuthoProvider.User;
            DataSet dsTigerReserveId = APOBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
            if (dsTigerReserveId != null)
            {
                DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                if (dr[0] != DBNull.Value)
                    oApo.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                oApo.LoggedInUser = LoggedInUser;
            }
            if (!IsPostBack)
            {
                if (Session["IsEdit"] != null)
                {
                    IsEdit = (bool)Session["IsEdit"];
                    btnNonRecurringSave.Text = "Update";
                    btnRecurringSave.Text = "Update";
                }
                //BindQuarters();
                GetQuarterlyReportFormat();
                if (Session["NonRecurring"] != null)
                {
                    NonRecurringDiv.Visible = true;
                    RecuringDiv.Visible = false;
                    Session["NonRecurring"] = null;
                }
                if (Session["Recurring"] != null)
                {
                    RecuringDiv.Visible = true;
                    NonRecurringDiv.Visible = false;
                    Session["Recurring"] = null;
                }

                if (!string.IsNullOrEmpty(Request.QueryString["IsMonthly"]))
                {
                    Monthly.Style.Add("display", "block");
                    Periodic.Style.Add("display", "none");
                    SubmitApoHeader.InnerText = "Submit Monthly Report";
                }
                else
                {
                    Monthly.Style.Add("display", "none");
                    Periodic.Style.Add("display", "block");
                    SubmitApoHeader.InnerText = "Submit Periodic Report";
                }

            }


        }

        protected void gvNonRecurring_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manually once the header created
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView ProductGrid = (GridView)sender;
                APOBAL.Instance.GenerateQuarterlyReportHeader(ProductGrid, gvNonRecurring);
            }
        }

        protected void gvRecurring_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manually once the header created
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView ProductGrid = (GridView)sender;
                APOBAL.Instance.GenerateQuarterlyReportHeader(ProductGrid, gvRecurring);
            }
        }


        private void BindQuarters()
        {
            //DataSet dsQuarter = QuarterlyReportBAL.Instance.GetQuarterListt();
            //ddlNRQuarter.DataSource = dsQuarter.Tables[0];
            //ddlNRQuarter.DataValueField = "QuarterId";
            //ddlNRQuarter.DataTextField = "QuarterName";
            //ddlNRQuarter.DataBind();
            ////ddlNRQuarter.Items.Insert(0, "Select");

            //ddlRQuarter.DataSource = dsQuarter.Tables[0];
            //ddlRQuarter.DataValueField = "QuarterId";
            //ddlRQuarter.DataTextField = "QuarterName";
            //ddlRQuarter.DataBind();
            ////ddlRQuarter.Items.Insert(0, "Select");
        }
        private void GetQuarterlyReportFormat()
        {
            DataSet dsReport = new DataSet();
            //if (IsEdit == true)
            //    dsReport = APOBAL.Instance.GetApoForModification(Convert.ToInt32(oApo.TigerReserveId));
            //else
            dsReport = QuarterlyReportBAL.Instance.GetQuarterlyReportFormat(oApo.TigerReserveId);
            if (dsReport.Tables[0].Rows.Count > 0)

            {
                gvNonRecurring.DataSource = dsReport.Tables[0];
                gvNonRecurring.DataBind();
                LoadQuarterlyReportDraftData(gvNonRecurring);
                //btnNonRecurringSave.Style.Add("display", "block");
            }
            else
            {
                gvNonRecurring.DataSource = dsReport.Tables[0];
                gvNonRecurring.DataBind();
                btnNonRecurringSave.Style.Add("display", "none");
            }
            //if (dsReport.Tables[1].Rows.Count > 0)
            //{
            //    gvRecurring.DataSource = dsReport.Tables[1];
            //    gvRecurring.DataBind();
            //    LoadQuarterlyReportDraftData(gvRecurring);
            //    //btnRecurringSave.Style.Add("display", "block");
            //}
            //else
            //{
            //    gvRecurring.DataSource = dsReport.Tables[1];
            //    gvRecurring.DataBind();
            //    btnRecurringSave.Style.Add("display", "none");
            //}
        }

        protected void btnNonRecurringSave_Click(object sender, EventArgs e)
        {
            if (ddlNRMonth.Visible)
            {
                if (ddlNRMonth.SelectedValue == "0")
                {
                    vmError.Message = "Please Select Month......";
                    FlashMessage.ErrorMessage("Please Select Month.......");
                    return;
                }
            }
            
            try
            {
                if (ddlNRMonth.SelectedValue != "0")
                    oApo.Month = Convert.ToString(ddlNRMonth.SelectedValue);
                else if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                {
                    oApo.FromDate = txtFromDate.Text.Trim();
                    oApo.ToDate = txtToDate.Text.Trim();
                }
                SubmitReport(gvNonRecurring);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                 Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
            finally
            {
                if (oApo != null)
                {
                    oApo = null;
                }
            }
        }
        protected void btnRecurringSave_Click(object sender, EventArgs e)
        {
            try
            {
                oApo.QuarterId = Convert.ToInt32(ddlRQuarter.SelectedValue);
                SubmitReport(gvRecurring);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                 Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
            finally
            {
                if (oApo != null)
                {
                    oApo = null;
                }
            }
        }

        private bool ValidateGridData(GridView gv)
        {
            bool flag = true;

            foreach (GridViewRow row in gv.Rows)
            {
                Label lblActivityTypeId = (Label)row.FindControl("lblActivityTypeId");
                Label lblActivityId = (Label)row.FindControl("lblActivityId");
                Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                Label lblQuantity = (Label)row.FindControl("lblQuantity");
                Label lblCentralShare = (Label)row.FindControl("lblCentralShare");
                Label lblStateShare = (Label)row.FindControl("lblStateShare");
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                TextBox txtLocation = (TextBox)row.FindControl("txtLocation");
                TextBox txtPhysicalProgress = (TextBox)row.FindControl("txtPhysicalProgress");
                TextBox txtCentralFinancialProgress = (TextBox)row.FindControl("txtCentralFinancialProgress");
                TextBox txtStateFinancialProgress = (TextBox)row.FindControl("txtStateFinancialProgress");
                TextBox txtTotalProgress = (TextBox)row.FindControl("txtTotalProgress");

                oApo.ActivityTypeId = Convert.ToInt32(lblActivityTypeId.Text);
                oApo.ActivityId = Convert.ToInt32(lblActivityId.Text);
                oApo.ActivityItemId = Convert.ToInt32(lblActivityItemId.Text);
                if (!string.IsNullOrEmpty(txtQuantity.Text) && !string.IsNullOrEmpty(txtPhysicalProgress.Text) && !string.IsNullOrEmpty(txtCentralFinancialProgress.Text)
                    && !string.IsNullOrEmpty(txtStateFinancialProgress.Text) && !string.IsNullOrEmpty(txtTotalProgress.Text))
                {
                    // int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
                    if (Convert.ToDouble(txtQuantity.Text) <= Convert.ToDouble(lblQuantity.Text))
                    {
                       // oApo.NumberOfItems = Convert.ToDouble(txtQuantity.Text);
                    }
                    else
                    {
                        flag = false;
                    }
                    
                    if (Convert.ToDouble(txtCentralFinancialProgress.Text) <= Convert.ToDouble(lblCentralShare.Text))
                    {
                        //oApo.CentralFinancialProgress = txtCentralFinancialProgress.Text;
                    }
                    else
                    {
                        flag = false;
                    }
                    if (Convert.ToDouble(txtStateFinancialProgress.Text) <= Convert.ToDouble(lblStateShare.Text))
                    {
                       // oApo.StateFinancialProgress = txtStateFinancialProgress.Text;
                    }
                    else
                    {
                        flag = false;
                    }

                }
            }
            return flag;
        }

        private void SubmitReport(GridView gv)
        {
            try
            {
                if(ValidateGridData(gv))
                {
                    foreach (GridViewRow row in gv.Rows)
                    {
                        Label lblActivityTypeId = (Label)row.FindControl("lblActivityTypeId");
                        Label lblActivityId = (Label)row.FindControl("lblActivityId");
                        Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                        Label lblQuantity = (Label)row.FindControl("lblQuantity");
                        Label lblCentralShare = (Label)row.FindControl("lblCentralShare");
                        Label lblStateShare = (Label)row.FindControl("lblStateShare");
                        TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                        TextBox txtLocation = (TextBox)row.FindControl("txtLocation");
                        TextBox txtPhysicalProgress = (TextBox)row.FindControl("txtPhysicalProgress");
                        TextBox txtCentralFinancialProgress = (TextBox)row.FindControl("txtCentralFinancialProgress");
                        TextBox txtStateFinancialProgress = (TextBox)row.FindControl("txtStateFinancialProgress");
                        TextBox txtTotalProgress = (TextBox)row.FindControl("txtTotalProgress");

                        oApo.ActivityTypeId = Convert.ToInt32(lblActivityTypeId.Text);
                        oApo.ActivityId = Convert.ToInt32(lblActivityId.Text);
                        oApo.ActivityItemId = Convert.ToInt32(lblActivityItemId.Text);
                        if (!string.IsNullOrEmpty(txtQuantity.Text) && !string.IsNullOrEmpty(txtPhysicalProgress.Text) && !string.IsNullOrEmpty(txtCentralFinancialProgress.Text)
                            && !string.IsNullOrEmpty(txtStateFinancialProgress.Text) && !string.IsNullOrEmpty(txtTotalProgress.Text))
                        {
                            // int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
                            if (Convert.ToDouble(txtQuantity.Text) <= Convert.ToDouble(lblQuantity.Text))
                            {
                                oApo.NumberOfItems = Convert.ToDouble(txtQuantity.Text);
                            }
                            else
                            {
                                string msg = "No. of Item inserted should be less than sanctioned limit";
                                vmError.Message = msg;
                                FlashMessage.ErrorMessage(vmSuccess.Message);
                                return;
                            }
                            oApo.Location = txtLocation.Text;
                            oApo.PhysicalProgress = txtPhysicalProgress.Text;
                            if (Convert.ToDouble(txtCentralFinancialProgress.Text) <= Convert.ToDouble(lblCentralShare.Text))
                            {
                                oApo.CentralFinancialProgress = txtCentralFinancialProgress.Text;
                            }
                            else
                            {
                                string msg = "Central Share inserted should be less than sanctioned limit";
                                vmError.Message = msg;
                                FlashMessage.ErrorMessage(vmSuccess.Message);
                                return;
                            }
                            if (Convert.ToDouble(txtStateFinancialProgress.Text) <= Convert.ToDouble(lblStateShare.Text))
                            {
                                oApo.StateFinancialProgress = txtStateFinancialProgress.Text;
                            }
                            else
                            {
                                string msg = "State Share inserted should be less than sanctioned limit";
                                vmError.Message = msg;
                                FlashMessage.ErrorMessage(vmSuccess.Message);
                                return;
                            }

                            oApo.FinancialTotal = txtTotalProgress.Text;
                            oApo.IsFilled = true;
                            //int Id = Convert.ToInt32(gv.DataKeys[row.RowIndex].Values["ID"]);
                            //if (Id == 0)
                            //    QuarterlyReportBAL.Instance.SubmitQuarterlyReport(oApo);
                            //else
                            //    QuarterlyReportBAL.Instance.ModifyQuarterlyReport(oApo);
                            if (Session["IsEdit"] == null)
                            {

                                QuarterlyReportBAL.Instance.SubmitQuarterlyReport(oApo);
                                string strSuccess = "Data saved successfully.";
                                vmSuccess.Message = strSuccess;
                                FlashMessage.ErrorMessage(vmSuccess.Message);
                            }
                            else
                            {
                                HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
                                oApo.APOId = Convert.ToInt32(hdnId.Value);
                                oApo.LoggedInUser = AuthoProvider.User;
                                QuarterlyReportBAL.Instance.ModifyQuarterlyReport(oApo);
                                string strSuccess = "Data updated successfully.";
                                vmSuccess.Message = strSuccess;
                                FlashMessage.ErrorMessage(vmSuccess.Message);
                            }
                        }
                    }
                }
                else
                {
                    string msg = "Data submitted is not correct, Please check again. (No. of Items, Central Share, State cannot be greater than sanction limit)";
                    vmError.Message = msg;
                    FlashMessage.ErrorMessage(vmSuccess.Message);
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
        private void LoadQuarterlyReportDraftData(GridView gv)
        {
            try
            {
                int trId = Convert.ToInt32(oApo.TigerReserveId);
                int quarterId;
                DataSet dsDraft = null;
                if (Request.CurrentExecutionFilePath.Contains("#RecuringDiv"))
                {
                    quarterId = Convert.ToInt32(ddlRQuarter.SelectedValue);
                    dsDraft = QuarterlyReportBAL.Instance.GetQuarterlyReportDraftDataByMonth(trId, quarterId);
                }
                else
                {
                    quarterId = Convert.ToInt32(ddlNRMonth.SelectedValue);
                    dsDraft = QuarterlyReportBAL.Instance.GetQuarterlyReportDraftDataByMonth(trId, quarterId);
                }
                if (quarterId != 0)
                {

                    for (int i = 0; i < dsDraft.Tables.Count; i++)
                    {
                        if (dsDraft.Tables[i].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsDraft.Tables[i].Rows.Count; j++)
                            {
                                DataRow dr = dsDraft.Tables[i].Rows[j];
                                foreach (GridViewRow row in gv.Rows)
                                {
                                    Label lblActivityTypeId = (Label)row.FindControl("lblActivityTypeId");
                                    Label lblActivityId = (Label)row.FindControl("lblActivityId");
                                    Label lblActivityItemId = (Label)row.FindControl("lblActivityItemId");
                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    TextBox txtLocation = (TextBox)row.FindControl("txtLocation");
                                    TextBox txtPhysicalProgress = (TextBox)row.FindControl("txtPhysicalProgress");
                                    TextBox txtTotalProgress = (TextBox)row.FindControl("txtTotalProgress");
                                    TextBox txtCentralFinancialProgress = (TextBox)row.FindControl("txtCentralFinancialProgress");
                                    TextBox txtStateFinancialProgress = (TextBox)row.FindControl("txtStateFinancialProgress");

                                    if (lblActivityId.Text == dr["ActivityId"].ToString() && lblActivityItemId.Text == dr["ActivityItemId"].ToString())
                                    {
                                        if (IsEdit == true)
                                        {
                                            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
                                            hdnId.Value = dr["ID"].ToString();
                                        }
                                        txtQuantity.Text = dr["NumberOfItems"].ToString();
                                        txtLocation.Text = dr["TigerReserveName"].ToString();
                                        txtPhysicalProgress.Text = dr["PhysicalAssessment"].ToString();
                                        txtTotalProgress.Text = dr["FinancialAssessment"].ToString();
                                        txtCentralFinancialProgress.Text = dr["CentralFinancialProgress"].ToString();
                                        txtStateFinancialProgress.Text = dr["StateFinancialProgress"].ToString();
                                    }
                                }
                            }
                        }
                    }
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

        protected void ddlNRQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetQuarterlyReportFormat();
        }

        protected void lbtnNonRecurring_Click(object sender, EventArgs e)
        {
            NonRecurringDiv.Visible = true;
            RecuringDiv.Visible = false;
            Session["NonRecurring"] = null;
        }

        protected void lbtnRecurring_Click(object sender, EventArgs e)
        {
            RecuringDiv.Visible = true;
            NonRecurringDiv.Visible = false;
            Session["Recurring"] = null;
        }

        int Total = 0;
        protected void txtStateFinancialProgress_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalFinancialProgress(gvNonRecurring, gvRecurring);
        }

        protected void txtCentralFinancialProgress_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalFinancialProgress(gvNonRecurring, gvRecurring);
        }

        private void CalculateTotalFinancialProgress(GridView gv, GridView gvr)
        {
            foreach (GridViewRow row in gv.Rows)
            {
                TextBox txtCentralFinancialProgress = (TextBox)row.FindControl("txtCentralFinancialProgress");
                TextBox txtStateFinancialProgress = (TextBox)row.FindControl("txtStateFinancialProgress");
                TextBox txtTotalProgress = (TextBox)row.FindControl("txtTotalProgress");

                if (!string.IsNullOrEmpty(txtCentralFinancialProgress.Text) && !string.IsNullOrEmpty(txtStateFinancialProgress.Text))
                {
                    Total = Convert.ToInt32(Convert.ToDecimal((txtCentralFinancialProgress.Text))) + Convert.ToInt32(Convert.ToDecimal((txtStateFinancialProgress.Text)));
                    txtTotalProgress.Text = Convert.ToString(Total);
                }
                else if (!string.IsNullOrEmpty(txtCentralFinancialProgress.Text) && string.IsNullOrEmpty(txtStateFinancialProgress.Text))
                {
                    Total = Convert.ToInt32(Convert.ToDecimal((txtCentralFinancialProgress.Text)));
                    txtTotalProgress.Text = Convert.ToString(Total);
                }
                else if (string.IsNullOrEmpty(txtCentralFinancialProgress.Text) && !string.IsNullOrEmpty(txtStateFinancialProgress.Text))
                {
                    Total = Convert.ToInt32(Convert.ToDecimal((txtStateFinancialProgress.Text)));
                    txtTotalProgress.Text = Convert.ToString(Total);
                }
            }
            foreach (GridViewRow row in gvr.Rows)
            {
                TextBox txtCentralFinancialProgress = (TextBox)row.FindControl("txtCentralFinancialProgress");
                TextBox txtStateFinancialProgress = (TextBox)row.FindControl("txtStateFinancialProgress");
                TextBox txtTotalProgress = (TextBox)row.FindControl("txtTotalProgress");

                if (!string.IsNullOrEmpty(txtCentralFinancialProgress.Text) && !string.IsNullOrEmpty(txtStateFinancialProgress.Text))
                {
                    Total = Convert.ToInt32(Convert.ToDecimal((txtCentralFinancialProgress.Text))) + Convert.ToInt32(Convert.ToDecimal((txtStateFinancialProgress.Text)));
                    txtTotalProgress.Text = Convert.ToString(Total);
                }
                else if (!string.IsNullOrEmpty(txtCentralFinancialProgress.Text) && string.IsNullOrEmpty(txtStateFinancialProgress.Text))
                {
                    Total = Convert.ToInt32(Convert.ToDecimal((txtCentralFinancialProgress.Text)));
                    txtTotalProgress.Text = Convert.ToString(Total);
                }
                else if (string.IsNullOrEmpty(txtCentralFinancialProgress.Text) && !string.IsNullOrEmpty(txtStateFinancialProgress.Text))
                {
                    Total = Convert.ToInt32(Convert.ToDecimal((txtStateFinancialProgress.Text)));
                    txtTotalProgress.Text = Convert.ToString(Total);
                }

            }
        }

        protected void ddlNRMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetQuarterlyReportFormat();
        }



    }
}