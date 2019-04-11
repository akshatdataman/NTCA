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
using System.Configuration;

namespace WiseThink.NTCA.Web.CWW_Secretary
{
    public partial class ObligationCWW : BasePage
    {
        Obligations obligation = new Obligations();
        protected void Page_Load(object sender, EventArgs e)
        {
            string LoggedInUser = AuthoProvider.User;
            //DataSet dsStateId = ObligationBAL.Instance.GetStateId(LoggedInUser);
            //if (dsStateId != null)
            //{
            //    DataRow dr = dsStateId.Tables[0].Rows[0];
            //    if (dr[0] != DBNull.Value)
            //    {
            //        obligation.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
            //    }
            //    obligation.StateId = Convert.ToInt32(dr["StateId"]);
            //    obligation.LoggedInUser = LoggedInUser;
            //}
            if (Session["APOFileId"] != null)
            {
                int apoFileId = Convert.ToInt32(Session["APOFileId"]);
                DataSet dsTigerReserveId = APOBAL.Instance.GetAPOStateAndTigerReserveId(apoFileId);

                if (dsTigerReserveId != null)
                {
                    DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                    if (dr[0] != DBNull.Value)
                    {
                        obligation.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                        obligation.StateId = Convert.ToInt32(dr["StateId"]);
                        Session["TigerReserveId"] = obligation.TigerReserveId;
                    }
                    obligation.LoggedInUser = LoggedInUser;
                    Session["ApoFileIdTemp"] = apoFileId;
                    Session["APOFileId"] = null;
                }
            }
            if (!IsPostBack)
            {
                GetObligationCWWFormat();
                UserBAL.Instance.InsertAuditTrailDetail("Visited CWLW Obligation Under Tri-MOU", "CWLW Obligation");
            }
        }

        protected void cgvObligationCWW_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cgvObligationCWW.PageIndex = e.NewPageIndex;
            GetObligationCWWFormat();
        }
        private void GetObligationCWWFormat()
        {
            DataSet dsObligationCWW = ObligationBAL.Instance.GetObligationCWWFormat();
            cgvObligationCWW.DataSource = dsObligationCWW;
            cgvObligationCWW.DataBind();
            LoadObligationData(cgvObligationCWW);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SubmitObligation(cgvObligationCWW);
            UserBAL.Instance.InsertAuditTrailDetail("Saved CWLW Obligation Under Tri-MOU", "CWLW Obligation");
            string strSuccess = "Obligations saved successfully";
            vmSuccess.Message = strSuccess;
            return;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["TigerReserveId"] != null)
                {
                    ObligationBAL.Instance.UpdateCWWObligations(Convert.ToInt32(Session["TigerReserveId"]));
                    UserBAL.Instance.InsertAuditTrailDetail("Submitted CWLW Obligation Under Tri-MOU", "CWLW Obligation");
                    string strSuccess = ConfigurationManager.AppSettings["CWLWObligationSubmit"];
                    vmSuccess.Message = strSuccess;
                    return;
                }
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
                int apoFileId = Convert.ToInt32(Session["ApoFileIdTemp"]);
                DataSet dsTigerReserveId = APOBAL.Instance.GetAPOStateAndTigerReserveId(apoFileId);

                if (dsTigerReserveId != null)
                {
                    DataRow dr = dsTigerReserveId.Tables[0].Rows[0];
                    if (dr[0] != DBNull.Value)
                    {
                        obligation.TigerReserveId = Convert.ToInt32(dr["TigerReserveId"]);
                        obligation.StateId = Convert.ToInt32(dr["StateId"]);
                    }
                }

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
                        ObligationBAL.Instance.SubmitObligationsForCWW(obligation);

                    Session["ApoFileIdTemp"] = null;
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogFatal((ex.InnerException != null ? ex.InnerException.Message : ex.Message), ex, this.GetType());
                Response.RedirectPermanent("~/ErrorPage.aspx", false);
            }
        }
        protected void rblCompiledOrNot_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList rblName = sender as RadioButtonList;
            GridViewRow parentRow = rblName.NamingContainer as GridViewRow;
            string strName = rblName.Text;
            //Mutually exclusive RadioButtons
            foreach (GridViewRow row in cgvObligationCWW.Rows)
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
                }
                else if (rblCompiledOrNot.SelectedValue == "2")
                {
                    txtLevelofComplaince.Enabled = true;
                    txtReason.Enabled = true;
                    txtReason.Attributes.Add("req", "1");
                    txtLevelofComplaince.Attributes.Add("num", "1");
                    txtLevelofComplaince.Enabled = true;
                    txtReason.Enabled = true;
                }
                else if (rblCompiledOrNot.SelectedValue == "3")
                {
                    txtLevelofComplaince.Enabled = false;
                    txtReason.Enabled = false;
                    txtLevelofComplaince.Text = string.Empty;
                    txtReason.Text = string.Empty;
                    txtLevelofComplaince.Attributes.Remove("req");
                    txtReason.Attributes.Remove("req");
                }
                
            }
        }
        private void LoadObligationData(GridView gv)
        {
            try
            {
                //int stateId = Convert.ToInt32(obligation.StateId);
                DataSet dsDraft = ObligationBAL.Instance.GetCWWObligations(obligation.TigerReserveId,obligation.StateId);
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
                                txtLevelofComplaince.Enabled = true;
                                txtReason.Enabled = true;
                                txtReason.Attributes.Add("req", "1");
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

            foreach (GridViewRow row in cgvObligationCWW.Rows)
            {
                RadioButtonList rblCompiledOrNot = row.FindControl("rblCompiledOrNot") as RadioButtonList;
                if (LbtnSelectAll.Text == "Select All")
                {
                    for (int i = 0; i < cgvObligationCWW.Rows.Count; i++)
                    {
                        rblCompiledOrNot.SelectedIndex = 0;
                    }

                }
                if (LbtnSelectAll.Text == "Unselect All")
                {
                    for (int i = 0; i < cgvObligationCWW.Rows.Count; i++)
                    {
                        rblCompiledOrNot.ClearSelection();
                    }
                }
            }
        }
       
    }
}