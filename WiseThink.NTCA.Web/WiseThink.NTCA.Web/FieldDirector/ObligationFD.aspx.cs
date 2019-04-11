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
using WiseThink.NTCA.App_Code;

namespace WiseThink.NTCA.Web.FieldDirector
{
    public partial class ObligationFD : BasePage
    {
        Obligations obligation = new Obligations();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //GetSelectedRecord();
            Session["Recurring"] = null;
            Session["NonRecurring"] = null;
            string LoggedInUser = AuthoProvider.User;
            DataSet dsTigerReserveId = ObligationBAL.Instance.GetLoggedInUserTigerReserveId(LoggedInUser);
            if (dsTigerReserveId != null)
            {
                DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                if (dr[0] != DBNull.Value)
                    obligation.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                obligation.LoggedInUser = LoggedInUser;
            }
            if (!Page.IsPostBack)
            {
                GetObligationFDFormat();
            }
            
        }

        protected void cgvObligationFD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetSelectedRecord();
            cgvObligationFD.PageIndex = e.NewPageIndex;
            GetObligationFDFormat();
            SetSelectedRecord();
            
        }
        private void GetObligationFDFormat()
        {
            DataSet dsObligationFD = ObligationBAL.Instance.GetObligationFDFormat();
            cgvObligationFD.DataSource = dsObligationFD;
            cgvObligationFD.DataBind();
            LoadObligationData(cgvObligationFD);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                SubmitObligation(cgvObligationFD);
                string strSuccess = "Data has been saved successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ObligationBAL.Instance.UpdateFieldDirectorObligations(obligation.TigerReserveId);
                string strSuccess = "Obligation has been Submitted successfully.";
                vmSuccess.Message = strSuccess;
                FlashMessage.InfoMessage(vmSuccess.Message);
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        private void SubmitObligation(GridView gv)
        {
            try
            {
                foreach (GridViewRow row in gv.Rows)
                {

                    Label lblObligationId = (Label)row.FindControl("lblObligationId");
                    RadioButtonList rblCompiledOrNot = row.FindControl("rblCompiledOrNot") as RadioButtonList;
                    TextBox txtLevelofComplaince = (TextBox)row.FindControl("txtLevelofComplaince");
                    TextBox txtReason = (TextBox)row.FindControl("txtReason");
                    obligation.ObligationId = Convert.ToInt32(lblObligationId.Text);
                    if (rblCompiledOrNot.SelectedValue == "1")
                    {
                        obligation.CompiledOrNot = rblCompiledOrNot.SelectedItem.Text;
                        txtLevelofComplaince.Enabled = false;
                        txtLevelofComplaince.Text = "100";
                        obligation.ComplianceLevel = Convert.ToInt32(txtLevelofComplaince.Text);
                        txtReason.Enabled = false;
                        obligation.Reason = null;
                    }
                    else if (rblCompiledOrNot.SelectedValue == "2")
                    {
                        obligation.CompiledOrNot = rblCompiledOrNot.SelectedItem.Text;
                        txtLevelofComplaince.Enabled = true;
                        if (!string.IsNullOrEmpty(txtLevelofComplaince.Text))
                            obligation.ComplianceLevel = Convert.ToInt32(txtLevelofComplaince.Text);
                        else
                            obligation.ComplianceLevel = 0;
                        obligation.Reason = txtReason.Text;
                        txtReason.Enabled = true;
                    }
                    else if (rblCompiledOrNot.SelectedValue == "3")
                    {
                        obligation.CompiledOrNot = rblCompiledOrNot.SelectedItem.Text;
                        txtLevelofComplaince.Enabled = false;
                        obligation.ComplianceLevel = 0;
                        txtLevelofComplaince.Text = string.Empty;
                        txtReason.Enabled = false;
                        obligation.Reason = null;
                        txtReason.Text = string.Empty;
                        
                    }
                    if (rblCompiledOrNot.SelectedValue != "")
                        ObligationBAL.Instance.SubmitObligationsForFD(obligation);
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        private void GetSelectedRecord()
        {
            for (int i = 0; i < cgvObligationFD.Rows.Count; i++)
            {
                RadioButtonList rb = (RadioButtonList)cgvObligationFD.Rows[i]
                                .Cells[0].FindControl("rblCompiledOrNot");
                if (rb != null)
                {
                    if (rb.SelectedValue != null)
                    {
                        HiddenField hf = (HiddenField)cgvObligationFD.Rows[i]
                                        .Cells[0].FindControl("HiddenField1");
                        if (hf != null)
                        {
                            ViewState["SelectedItem"] = hf.Value;
                        }

                        //break;
                    }
                }
            }
        }

        private void SetSelectedRecord()
        {
            for (int i = 0; i < cgvObligationFD.Rows.Count; i++)
            {
                RadioButtonList rb = (RadioButtonList)cgvObligationFD.Rows[i].Cells[0]
                                                .FindControl("rblCompiledOrNot");
                if (rb != null)
                {
                    HiddenField hf = (HiddenField)cgvObligationFD.Rows[i]
                                        .Cells[0].FindControl("HiddenField1");
                    if (hf != null && ViewState["SelectedItem"] != null)
                    {
                        if (hf.Value.Equals(ViewState["SelectedItem"].ToString()))
                        {
                            rb.SelectedValue = hf.Value;
                            //break;
                        }
                    }
                }
            }
        }
        protected void rblCompiledOrNot_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rblName = sender as RadioButtonList;
            GridViewRow parentRow = rblName.NamingContainer as GridViewRow;
            string strName = rblName.Text;
            //Mutually exclusive RadioButtons
            foreach (GridViewRow row in cgvObligationFD.Rows)
            {
                RadioButtonList rblCompiledOrNot = row.FindControl("rblCompiledOrNot") as RadioButtonList;
                TextBox txtLevelofComplaince = (TextBox)row.FindControl("txtLevelofComplaince");
                TextBox txtReason = (TextBox)row.FindControl("txtReason");
                if (rblCompiledOrNot.SelectedValue == "1")
                {
                    txtLevelofComplaince.Enabled = false;
                    txtLevelofComplaince.Text = "100";
                    //txtLevelofComplaince.Attributes.Add("req", "1");
                    txtLevelofComplaince.Attributes.Add("num", "1");
                    txtReason.Enabled = false;
                    //txtLevelofComplaince.Text = string.Empty;
                    txtReason.Text = string.Empty;
                    //txtLevelofComplaince.Attributes.Remove("danger");
                    //txtReason.Attributes.Remove("danger");
                    txtLevelofComplaince.CssClass = "";
                    txtReason.CssClass = "";
                }
                else if (rblCompiledOrNot.SelectedValue == "2")
                {
                   
                  
                 
                   // txtLevelofComplaince.Attributes.Add("class", "danger");
                  //  txtReason.Attributes.Add("req", "1");
                   if (string.IsNullOrEmpty(txtLevelofComplaince.Text))
                   {
                       txtLevelofComplaince.CssClass = "less_size";
                       txtLevelofComplaince.CssClass = "danger";
                       txtLevelofComplaince.Enabled = true;
                       txtReason.Enabled = true;
                       return;
                     
                       
                   }
                   if(string.IsNullOrEmpty(txtReason.Text))
                   {
                       txtReason.CssClass = "less_size";
                       txtReason.CssClass = "danger";
                       txtLevelofComplaince.Enabled = true;
                       txtReason.Enabled = true;
                       return;
                      
                       
                   }
                 
                  
                   
                }
                else if (rblCompiledOrNot.SelectedValue == "3")
                {
                    txtLevelofComplaince.Enabled = false;
                    txtReason.Enabled = false;
                    txtLevelofComplaince.Text = string.Empty;
                    txtReason.Text = string.Empty;
                    txtLevelofComplaince.CssClass = "";
                    txtReason.CssClass = "";
                  //  txtLevelofComplaince.Attributes.Remove("req");
                   // txtReason.Attributes.Remove("req");
                  }
               
            }

        }
        private void LoadObligationData(GridView gv)
        {
            try
            {
                int trId = Convert.ToInt32(obligation.TigerReserveId);
                DataSet dsDraft = ObligationBAL.Instance.GetFieldDirectorObligations(trId);
                if (dsDraft.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsDraft.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dsDraft.Tables[0].Rows[i];
                        foreach (GridViewRow row in gv.Rows)
                        {
                            Label lblObligationId = (Label)row.FindControl("lblObligationId");
                            Label lblObligation = (Label)row.FindControl("Descriptions");
                            RadioButtonList rblCompiledOrNot = row.FindControl("rblCompiledOrNot") as RadioButtonList;
                            TextBox txtLevelofComplaince = (TextBox)row.FindControl("txtLevelofComplaince");
                            TextBox txtReason = (TextBox)row.FindControl("txtReason");
                            //DataRow dr = dsDraft.Tables[0].Rows[0];
                            string str = dr["CompledOrNotOrNotApplicable"].ToString();
                            string id = lblObligationId.Text;
                            if (dr["CompledOrNotOrNotApplicable"].ToString() == "Complied" && dr["ObligationId"].ToString() == lblObligationId.Text)
                            {
                                rblCompiledOrNot.SelectedValue = "1";
                                txtLevelofComplaince.Text = "100";
                                txtLevelofComplaince.Enabled = false;
                                txtReason.Enabled = false;
                                //txtLevelofComplaince.Attributes.Add("req", "1");
                                //txtLevelofComplaince.Attributes.Add("num", "1");
                            }
                            else if (dr["CompledOrNotOrNotApplicable"].ToString() == "Not Complied" && dr["ObligationId"].ToString() == lblObligationId.Text)
                            {
                                rblCompiledOrNot.SelectedValue = "2";
                                txtLevelofComplaince.Text = Convert.ToString(dr["LevelOfCompliance"]);
                                txtReason.Text = dr["ReasonIfNotCompiled"].ToString();

                                if (string.IsNullOrEmpty(txtLevelofComplaince.Text))
                                {
                                     txtLevelofComplaince.CssClass = "less_size";
                                    txtLevelofComplaince.CssClass = "danger";
                                }
                                if (string.IsNullOrEmpty(txtReason.Text))
                                {
                                    txtReason.CssClass = "less_size";
                                    txtReason.CssClass = "danger";
                                }
                                
                                txtLevelofComplaince.Enabled = true;
                                txtReason.Enabled = true;
                              //  txtReason.Attributes.Add("req", "1");
                            }
                            else if (dr["CompledOrNotOrNotApplicable"].ToString() == "Not Applicable" && dr["ObligationId"].ToString() == lblObligationId.Text)
                            {
                                rblCompiledOrNot.SelectedValue = "3";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }

        protected void LbtnSelectAll_Click(object sender, EventArgs e)
        {
            if (LbtnSelectAll.Text == "Select All")
            {
                SelectRadioButton();
                LbtnSelectAll.Text = "Unselect All";
            }
            else
            {
                SelectRadioButton();
                LbtnSelectAll.Text = "Select All";
            }
          
        }

        private void SelectRadioButton()
        {
            
            foreach (GridViewRow row in cgvObligationFD.Rows)
            {
                RadioButtonList rblCompiledOrNot = row.FindControl("rblCompiledOrNot") as RadioButtonList;
                if(LbtnSelectAll.Text== "Select All")
                { 
                  for (int i = 0; i < cgvObligationFD.Rows.Count; i++)
                  {
                    rblCompiledOrNot.SelectedIndex = 0;
                  }
                  
                }
                if (LbtnSelectAll.Text == "Unselect All")
                {
                    for (int i = 0; i < cgvObligationFD.Rows.Count; i++)
                    {
                        rblCompiledOrNot.ClearSelection();
                    }
                }               
            }
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